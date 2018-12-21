// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.DigestAuthenticationModule
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using Efx.Core.Hashing;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace Efx.Web.Authorization
{
  public sealed class DigestAuthenticationModule : AbstractAuthenticationModule
  {
    private readonly object object_0 = new object();
    private HttpApplication httpApplication_0;
    private string string_0;

    protected override void onAuthenticateRequest(object sender, EventArgs e)
    {
      lock (this.object_0)
      {
        this.httpApplication_0 = (HttpApplication) sender;
        string header = this.httpApplication_0.Context.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(header) || !header.Trim().StartsWith("Digest", StringComparison.Ordinal))
          return;
        UserAuthenticateService authenticateService = new UserAuthenticateService();
        this.string_0 = UserAuthenticateService.smethod_0();
        IIdentity identity = authenticateService.Authenticate(header, (AbstractAuthenticationModule) this);
        if (identity == null)
          return;
        this.httpApplication_0.Context.User = (IPrincipal) new GenericPrincipal(identity, new string[0]);
      }
    }

    public override IIdentity Authenticate(
      string pToken,
      Func<string, string> pGetPassForUser)
    {
      pToken = pToken.Substring(7);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string str1 = pToken;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        char[] separator = new char[1]{ '=' };
        string[] strArray = str2.Split(separator, 2);
        string key = strArray[0].Trim(' ', '"');
        string str3 = strArray[1].Trim(' ', '"');
        dictionary.Add(key, str3);
      }
      string name = dictionary["username"];
      string str4 = (string) null;
      if (pGetPassForUser != null)
        str4 = pGetPassForUser(name);
      string hexHash1 = string.Format("{0}:{1}:{2}", (object) dictionary["username"], (object) this.string_0, (object) str4).GetHexHash(HashType.Md5);
      string hexHash2 = string.Format("{0}:{1}", (object) this.httpApplication_0.Request.HttpMethod, (object) dictionary["uri"]).GetHexHash(HashType.Md5);
      string pInput;
      if (dictionary["qop"] != null)
        pInput = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", (object) hexHash1, (object) dictionary["nonce"], (object) dictionary["nc"], (object) dictionary["cnonce"], (object) dictionary["qop"], (object) hexHash2);
      else
        pInput = string.Format("{0}:{1}:{2}", (object) hexHash1, (object) dictionary["nonce"], (object) hexHash2);
      string hexHash3 = pInput.GetHexHash(HashType.Md5);
      bool flag = !DigestAuthenticationModule.smethod_1(dictionary["nonce"]);
      this.httpApplication_0.Context.Items[(object) "staleNonce"] = (object) flag;
      if (dictionary["response"].Equals(hexHash3, StringComparison.OrdinalIgnoreCase) && !flag)
        return (IIdentity) new GenericIdentity(name, "Efx.Web.Authorization.DigestAuthenticationModule");
      return (IIdentity) null;
    }

    protected override void onEndRequest(object sender, EventArgs e)
    {
      HttpApplication httpApplication = (HttpApplication) sender;
      if (httpApplication.Context.Response.StatusCode != 401)
        return;
      string str = DigestAuthenticationModule.smethod_0();
      bool flag = false;
      object obj = httpApplication.Context.Items[(object) "staleNonce"];
      if (obj != null)
        flag = (bool) obj;
      StringBuilder stringBuilder = new StringBuilder("Digest");
      stringBuilder.Append(" realm=\"");
      stringBuilder.Append(this.string_0);
      stringBuilder.Append('"');
      stringBuilder.Append(", nonce=\"");
      stringBuilder.Append(str);
      stringBuilder.Append('"');
      stringBuilder.Append(", opaque=\"0000000000000000\"");
      stringBuilder.Append(", stale=");
      stringBuilder.Append(flag ? "true" : "false");
      stringBuilder.Append(", algorithm=MD5");
      stringBuilder.Append(", qop=\"auth\"");
      httpApplication.Response.AppendHeader("WWW-Authenticate", stringBuilder.ToString());
      httpApplication.Response.StatusCode = 401;
    }

    private static string smethod_0()
    {
      return Convert.ToBase64String(new ASCIIEncoding().GetBytes((DateTime.Now + TimeSpan.FromMinutes(1.0)).ToString("G"))).TrimEnd('=');
    }

    private static bool smethod_1(string string_1)
    {
      int num = string_1.Length % 4;
      if (num > 0)
        num = 4 - num;
      string s = string_1.PadRight(string_1.Length + num, '=');
      DateTime dateTime;
      try
      {
        dateTime = DateTime.Parse(new ASCIIEncoding().GetString(Convert.FromBase64String(s)));
      }
      catch (FormatException ex)
      {
        return false;
      }
      return DateTime.Now <= dateTime;
    }
  }
}
