// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.Collections
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;

namespace Efx.Web.BrowserCaps
{
  internal static class Collections
  {
    public static bool Exist<T>(IEnumerable<T> collections, Predicate<T> match)
    {
      bool flag = false;
      foreach (T collection in collections)
      {
        if (match(collection))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public static T Find<T>(IEnumerable<T> collection, Predicate<T> match)
    {
      T obj1 = default (T);
      foreach (T obj2 in collection)
      {
        if (match(obj2))
        {
          obj1 = obj2;
          break;
        }
      }
      return obj1;
    }

    public static ICollection<T> Select<T>(
      IEnumerable<T> userAgentsSet,
      Predicate<T> match)
    {
      IList<T> objList = (IList<T>) new List<T>();
      foreach (T userAgents in userAgentsSet)
      {
        if (match(userAgents))
          objList.Add(userAgents);
      }
      return (ICollection<T>) objList;
    }
  }
}
