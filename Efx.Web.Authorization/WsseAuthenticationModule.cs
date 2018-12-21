// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.WsseAuthenticationModule
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System;
using System.Security.Principal;
using System.Web;

namespace Efx.Web.Authorization
{
  public sealed class WsseAuthenticationModule : AbstractAuthenticationModule
  {
    protected override void onAuthenticateRequest(object sender, EventArgs e)
    {
      HttpApplication httpApplication = (HttpApplication) sender;
      string header = httpApplication.Context.Request.Headers["Authorization"];
      if (string.IsNullOrEmpty(header) || !header.Contains("WSSE profile=\"UsernameToken\""))
        return;
      IIdentity identity = new UserAuthenticateService().Authenticate(httpApplication.Context.Request.Headers["X-WSSE"], (AbstractAuthenticationModule) this);
      if (identity == null)
        return;
      httpApplication.Context.User = (IPrincipal) new GenericPrincipal(identity, new string[0]);
    }

    public override IIdentity Authenticate(
      string pToken,
      Func<string, string> pGetPassForUser)
    {
      if (!pToken.StartsWith("UsernameToken", StringComparison.Ordinal))
        return (IIdentity) null;
      return WsseClient.WsseTokenAuthentication(pToken, pGetPassForUser) ?? (IIdentity) new GenericIdentity(string.Empty);
    }

    protected override void onEndRequest(object sender, EventArgs e)
    {
      HttpApplication httpApplication = (HttpApplication) sender;
      if (httpApplication.Context.Response.StatusCode != 401)
        return;
      httpApplication.Context.Response.AddHeader("WWW-Authenticate", "WSSE realm=\"AtomPub\", profile=\"UsernameToken\"");
    }
  }
}
