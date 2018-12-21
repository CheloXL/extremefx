// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Repository.IUserRepository
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System.Collections.Generic;

namespace Efx.Web.Authorization.Repository
{
  public interface IUserRepository
  {
    User GetUser(string pUserName);

    void CreateUser(User pUser);

    User UpdateUser(User pUser);

    void DeleteUser(string pUserName);

    IEnumerable<User> GetUsers(int pPageIndex, int pPageSize, out int pTotalUsers);
  }
}
