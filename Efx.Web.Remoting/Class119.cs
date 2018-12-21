// Decompiled with JetBrains decompiler
// Type: Class119
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core;
using Efx.Web.Remoting;
using Efx.Web.Remoting.Serialization.Amf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

internal sealed class Class119 : BinaryWriter
{
  private readonly Dictionary<string, Class120> dictionary_1 = new Dictionary<string, Class120>();
  private readonly Dictionary<Class120, int> dictionary_2 = new Dictionary<Class120, int>();
  private readonly Dictionary<object, int> dictionary_3 = new Dictionary<object, int>();
  private readonly Dictionary<string, int> dictionary_4 = new Dictionary<string, int>();
  private static readonly Dictionary<Type, Action<object, Class119>> dictionary_0 = new Dictionary<Type, Action<object, Class119>>();
  private static readonly DateTime dateTime_0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
  private static Action<object, Class119> action_0;
  private static Action<object, Class119> action_1;
  private static Action<object, Class119> action_2;
  private static Action<object, Class119> action_3;
  private static Action<object, Class119> action_4;
  private static Action<object, Class119> action_5;
  private static Action<object, Class119> action_6;
  private static Action<object, Class119> action_7;
  private static Action<object, Class119> action_8;
  private static Action<object, Class119> action_9;
  private static Action<object, Class119> action_10;
  private static Action<object, Class119> action_11;
  private static Action<object, Class119> action_12;
  private static Action<object, Class119> action_13;
  private static Action<object, Class119> action_14;

  static Class119()
  {
    if (Class119.action_0 == null)
      Class119.action_0 = new Action<object, Class119>(Class119.smethod_1);
    Action<object, Class119> action0 = Class119.action_0;
    if (Class119.action_1 == null)
      Class119.action_1 = new Action<object, Class119>(Class119.smethod_2);
    Action<object, Class119> action1 = Class119.action_1;
    Class119.dictionary_0.Add(typeof (sbyte), action0);
    Class119.dictionary_0.Add(typeof (byte), action0);
    Class119.dictionary_0.Add(typeof (short), action0);
    Class119.dictionary_0.Add(typeof (ushort), action0);
    Class119.dictionary_0.Add(typeof (int), action0);
    Class119.dictionary_0.Add(typeof (uint), action0);
    Class119.dictionary_0.Add(typeof (long), action1);
    Class119.dictionary_0.Add(typeof (ulong), action1);
    Class119.dictionary_0.Add(typeof (float), action1);
    Class119.dictionary_0.Add(typeof (double), action1);
    Class119.dictionary_0.Add(typeof (Decimal), action1);
    Dictionary<Type, Action<object, Class119>> dictionary0_1 = Class119.dictionary_0;
    Type key1 = typeof (DBNull);
    if (Class119.action_2 == null)
      Class119.action_2 = new Action<object, Class119>(Class119.smethod_3);
    Action<object, Class119> action2 = Class119.action_2;
    dictionary0_1.Add(key1, action2);
    Dictionary<Type, Action<object, Class119>> dictionary0_2 = Class119.dictionary_0;
    Type key2 = typeof (Guid);
    if (Class119.action_3 == null)
      Class119.action_3 = new Action<object, Class119>(Class119.smethod_4);
    Action<object, Class119> action3 = Class119.action_3;
    dictionary0_2.Add(key2, action3);
    Dictionary<Type, Action<object, Class119>> dictionary0_3 = Class119.dictionary_0;
    Type key3 = typeof (string);
    if (Class119.action_4 == null)
      Class119.action_4 = new Action<object, Class119>(Class119.smethod_5);
    Action<object, Class119> action4 = Class119.action_4;
    dictionary0_3.Add(key3, action4);
    Dictionary<Type, Action<object, Class119>> dictionary0_4 = Class119.dictionary_0;
    Type key4 = typeof (bool);
    if (Class119.action_5 == null)
      Class119.action_5 = new Action<object, Class119>(Class119.smethod_6);
    Action<object, Class119> action5 = Class119.action_5;
    dictionary0_4.Add(key4, action5);
    Dictionary<Type, Action<object, Class119>> dictionary0_5 = Class119.dictionary_0;
    Type key5 = typeof (Enum);
    if (Class119.action_6 == null)
      Class119.action_6 = new Action<object, Class119>(Class119.smethod_7);
    Action<object, Class119> action6 = Class119.action_6;
    dictionary0_5.Add(key5, action6);
    Dictionary<Type, Action<object, Class119>> dictionary0_6 = Class119.dictionary_0;
    Type key6 = typeof (char);
    if (Class119.action_7 == null)
      Class119.action_7 = new Action<object, Class119>(Class119.smethod_8);
    Action<object, Class119> action7 = Class119.action_7;
    dictionary0_6.Add(key6, action7);
    Dictionary<Type, Action<object, Class119>> dictionary0_7 = Class119.dictionary_0;
    Type key7 = typeof (DateTime);
    if (Class119.action_8 == null)
      Class119.action_8 = new Action<object, Class119>(Class119.smethod_9);
    Action<object, Class119> action8 = Class119.action_8;
    dictionary0_7.Add(key7, action8);
    Dictionary<Type, Action<object, Class119>> dictionary0_8 = Class119.dictionary_0;
    Type key8 = typeof (Array);
    if (Class119.action_9 == null)
      Class119.action_9 = new Action<object, Class119>(Class119.smethod_10);
    Action<object, Class119> action9 = Class119.action_9;
    dictionary0_8.Add(key8, action9);
    Dictionary<Type, Action<object, Class119>> dictionary0_9 = Class119.dictionary_0;
    Type key9 = typeof (XmlDocument);
    if (Class119.action_10 == null)
      Class119.action_10 = new Action<object, Class119>(Class119.smethod_11);
    Action<object, Class119> action10 = Class119.action_10;
    dictionary0_9.Add(key9, action10);
    Dictionary<Type, Action<object, Class119>> dictionary0_10 = Class119.dictionary_0;
    Type key10 = typeof (AmfObject);
    if (Class119.action_11 == null)
      Class119.action_11 = new Action<object, Class119>(Class119.smethod_12);
    Action<object, Class119> action11 = Class119.action_11;
    dictionary0_10.Add(key10, action11);
    Dictionary<Type, Action<object, Class119>> dictionary0_11 = Class119.dictionary_0;
    Type key11 = typeof (byte[]);
    if (Class119.action_12 == null)
      Class119.action_12 = new Action<object, Class119>(Class119.smethod_13);
    Action<object, Class119> action12 = Class119.action_12;
    dictionary0_11.Add(key11, action12);
    Dictionary<Type, Action<object, Class119>> dictionary0_12 = Class119.dictionary_0;
    Type key12 = typeof (NameObjectCollectionBase);
    if (Class119.action_13 == null)
      Class119.action_13 = new Action<object, Class119>(Class119.smethod_14);
    Action<object, Class119> action13 = Class119.action_13;
    dictionary0_12.Add(key12, action13);
  }

  public Class119(Stream stream_0)
    : base(stream_0)
  {
  }

  internal void method_0(byte byte_0)
  {
    this.BaseStream.WriteByte(byte_0);
  }

  private void method_1(Enum2 enum2_0)
  {
    this.BaseStream.WriteByte((byte) enum2_0);
  }

  private void method_2(int int_0)
  {
    this.BaseStream.WriteByte((byte) int_0);
  }

  private void method_3(IList<byte> ilist_0)
  {
    for (int index = 0; ilist_0 != null && index < ilist_0.Count; ++index)
      this.BaseStream.WriteByte(ilist_0[index]);
  }

  internal void method_4(int int_0)
  {
    this.method_13((IList<byte>) BitConverter.GetBytes((ushort) int_0));
  }

  internal void method_5(string string_0)
  {
    UTF8Encoding utF8Encoding = new UTF8Encoding();
    int byteCount = utF8Encoding.GetByteCount(string_0);
    byte[] bytes = utF8Encoding.GetBytes(string_0);
    this.method_4(byteCount);
    if (bytes.Length <= 0)
      return;
    this.Write(bytes);
  }

  internal void method_6(string string_0)
  {
    byte[] bytes = new UTF8Encoding().GetBytes(string_0);
    if (bytes.Length <= 0)
      return;
    this.Write(bytes);
  }

  private void method_7(object object_0)
  {
    IList ilist_0 = object_0 as IList;
    if (ilist_0 != null)
    {
      this.method_18(ilist_0);
    }
    else
    {
      IListSource listSource = object_0 as IListSource;
      if (listSource != null)
      {
        this.method_18(listSource.GetList());
      }
      else
      {
        IDictionary idictionary_0 = object_0 as IDictionary;
        if (idictionary_0 != null)
        {
          this.method_1(Enum2.Array);
          this.method_19(idictionary_0);
        }
        else if (object_0 is IExternalizable)
        {
          this.method_1(Enum2.Object);
          this.method_28(object_0);
        }
        else if (object_0 is IEnumerable)
        {
          List<object> objectList = new List<object>();
          foreach (object obj in object_0 as IEnumerable)
            objectList.Add(obj);
          this.method_18((IList) objectList);
        }
        else
        {
          this.method_1(Enum2.Object);
          this.method_28(object_0);
        }
      }
    }
  }

  internal void method_8(double double_0)
  {
    this.method_12(BitConverter.DoubleToInt64Bits(double_0));
  }

  internal void method_9(float float_0)
  {
    this.method_13((IList<byte>) BitConverter.GetBytes(float_0));
  }

  internal void method_10(int int_0)
  {
    this.method_13((IList<byte>) BitConverter.GetBytes(int_0));
  }

  internal void method_11(bool bool_0)
  {
    this.BaseStream.WriteByte(bool_0 ? (byte) 1 : (byte) 0);
  }

  private void method_12(long long_0)
  {
    this.method_13((IList<byte>) BitConverter.GetBytes(long_0));
  }

  private void method_13(IList<byte> ilist_0)
  {
    if (ilist_0 == null)
      return;
    for (int index = ilist_0.Count - 1; index >= 0; --index)
      this.BaseStream.WriteByte(ilist_0[index]);
  }

  private static object smethod_0(object object_0, string string_0)
  {
    if (object_0 is AmfObject)
    {
      AmfObject amfObject = object_0 as AmfObject;
      if (amfObject.ContainsKey((object) string_0))
        return amfObject[(object) string_0];
    }
    Type type = object_0.GetType();
    PropertyInfo property = type.GetProperty(string_0);
    if (property != (PropertyInfo) null)
    {
      if (property.GetIndexParameters().Length == 0)
        return property.GetValue(object_0, (object[]) null);
      throw new Exception(string.Format("Index parameters are required for the property {0}. Use the [Transient] attribute to identify a property that should be omitted from data that is sent to the client", (object) string_0));
    }
    FieldInfo field = type.GetField(string_0);
    if (!(field != (FieldInfo) null))
      throw new Exception(string.Format("Member {0} not found", (object) string_0));
    return field.GetValue(object_0);
  }

  internal void method_14(object object_0)
  {
    if (object_0 == null)
      this.method_15();
    else if (object_0 is DBNull)
    {
      this.method_15();
    }
    else
    {
      Type type = object_0.GetType();
      Action<object, Class119> action = (Action<object, Class119>) null;
      if (Class119.dictionary_0.ContainsKey(type))
      {
        action = Class119.dictionary_0[type];
      }
      else
      {
        if (type.BaseType == (Type) null)
        {
          this.method_15();
          return;
        }
        if (Class119.dictionary_0.ContainsKey(type.BaseType))
          action = Class119.dictionary_0[type.BaseType];
      }
      if (action == null)
      {
        lock (Class119.dictionary_0)
        {
          if (!Class119.dictionary_0.ContainsKey(type))
          {
            if (Class119.action_14 == null)
              Class119.action_14 = new Action<object, Class119>(Class119.smethod_15);
            action = Class119.action_14;
            Class119.dictionary_0.Add(type, action);
          }
        }
      }
      if (action == null)
        throw new Exception(string.Format("Could not find serializer for type {0}", (object) type.FullName));
      action(object_0, this);
    }
  }

  private void method_15()
  {
    this.method_1(Enum2.Null);
  }

  private void method_16(bool bool_0)
  {
    this.method_0(bool_0 ? (byte) 3 : (byte) 2);
  }

  private void method_17(Array array_0)
  {
    this.method_1(Enum2.Array);
    if (!this.dictionary_3.ContainsKey((object) array_0))
    {
      this.dictionary_3.Add((object) array_0, this.dictionary_3.Count);
      this.method_24(array_0.Length << 1 | 1);
      this.method_21(string.Empty);
      for (int index = 0; index < array_0.Length; ++index)
        this.method_14(array_0.GetValue(index));
    }
    else
      this.method_24(this.dictionary_3[(object) array_0] << 1);
  }

  private void method_18(IList ilist_0)
  {
    this.method_1(Enum2.Array);
    if (!this.dictionary_3.ContainsKey((object) ilist_0))
    {
      this.dictionary_3.Add((object) ilist_0, this.dictionary_3.Count);
      this.method_24(ilist_0.Count << 1 | 1);
      this.method_21(string.Empty);
      foreach (object object_0 in (IEnumerable) ilist_0)
        this.method_14(object_0);
    }
    else
      this.method_24(this.dictionary_3[(object) ilist_0] << 1);
  }

  private void method_19(IDictionary idictionary_0)
  {
    if (!this.dictionary_3.ContainsKey((object) idictionary_0))
    {
      this.dictionary_3.Add((object) idictionary_0, this.dictionary_3.Count);
      this.method_24(1);
      foreach (DictionaryEntry dictionaryEntry in idictionary_0)
      {
        this.method_21(dictionaryEntry.Key.ToString());
        this.method_14(dictionaryEntry.Value);
      }
      this.method_21(string.Empty);
    }
    else
      this.method_24(this.dictionary_3[(object) idictionary_0] << 1);
  }

  private void method_20(IList<byte> ilist_0)
  {
    this.dictionary_3.Add((object) ilist_0, this.dictionary_3.Count);
    this.method_1(Enum2.Bytearray);
    this.method_24(ilist_0.Count << 1 | 1);
    this.method_3(ilist_0);
  }

  private void method_21(string string_0)
  {
    if (string_0 == string.Empty)
      this.method_24(1);
    else if (!this.dictionary_4.ContainsKey(string_0))
    {
      this.dictionary_4.Add(string_0, this.dictionary_4.Count);
      UTF8Encoding utF8Encoding = new UTF8Encoding();
      this.method_24(utF8Encoding.GetByteCount(string_0) << 1 | 1);
      byte[] bytes = utF8Encoding.GetBytes(string_0);
      if (bytes.Length <= 0)
        return;
      this.Write(bytes);
    }
    else
      this.method_24(this.dictionary_4[string_0] << 1);
  }

  private void method_22(string string_0)
  {
    this.method_1(Enum2.String);
    this.method_21(string_0);
  }

  private void method_23(DateTime dateTime_1)
  {
    this.method_1(Enum2.Datetime);
    if (!this.dictionary_3.ContainsKey((object) dateTime_1))
    {
      this.dictionary_3.Add((object) dateTime_1, this.dictionary_3.Count);
      this.method_24(1);
      dateTime_1 = dateTime_1.ToUniversalTime();
      this.method_12(BitConverter.DoubleToInt64Bits((double) (long) dateTime_1.Subtract(Class119.dateTime_0).TotalMilliseconds));
    }
    else
      this.method_24(this.dictionary_3[(object) dateTime_1] << 1);
  }

  private void method_24(int int_0)
  {
    int_0 &= 536870911;
    if (int_0 < 128)
      this.method_2(int_0);
    else if (int_0 < 16384)
    {
      this.method_2(int_0 >> 7 & (int) sbyte.MaxValue | 128);
      this.method_2(int_0 & (int) sbyte.MaxValue);
    }
    else if (int_0 < 2097152)
    {
      this.method_2(int_0 >> 14 & (int) sbyte.MaxValue | 128);
      this.method_2(int_0 >> 7 & (int) sbyte.MaxValue | 128);
      this.method_2(int_0 & (int) sbyte.MaxValue);
    }
    else
    {
      this.method_2(int_0 >> 22 & (int) sbyte.MaxValue | 128);
      this.method_2(int_0 >> 15 & (int) sbyte.MaxValue | 128);
      this.method_2(int_0 >> 8 & (int) sbyte.MaxValue | 128);
      this.method_2(int_0 & (int) byte.MaxValue);
    }
  }

  private void method_25(int int_0)
  {
    if (int_0 >= -268435456 && int_0 <= 268435455)
    {
      this.method_1(Enum2.Integer);
      this.method_24(int_0);
    }
    else
      this.method_26((double) int_0);
  }

  private void method_26(double double_0)
  {
    this.method_1(Enum2.Number);
    this.method_12(BitConverter.DoubleToInt64Bits(double_0));
  }

  private void method_27(XmlDocument xmlDocument_0)
  {
    this.method_1(Enum2.Xml);
    string s = string.Empty;
    if (xmlDocument_0.DocumentElement != null)
      s = xmlDocument_0.DocumentElement.OuterXml;
    if (s == string.Empty)
      this.method_24(1);
    else if (this.dictionary_3.ContainsKey((object) s))
    {
      this.method_24(this.dictionary_3[(object) s] << 1);
    }
    else
    {
      this.dictionary_3.Add((object) s, this.dictionary_3.Count);
      UTF8Encoding utF8Encoding = new UTF8Encoding();
      this.method_24(utF8Encoding.GetByteCount(s) << 1 | 1);
      byte[] bytes = utF8Encoding.GetBytes(s);
      if (bytes.Length <= 0)
        return;
      this.Write(bytes);
    }
  }

  private void method_28(object object_0)
  {
    if (!this.dictionary_3.ContainsKey(object_0))
    {
      this.dictionary_3.Add(object_0, this.dictionary_3.Count);
      Class120 index1 = this.method_30(object_0);
      if (index1 != null)
      {
        this.method_24(this.dictionary_2[index1] << 2 | 1);
      }
      else
      {
        index1 = this.method_31(object_0);
        this.method_24(((index1.method_1() << 1 | (index1.method_4() ? 1 : 0)) << 1 | (index1.method_3() ? 1 : 0)) << 2 | 3);
        this.method_21(index1.method_0());
        for (int index2 = 0; index2 < index1.method_1(); ++index2)
          this.method_21(index1.method_2()[index2]);
      }
      if (index1.method_3())
      {
        if (!(object_0 is IExternalizable))
          throw new Exception(string.Format("Object of type {0} does not implement IExternalizable", (object) index1.method_0()));
        (object_0 as IExternalizable).WriteExternal((IDataOutput) new Class123(this));
      }
      else
      {
        for (int index2 = 0; index2 < index1.method_1(); ++index2)
          this.method_14(Class119.smethod_0(object_0, index1.method_2()[index2]));
        if (!index1.method_4())
          return;
        IDictionary dictionary = object_0 as IDictionary;
        if (dictionary == null)
          return;
        foreach (DictionaryEntry dictionaryEntry in dictionary)
        {
          this.method_21(dictionaryEntry.Key.ToString());
          this.method_14(dictionaryEntry.Value);
        }
        this.method_21(string.Empty);
      }
    }
    else
      this.method_24(this.dictionary_3[object_0] << 1);
  }

  private void method_29(
    NameObjectCollectionBase nameObjectCollectionBase_0)
  {
    if (nameObjectCollectionBase_0 == null)
      return;
    object[] customAttributes = nameObjectCollectionBase_0.GetType().GetCustomAttributes(typeof (DefaultMemberAttribute), false);
    if (customAttributes.Length > 0)
    {
      DefaultMemberAttribute defaultMemberAttribute = customAttributes[0] as DefaultMemberAttribute;
      if (defaultMemberAttribute == null)
        return;
      PropertyInfo property = nameObjectCollectionBase_0.GetType().GetProperty(defaultMemberAttribute.MemberName, new Type[1]
      {
        typeof (string)
      });
      if (property != (PropertyInfo) null)
      {
        AmfObject amfObject = new AmfObject(string.Empty);
        foreach (string key in nameObjectCollectionBase_0.Keys)
        {
          object obj = property.GetValue((object) nameObjectCollectionBase_0, new object[1]
          {
            (object) key
          });
          amfObject.Add((object) key, obj);
        }
        this.method_1(Enum2.Object);
        this.method_28((object) amfObject);
        return;
      }
    }
    this.method_1(Enum2.Object);
    this.method_28((object) nameObjectCollectionBase_0);
  }

  private Class120 method_30(object object_0)
  {
    string key;
    if (object_0 is AmfObject)
    {
      AmfObject amfObject = object_0 as AmfObject;
      key = amfObject.TypeName;
      if (!amfObject.IsTypedObject)
        return (Class120) null;
    }
    else
      key = object_0.GetType().FullName;
    if (!this.dictionary_1.ContainsKey(key))
      return (Class120) null;
    return this.dictionary_1[key];
  }

  private Class120 method_31(object object_0)
  {
    Type type = object_0.GetType();
    bool bool_2 = type.GetInterface(typeof (IExternalizable).FullName) != (Type) null;
    bool bool_3 = false;
    Class120 key;
    if (object_0 is IDictionary)
    {
      string[] string_3 = new string[0];
      AmfObject amfObject1 = object_0 as AmfObject;
      string string_2;
      if (amfObject1 != null && amfObject1.IsTypedObject)
      {
        AmfObject amfObject2 = object_0 as AmfObject;
        string_3 = new string[amfObject2.Count];
        int index = 0;
        foreach (KeyValuePair<string, object> keyValuePair in (Hashtable) amfObject2)
        {
          string_3[index] = keyValuePair.Key;
          ++index;
        }
        string_2 = amfObject2.TypeName;
      }
      else
      {
        bool_3 = true;
        string_2 = string.Empty;
      }
      key = new Class120(string_2, string_3, bool_2, bool_3);
      this.dictionary_1.Add(object_0.GetType().FullName, key);
      this.dictionary_2.Add(key, this.dictionary_2.Count);
    }
    else if (object_0 is IExternalizable)
    {
      key = new Class120(Configuration.Data.ToRemoteType("amf", object_0.GetType().FullName), new string[0], true, false);
      this.dictionary_1.Add(type.FullName, key);
      this.dictionary_2.Add(key, this.dictionary_2.Count);
    }
    else
    {
      ArrayList arrayList1 = new ArrayList((ICollection) object_0.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public));
      for (int index = arrayList1.Count - 1; index >= 0; --index)
      {
        PropertyInfo pType = (PropertyInfo) arrayList1[index];
        if (pType.HasAttribute<NonSerializedAttribute>(true))
          arrayList1.RemoveAt(index);
        if (!(pType.GetGetMethod() != (MethodInfo) null) || pType.GetGetMethod().GetParameters().Length > 0)
          arrayList1.RemoveAt(index);
      }
      ArrayList arrayList2 = new ArrayList((ICollection) object_0.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public));
      for (int index = arrayList2.Count - 1; index >= 0; --index)
      {
        if (((MemberInfo) arrayList2[index]).HasAttribute<NonSerializedAttribute>(true))
          arrayList2.RemoveAt(index);
      }
      string[] string_3 = new string[arrayList1.Count + arrayList2.Count];
      int index1 = 0;
      foreach (PropertyInfo propertyInfo in arrayList1)
      {
        string_3[index1] = propertyInfo.Name;
        ++index1;
      }
      foreach (FieldInfo fieldInfo in arrayList2)
      {
        string_3[index1] = fieldInfo.Name;
        ++index1;
      }
      key = new Class120(Configuration.Data.ToRemoteType("amf", type.FullName), string_3, bool_2, false);
      this.dictionary_1.Add(type.FullName, key);
      this.dictionary_2.Add(key, this.dictionary_2.Count);
    }
    return key;
  }

  private static void smethod_1(object object_0, Class119 class119_0)
  {
    class119_0.method_25(Convert.ToInt32(object_0));
  }

  private static void smethod_2(object object_0, Class119 class119_0)
  {
    class119_0.method_26(Convert.ToDouble(object_0));
  }

  private static void smethod_3(object object_0, Class119 class119_0)
  {
    class119_0.method_15();
  }

  private static void smethod_4(object object_0, Class119 class119_0)
  {
    class119_0.method_22(((Guid) object_0).ToString());
  }

  private static void smethod_5(object object_0, Class119 class119_0)
  {
    class119_0.method_22(object_0 as string);
  }

  private static void smethod_6(object object_0, Class119 class119_0)
  {
    class119_0.method_16((bool) object_0);
  }

  private static void smethod_7(object object_0, Class119 class119_0)
  {
    class119_0.method_25(Convert.ToInt32(object_0));
  }

  private static void smethod_8(object object_0, Class119 class119_0)
  {
    class119_0.method_22(new string((char) object_0, 1));
  }

  private static void smethod_9(object object_0, Class119 class119_0)
  {
    class119_0.method_23((DateTime) object_0);
  }

  private static void smethod_10(object object_0, Class119 class119_0)
  {
    class119_0.method_17(object_0 as Array);
  }

  private static void smethod_11(object object_0, Class119 class119_0)
  {
    class119_0.method_27(object_0 as XmlDocument);
  }

  private static void smethod_12(object object_0, Class119 class119_0)
  {
    class119_0.method_1(Enum2.Object);
    class119_0.method_28(object_0);
  }

  private static void smethod_13(object object_0, Class119 class119_0)
  {
    class119_0.method_20((IList<byte>) (object_0 as byte[]));
  }

  private static void smethod_14(object object_0, Class119 class119_0)
  {
    class119_0.method_29(object_0 as NameObjectCollectionBase);
  }

  private static void smethod_15(object object_0, Class119 class119_0)
  {
    class119_0.method_7(object_0);
  }
}
