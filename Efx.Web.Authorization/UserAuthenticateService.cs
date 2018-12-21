// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.UserAuthenticateService
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using Efx.Core;
using Efx.Core.Reflection;
using Efx.Web.Authorization.Repository;
using System;
using System.Security.Principal;

namespace Efx.Web.Authorization
{
  public sealed class UserAuthenticateService : IAuthenticateService
  {
    private static readonly IIdentity iidentity_0 = (IIdentity) new GenericIdentity(string.Empty);
    private readonly IUserRepository iuserRepository_0;

    public UserAuthenticateService()
    {
      RepositorySettings data = Configuration.Data;
      this.iuserRepository_0 = string.IsNullOrEmpty(data.Repository) ? (IUserRepository) new FileUserRepository() : TypeHelper.GetTypeByName(data.Repository).CreateInstance<IUserRepository>();
    }

    public UserAuthenticateService(IUserRepository pUserRepo)
    {
      if (pUserRepo == null)
        throw new ArgumentNullException(nameof (pUserRepo));
      this.iuserRepository_0 = pUserRepo;
    }

    internal static string smethod_0()
    {
      return Configuration.Data.Realm;
    }

    public IIdentity Authenticate(string pUserName, string pPassword)
    {
      if (string.IsNullOrEmpty(pUserName))
        throw new ArgumentNullException(nameof (pUserName));
      if (string.IsNullOrEmpty(pPassword))
        throw new ArgumentNullException(nameof (pPassword));
      User user = this.iuserRepository_0.GetUser(pUserName);
      if (user != null && user.Password == pPassword)
        return (IIdentity) new GenericIdentity(user.Name);
      return UserAuthenticateService.iidentity_0;
    }

    public IIdentity Authenticate(
      string pToken,
      AbstractAuthenticationModule pAuthenticationModule)
    {
      if (pAuthenticationModule == null)
        throw new ArgumentNullException(nameof (pAuthenticationModule));
      if (string.IsNullOrEmpty(pToken))
        throw new ArgumentNullException(nameof (pToken));
      return pAuthenticationModule.Authenticate(pToken, new Func<string, string>(this.method_0));
    }

    public IIdentity Authenticate(Uri pServiceUri)
    {
      return UserAuthenticateService.iidentity_0;
    }

    private string method_0(string string_0)
    {
      return this.iuserRepository_0.GetUser(string_0).Password;
    }
  }
}
