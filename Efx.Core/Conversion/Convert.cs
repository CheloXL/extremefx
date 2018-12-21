// Decompiled with JetBrains decompiler
// Type: Efx.Core.Conversion.Convert
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Efx.Core.Conversion
{
  internal static class Convert
  {
    private static string ToString(char pValue)
    {
      return pValue.ToString();
    }

    private static string ToString(TimeSpan pValue)
    {
      return pValue.ToString();
    }

    private static string ToString(DateTime pValue)
    {
      return pValue.ToString();
    }

    private static string ToString(Guid pValue)
    {
      return pValue.ToString();
    }

    private static string ToString(Type pValue)
    {
      if (!(pValue == (Type) null))
        return pValue.FullName;
      return (string) null;
    }

    private static string ToString(XmlNode pValue)
    {
      return pValue?.InnerXml;
    }

    public static string ToString(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return string.Empty;
      if (pValue is string)
        return (string) pValue;
      if (pValue is char)
        return Convert.ToString((char) pValue);
      if (pValue is TimeSpan)
        return Convert.ToString((TimeSpan) pValue);
      if (pValue is DateTime)
        return Convert.ToString((DateTime) pValue);
      if (pValue is Guid)
        return Convert.ToString((Guid) pValue);
      if (pValue is XmlDocument)
        return Convert.ToString((XmlNode) pValue);
      if ((object) (pValue as Type) != null)
        return Convert.ToString((Type) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToString((IFormatProvider) null);
      if (pValue is IFormattable)
        return ((IFormattable) pValue).ToString((string) null, (IFormatProvider) null);
      return pValue.ToString();
    }

    private static sbyte ToSByte(string pValue)
    {
      if (pValue != null)
        return sbyte.Parse(pValue);
      return 0;
    }

    private static sbyte ToSByte(bool pValue)
    {
      return pValue ? (sbyte) 1 : (sbyte) 0;
    }

    private static sbyte ToSByte(char pValue)
    {
      return checked ((sbyte) pValue);
    }

    public static sbyte ToSByte(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is sbyte)
        return (sbyte) pValue;
      if (pValue is string)
        return Convert.ToSByte((string) pValue);
      if (pValue is bool)
        return Convert.ToSByte((bool) pValue);
      if (pValue is char)
        return Convert.ToSByte((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToSByte((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (sbyte));
    }

    private static short ToInt16(string pValue)
    {
      if (pValue != null)
        return short.Parse(pValue);
      return 0;
    }

    private static short ToInt16(bool pValue)
    {
      return pValue ? (short) 1 : (short) 0;
    }

    private static short ToInt16(char pValue)
    {
      return checked ((short) pValue);
    }

    public static short ToInt16(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is short)
        return (short) pValue;
      if (pValue is string)
        return Convert.ToInt16((string) pValue);
      if (pValue is bool)
        return Convert.ToInt16((bool) pValue);
      if (pValue is char)
        return Convert.ToInt16((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToInt16((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (short));
    }

    private static int ToInt32(string pValue)
    {
      if (pValue != null)
        return int.Parse(pValue);
      return 0;
    }

    private static int ToInt32(bool pValue)
    {
      return !pValue ? 0 : 1;
    }

    private static int ToInt32(char pValue)
    {
      return (int) pValue;
    }

    public static int ToInt32(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is int)
        return (int) pValue;
      if (pValue is string)
        return Convert.ToInt32((string) pValue);
      if (pValue is bool)
        return Convert.ToInt32((bool) pValue);
      if (pValue is char)
        return Convert.ToInt32((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToInt32((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (int));
    }

    private static long ToInt64(string pValue)
    {
      if (pValue != null)
        return long.Parse(pValue);
      return 0;
    }

    private static long ToInt64(char pValue)
    {
      return (long) pValue;
    }

    private static long ToInt64(bool pValue)
    {
      return pValue ? 1L : 0L;
    }

    private static long ToInt64(DateTime pValue)
    {
      return (pValue - DateTime.MinValue).Ticks;
    }

    private static long ToInt64(TimeSpan pValue)
    {
      return pValue.Ticks;
    }

    public static long ToInt64(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is long)
        return (long) pValue;
      if (pValue is string)
        return Convert.ToInt64((string) pValue);
      if (pValue is char)
        return Convert.ToInt64((char) pValue);
      if (pValue is bool)
        return Convert.ToInt64((bool) pValue);
      if (pValue is DateTime)
        return Convert.ToInt64((DateTime) pValue);
      if (pValue is TimeSpan)
        return Convert.ToInt64((TimeSpan) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToInt64((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (long));
    }

    private static byte ToByte(string pValue)
    {
      if (pValue != null)
        return byte.Parse(pValue);
      return 0;
    }

    private static byte ToByte(bool pValue)
    {
      return pValue ? (byte) 1 : (byte) 0;
    }

    private static byte ToByte(char pValue)
    {
      return checked ((byte) pValue);
    }

    public static byte ToByte(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is byte)
        return (byte) pValue;
      if (pValue is string)
        return Convert.ToByte((string) pValue);
      if (pValue is bool)
        return Convert.ToByte((bool) pValue);
      if (pValue is char)
        return Convert.ToByte((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToByte((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (byte));
    }

    private static ushort ToUInt16(string pValue)
    {
      if (pValue != null)
        return ushort.Parse(pValue);
      return 0;
    }

    private static ushort ToUInt16(bool pValue)
    {
      return pValue ? (ushort) 1 : (ushort) 0;
    }

    private static ushort ToUInt16(char pValue)
    {
      return (ushort) pValue;
    }

    public static ushort ToUInt16(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is ushort)
        return (ushort) pValue;
      if (pValue is string)
        return Convert.ToUInt16((string) pValue);
      if (pValue is bool)
        return Convert.ToUInt16((bool) pValue);
      if (pValue is char)
        return Convert.ToUInt16((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToUInt16((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (ushort));
    }

    private static uint ToUInt32(string pValue)
    {
      if (pValue != null)
        return uint.Parse(pValue);
      return 0;
    }

    private static uint ToUInt32(bool pValue)
    {
      return !pValue ? 0U : 1U;
    }

    private static uint ToUInt32(char pValue)
    {
      return (uint) pValue;
    }

    public static uint ToUInt32(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is uint)
        return (uint) pValue;
      if (pValue is string)
        return Convert.ToUInt32((string) pValue);
      if (pValue is bool)
        return Convert.ToUInt32((bool) pValue);
      if (pValue is char)
        return Convert.ToUInt32((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToUInt32((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (uint));
    }

    private static ulong ToUInt64(string pValue)
    {
      if (pValue != null)
        return ulong.Parse(pValue);
      return 0;
    }

    private static ulong ToUInt64(bool pValue)
    {
      return pValue ? 1UL : 0UL;
    }

    private static ulong ToUInt64(char pValue)
    {
      return (ulong) pValue;
    }

    public static ulong ToUInt64(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0;
      if (pValue is ulong)
        return (ulong) pValue;
      if (pValue is string)
        return Convert.ToUInt64((string) pValue);
      if (pValue is bool)
        return Convert.ToUInt64((bool) pValue);
      if (pValue is char)
        return Convert.ToUInt64((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToUInt64((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (ulong));
    }

    private static char ToChar(string pValue)
    {
      char result;
      if (char.TryParse(pValue, out result))
        return result;
      if (!string.IsNullOrEmpty(pValue))
        return pValue[0];
      return char.MinValue;
    }

    private static char ToChar(bool pValue)
    {
      return pValue ? '\x0001' : char.MinValue;
    }

    public static char ToChar(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return char.MinValue;
      if (pValue is char)
        return (char) pValue;
      if (pValue is string)
        return Convert.ToChar((string) pValue);
      if (pValue is bool)
        return Convert.ToChar((bool) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToChar((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (char));
    }

    private static float ToSingle(string pValue)
    {
      if (pValue != null)
        return float.Parse(pValue);
      return 0.0f;
    }

    private static float ToSingle(bool pValue)
    {
      return !pValue ? 0.0f : 1f;
    }

    private static float ToSingle(char pValue)
    {
      return (float) pValue;
    }

    public static float ToSingle(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0.0f;
      if (pValue is float)
        return (float) pValue;
      if (pValue is string)
        return Convert.ToSingle((string) pValue);
      if (pValue is bool)
        return Convert.ToSingle((bool) pValue);
      if (pValue is char)
        return Convert.ToSingle((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToSingle((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (float));
    }

    private static double ToDouble(string pValue)
    {
      if (pValue != null)
        return double.Parse(pValue);
      return 0.0;
    }

    private static double ToDouble(bool pValue)
    {
      return !pValue ? 0.0 : 1.0;
    }

    private static double ToDouble(char pValue)
    {
      return (double) pValue;
    }

    private static double ToDouble(DateTime pValue)
    {
      return (pValue - DateTime.MinValue).TotalDays;
    }

    private static double ToDouble(TimeSpan pValue)
    {
      return pValue.TotalDays;
    }

    public static double ToDouble(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return 0.0;
      if (pValue is double)
        return (double) pValue;
      if (pValue is string)
        return Convert.ToDouble((string) pValue);
      if (pValue is bool)
        return Convert.ToDouble((bool) pValue);
      if (pValue is char)
        return Convert.ToDouble((char) pValue);
      if (pValue is DateTime)
        return Convert.ToDouble((DateTime) pValue);
      if (pValue is TimeSpan)
        return Convert.ToDouble((TimeSpan) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToDouble((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (double));
    }

    private static bool ToBoolean(string pValue)
    {
      if (pValue != null)
        return bool.Parse(pValue);
      return false;
    }

    private static bool ToBoolean(char pValue)
    {
      switch (pValue)
      {
        case char.MinValue:
          return false;
        case '\x0001':
          return true;
        case '0':
          return false;
        case '1':
          return true;
        case 'F':
          return false;
        case 'N':
          return false;
        case 'T':
          return true;
        case 'Y':
          return true;
        case 'f':
          return false;
        case 'n':
          return false;
        case 't':
          return true;
        case 'y':
          return true;
        default:
          throw new InvalidCastException(string.Format("Invalid cast from {0} to {1}", (object) typeof (char).FullName, (object) typeof (bool).FullName));
      }
    }

    public static bool ToBoolean(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return false;
      if (pValue is bool)
        return (bool) pValue;
      if (pValue is string)
        return Convert.ToBoolean((string) pValue);
      if (pValue is char)
        return Convert.ToBoolean((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToBoolean((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (bool));
    }

    private static Decimal ToDecimal(string pValue)
    {
      if (pValue != null)
        return Decimal.Parse(pValue);
      return new Decimal(0, 0, 0, false, (byte) 1);
    }

    private static Decimal ToDecimal(bool pValue)
    {
      if (!pValue)
        return new Decimal(0, 0, 0, false, (byte) 1);
      return new Decimal(10, 0, 0, false, (byte) 1);
    }

    private static Decimal ToDecimal(char pValue)
    {
      return (Decimal) pValue;
    }

    public static Decimal ToDecimal(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new Decimal(0, 0, 0, false, (byte) 1);
      if (pValue is Decimal)
        return (Decimal) pValue;
      if (pValue is string)
        return Convert.ToDecimal((string) pValue);
      if (pValue is bool)
        return Convert.ToDecimal((bool) pValue);
      if (pValue is char)
        return Convert.ToDecimal((char) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToDecimal((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (Decimal));
    }

    private static DateTime ToDateTime(string pValue)
    {
      if (pValue != null)
        return DateTime.Parse(pValue);
      return DateTime.MinValue;
    }

    private static DateTime ToDateTime(TimeSpan pValue)
    {
      return DateTime.MinValue + pValue;
    }

    private static DateTime ToDateTime(long pValue)
    {
      return DateTime.MinValue + TimeSpan.FromTicks(pValue);
    }

    private static DateTime ToDateTime(double pValue)
    {
      return DateTime.MinValue + TimeSpan.FromDays(pValue);
    }

    public static DateTime ToDateTime(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return DateTime.MinValue;
      if (pValue is DateTime)
        return (DateTime) pValue;
      if (pValue is string)
        return Convert.ToDateTime((string) pValue);
      if (pValue is TimeSpan)
        return Convert.ToDateTime((TimeSpan) pValue);
      if (pValue is long)
        return Convert.ToDateTime((long) pValue);
      if (pValue is double)
        return Convert.ToDateTime((double) pValue);
      if (pValue is IConvertible)
        return ((IConvertible) pValue).ToDateTime((IFormatProvider) null);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (DateTime));
    }

    private static Guid ToGuid(string pValue)
    {
      if (pValue != null)
        return new Guid(pValue);
      return Guid.Empty;
    }

    private static Guid ToGuid(byte[] pValue)
    {
      if (pValue != null)
        return new Guid(pValue);
      return Guid.Empty;
    }

    private static Guid ToGuid(Type pValue)
    {
      if (!(pValue == (Type) null))
        return pValue.GUID;
      return Guid.Empty;
    }

    public static Guid ToGuid(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return Guid.Empty;
      if (pValue is Guid)
        return (Guid) pValue;
      if (pValue is string)
        return Convert.ToGuid((string) pValue);
      if (pValue is byte[])
        return Convert.ToGuid((byte[]) pValue);
      if ((object) (pValue as Type) != null)
        return Convert.ToGuid((Type) pValue);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (Guid));
    }

    private static sbyte? TtoNullableSByte(sbyte pValue)
    {
      return new sbyte?(pValue);
    }

    private static sbyte? TtoNullableSByte(string pValue)
    {
      if (pValue != null)
        return new sbyte?(sbyte.Parse(pValue));
      return new sbyte?();
    }

    private static sbyte? TtoNullableSByte(char pValue)
    {
      return new sbyte?(checked ((sbyte) pValue));
    }

    private static sbyte? TtoNullableSByte(bool pValue)
    {
      return new sbyte?(pValue ? (sbyte) 1 : (sbyte) 0);
    }

    public static sbyte? ToNullableSByte(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new sbyte?();
      if (pValue is sbyte)
        return Convert.TtoNullableSByte((sbyte) pValue);
      if (pValue is string)
        return Convert.TtoNullableSByte((string) pValue);
      if (pValue is char)
        return Convert.TtoNullableSByte((char) pValue);
      if (pValue is bool)
        return Convert.TtoNullableSByte((bool) pValue);
      if (pValue is IConvertible)
        return new sbyte?(((IConvertible) pValue).ToSByte((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (sbyte?));
    }

    private static short? ToNullableInt16(short pValue)
    {
      return new short?(pValue);
    }

    private static short? ToNullableInt16(string pValue)
    {
      if (pValue != null)
        return new short?(short.Parse(pValue));
      return new short?();
    }

    private static short? ToNullableInt16(char pValue)
    {
      return new short?(checked ((short) pValue));
    }

    private static short? ToNullableInt16(bool pValue)
    {
      return new short?(pValue ? (short) 1 : (short) 0);
    }

    public static short? ToNullableInt16(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new short?();
      if (pValue is short)
        return Convert.ToNullableInt16((short) pValue);
      if (pValue is string)
        return Convert.ToNullableInt16((string) pValue);
      if (pValue is char)
        return Convert.ToNullableInt16((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableInt16((bool) pValue);
      if (pValue is IConvertible)
        return new short?(((IConvertible) pValue).ToInt16((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (short?));
    }

    private static int? ToNullableInt32(int pValue)
    {
      return new int?(pValue);
    }

    private static int? ToNullableInt32(string pValue)
    {
      if (pValue != null)
        return new int?(int.Parse(pValue));
      return new int?();
    }

    private static int? ToNullableInt32(char pValue)
    {
      return new int?((int) pValue);
    }

    private static int? ToNullableInt32(bool pValue)
    {
      return new int?(pValue ? 1 : 0);
    }

    public static int? ToNullableInt32(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new int?();
      if (pValue is int)
        return Convert.ToNullableInt32((int) pValue);
      if (pValue is string)
        return Convert.ToNullableInt32((string) pValue);
      if (pValue is char)
        return Convert.ToNullableInt32((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableInt32((bool) pValue);
      if (pValue is IConvertible)
        return new int?(((IConvertible) pValue).ToInt32((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (int?));
    }

    private static long? ToNullableInt64(long pValue)
    {
      return new long?(pValue);
    }

    private static long? ToNullableInt64(string pValue)
    {
      if (pValue != null)
        return new long?(long.Parse(pValue));
      return new long?();
    }

    private static long? ToNullableInt64(char pValue)
    {
      return new long?((long) pValue);
    }

    private static long? ToNullableInt64(bool pValue)
    {
      return new long?(pValue ? 1L : 0L);
    }

    private static long? ToNullableInt64(DateTime pValue)
    {
      return new long?((pValue - DateTime.MinValue).Ticks);
    }

    private static long? ToNullableInt64(TimeSpan pValue)
    {
      return new long?(pValue.Ticks);
    }

    public static long? ToNullableInt64(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new long?();
      if (pValue is long)
        return Convert.ToNullableInt64((long) pValue);
      if (pValue is string)
        return Convert.ToNullableInt64((string) pValue);
      if (pValue is char)
        return Convert.ToNullableInt64((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableInt64((bool) pValue);
      if (pValue is DateTime)
        return Convert.ToNullableInt64((DateTime) pValue);
      if (pValue is TimeSpan)
        return Convert.ToNullableInt64((TimeSpan) pValue);
      if (pValue is IConvertible)
        return new long?(((IConvertible) pValue).ToInt64((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (long?));
    }

    private static byte? ToNullableByte(byte pValue)
    {
      return new byte?(pValue);
    }

    private static byte? ToNullableByte(string pValue)
    {
      if (pValue != null)
        return new byte?(byte.Parse(pValue));
      return new byte?();
    }

    private static byte? ToNullableByte(char pValue)
    {
      return new byte?(checked ((byte) pValue));
    }

    private static byte? ToNullableByte(bool pValue)
    {
      return new byte?(pValue ? (byte) 1 : (byte) 0);
    }

    public static byte? ToNullableByte(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new byte?();
      if (pValue is byte)
        return Convert.ToNullableByte((byte) pValue);
      if (pValue is string)
        return Convert.ToNullableByte((string) pValue);
      if (pValue is char)
        return Convert.ToNullableByte((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableByte((bool) pValue);
      if (pValue is IConvertible)
        return new byte?(((IConvertible) pValue).ToByte((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (byte?));
    }

    private static ushort? ToNullableUInt16(ushort pValue)
    {
      return new ushort?(pValue);
    }

    private static ushort? ToNullableUInt16(string pValue)
    {
      if (pValue != null)
        return new ushort?(ushort.Parse(pValue));
      return new ushort?();
    }

    private static ushort? ToNullableUInt16(char pValue)
    {
      return new ushort?((ushort) pValue);
    }

    private static ushort? ToNullableUInt16(bool pValue)
    {
      return new ushort?(pValue ? (ushort) 1 : (ushort) 0);
    }

    public static ushort? ToNullableUInt16(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new ushort?();
      if (pValue is ushort)
        return Convert.ToNullableUInt16((ushort) pValue);
      if (pValue is string)
        return Convert.ToNullableUInt16((string) pValue);
      if (pValue is char)
        return Convert.ToNullableUInt16((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableUInt16((bool) pValue);
      if (pValue is IConvertible)
        return new ushort?(((IConvertible) pValue).ToUInt16((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (ushort?));
    }

    private static uint? ToNullableUInt32(uint pValue)
    {
      return new uint?(pValue);
    }

    private static uint? ToNullableUInt32(string pValue)
    {
      if (pValue != null)
        return new uint?(uint.Parse(pValue));
      return new uint?();
    }

    private static uint? ToNullableUInt32(char pValue)
    {
      return new uint?((uint) pValue);
    }

    private static uint? ToNullableUInt32(bool pValue)
    {
      return new uint?(pValue ? 1U : 0U);
    }

    public static uint? ToNullableUInt32(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new uint?();
      if (pValue is uint)
        return Convert.ToNullableUInt32((uint) pValue);
      if (pValue is string)
        return Convert.ToNullableUInt32((string) pValue);
      if (pValue is char)
        return Convert.ToNullableUInt32((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableUInt32((bool) pValue);
      if (pValue is IConvertible)
        return new uint?(((IConvertible) pValue).ToUInt32((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (uint?));
    }

    private static ulong? ToNullableUInt64(ulong pValue)
    {
      return new ulong?(pValue);
    }

    private static ulong? ToNullableUInt64(string pValue)
    {
      if (pValue != null)
        return new ulong?(ulong.Parse(pValue));
      return new ulong?();
    }

    private static ulong? ToNullableUInt64(char pValue)
    {
      return new ulong?((ulong) pValue);
    }

    private static ulong? ToNullableUInt64(bool pValue)
    {
      return new ulong?(pValue ? 1UL : 0UL);
    }

    public static ulong? ToNullableUInt64(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new ulong?();
      if (pValue is ulong)
        return Convert.ToNullableUInt64((ulong) pValue);
      if (pValue is string)
        return Convert.ToNullableUInt64((string) pValue);
      if (pValue is char)
        return Convert.ToNullableUInt64((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableUInt64((bool) pValue);
      if (pValue is IConvertible)
        return new ulong?(((IConvertible) pValue).ToUInt64((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (ulong?));
    }

    private static char? ToNullableChar(char pValue)
    {
      return new char?(pValue);
    }

    private static char? ToNullableChar(string pValue)
    {
      char result;
      if (char.TryParse(pValue, out result))
        return new char?(result);
      if (!string.IsNullOrEmpty(pValue))
        return new char?(pValue[0]);
      return new char?(char.MinValue);
    }

    private static char? ToNullableChar(bool pValue)
    {
      return new char?(pValue ? '\x0001' : char.MinValue);
    }

    public static char? ToNullableChar(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new char?();
      if (pValue is char)
        return Convert.ToNullableChar((char) pValue);
      if (pValue is string)
        return Convert.ToNullableChar((string) pValue);
      if (pValue is bool)
        return Convert.ToNullableChar((bool) pValue);
      if (pValue is IConvertible)
        return new char?(((IConvertible) pValue).ToChar((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (char?));
    }

    private static float? ToNullableSingle(float pValue)
    {
      return new float?(pValue);
    }

    private static float? ToNullableSingle(string pValue)
    {
      if (pValue != null)
        return new float?(float.Parse(pValue));
      return new float?();
    }

    private static float? ToNullableSingle(char pValue)
    {
      return new float?((float) pValue);
    }

    private static float? ToNullableSingle(bool pValue)
    {
      return new float?(pValue ? 1f : 0.0f);
    }

    public static float? ToNullableSingle(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new float?();
      if (pValue is float)
        return Convert.ToNullableSingle((float) pValue);
      if (pValue is string)
        return Convert.ToNullableSingle((string) pValue);
      if (pValue is char)
        return Convert.ToNullableSingle((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableSingle((bool) pValue);
      if (pValue is IConvertible)
        return new float?(((IConvertible) pValue).ToSingle((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (float?));
    }

    private static double? ToNullableDouble(double pValue)
    {
      return new double?(pValue);
    }

    private static double? ToNullableDouble(string pValue)
    {
      if (pValue != null)
        return new double?(double.Parse(pValue));
      return new double?();
    }

    private static double? ToNullableDouble(char pValue)
    {
      return new double?((double) pValue);
    }

    private static double? ToNullableDouble(bool pValue)
    {
      return new double?(pValue ? 1.0 : 0.0);
    }

    private static double? ToNullableDouble(DateTime pValue)
    {
      return new double?((pValue - DateTime.MinValue).TotalDays);
    }

    private static double? ToNullableDouble(TimeSpan pValue)
    {
      return new double?(pValue.TotalDays);
    }

    public static double? ToNullableDouble(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new double?();
      if (pValue is double)
        return Convert.ToNullableDouble((double) pValue);
      if (pValue is string)
        return Convert.ToNullableDouble((string) pValue);
      if (pValue is char)
        return Convert.ToNullableDouble((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableDouble((bool) pValue);
      if (pValue is DateTime)
        return Convert.ToNullableDouble((DateTime) pValue);
      if (pValue is TimeSpan)
        return Convert.ToNullableDouble((TimeSpan) pValue);
      if (pValue is IConvertible)
        return new double?(((IConvertible) pValue).ToDouble((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (double?));
    }

    private static bool? ToNullableBoolean(bool pValue)
    {
      return new bool?(pValue);
    }

    private static bool? ToNullableBoolean(string pValue)
    {
      if (pValue != null)
        return new bool?(bool.Parse(pValue));
      return new bool?();
    }

    private static bool? ToNullableBoolean(char pValue)
    {
      return new bool?(Convert.ToBoolean(pValue));
    }

    public static bool? ToNullableBoolean(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new bool?();
      if (pValue is bool)
        return Convert.ToNullableBoolean((bool) pValue);
      if (pValue is string)
        return Convert.ToNullableBoolean((string) pValue);
      if (pValue is char)
        return Convert.ToNullableBoolean((char) pValue);
      if (pValue is IConvertible)
        return new bool?(((IConvertible) pValue).ToBoolean((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (bool?));
    }

    private static Decimal? ToNullableDecimal(Decimal pValue)
    {
      return new Decimal?(pValue);
    }

    private static Decimal? ToNullableDecimal(string pValue)
    {
      if (pValue != null)
        return new Decimal?(Decimal.Parse(pValue));
      return new Decimal?();
    }

    private static Decimal? ToNullableDecimal(char pValue)
    {
      return new Decimal?((Decimal) pValue);
    }

    private static Decimal? ToNullableDecimal(bool pValue)
    {
      return new Decimal?(pValue ? new Decimal(10, 0, 0, false, (byte) 1) : new Decimal(0, 0, 0, false, (byte) 1));
    }

    public static Decimal? ToNullableDecimal(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new Decimal?();
      if (pValue is double && double.IsNaN((double) pValue))
        return new Decimal?();
      if (pValue is Decimal)
        return Convert.ToNullableDecimal((Decimal) pValue);
      if (pValue is string)
        return Convert.ToNullableDecimal((string) pValue);
      if (pValue is char)
        return Convert.ToNullableDecimal((char) pValue);
      if (pValue is bool)
        return Convert.ToNullableDecimal((bool) pValue);
      if (pValue is IConvertible)
        return new Decimal?(((IConvertible) pValue).ToDecimal((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (Decimal?));
    }

    private static DateTime? ToNullableDateTime(DateTime pValue)
    {
      return new DateTime?(pValue);
    }

    private static DateTime? ToNullableDateTime(string pValue)
    {
      if (pValue != null)
        return new DateTime?(DateTime.Parse(pValue));
      return new DateTime?();
    }

    private static DateTime? ToNullableDateTime(TimeSpan pValue)
    {
      return new DateTime?(DateTime.MinValue + pValue);
    }

    private static DateTime? ToNullableDateTime(long pValue)
    {
      return new DateTime?(DateTime.MinValue + TimeSpan.FromTicks(pValue));
    }

    private static DateTime? ToNullableDateTime(double pValue)
    {
      return new DateTime?(DateTime.MinValue + TimeSpan.FromDays(pValue));
    }

    public static DateTime? ToNullableDateTime(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new DateTime?();
      if (pValue is DateTime)
        return Convert.ToNullableDateTime((DateTime) pValue);
      if (pValue is string)
        return Convert.ToNullableDateTime((string) pValue);
      if (pValue is TimeSpan)
        return Convert.ToNullableDateTime((TimeSpan) pValue);
      if (pValue is long)
        return Convert.ToNullableDateTime((long) pValue);
      if (pValue is double)
        return Convert.ToNullableDateTime((double) pValue);
      if (pValue is IConvertible)
        return new DateTime?(((IConvertible) pValue).ToDateTime((IFormatProvider) null));
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (DateTime?));
    }

    private static Guid? ToNullableGuid(Guid pValue)
    {
      return new Guid?(pValue);
    }

    private static Guid? ToNullableGuid(string pValue)
    {
      if (pValue != null)
        return new Guid?(new Guid(pValue));
      return new Guid?();
    }

    private static Guid? ToNullableGuid(Type pValue)
    {
      if (!(pValue == (Type) null))
        return new Guid?(pValue.GUID);
      return new Guid?();
    }

    private static Guid? ToNullableGuid(byte[] pValue)
    {
      if (pValue != null)
        return new Guid?(new Guid(pValue));
      return new Guid?();
    }

    public static Guid? ToNullableGuid(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return new Guid?();
      if (pValue is Guid)
        return Convert.ToNullableGuid((Guid) pValue);
      if (pValue is string)
        return Convert.ToNullableGuid((string) pValue);
      if ((object) (pValue as Type) != null)
        return Convert.ToNullableGuid((Type) pValue);
      if (pValue is byte[])
        return Convert.ToNullableGuid((byte[]) pValue);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (Guid?));
    }

    private static byte[] ToByteArray(string pValue)
    {
      if (pValue != null)
        return Encoding.UTF8.GetBytes(pValue);
      return (byte[]) null;
    }

    private static byte[] ToByteArray(sbyte pValue)
    {
      return new byte[1]{ checked ((byte) pValue) };
    }

    private static byte[] ToByteArray(short pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(int pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(long pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(byte pValue)
    {
      return new byte[1]{ pValue };
    }

    private static byte[] ToByteArray(ushort pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(uint pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(ulong pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(char pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(float pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(double pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(bool pValue)
    {
      return BitConverter.GetBytes(pValue);
    }

    private static byte[] ToByteArray(DateTime pValue)
    {
      return Convert.ToByteArray(pValue.ToBinary());
    }

    private static byte[] ToByteArray(TimeSpan pValue)
    {
      return Convert.ToByteArray(pValue.Ticks);
    }

    private static byte[] ToByteArray(Guid pValue)
    {
      if (!(pValue == Guid.Empty))
        return pValue.ToByteArray();
      return (byte[]) null;
    }

    private static byte[] ToByteArray(Decimal pValue)
    {
      int[] bits = Decimal.GetBits(pValue);
      byte[] numArray = new byte[bits.Length << 2];
      for (int index = 0; index < bits.Length; ++index)
        Buffer.BlockCopy((Array) BitConverter.GetBytes(bits[index]), 0, (Array) numArray, index * 4, 4);
      return numArray;
    }

    private static byte[] ToByteArray(Stream pValue)
    {
      if (pValue == null)
        return (byte[]) null;
      if (pValue is MemoryStream)
        return ((MemoryStream) pValue).ToArray();
      long num = pValue.Seek(0L, SeekOrigin.Begin);
      byte[] buffer = new byte[pValue.Length];
      pValue.Read(buffer, 0, buffer.Length);
      pValue.Position = num;
      return buffer;
    }

    public static byte[] ToByteArray(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return (byte[]) null;
      if (pValue is byte[])
        return (byte[]) pValue;
      if (pValue is string)
        return Convert.ToByteArray((string) pValue);
      if (pValue is sbyte)
        return Convert.ToByteArray((sbyte) pValue);
      if (pValue is short)
        return Convert.ToByteArray((short) pValue);
      if (pValue is int)
        return Convert.ToByteArray((int) pValue);
      if (pValue is long)
        return Convert.ToByteArray((long) pValue);
      if (pValue is byte)
        return Convert.ToByteArray((byte) pValue);
      if (pValue is ushort)
        return Convert.ToByteArray((ushort) pValue);
      if (pValue is uint)
        return Convert.ToByteArray((uint) pValue);
      if (pValue is ulong)
        return Convert.ToByteArray((ulong) pValue);
      if (pValue is char)
        return Convert.ToByteArray((char) pValue);
      if (pValue is float)
        return Convert.ToByteArray((float) pValue);
      if (pValue is double)
        return Convert.ToByteArray((double) pValue);
      if (pValue is bool)
        return Convert.ToByteArray((bool) pValue);
      if (pValue is Decimal)
        return Convert.ToByteArray((Decimal) pValue);
      if (pValue is DateTime)
        return Convert.ToByteArray((DateTime) pValue);
      if (pValue is TimeSpan)
        return Convert.ToByteArray((TimeSpan) pValue);
      if (pValue is Stream)
        return Convert.ToByteArray((Stream) pValue);
      if (pValue is Guid)
        return Convert.ToByteArray((Guid) pValue);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (byte[]));
    }

    private static char[] ToCharArray(string pValue)
    {
      return pValue?.ToCharArray();
    }

    public static char[] ToCharArray(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return (char[]) null;
      if (pValue is char[])
        return (char[]) pValue;
      if (pValue is string)
        return Convert.ToCharArray((string) pValue);
      return Convert.ToString(pValue).ToCharArray();
    }

    private static XmlDocument ToXmlDocument(string pValue)
    {
      if (pValue == null)
        return (XmlDocument) null;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(pValue);
      return xmlDocument;
    }

    private static XmlDocument ToXmlDocument(Stream pValue)
    {
      if (pValue == null)
        return (XmlDocument) null;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(pValue);
      return xmlDocument;
    }

    private static XmlDocument ToXmlDocument(TextReader pValue)
    {
      if (pValue == null)
        return (XmlDocument) null;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(pValue);
      return xmlDocument;
    }

    private static XmlDocument ToXmlDocument(XDocument p)
    {
      if (p == null)
        return (XmlDocument) null;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(p.ToString());
      return xmlDocument;
    }

    private static XmlDocument ToXmlDocument(XElement p)
    {
      if (p == null)
        return (XmlDocument) null;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(p.ToString());
      return xmlDocument;
    }

    private static XmlDocument ToXmlDocument(char[] pValue)
    {
      if (pValue != null)
        return Convert.ToXmlDocument(new string(pValue));
      return (XmlDocument) null;
    }

    private static XmlDocument ToXmlDocument(byte[] pValue)
    {
      if (pValue != null)
        return Convert.ToXmlDocument((Stream) new MemoryStream(pValue));
      return (XmlDocument) null;
    }

    private static XmlDocument ToXmlDocument(XmlReader pValue)
    {
      if (pValue == null)
        return (XmlDocument) null;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(pValue);
      return xmlDocument;
    }

    public static XmlDocument ToXmlDocument(object pValue)
    {
      if (pValue == null || pValue is DBNull)
        return (XmlDocument) null;
      if (pValue is XmlDocument)
        return (XmlDocument) pValue;
      if (pValue is string)
        return Convert.ToXmlDocument((string) pValue);
      if (pValue is Stream)
        return Convert.ToXmlDocument((Stream) pValue);
      if (pValue is TextReader)
        return Convert.ToXmlDocument((TextReader) pValue);
      if (pValue is XmlReader)
        return Convert.ToXmlDocument((XmlReader) pValue);
      if (pValue is char[])
        return Convert.ToXmlDocument((char[]) pValue);
      if (pValue is byte[])
        return Convert.ToXmlDocument((byte[]) pValue);
      if (pValue is XDocument)
        return Convert.ToXmlDocument((XDocument) pValue);
      if (pValue is XElement)
        return Convert.ToXmlDocument((XElement) pValue);
      throw Convert.CreateInvalidCastException(pValue.GetType(), typeof (XmlDocument));
    }

    private static InvalidCastException CreateInvalidCastException(
      Type pOriginalType,
      Type pConversionType)
    {
      return new InvalidCastException(string.Format("Invalid cast from {0} to {1}", (object) pOriginalType.FullName, (object) pConversionType.FullName));
    }
  }
}
