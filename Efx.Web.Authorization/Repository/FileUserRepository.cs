// Decompiled with JetBrains decompiler
// Type: Efx.Web.Authorization.Repository.FileUserRepository
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Efx.Web.Authorization.Repository
{
  public sealed class FileUserRepository : IUserRepository
  {
    public static List<User> Users
    {
      get
      {
        return WebConfiguration<FileUserRepositorySettings>.Instance("SecurityRepositoryUsers").UsersList;
      }
    }

    public void DeleteUser(string pUserName)
    {
      throw new NotImplementedException();
    }

    public User GetUser(string pUserName)
    {
      FileUserRepository.Class77 class77 = new FileUserRepository.Class77();
      class77.string_0 = pUserName;
      return FileUserRepository.Users.Where<User>(new Func<User, bool>(class77.method_0)).SingleOrDefault<User>() ?? new User(class77.string_0);
    }

    public void CreateUser(User pUser)
    {
      throw new NotImplementedException();
    }

    public User UpdateUser(User pUser)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<User> GetUsers(
      int pPageIndex,
      int pPageSize,
      out int pTotalUsers)
    {
      pTotalUsers = FileUserRepository.Users.Count;
      return FileUserRepository.Users.Skip<User>(pPageIndex * pPageSize).Take<User>(pPageSize);
    }

    private sealed class Class77
    {
      public string string_0;

      public bool method_0(User user_0)
      {
        return user_0.Name.Equals(this.string_0, StringComparison.OrdinalIgnoreCase);
      }
    }
  }
}
