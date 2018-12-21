// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.WsseClient
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using Efx.Core.Hashing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Efx.Web.Authorization
{
  public sealed class WsseClient : IAuthenticationModule
  {
    private readonly RNGCryptoServiceProvider rngcryptoServiceProvider_0;
    private static Func<string, bool> func_0;
    private static Func<string, bool> func_1;
    private static Func<string, bool> func_2;
    private static Func<string, bool> func_3;

    public WsseClient()
    {
      this.rngcryptoServiceProvider_0 = new RNGCryptoServiceProvider();
    }

    public string AuthenticationType
    {
      get
      {
        return "WSSE";
      }
    }

    public bool CanPreAuthenticate
    {
      get
      {
        return true;
      }
    }

    public System.Net.Authorization Authenticate(
      string pChallenge,
      WebRequest pRequest,
      ICredentials pCredentials)
    {
      if (WsseClient.smethod_0(pChallenge, "WSSE"))
        return this.method_0(pRequest, pCredentials);
      return (System.Net.Authorization) null;
    }

    public System.Net.Authorization PreAuthenticate(
      WebRequest pRequest,
      ICredentials pCredentials)
    {
      return this.method_0(pRequest, pCredentials);
    }

    private System.Net.Authorization method_0(
      WebRequest webRequest_0,
      ICredentials icredentials_0)
    {
      try
      {
        if (icredentials_0 == null)
          return (System.Net.Authorization) null;
        NetworkCredential credential = icredentials_0.GetCredential(webRequest_0.RequestUri, this.AuthenticationType);
        if (credential == null)
          return (System.Net.Authorization) null;
        ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
        if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(webRequest_0.RequestUri, webRequest_0, credential, (IAuthenticationModule) this))
          return (System.Net.Authorization) null;
        string str1 = this.method_1();
        string str2 = DateTime.UtcNow.ToString(DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern) + "Z";
        string base64String = Convert.ToBase64String((str1 + str2 + credential.Password).GetHash(HashType.Sha1));
        webRequest_0.Headers.Add("X-WSSE", string.Join(string.Empty, "UsernameToken ", "Username=\"", credential.UserName, "\", ", "PasswordDigest=\"", base64String, "\", ", "Nonce=\"", str1, "\", ", "Created=\"", str2, "\""));
        return new System.Net.Authorization("WSSE profile=\"UsernameToken\"", true);
      }
      catch
      {
        return (System.Net.Authorization) null;
      }
    }

    private static bool smethod_0(string string_0, string string_1)
    {
      int startIndex = 0;
      int length = string_0.Length;
      do
      {
        int num = string_0.IndexOf("\"");
        if (num < 0)
          goto label_4;
label_1:
        if (!string_0.Substring(startIndex, num - startIndex).Contains(string_1))
        {
          if (num + 1 < length)
          {
            startIndex = string_0.IndexOf("\"", num + 1);
            continue;
          }
          goto label_7;
        }
        else
          goto label_6;
label_4:
        num = length;
        goto label_1;
      }
      while (startIndex >= 0 && startIndex + 1 < length);
      goto label_8;
label_6:
      return true;
label_7:
      return false;
label_8:
      return false;
    }

    private string method_1()
    {
      byte[] numArray = new byte[16];
      this.rngcryptoServiceProvider_0.GetBytes(numArray);
      return Convert.ToBase64String(numArray);
    }

    public static IIdentity WsseTokenAuthentication(
      string pXwsse,
      Func<string, string> pGetPassForUser)
    {
      if (string.IsNullOrEmpty(pXwsse))
        throw new ArgumentNullException(nameof (pXwsse));
      string[] strArray1 = pXwsse.Split(',');
      if (WsseClient.func_0 == null)
        WsseClient.func_0 = new Func<string, bool>(WsseClient.smethod_1);
      Func<string, bool> func0 = WsseClient.func_0;
      string name = ((IEnumerable<string>) strArray1).Where<string>(func0).Single<string>().Split('"')[1];
      string[] strArray2 = pXwsse.Split(',');
      if (WsseClient.func_1 == null)
        WsseClient.func_1 = new Func<string, bool>(WsseClient.smethod_2);
      Func<string, bool> func1 = WsseClient.func_1;
      string str1 = ((IEnumerable<string>) strArray2).Where<string>(func1).Single<string>().Split('"')[1];
      string[] strArray3 = pXwsse.Split(',');
      if (WsseClient.func_2 == null)
        WsseClient.func_2 = new Func<string, bool>(WsseClient.smethod_3);
      Func<string, bool> func2 = WsseClient.func_2;
      string str2 = ((IEnumerable<string>) strArray3).Where<string>(func2).Single<string>().Split('"')[1];
      string[] strArray4 = pXwsse.Split(',');
      if (WsseClient.func_3 == null)
        WsseClient.func_3 = new Func<string, bool>(WsseClient.smethod_4);
      Func<string, bool> func3 = WsseClient.func_3;
      string input = ((IEnumerable<string>) strArray4).Where<string>(func3).Single<string>().Split('"')[1];
      DateTimeOffset dateTimeOffset;
      try
      {
        dateTimeOffset = DateTimeOffset.Parse(input);
      }
      catch (Exception ex)
      {
        dateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(10.0);
      }
      if (dateTimeOffset > DateTimeOffset.UtcNow.AddMinutes(5.0))
        throw new WsseClient.TokenExpiredException();
      string str3 = (string) null;
      if (pGetPassForUser != null)
        str3 = pGetPassForUser(name);
      if (!str1.Equals(Convert.ToBase64String((str2 + input + str3).GetHash(HashType.Sha1)), StringComparison.OrdinalIgnoreCase))
        return (IIdentity) null;
      return (IIdentity) new GenericIdentity(name, "Efx.Web.Authorization.WsseAuthenticationModule");
    }

    private static bool smethod_1(string string_0)
    {
      return string_0.ToLower().Contains("user");
    }

    private static bool smethod_2(string string_0)
    {
      return string_0.ToLower().Contains("pass");
    }

    private static bool smethod_3(string string_0)
    {
      return string_0.ToLower().Contains("nonce");
    }

    private static bool smethod_4(string string_0)
    {
      return string_0.ToLower().Contains("created");
    }

    public sealed class TokenExpiredException : SecurityException
    {
    }
  }
}
