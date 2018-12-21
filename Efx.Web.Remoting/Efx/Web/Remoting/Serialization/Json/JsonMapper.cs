// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.Serialization.Json.JsonMapper
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core;
using Efx.Core.Caching;
using Efx.Core.Collections;
using Efx.Core.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Serialization;

namespace Efx.Web.Remoting.Serialization.Json
{
  public static class JsonMapper
  {
    private static readonly string[] string_0 = new string[7]
    {
      "yyyy'-'MM'-'dd'T'HH':'mm':'ssK",
      "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fK",
      "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK",
      "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK",
      "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffK",
      "s",
      "u"
    };
    private static readonly MemoryLockedCache<Type, List<Class122>> memoryLockedCache_0 = new MemoryLockedCache<Type, List<Class122>>();
    private static MemoryLockedCache<string, Dictionary<string, JsonMapper.Struct12>> memoryLockedCache_1 = new MemoryLockedCache<string, Dictionary<string, JsonMapper.Struct12>>();
    private static readonly MemoryLockedCache<string, Type> memoryLockedCache_2 = new MemoryLockedCache<string, Type>();
    internal static readonly SyncedDictionary<Type, Func<object, string>> syncedDictionary_0 = new SyncedDictionary<Type, Func<object, string>>();
    private static readonly SyncedDictionary<Type, Func<string, object>> syncedDictionary_1 = new SyncedDictionary<Type, Func<string, object>>();

    public static string ToJson(object pObj, bool pEnableSerializerExtensions = false)
    {
      return new Class125(pEnableSerializerExtensions).method_0(pObj);
    }

    public static object Parse(string pJson)
    {
      return new Class124(pJson).method_0();
    }

    public static T ToObject<T>(string pJson)
    {
      return (T) JsonMapper.ToObject(pJson, typeof (T));
    }

    public static object ToObject(string pJson, Type pType = null)
    {
      object obj = new Class124(pJson).method_0();
      Dictionary<string, object> dictionary_0 = obj as Dictionary<string, object>;
      if (dictionary_0 != null)
        return JsonMapper.smethod_0(dictionary_0, pType);
      return obj;
    }

    internal static object smethod_0(Dictionary<string, object> dictionary_0, Type type_0)
    {
      object obj1;
      if (dictionary_0.TryGetValue("$type", out obj1))
        type_0 = JsonMapper.smethod_11(Configuration.Data.ToDotnetType("json", (string) obj1));
      if (type_0 == (Type) null)
        throw new Exception("Cannot determine type");
      if (type_0 == typeof (object))
        return (object) dictionary_0;
      string fullName = type_0.FullName;
      object instance = type_0.CreateInstance();
      Dictionary<string, JsonMapper.Struct12> dictionary = JsonMapper.smethod_12(type_0, fullName);
      foreach (string key in dictionary_0.Keys)
      {
        JsonMapper.Struct12 struct12;
        if (dictionary.TryGetValue(key, out struct12) && struct12.bool_0)
        {
          object object_0 = dictionary_0[key];
          if (object_0 != null)
          {
            object obj2 = !struct12.bool_12 ? (!struct12.bool_5 ? (!struct12.bool_13 ? (!struct12.bool_14 ? (!struct12.bool_2 ? (!struct12.bool_9 || struct12.bool_16 || struct12.bool_7 ? (!struct12.bool_3 ? (!struct12.bool_1 || struct12.bool_16 ? (!struct12.bool_10 ? (!struct12.bool_15 ? (struct12.bool_7 || struct12.bool_11 ? JsonMapper.smethod_9((IEnumerable) object_0, struct12.type_3, (IList<Type>) struct12.type_2) : (!struct12.bool_8 ? (!struct12.bool_6 ? (!struct12.bool_4 || !(object_0 is Dictionary<string, object>) ? (!struct12.bool_16 ? (!(object_0 is ArrayList) ? object_0 : JsonMapper.smethod_6((IEnumerable) object_0, typeof (object))) : JsonMapper.smethod_21(object_0, struct12.type_1)) : JsonMapper.smethod_0((Dictionary<string, object>) object_0, struct12.type_3)) : (object) JsonMapper.smethod_5((string) object_0)) : JsonMapper.smethod_4(struct12.type_3, (string) object_0))) : JsonMapper.smethod_8((Dictionary<string, object>) object_0, struct12.type_3, (IList<Type>) struct12.type_2)) : (object) JsonMapper.smethod_2((string) object_0)) : JsonMapper.smethod_6((IEnumerable) object_0, struct12.type_0)) : (object) Convert.FromBase64String((string) object_0)) : JsonMapper.smethod_7((IEnumerable) object_0, struct12.type_3, struct12.type_0)) : (object) (bool) object_0) : object_0) : (object) JsonMapper.smethod_3((IEnumerable<char>) (string) object_0)) : JsonMapper.smethod_1((string) object_0, struct12.type_3)) : (object) (int) JsonMapper.smethod_3((IEnumerable<char>) (string) object_0);
            struct12.action_0(instance, obj2);
          }
        }
      }
      return instance;
    }

    private static object smethod_1(string string_1, Type type_0)
    {
      Func<string, object> func;
      if (!JsonMapper.syncedDictionary_1.TryGetValue(type_0, out func))
        throw new Exception(string.Format("Custom type {0} was not found", (object) type_0));
      return func(string_1);
    }

    private static Guid smethod_2(string string_1)
    {
      if (string_1.Length <= 30)
        return new Guid(Convert.FromBase64String(string_1));
      return new Guid(string_1);
    }

    private static long smethod_3(IEnumerable<char> ienumerable_0)
    {
      long num = 0;
      bool flag = false;
      foreach (char ch in ienumerable_0)
      {
        switch (ch)
        {
          case '+':
            flag = false;
            continue;
          case '-':
            flag = true;
            continue;
          default:
            num *= 10L;
            num += (long) ((int) ch - 48);
            continue;
        }
      }
      if (!flag)
        return num;
      return -num;
    }

    private static object smethod_4(Type type_0, string string_1)
    {
      return Enum.Parse(type_0, string_1);
    }

    private static DateTime smethod_5(string string_1)
    {
      DateTime result;
      DateTime.TryParseExact(string_1, JsonMapper.string_0, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result);
      return result;
    }

    private static object smethod_6(IEnumerable ienumerable_0, Type type_0)
    {
      ArrayList arrayList = new ArrayList();
      foreach (object object_0 in ienumerable_0)
      {
        if (object_0 is IDictionary)
          arrayList.Add(JsonMapper.smethod_0((Dictionary<string, object>) object_0, type_0));
        else if (object_0 is IEnumerable && !(object_0 is string))
          arrayList.Add(object_0);
        else
          arrayList.Add(JsonMapper.smethod_21(object_0, type_0));
      }
      return (object) arrayList.ToArray(type_0);
    }

    private static object smethod_7(IEnumerable ienumerable_0, Type type_0, Type type_1)
    {
      IList instance = type_0.CreateInstance<IList>();
      foreach (object object_0 in ienumerable_0)
      {
        if (object_0 is IDictionary)
          instance.Add(JsonMapper.smethod_0((Dictionary<string, object>) object_0, type_1));
        else if (object_0 is ArrayList)
          instance.Add((object) ((ArrayList) object_0).ToArray());
        else
          instance.Add(JsonMapper.smethod_21(object_0, type_1));
      }
      return (object) instance;
    }

    private static object smethod_8(
      Dictionary<string, object> dictionary_0,
      Type type_0,
      IList<Type> ilist_0)
    {
      IDictionary instance = type_0.CreateInstance<IDictionary>();
      Type type_0_1 = (Type) null;
      if (ilist_0 != null)
        type_0_1 = ilist_0[1];
      foreach (KeyValuePair<string, object> keyValuePair in dictionary_0)
      {
        string key = keyValuePair.Key;
        object obj = JsonMapper.smethod_21(keyValuePair.Value, type_0_1);
        instance.Add((object) key, obj);
      }
      return (object) instance;
    }

    private static object smethod_9(IEnumerable ienumerable_0, Type type_0, IList<Type> ilist_0)
    {
      IDictionary instance = type_0.CreateInstance<IDictionary>();
      Type type_0_1 = (Type) null;
      Type type_0_2 = (Type) null;
      if (ilist_0 != null)
      {
        type_0_1 = ilist_0[0];
        type_0_2 = ilist_0[1];
      }
      foreach (Dictionary<string, object> dictionary in ienumerable_0)
      {
        object object_0_1 = dictionary["k"];
        object object_0_2 = dictionary["v"];
        object key = !(object_0_1 is Dictionary<string, object>) ? JsonMapper.smethod_21(object_0_1, type_0_1) : JsonMapper.smethod_0((Dictionary<string, object>) object_0_1, type_0_1);
        object obj = !(object_0_2 is Dictionary<string, object>) ? JsonMapper.smethod_21(object_0_2, type_0_2) : JsonMapper.smethod_0((Dictionary<string, object>) object_0_2, type_0_2);
        instance.Add(key, obj);
      }
      return (object) instance;
    }

    private static Type smethod_10(Type type_0)
    {
      if (type_0.IsGenericType && type_0.GetGenericTypeDefinition() == typeof (Nullable<>))
        return type_0.GetGenericArguments()[0];
      return type_0;
    }

    private static Type smethod_11(string string_1)
    {
      return JsonMapper.memoryLockedCache_2.GetOrAdd(string_1, new Func<string, Type>(Type.GetType));
    }

    private static Dictionary<string, JsonMapper.Struct12> smethod_12(
      Type type_0,
      string string_1)
    {
      return JsonMapper.memoryLockedCache_1.GetOrAdd(string_1, new Func<string, Dictionary<string, JsonMapper.Struct12>>(new JsonMapper.Class135()
      {
        type_0 = type_0
      }.method_0));
    }

    private static Func<object, object> smethod_13(Type type_0, FieldInfo fieldInfo_0)
    {
      DynamicMethod dynamicMethod = new DynamicMethod("_", typeof (object), new Type[1]
      {
        typeof (object)
      }, type_0, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldfld, fieldInfo_0);
      if (fieldInfo_0.FieldType.IsValueType)
        ilGenerator.Emit(OpCodes.Box, fieldInfo_0.FieldType);
      ilGenerator.Emit(OpCodes.Ret);
      return (Func<object, object>) dynamicMethod.CreateDelegate(typeof (Func<object, object>));
    }

    private static Action<object, object> smethod_14(Type type_0, FieldInfo fieldInfo_0)
    {
      Type[] parameterTypes = new Type[2];
      parameterTypes[0] = parameterTypes[1] = typeof (object);
      DynamicMethod dynamicMethod = new DynamicMethod("_", typeof (void), parameterTypes, type_0, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      if (fieldInfo_0.FieldType.IsValueType)
        ilGenerator.Emit(OpCodes.Unbox_Any, fieldInfo_0.FieldType);
      ilGenerator.Emit(OpCodes.Stfld, fieldInfo_0);
      ilGenerator.Emit(OpCodes.Ret);
      return (Action<object, object>) dynamicMethod.CreateDelegate(typeof (Action<object, object>));
    }

    private static JsonMapper.Struct12 smethod_15(Type type_0, string string_1)
    {
      JsonMapper.Struct12 struct12 = new JsonMapper.Struct12()
      {
        bool_0 = true,
        string_0 = string_1,
        type_3 = type_0,
        bool_7 = type_0.Name.Contains("Dictionary")
      };
      if (struct12.bool_7)
        struct12.type_2 = type_0.GetGenericArguments();
      struct12.bool_16 = type_0.IsValueType;
      struct12.bool_9 = type_0.IsGenericType;
      struct12.bool_1 = type_0.IsArray;
      if (struct12.bool_1)
        struct12.type_0 = type_0.GetElementType();
      if (struct12.bool_9)
        struct12.type_0 = type_0.GetGenericArguments()[0];
      struct12.bool_3 = type_0 == typeof (byte[]);
      struct12.bool_10 = type_0 == typeof (Guid) || type_0 == typeof (Guid?);
      struct12.bool_11 = type_0 == typeof (Hashtable);
      struct12.type_1 = JsonMapper.smethod_10(type_0);
      struct12.bool_8 = type_0.IsEnum;
      struct12.bool_6 = type_0 == typeof (DateTime) || type_0 == typeof (DateTime?);
      struct12.bool_12 = type_0 == typeof (int) || type_0 == typeof (int?);
      struct12.bool_13 = type_0 == typeof (long) || type_0 == typeof (long?);
      struct12.bool_14 = type_0 == typeof (string);
      struct12.bool_2 = type_0 == typeof (bool) || type_0 == typeof (bool?);
      struct12.bool_4 = type_0.IsClass;
      if (struct12.bool_7 && struct12.type_2[0] == typeof (string) && struct12.type_2[1] == typeof (string))
        struct12.bool_15 = true;
      struct12.bool_5 = JsonMapper.smethod_16(type_0);
      return struct12;
    }

    internal static bool smethod_16(Type type_0)
    {
      return JsonMapper.syncedDictionary_0.ContainsKey(type_0);
    }

    private static Action<object, object> smethod_17(PropertyInfo propertyInfo_0)
    {
      MethodInfo setMethod = propertyInfo_0.GetSetMethod();
      if (setMethod == (MethodInfo) null)
        return (Action<object, object>) null;
      Type[] parameterTypes = new Type[2];
      parameterTypes[0] = parameterTypes[1] = typeof (object);
      DynamicMethod dynamicMethod = new DynamicMethod("_", typeof (void), parameterTypes);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Castclass, propertyInfo_0.DeclaringType);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(propertyInfo_0.PropertyType.IsClass ? OpCodes.Castclass : OpCodes.Unbox_Any, propertyInfo_0.PropertyType);
      ilGenerator.EmitCall(OpCodes.Callvirt, setMethod, (Type[]) null);
      ilGenerator.Emit(OpCodes.Ret);
      return (Action<object, object>) dynamicMethod.CreateDelegate(typeof (Action<object, object>));
    }

    private static Func<object, object> smethod_18(PropertyInfo propertyInfo_0)
    {
      MethodInfo getMethod = propertyInfo_0.GetGetMethod();
      if (getMethod == (MethodInfo) null)
        return (Func<object, object>) null;
      DynamicMethod dynamicMethod = new DynamicMethod("_", typeof (object), new Type[1]
      {
        typeof (object)
      });
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Castclass, propertyInfo_0.DeclaringType);
      ilGenerator.EmitCall(OpCodes.Callvirt, getMethod, (Type[]) null);
      if (!propertyInfo_0.PropertyType.IsClass)
        ilGenerator.Emit(OpCodes.Box, propertyInfo_0.PropertyType);
      ilGenerator.Emit(OpCodes.Ret);
      return (Func<object, object>) dynamicMethod.CreateDelegate(typeof (Func<object, object>));
    }

    private static List<Class122> smethod_19(Type type_0)
    {
      PropertyInfo[] properties = type_0.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      List<Class122> class122List = new List<Class122>();
      foreach (PropertyInfo propertyInfo in properties)
      {
        if (propertyInfo.CanWrite && !propertyInfo.HasAttribute<NonSerializedAttribute>(true))
        {
          Func<object, object> func = JsonMapper.smethod_18(propertyInfo);
          if (func != null)
          {
            Class122 class122 = new Class122()
            {
              string_0 = propertyInfo.Name,
              func_0 = func,
              type_0 = propertyInfo.PropertyType
            };
            class122List.Add(class122);
          }
        }
      }
      foreach (FieldInfo field in type_0.GetFields(BindingFlags.Instance | BindingFlags.Public))
      {
        if (!field.HasAttribute<XmlIgnoreAttribute>(true))
        {
          Func<object, object> func = JsonMapper.smethod_13(type_0, field);
          if (func != null)
          {
            Class122 class122 = new Class122()
            {
              string_0 = field.Name,
              func_0 = func,
              type_0 = field.FieldType
            };
            class122List.Add(class122);
          }
        }
      }
      return class122List;
    }

    internal static IEnumerable<Class122> smethod_20(Type type_0)
    {
      return (IEnumerable<Class122>) JsonMapper.memoryLockedCache_0.GetOrAdd(type_0, new Func<Type, List<Class122>>(JsonMapper.smethod_19));
    }

    private static object smethod_21(object object_0, Type type_0)
    {
      if (type_0 == typeof (int))
        return (object) (int) JsonMapper.smethod_3((IEnumerable<char>) (string) object_0);
      if (type_0 == typeof (long))
        return (object) JsonMapper.smethod_3((IEnumerable<char>) (string) object_0);
      if (type_0 == typeof (Guid))
        return (object) JsonMapper.smethod_2((string) object_0);
      if (type_0 == typeof (string))
        return object_0;
      if (!type_0.IsEnum)
        return Convert.ChangeType(object_0, type_0, (IFormatProvider) CultureInfo.InvariantCulture);
      return JsonMapper.smethod_4(type_0, (string) object_0);
    }

    public static void RegisterCustomType(
      Type pCustomType,
      Func<object, string> pSerializer,
      Func<string, object> pDeserializer)
    {
      if (pCustomType == (Type) null || pSerializer == null || pDeserializer == null)
        return;
      JsonMapper.syncedDictionary_0[pCustomType] = pSerializer;
      JsonMapper.syncedDictionary_1[pCustomType] = pDeserializer;
      JsonMapper.memoryLockedCache_1 = new MemoryLockedCache<string, Dictionary<string, JsonMapper.Struct12>>();
    }

    private sealed class Class135
    {
      public Type type_0;

      public Dictionary<string, JsonMapper.Struct12> method_0(string string_0)
      {
        Dictionary<string, JsonMapper.Struct12> dictionary = new Dictionary<string, JsonMapper.Struct12>();
        foreach (PropertyInfo property in this.type_0.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          if (!property.HasAttribute<XmlIgnoreAttribute>(true))
          {
            JsonMapper.Struct12 struct12 = JsonMapper.smethod_15(property.PropertyType, property.Name);
            struct12.action_0 = JsonMapper.smethod_17(property);
            struct12.func_0 = JsonMapper.smethod_18(property);
            dictionary.Add(property.Name, struct12);
          }
        }
        foreach (FieldInfo field in this.type_0.GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
          if (!field.HasAttribute<NonSerializedAttribute>(true))
          {
            JsonMapper.Struct12 struct12 = JsonMapper.smethod_15(field.FieldType, field.Name);
            struct12.action_0 = JsonMapper.smethod_14(this.type_0, field);
            struct12.func_0 = JsonMapper.smethod_13(this.type_0, field);
            dictionary.Add(field.Name, struct12);
          }
        }
        return dictionary;
      }
    }

    private struct Struct12
    {
      public Type type_0;
      public Type type_1;
      public bool bool_0;
      public Type[] type_2;
      public Func<object, object> func_0;
      public bool bool_1;
      public bool bool_2;
      public bool bool_3;
      public bool bool_4;
      public bool bool_5;
      public bool bool_6;
      public bool bool_7;
      public bool bool_8;
      public bool bool_9;
      public bool bool_10;
      public bool bool_11;
      public bool bool_12;
      public bool bool_13;
      public bool bool_14;
      public bool bool_15;
      public bool bool_16;
      public string string_0;
      public Type type_3;
      public Action<object, object> action_0;
    }
  }
}
