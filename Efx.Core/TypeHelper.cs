// Decompiled with JetBrains decompiler
// Type: Efx.Core.TypeHelper
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web;

namespace Efx.Core
{
  public static class TypeHelper
  {
    private static readonly MemoryLockedCache<string, Type> _typeCache = new MemoryLockedCache<string, Type>();
    private static readonly IEnumerable<string> _lacLocations = TypeHelper.GetLacLocations();

    public static Type GetTypeByName(string typeName)
    {
      if (!string.IsNullOrEmpty(typeName))
        return TypeHelper._typeCache.GetOrAdd(typeName, (Func<string, Type>) (x =>
        {
          Type type1 = Type.GetType(x, false, true);
          if ((object) type1 != null)
            return type1;
          Type type2 = TypeHelper.Locate2(x);
          if ((object) type2 != null)
            return type2;
          return TypeHelper.LocateInLac(x);
        }));
      return (Type) null;
    }

    [SecuritySafeCritical]
    private static IEnumerable<string> GetLacLocations()
    {
      List<string> stringList = new List<string>();
      string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
      if (directoryName != null)
      {
        if (directoryName.StartsWith("file:\\", StringComparison.OrdinalIgnoreCase))
        {
          stringList.Add(directoryName.Substring(6));
        }
        else
        {
          Uri uri = new Uri(directoryName);
          stringList.Add(uri.LocalPath);
        }
      }
      try
      {
        if (typeof (object).Assembly.GetType("System.MonoType") != (Type) null)
        {
          if (AppDomain.CurrentDomain.SetupInformation.DynamicBase != null)
          {
            Uri uri = new Uri(AppDomain.CurrentDomain.SetupInformation.DynamicBase);
            stringList.Add(uri.LocalPath);
          }
        }
        else if (!string.IsNullOrEmpty(AppDomain.CurrentDomain.DynamicDirectory))
        {
          Uri uri = new Uri(AppDomain.CurrentDomain.DynamicDirectory);
          stringList.Add(uri.LocalPath);
        }
      }
      catch (SecurityException ex)
      {
      }
      if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.PhysicalApplicationPath))
      {
        string str1 = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "bin");
        stringList.Add(str1);
        string str2 = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Bin");
        stringList.Add(str2);
      }
      return (IEnumerable<string>) stringList;
    }

    private static Type Locate2(string pTypeName)
    {
      if (string.IsNullOrEmpty(pTypeName))
        return (Type) null;
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        Type type = assembly.GetType(pTypeName, false);
        if (type != (Type) null)
          return type;
      }
      return (Type) null;
    }

    private static Type LocateInLac(string pTypeName)
    {
      if (string.IsNullOrEmpty(pTypeName))
        return (Type) null;
      if (string.IsNullOrEmpty(pTypeName))
        return (Type) null;
      return TypeHelper._typeCache.GetOrAdd(pTypeName, (Func<string, Type>) (x =>
      {
        using (IEnumerator<Type> enumerator = TypeHelper._lacLocations.Select<string, Type>((Func<string, Type>) (pT => TypeHelper.LocateInLac2(x, pT))).Where<Type>((Func<Type, bool>) (pType => pType != (Type) null)).GetEnumerator())
        {
          if (enumerator.MoveNext())
            return enumerator.Current;
        }
        return (Type) null;
      }));
    }

    private static Type LocateInLac2(string pTypeName, string pLac)
    {
      if (pLac == null)
        return (Type) null;
      if (string.IsNullOrEmpty(pTypeName))
        return (Type) null;
      foreach (string file in Directory.GetFiles(pLac, "*.dll"))
      {
        try
        {
          Type type = Assembly.LoadFrom(file).GetType(pTypeName, false);
          if (type != (Type) null)
            return type;
        }
        catch
        {
        }
      }
      return (Type) null;
    }
  }
}
