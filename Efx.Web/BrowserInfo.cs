// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserInfo
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core;
using Efx.Core.Caching;
using Efx.Core.ExtensionMethods;
using Efx.Core.Serialization;
using Efx.Web.BrowserCaps;
using Efx.Web.BrowserCaps.Matchers;
using Efx.Web.BrowserCaps.Request;
using Efx.Web.BrowserCaps.Request.Normalizers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Efx.Web
{
  public static class BrowserInfo
  {
    private static readonly MatchersChainFactory _chainFactory = new MatchersChainFactory((IUserAgentNormalizerChain) new UserAgentNormalizerChain());
    private static readonly string _fileInfo = Application.GetFilePath("~/App_Data/EfxBrowserInfo.bin");
    private static readonly LRUCache<string, DeviceInfo> _cache = new LRUCache<string, DeviceInfo>();
    private static MatchersChain _matchersChain;

    private static MatchersChain MatchersChain
    {
      get
      {
        return BrowserInfo._matchersChain ?? (BrowserInfo._matchersChain = BrowserInfo._chainFactory.Create((IEnumerable<DeviceInfo>) BrowserInfo.Info.Values));
      }
    }

    private static Dictionary<string, DeviceInfo> Info
    {
      get
      {
        Dictionary<string, DeviceInfo> dictionary = HttpRuntime.Cache["Efx::BrowserCaps::Data"] as Dictionary<string, DeviceInfo>;
        if (dictionary == null)
        {
          dictionary = BrowserInfo.RefreshInfo();
          if (dictionary == null)
            return (Dictionary<string, DeviceInfo>) null;
          HttpRuntime.Cache.Add("Efx::BrowserCaps::Data", (object) dictionary, new CacheDependency(BrowserInfo._fileInfo), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(BrowserInfo.OnRemoveCallback));
        }
        return dictionary;
      }
    }

    private static void OnRemoveCallback(string key, object value, CacheItemRemovedReason reason)
    {
      BrowserInfo._matchersChain = (MatchersChain) null;
    }

    private static Dictionary<string, DeviceInfo> RefreshInfo()
    {
      if (!File.Exists(BrowserInfo._fileInfo))
        return (Dictionary<string, DeviceInfo>) null;
      using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(BrowserInfo._fileInfo).Decompress(CompressionMethod.Deflate)))
        return FastBinaryFormatter.Deserialize<Dictionary<string, DeviceInfo>>((Stream) memoryStream);
    }

    public static DeviceInfo GetDevice(HttpRequest request)
    {
      if (BrowserInfo.Info == null)
        return new DeviceInfo();
      return BrowserInfo._cache.GetOrAdd(UserAgentResolver.Resolve(request), (Func<string, DeviceInfo>) (key =>
      {
        DeviceInfo deviceInfo;
        BrowserInfo.Info.TryGetValue(BrowserInfo.MatchersChain.Match(key), out deviceInfo);
        return deviceInfo;
      }));
    }
  }
}
