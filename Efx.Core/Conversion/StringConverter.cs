// Decompiled with JetBrains decompiler
// Type: Efx.Core.Conversion.StringConverter
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Efx.Core.Conversion
{
  public static class StringConverter
  {
    private static readonly object _staticLock = new object();
    private static string[] _dateFormats = new string[4]
    {
      "yyyyMMdd",
      "yyyy-MM-dd",
      "yyyy.MM.dd",
      "yyyy/MM/dd"
    };
    private static List<IStringConverter> _stringConverters;

    public static void UnregisterAllStringConverters()
    {
      lock (StringConverter._staticLock)
        StringConverter._stringConverters = (List<IStringConverter>) null;
    }

    public static void UnregisterStringConverter(IStringConverter stringConverter)
    {
      lock (StringConverter._staticLock)
      {
        if (StringConverter._stringConverters == null)
          return;
        StringConverter._stringConverters.Remove(stringConverter);
      }
    }

    public static void RegisterStringConverter(IStringConverter stringConverter)
    {
      lock (StringConverter._staticLock)
      {
        StringConverter._stringConverters = StringConverter._stringConverters ?? new List<IStringConverter>();
        StringConverter._stringConverters.Add(stringConverter);
      }
    }

    public static void RegisterStringConverter<T>(IStringConverter<T> stringConverter)
    {
      StringConverter.RegisterStringConverter((IStringConverter) new StringConverter.TypedStringConverter<T>(stringConverter));
    }

    public static void RegisterDateFormats(params string[] dateFormats)
    {
      StringConverter._dateFormats = dateFormats;
    }

    public static DateTime Convert(this string stringValue, params string[] dateFormats)
    {
      return (DateTime) stringValue.Convert(typeof (DateTime), dateFormats);
    }

    public static T Convert<T>(this string stringValue)
    {
      return (T) StringConverter.Convert(stringValue, typeof (T));
    }

    public static object Convert(this string stringValue, Type targetType)
    {
      return stringValue.Convert(targetType, (string[]) null);
    }

    private static object Convert(
      this string stringValue,
      Type targetType,
      params string[] dateFormats)
    {
      if (stringValue == null)
        return targetType.DefaultValue();
      if (targetType == typeof (string))
        return (object) stringValue;
      object obj1 = (object) null;
      Type realType = targetType.GetRealType();
      if (stringValue.Trim().Length == 0)
        return targetType.DefaultValue();
      if (StringConverter._stringConverters != null)
      {
        foreach (IStringConverter stringConverter in StringConverter._stringConverters)
        {
          if (stringConverter.TryConvert(stringValue, realType, out obj1))
            return obj1;
        }
      }
      if (!(realType == typeof (double)) && !(realType == typeof (float)))
      {
        if (realType == typeof (Decimal))
        {
          Decimal result;
          obj1 = Decimal.TryParse(stringValue.Replace(',', '.'), NumberStyles.Any, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result) ? (object) result : (object) null;
        }
        else if (realType == typeof (Guid))
        {
          try
          {
            obj1 = (object) new Guid(stringValue);
          }
          catch (Exception ex)
          {
            obj1 = (object) null;
          }
        }
        else if (!(realType == typeof (int)) && !(realType == typeof (short)) && (!(realType == typeof (long)) && !(realType == typeof (sbyte))))
        {
          if (!(realType == typeof (uint)) && !(realType == typeof (ushort)) && (!(realType == typeof (ulong)) && !(realType == typeof (byte))))
          {
            if (realType == typeof (DateTime))
            {
              DateTime result;
              obj1 = DateTime.TryParseExact(stringValue, dateFormats ?? StringConverter._dateFormats, (IFormatProvider) null, DateTimeStyles.NoCurrentDateDefault, out result) ? (object) result : (object) null;
            }
            else if (realType == typeof (bool))
              obj1 = (object) (stringValue == "1" || stringValue.ToUpper() == "Y" || (stringValue.ToUpper() == "YES" || stringValue.ToUpper() == "T") ? 1 : (stringValue.ToUpper() == "TRUE" ? 1 : 0));
            else if (realType == typeof (char))
              obj1 = stringValue.Length != 1 ? (object) null : (object) stringValue[0];
            else if (realType.IsEnum)
            {
              if (char.IsNumber(stringValue, 0))
              {
                long result;
                if (long.TryParse(stringValue, out result))
                {
                  object obj2 = Enum.ToObject(realType, result);
                  if (Enum.IsDefined(realType, obj2))
                    return obj2;
                }
              }
              else if (Enum.IsDefined(realType, (object) stringValue))
                return Enum.Parse(realType, stringValue, true);
              return targetType.DefaultValue();
            }
          }
          else
          {
            ulong result;
            obj1 = ulong.TryParse(stringValue, out result) ? (object) result : (object) null;
          }
        }
        else
        {
          long result;
          obj1 = long.TryParse(stringValue, out result) ? (object) result : (object) null;
        }
      }
      else
      {
        double result;
        obj1 = double.TryParse(stringValue.Replace(',', '.'), NumberStyles.Any, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result) ? (object) result : (object) null;
      }
      if (obj1 == null)
        return targetType.DefaultValue();
      try
      {
        return System.Convert.ChangeType(obj1, realType, (IFormatProvider) null);
      }
      catch
      {
        return targetType.DefaultValue();
      }
    }

    private sealed class TypedStringConverter<T> : IStringConverter
    {
      private readonly IStringConverter<T> _converter;

      public TypedStringConverter(IStringConverter<T> pConverter)
      {
        this._converter = pConverter;
      }

      public bool TryConvert(string pS, Type targetType, out object pValue)
      {
        pValue = (object) null;
        T obj;
        if (!typeof (T).IsAssignableFrom(targetType) || !this._converter.TryConvert(pS, out obj))
          return false;
        pValue = (object) obj;
        return true;
      }
    }
  }
}
