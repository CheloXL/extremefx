// Decompiled with JetBrains decompiler
// Type: Efx.Core.AttributeHelper
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Reflection;

namespace Efx.Core
{
  public static class AttributeHelper
  {
    public static T GetAttribute<T>(this MemberInfo pType, bool pInherit) where T : Attribute
    {
      T[] customAttributes = (T[]) pType.GetCustomAttributes(typeof (T), pInherit);
      if (customAttributes.Length <= 0)
        return default (T);
      return customAttributes[0];
    }

    public static T[] GetAttributes<T>(this MemberInfo pType, bool pInherit) where T : Attribute
    {
      return (T[]) pType.GetCustomAttributes(typeof (T), pInherit);
    }

    public static bool HasAttribute<T>(this MemberInfo pType, bool pInherit) where T : Attribute
    {
      return pType.IsDefined(typeof (T), pInherit);
    }
  }
}
