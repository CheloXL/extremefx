// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.UserAgentResolver
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;
using System.Web;

namespace Efx.Web.BrowserCaps
{
  internal static class UserAgentResolver
  {
    private static readonly List<Func<HttpRequest, string>> _extractors = new List<Func<HttpRequest, string>>();
    private const string UA_PARAMETER = "UA";

    static UserAgentResolver()
    {
      UserAgentResolver._extractors.Add(new Func<HttpRequest, string>(UserAgentResolver.FromQueryString));
      UserAgentResolver._extractors.Add(new Func<HttpRequest, string>(UserAgentResolver.FromXSkyFireVersionHeader));
      UserAgentResolver._extractors.Add(new Func<HttpRequest, string>(UserAgentResolver.FromXDeviceUserAgentHeader));
      UserAgentResolver._extractors.Add(new Func<HttpRequest, string>(UserAgentResolver.FromUserAgent));
    }

    public static string Resolve(HttpRequest httpRequest)
    {
      string str = string.Empty;
      foreach (Func<HttpRequest, string> extractor in UserAgentResolver._extractors)
      {
        str = extractor(httpRequest);
        if (!string.IsNullOrEmpty(str))
          break;
      }
      return str;
    }

    private static string FromQueryString(HttpRequest httpRequest)
    {
      return httpRequest.QueryString["UA"];
    }

    private static string FromUserAgent(HttpRequest httpRequest)
    {
      return httpRequest.UserAgent;
    }

    private static string FromXDeviceUserAgentHeader(HttpRequest httpRequest)
    {
      return httpRequest.Headers["x-device-user-agent"];
    }

    private static string FromXSkyFireVersionHeader(HttpRequest httpRequest)
    {
      if (!string.IsNullOrEmpty(httpRequest.Headers["X-Skyfire-Version"]))
        return "Generic_Skyfire_Browser";
      return (string) null;
    }
  }
}
