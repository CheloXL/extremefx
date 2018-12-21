// Decompiled with JetBrains decompiler
// Type: Efx.Core.ExtensionMethods.EnumExtensions
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using System;
using System.ComponentModel;

namespace Efx.Core.ExtensionMethods
{
  public static class EnumExtensions
  {
    private static readonly MemoryLockedCache<string, DescriptionAttribute> _fieldInfos = new MemoryLockedCache<string, DescriptionAttribute>();

    //public static T Append<T>(this Enum pType, T pValue)
    //{
    //  return (T) ((int) pType | (int) (object) pValue);
    //}

    //public static bool Has<T>(this Enum pType, T pValue)
    //{
    //  return ((int) pType & (int) (object) pValue) == (int) (object) pValue;
    //}

    //public static T Remove<T>(this Enum pType, T pValue)
    //{
    //  return (T) (ValueType) ((int) pType & ~(int) (object) pValue);
    //}

    public static string GetLocalizedDescription(this Enum pEnum)
    {
      if (pEnum == null)
        return (string) null;
      string key = pEnum.ToString();
      DescriptionAttribute orAdd = EnumExtensions._fieldInfos.GetOrAdd(key, (Func<string, DescriptionAttribute>) (desc =>
      {
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) pEnum.GetType().GetField(desc).GetCustomAttributes(typeof (DescriptionAttribute), false);
        if (customAttributes.Length <= 0)
          return (DescriptionAttribute) null;
        return customAttributes[0];
      }));
      if (orAdd != null)
        return orAdd.Description ?? key;
      return key;
    }
  }
}
