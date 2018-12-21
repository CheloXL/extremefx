// Decompiled with JetBrains decompiler
// Type: Efx.Core.Conversion.ObjectConverter
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Reflection;
using System;

namespace Efx.Core.Conversion
{
  public static class ObjectConverter
  {
    public static T Convert<T>(this object value)
    {
      return (T) value.Convert(typeof (T));
    }

    public static object Convert(this object value, Type targetType)
    {
      if (value == null)
        return targetType.DefaultValue();
      if (value is string)
        return StringConverter.Convert((string) value, targetType);
      Type realType = targetType.GetRealType();
      if (value.GetType() == realType)
        return value;
      if (realType == typeof (Guid) && value is byte[])
        return (object) new Guid((byte[]) value);
      if (realType == typeof (byte[]) && value is Guid)
        return (object) ((Guid) value).ToByteArray();
      if (realType.IsEnum)
      {
        try
        {
          value = (object) System.Convert.ToInt64(value);
          value = Enum.ToObject(realType, value);
        }
        catch
        {
          return targetType.DefaultValue();
        }
        if (!Enum.IsDefined(realType, value))
          return targetType.DefaultValue();
        return value;
      }
      if (realType.IsAssignableFrom(value.GetType()))
        return value;
      if (realType == typeof (string))
        return (object) value.ToString();
      try
      {
        return TypeHelper.ChangeType(value, realType);
      }
      catch
      {
        try
        {
          return System.Convert.ChangeType(value, realType, (IFormatProvider) null);
        }
        catch
        {
          return targetType.DefaultValue();
        }
      }
    }
  }
}
