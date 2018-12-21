// Decompiled with JetBrains decompiler
// Type: Efx.Web.WebConfiguration`1
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core;
using Efx.Core.Debug;
using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;

namespace Efx.Web
{
  public abstract class WebConfiguration<T> where T : WebConfiguration<T>, new()
  {
    private string _fileName;

    public void Save()
    {
      Efx.Core.Xml.XmlSerializerFactory.Save<T>(this._fileName, (T) this);
    }

    public static T Instance(string configurationLocation)
    {
      if (string.IsNullOrEmpty(configurationLocation))
        throw new ArgumentNullException(nameof (configurationLocation));
      if (HttpRuntime.Cache[configurationLocation] == null)
      {
        string filePath = Application.GetFilePath(StaticInfo.configDir, configurationLocation + ".xml");
        if (!File.Exists(filePath))
          Trace.LogError(string.Format("The configuration file [{0}] can't be loaded", (object) filePath));
        try
        {
          using (XmlTextReader xmlTextReader = new XmlTextReader(filePath))
          {
            T obj = (T) new XmlSerializer(typeof (T)).Deserialize((XmlReader) xmlTextReader);
            obj._fileName = filePath;
            HttpRuntime.Cache.Add(configurationLocation, (object) obj, new CacheDependency(filePath), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, (CacheItemRemovedCallback) null);
          }
        }
        catch (Exception ex)
        {
          T obj1 = new T();
          obj1._fileName = filePath;
          T obj2 = obj1;
          HttpRuntime.Cache.Add(configurationLocation, (object) obj2, (CacheDependency) null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, (CacheItemRemovedCallback) null);
        }
      }
      return (T) HttpRuntime.Cache[configurationLocation];
    }
  }
}
