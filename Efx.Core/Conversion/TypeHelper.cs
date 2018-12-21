// Decompiled with JetBrains decompiler
// Type: Efx.Core.Conversion.TypeHelper
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Xml;

namespace Efx.Core.Conversion
{
  internal static class TypeHelper
  {
    private static readonly MethodInfo _createListMethod = typeof (TypeHelper).GetMethod("CreateList", BindingFlags.Static | BindingFlags.NonPublic);

    public static object ChangeType(object pValue, Type pTargetType)
    {
      return TypeHelper.ConvertChangeType(pValue, pTargetType, pTargetType.IsNullable());
    }

    private static TypeConverter GetTypeConverter(object pObj)
    {
      if (pObj == null)
        return (TypeConverter) null;
      return TypeDescriptor.GetConverter(pObj);
    }

    private static Type GetUnderlyingType(Type pType)
    {
      if (pType == (Type) null)
        throw new ArgumentNullException(nameof (pType));
      if (pType.IsNullable())
        pType = pType.GetGenericArguments()[0];
      if (pType.IsEnum)
        pType = Enum.GetUnderlyingType(pType);
      return pType;
    }

    private static object ConvertChangeType(object pValue, Type pTargetType, bool pIsNullable)
    {
      if (pTargetType.IsArray)
      {
        if (pValue == null)
          return (object) null;
        Type type = pValue.GetType();
        if (type == pTargetType)
          return pValue;
        if (type.IsArray)
        {
          Type elementType1 = type.GetElementType();
          Type elementType2 = pTargetType.GetElementType();
          if (elementType1.IsArray != elementType2.IsArray || elementType1.IsArray && elementType1.GetArrayRank() != elementType2.GetArrayRank())
            throw new InvalidCastException(string.Format("Can not convert array of type '{0}' to array of '{1}'.", (object) type.FullName, (object) pTargetType.FullName));
          Array sourceArray = (Array) pValue;
          int rank = sourceArray.Rank;
          Array instance;
          if (rank == 1 && sourceArray.GetLowerBound(0) == 0)
          {
            int length = sourceArray.Length;
            instance = Array.CreateInstance(elementType2, length);
            if (elementType2.IsAssignableFrom(elementType1))
            {
              Array.Copy(sourceArray, instance, length);
            }
            else
            {
              for (int index = 0; index < length; ++index)
                instance.SetValue(TypeHelper.ConvertChangeType(sourceArray.GetValue(index), elementType2, pIsNullable), index);
            }
          }
          else
          {
            int num1 = 1;
            int[] lengths = new int[rank];
            int[] numArray = new int[rank];
            int[] lowerBounds = new int[rank];
            for (int dimension = 0; dimension < rank; ++dimension)
            {
              num1 *= lengths[dimension] = sourceArray.GetLength(dimension);
              lowerBounds[dimension] = sourceArray.GetLowerBound(dimension);
            }
            instance = Array.CreateInstance(elementType2, lengths, lowerBounds);
            for (int index1 = 0; index1 < num1; ++index1)
            {
              int num2 = index1;
              for (int index2 = rank - 1; index2 >= 0; --index2)
              {
                numArray[index2] = num2 % lengths[index2] + lowerBounds[index2];
                num2 /= lengths[index2];
              }
              instance.SetValue(TypeHelper.ConvertChangeType(sourceArray.GetValue(numArray), elementType2, pIsNullable), numArray);
            }
          }
          return (object) instance;
        }
      }
      else if (pTargetType.IsEnum)
      {
        try
        {
          return Enum.Parse(pTargetType, pValue.ToString(), true);
        }
        catch (ArgumentException ex)
        {
          throw new InvalidCastException("Type conversion failed", (Exception) ex);
        }
      }
      if (pIsNullable)
      {
        switch (Type.GetTypeCode(TypeHelper.GetUnderlyingType(pTargetType)))
        {
          case TypeCode.Boolean:
            return (object) TypeHelper.ConvertToNullableBoolean(pValue);
          case TypeCode.Char:
            return (object) TypeHelper.ConvertToNullableChar(pValue);
          case TypeCode.SByte:
            return (object) TypeHelper.ConvertToNullableSByte(pValue);
          case TypeCode.Byte:
            return (object) TypeHelper.ConvertToNullableByte(pValue);
          case TypeCode.Int16:
            return (object) TypeHelper.ConvertToNullableInt16(pValue);
          case TypeCode.UInt16:
            return (object) TypeHelper.ConvertToNullableUInt16(pValue);
          case TypeCode.Int32:
            return (object) TypeHelper.ConvertToNullableInt32(pValue);
          case TypeCode.UInt32:
            return (object) TypeHelper.ConvertToNullableUInt32(pValue);
          case TypeCode.Int64:
            return (object) TypeHelper.ConvertToNullableInt64(pValue);
          case TypeCode.UInt64:
            return (object) TypeHelper.ConvertToNullableUInt64(pValue);
          case TypeCode.Single:
            return (object) TypeHelper.ConvertToNullableSingle(pValue);
          case TypeCode.Double:
            return (object) TypeHelper.ConvertToNullableDouble(pValue);
          case TypeCode.Decimal:
            return (object) TypeHelper.ConvertToNullableDecimal(pValue);
          case TypeCode.DateTime:
            return (object) TypeHelper.ConvertToNullableDateTime(pValue);
          default:
            if (typeof (Guid) == TypeHelper.GetUnderlyingType(pTargetType))
              return (object) TypeHelper.ConvertToNullableGuid(pValue);
            break;
        }
      }
      switch (Type.GetTypeCode(pTargetType))
      {
        case TypeCode.Boolean:
          return (object) TypeHelper.ConvertToBoolean(pValue);
        case TypeCode.Char:
          return (object) TypeHelper.ConvertToChar(pValue);
        case TypeCode.SByte:
          return (object) TypeHelper.ConvertToSByte(pValue);
        case TypeCode.Byte:
          return (object) TypeHelper.ConvertToByte(pValue);
        case TypeCode.Int16:
          return (object) TypeHelper.ConvertToInt16(pValue);
        case TypeCode.UInt16:
          return (object) TypeHelper.ConvertToUInt16(pValue);
        case TypeCode.Int32:
          return (object) TypeHelper.ConvertToInt32(pValue);
        case TypeCode.UInt32:
          return (object) TypeHelper.ConvertToUInt32(pValue);
        case TypeCode.Int64:
          return (object) TypeHelper.ConvertToInt64(pValue);
        case TypeCode.UInt64:
          return (object) TypeHelper.ConvertToUInt64(pValue);
        case TypeCode.Single:
          return (object) TypeHelper.ConvertToSingle(pValue);
        case TypeCode.Double:
          return (object) TypeHelper.ConvertToDouble(pValue);
        case TypeCode.Decimal:
          return (object) TypeHelper.ConvertToDecimal(pValue);
        case TypeCode.DateTime:
          return (object) TypeHelper.ConvertToDateTime(pValue);
        case TypeCode.String:
          return (object) TypeHelper.ConvertToString(pValue);
        default:
          if (typeof (Guid) == pTargetType)
            return (object) TypeHelper.ConvertToGuid(pValue);
          if (typeof (XmlDocument) == pTargetType)
            return (object) TypeHelper.ConvertToXmlDocument(pValue);
          if (typeof (byte[]) == pTargetType)
            return (object) TypeHelper.ConvertToByteArray(pValue);
          if (typeof (char[]) == pTargetType)
            return (object) TypeHelper.ConvertToCharArray(pValue);
          if (pValue == null)
            return (object) null;
          if (pTargetType.IsAssignableFrom(pValue.GetType()))
            return pValue;
          TypeConverter typeConverter1 = TypeHelper.GetTypeConverter((object) pTargetType);
          if (typeConverter1 != null && typeConverter1.CanConvertFrom(pValue.GetType()))
            return typeConverter1.ConvertFrom(pValue);
          TypeConverter typeConverter2 = TypeHelper.GetTypeConverter(pValue);
          if (typeConverter2 != null && typeConverter2.CanConvertTo(pTargetType))
            return typeConverter2.ConvertTo(pValue, pTargetType);
          if (pTargetType.IsInterface)
          {
            object obj = typeof (TypeHelper).GetMethod("Cast", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(pTargetType).Invoke((object) null, new object[1]{ pValue });
            if (obj != null)
              return obj;
          }
          IList list1 = pValue as IList;
          if (TypeHelper.ImplementsInterface(pTargetType, "System.Collections.Generic.ICollection`1") && list1 != null)
          {
            object obj = (object) null;
            if (TypeHelper.IsListType(pTargetType))
              obj = (object) TypeHelper.createList(pTargetType);
            if (obj == null)
              obj = pTargetType.CreateInstance();
            if (obj != null)
            {
              Type[] genericArguments = pTargetType.GetGenericArguments();
              if (genericArguments.Length == 1)
              {
                MethodInfo method = pTargetType.GetInterface("System.Collections.Generic.ICollection`1", true).GetMethod("Add");
                for (int index = 0; index < list1.Count; ++index)
                  method.Invoke(obj, new object[1]
                  {
                    TypeHelper.ChangeType(list1[index], genericArguments[0])
                  });
              }
              return obj;
            }
          }
          if (TypeHelper.ImplementsInterface(pTargetType, "System.Collections.IList") && list1 != null)
          {
            object instance = pTargetType.CreateInstance();
            if (instance != null)
            {
              IList list2 = instance as IList;
              foreach (object obj in (IEnumerable) list1)
                list2.Add(obj);
              return instance;
            }
          }
          if (TypeHelper.ImplementsInterface(pTargetType, "System.Collections.Generic.IDictionary`2") && pValue is IDictionary)
          {
            object instance = pTargetType.CreateInstance();
            if (instance != null)
            {
              Type[] genericArguments = pTargetType.GetGenericArguments();
              if (genericArguments.Length == 2)
              {
                MethodInfo method = pTargetType.GetInterface("System.Collections.Generic.IDictionary`2", true).GetMethod("Add");
                foreach (DictionaryEntry dictionaryEntry in pValue as IDictionary)
                  method.Invoke(instance, new object[2]
                  {
                    TypeHelper.ChangeType(dictionaryEntry.Key, genericArguments[0]),
                    TypeHelper.ChangeType(dictionaryEntry.Value, genericArguments[1])
                  });
              }
              return instance;
            }
          }
          if (TypeHelper.ImplementsInterface(pTargetType, "System.Collections.IDictionary") && pValue is IDictionary)
          {
            object instance = pTargetType.CreateInstance();
            if (instance != null)
            {
              IDictionary dictionary1 = pValue as IDictionary;
              IDictionary dictionary2 = instance as IDictionary;
              foreach (DictionaryEntry dictionaryEntry in dictionary1)
                dictionary2.Add(dictionaryEntry.Key, dictionaryEntry.Value);
              return instance;
            }
          }
          IEnumerable enumerable = pValue as IEnumerable;
          if ((pTargetType.FullName.StartsWith("System.Collections.Generic.IEnumerable`1", StringComparison.Ordinal) || TypeHelper.ImplementsInterface(pTargetType, "System.Collections.Generic.IEnumerable`1")) && enumerable != null)
          {
            Type genericArgument = pTargetType.GetGenericArguments()[0];
            IList list2 = (IList) TypeHelper._createListMethod.MakeGenericMethod(pTargetType.GetGenericArguments()).Invoke((object) null, new object[0]);
            foreach (object pValue1 in enumerable)
              list2.Add(TypeHelper.ChangeType(pValue1, genericArgument));
            return (object) list2;
          }
          if (pValue is IConvertible)
            return System.Convert.ChangeType(pValue, pTargetType, (IFormatProvider) null);
          return pValue;
      }
    }

    [Obfuscation(Feature = "renaming")]
    private static IList CreateList<T>()
    {
      return (IList) new List<T>();
    }

    [Obfuscation(Feature = "renaming")]
    private static T Cast<T>(object obj) where T : class
    {
      return obj as T;
    }

    private static IList createList(Type pListType)
    {
      bool flag1 = false;
      IList list;
      if (pListType.IsArray)
      {
        list = (IList) new List<object>();
        flag1 = true;
      }
      else
      {
        Type pImplementingType;
        if (TypeHelper.IsSubClass(pListType, typeof (ReadOnlyCollection<>), out pImplementingType))
        {
          Type genericArgument = pImplementingType.GetGenericArguments()[0];
          Type type = typeof (IEnumerable<>).MakeGenericType(genericArgument);
          bool flag2 = false;
          foreach (MethodBase constructor in pListType.GetConstructors())
          {
            IList<ParameterInfo> parameters = (IList<ParameterInfo>) constructor.GetParameters();
            if (parameters.Count == 1 && type.IsAssignableFrom(parameters[0].ParameterType))
            {
              flag2 = true;
              break;
            }
          }
          if (!flag2)
            throw new Exception(string.Format("Readonly type {0} does not have a public constructor that takes a type that implements {1}.", (object) pListType, (object) type));
          list = (IList) TypeHelper.CreateGeneric(typeof (List<>), genericArgument);
          flag1 = true;
        }
        else if (typeof (IList).IsAssignableFrom(pListType) && TypeHelper.IsInstantiatableType(pListType))
        {
          list = (IList) Activator.CreateInstance(pListType);
        }
        else
        {
          if (!TypeHelper.IsSubClass(pListType, typeof (IList<>)))
            throw new Exception(string.Format("Cannot create and populate list type {0}.", (object) pListType));
          list = TypeHelper.CreateGeneric(typeof (List<>), pListType.GetGenericArguments()[0]) as IList;
        }
      }
      if (flag1)
      {
        if (pListType.IsArray)
          list = (IList) ((List<object>) list).ToArray();
        else if (TypeHelper.IsSubClass(pListType, typeof (ReadOnlyCollection<>)))
          list = (IList) Activator.CreateInstance(pListType, (object) list);
      }
      return list;
    }

    private static object CreateGeneric(
      Type pGenericTypeDefinition,
      Type pInnerType,
      params object[] pArgs)
    {
      return TypeHelper.CreateGeneric(pGenericTypeDefinition, (IEnumerable) new Type[1]{ pInnerType }, pArgs);
    }

    private static object CreateGeneric(
      Type pGenericTypeDefinition,
      IEnumerable pInnerTypes,
      params object[] pArgs)
    {
      return Activator.CreateInstance(pGenericTypeDefinition.MakeGenericType(TypeHelper.CreateArray(pInnerTypes) as Type[]), pArgs);
    }

    private static Array CreateArray(IEnumerable pCollection)
    {
      Array array = pCollection as Array;
      if (array != null)
        return array;
      List<object> objectList = new List<object>();
      foreach (object p in pCollection)
        objectList.Add(p);
      return (Array) objectList.ToArray();
    }

    private static bool IsInstantiatableType(Type pT)
    {
      if (!pT.IsAbstract && !pT.IsInterface && (!pT.IsArray && !pT.IsGenericTypeDefinition) && !(pT == typeof (void)))
        return TypeHelper.HasDefaultConstructor(pT);
      return false;
    }

    private static bool HasDefaultConstructor(Type pT)
    {
      if (pT.IsValueType)
        return true;
      return pT.GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[0], (ParameterModifier[]) null) != (ConstructorInfo) null;
    }

    private static bool IsListType(Type pType)
    {
      if (pType.IsArray || typeof (IList).IsAssignableFrom(pType))
        return true;
      return TypeHelper.IsSubClass(pType, typeof (IList<>));
    }

    private static bool IsSubClass(Type pType, Type pCheck)
    {
      Type pImplementingType;
      return TypeHelper.IsSubClass(pType, pCheck, out pImplementingType);
    }

    private static bool IsSubClass(Type pType, Type pCheck, out Type pImplementingType)
    {
      return TypeHelper.IsSubClassInternal(pType, pType, pCheck, out pImplementingType);
    }

    private static bool IsSubClassInternal(
      Type pInitialType,
      Type pCurrentType,
      Type pCheck,
      out Type pImplementingType)
    {
      if (pCurrentType == pCheck)
      {
        pImplementingType = pCurrentType;
        return true;
      }
      if (pCheck.IsInterface && (pInitialType.IsInterface || pCurrentType == pInitialType))
      {
        foreach (Type pCurrentType1 in pCurrentType.GetInterfaces())
        {
          if (TypeHelper.IsSubClassInternal(pInitialType, pCurrentType1, pCheck, out pImplementingType))
          {
            if (pCheck == pImplementingType)
              pImplementingType = pCurrentType;
            return true;
          }
        }
      }
      if (pCurrentType.IsGenericType && !pCurrentType.IsGenericTypeDefinition && TypeHelper.IsSubClassInternal(pInitialType, pCurrentType.GetGenericTypeDefinition(), pCheck, out pImplementingType))
      {
        pImplementingType = pCurrentType;
        return true;
      }
      if (!(pCurrentType.BaseType == (Type) null))
        return TypeHelper.IsSubClassInternal(pInitialType, pCurrentType.BaseType, pCheck, out pImplementingType);
      pImplementingType = (Type) null;
      return false;
    }

    private static bool ImplementsInterface(Type pType, string pInterfaceName)
    {
      return pType.GetInterface(pInterfaceName, true) != (Type) null;
    }

    private static sbyte? ConvertToNullableSByte(object pValue)
    {
      if (pValue is sbyte)
        return (sbyte?) pValue;
      if (pValue != null)
        return Convert.ToNullableSByte(pValue);
      return new sbyte?();
    }

    private static short? ConvertToNullableInt16(object pValue)
    {
      if (pValue is short)
        return (short?) pValue;
      if (pValue != null)
        return Convert.ToNullableInt16(pValue);
      return new short?();
    }

    private static int? ConvertToNullableInt32(object pValue)
    {
      if (pValue is int)
        return (int?) pValue;
      if (pValue != null)
        return Convert.ToNullableInt32(pValue);
      return new int?();
    }

    private static long? ConvertToNullableInt64(object pValue)
    {
      if (pValue is long)
        return (long?) pValue;
      if (pValue != null)
        return Convert.ToNullableInt64(pValue);
      return new long?();
    }

    private static byte? ConvertToNullableByte(object pValue)
    {
      if (pValue is byte)
        return (byte?) pValue;
      if (pValue != null)
        return Convert.ToNullableByte(pValue);
      return new byte?();
    }

    private static ushort? ConvertToNullableUInt16(object pValue)
    {
      if (pValue is ushort)
        return (ushort?) pValue;
      if (pValue != null)
        return Convert.ToNullableUInt16(pValue);
      return new ushort?();
    }

    private static uint? ConvertToNullableUInt32(object pValue)
    {
      if (pValue is uint)
        return (uint?) pValue;
      if (pValue != null)
        return Convert.ToNullableUInt32(pValue);
      return new uint?();
    }

    private static ulong? ConvertToNullableUInt64(object pValue)
    {
      if (pValue is ulong)
        return (ulong?) pValue;
      if (pValue != null)
        return Convert.ToNullableUInt64(pValue);
      return new ulong?();
    }

    private static char? ConvertToNullableChar(object pValue)
    {
      if (pValue is char)
        return (char?) pValue;
      if (pValue != null)
        return Convert.ToNullableChar(pValue);
      return new char?();
    }

    private static double? ConvertToNullableDouble(object pValue)
    {
      if (pValue is double)
        return (double?) pValue;
      if (pValue != null)
        return Convert.ToNullableDouble(pValue);
      return new double?();
    }

    private static float? ConvertToNullableSingle(object pValue)
    {
      if (pValue is float)
        return (float?) pValue;
      if (pValue != null)
        return Convert.ToNullableSingle(pValue);
      return new float?();
    }

    private static bool? ConvertToNullableBoolean(object pValue)
    {
      if (pValue is bool)
        return (bool?) pValue;
      if (pValue != null)
        return Convert.ToNullableBoolean(pValue);
      return new bool?();
    }

    private static DateTime? ConvertToNullableDateTime(object pValue)
    {
      if (pValue is DateTime)
        return (DateTime?) pValue;
      if (pValue != null)
        return Convert.ToNullableDateTime(pValue);
      return new DateTime?();
    }

    private static Decimal? ConvertToNullableDecimal(object pValue)
    {
      if (pValue is Decimal)
        return (Decimal?) pValue;
      if (pValue != null)
        return Convert.ToNullableDecimal(pValue);
      return new Decimal?();
    }

    private static Guid? ConvertToNullableGuid(object pValue)
    {
      if (pValue is Guid)
        return (Guid?) pValue;
      if (pValue != null)
        return Convert.ToNullableGuid(pValue);
      return new Guid?();
    }

    private static sbyte ConvertToSByte(object pValue)
    {
      if (pValue is sbyte)
        return (sbyte) pValue;
      if (pValue != null)
        return Convert.ToSByte(pValue);
      return 0;
    }

    private static short ConvertToInt16(object pValue)
    {
      if (pValue is short)
        return (short) pValue;
      if (pValue != null)
        return Convert.ToInt16(pValue);
      return 0;
    }

    private static int ConvertToInt32(object pValue)
    {
      if (pValue is int)
        return (int) pValue;
      if (pValue != null)
        return Convert.ToInt32(pValue);
      return 0;
    }

    private static long ConvertToInt64(object pValue)
    {
      if (pValue is long)
        return (long) pValue;
      if (pValue != null)
        return Convert.ToInt64(pValue);
      return 0;
    }

    private static byte ConvertToByte(object pValue)
    {
      if (pValue is byte)
        return (byte) pValue;
      if (pValue != null)
        return Convert.ToByte(pValue);
      return 0;
    }

    private static ushort ConvertToUInt16(object pValue)
    {
      if (pValue is ushort)
        return (ushort) pValue;
      if (pValue != null)
        return Convert.ToUInt16(pValue);
      return 0;
    }

    private static uint ConvertToUInt32(object pValue)
    {
      if (pValue is uint)
        return (uint) pValue;
      if (pValue != null)
        return Convert.ToUInt32(pValue);
      return 0;
    }

    private static ulong ConvertToUInt64(object pValue)
    {
      if (pValue is ulong)
        return (ulong) pValue;
      if (pValue != null)
        return Convert.ToUInt64(pValue);
      return 0;
    }

    private static char ConvertToChar(object pValue)
    {
      if (pValue is char)
        return (char) pValue;
      if (pValue != null)
        return Convert.ToChar(pValue);
      return char.MinValue;
    }

    private static float ConvertToSingle(object pValue)
    {
      if (pValue is float)
        return (float) pValue;
      if (pValue != null)
        return Convert.ToSingle(pValue);
      return 0.0f;
    }

    private static double ConvertToDouble(object pValue)
    {
      if (pValue is double)
        return (double) pValue;
      if (pValue != null)
        return Convert.ToDouble(pValue);
      return 0.0;
    }

    private static bool ConvertToBoolean(object pValue)
    {
      if (pValue is bool)
        return (bool) pValue;
      if (pValue != null)
        return Convert.ToBoolean(pValue);
      return false;
    }

    private static string ConvertToString(object pValue)
    {
      if (pValue is string)
        return (string) pValue;
      if (pValue != null)
        return Convert.ToString(pValue);
      return (string) null;
    }

    private static DateTime ConvertToDateTime(object pValue)
    {
      if (pValue is DateTime)
        return (DateTime) pValue;
      if (pValue != null)
        return Convert.ToDateTime(pValue);
      return new DateTime();
    }

    private static Decimal ConvertToDecimal(object pValue)
    {
      if (pValue is Decimal)
        return (Decimal) pValue;
      if (pValue != null)
        return Convert.ToDecimal(pValue);
      return new Decimal(0);
    }

    private static Guid ConvertToGuid(object pValue)
    {
      if (pValue is Guid)
        return (Guid) pValue;
      if (pValue != null)
        return Convert.ToGuid(pValue);
      return new Guid();
    }

    private static XmlDocument ConvertToXmlDocument(object pValue)
    {
      if (pValue is XmlDocument)
        return (XmlDocument) pValue;
      if (pValue != null)
        return Convert.ToXmlDocument(pValue);
      return (XmlDocument) null;
    }

    private static byte[] ConvertToByteArray(object pValue)
    {
      if (pValue is byte[])
        return (byte[]) pValue;
      if (pValue != null)
        return Convert.ToByteArray(pValue);
      return (byte[]) null;
    }

    private static char[] ConvertToCharArray(object pValue)
    {
      if (pValue is char[])
        return (char[]) pValue;
      if (pValue != null)
        return Convert.ToCharArray(pValue);
      return (char[]) null;
    }
  }
}
