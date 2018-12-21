// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.AbstractAuthenticationModule
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using Efx.Web.Authorization.Repository;
using System;
using System.Security.Principal;
using System.Web;

namespace Efx.Web.Authorization
{
  public abstract class AbstractAuthenticationModule : IHttpModule
  {
    public void Init(HttpApplication pContext)
    {
      pContext.AuthenticateRequest += new EventHandler(this.onAuthenticateRequest);
      pContext.EndRequest += new EventHandler(this.method_0);
    }

    public virtual void Dispose()
    {
    }

    public abstract IIdentity Authenticate(
      string pToken,
      Func<string, string> pGetPassForUser);

    private void method_0(object sender, EventArgs e)
    {
      HttpContext context = ((HttpApplication) sender).Context;
      Discriminators discriminators = Configuration.Data.Discriminators;
      if (context.Response.StatusCode == 302 && discriminators.Qualifies((object) context))
        this.rewriteUnauthorizedResponse(context);
      this.onEndRequest(sender, e);
    }

    protected virtual void rewriteUnauthorizedResponse(HttpContext pContext)
    {
      if (pContext == null)
        throw new ArgumentNullException(nameof (pContext));
      HttpResponse response = pContext.Response;
      response.StatusCode = 401;
      response.StatusDescription = "Access Denied";
      response.RedirectLocation = (string) null;
      response.Clear();
    }

    protected abstract void onEndRequest(object sender, EventArgs e);

    protected abstract void onAuthenticateRequest(object sender, EventArgs e);
  }
}
