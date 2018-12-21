// Decompiled with JetBrains decompiler
// Type: Class118
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Conversion;
using Efx.Core.Reflection;
using Efx.Web.Remoting;
using Efx.Web.Remoting.Serialization.Amf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

internal sealed class Class118 : BinaryReader
{
  private static readonly DateTime dateTime_0 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
  private readonly List<Class120> list_0 = new List<Class120>();
  private readonly ArrayList arrayList_0 = new ArrayList();
  private readonly List<string> list_1 = new List<string>();

  public Class118(Stream stream_0)
    : base(stream_0)
  {
  }

  public override ushort ReadUInt16()
  {
    byte[] numArray = this.ReadBytes(2);
    return (ushort) (((int) numArray[0] & (int) byte.MaxValue) << 8 | (int) numArray[1] & (int) byte.MaxValue);
  }

  public override short ReadInt16()
  {
    byte[] numArray = this.ReadBytes(2);
    return (short) ((int) numArray[0] << 8 | (int) numArray[1]);
  }

  public override string ReadString()
  {
    return this.method_1((int) this.ReadUInt16());
  }

  public override int ReadInt32()
  {
    byte[] numArray = this.ReadBytes(4);
    return (int) numArray[0] << 24 | (int) numArray[1] << 16 | (int) numArray[2] << 8 | (int) numArray[3];
  }

  public override double ReadDouble()
  {
    byte[] numArray1 = this.ReadBytes(8);
    byte[] numArray2 = new byte[8];
    int index1 = 7;
    int index2 = 0;
    while (index1 >= 0)
    {
      numArray2[index2] = numArray1[index1];
      --index1;
      ++index2;
    }
    return BitConverter.ToDouble(numArray2, 0);
  }

  public float method_0()
  {
    byte[] numArray1 = this.ReadBytes(4);
    byte[] numArray2 = new byte[4];
    int index1 = 3;
    int index2 = 0;
    while (index1 >= 0)
    {
      numArray2[index2] = numArray1[index1];
      --index1;
      ++index2;
    }
    return BitConverter.ToSingle(numArray2, 0);
  }

  public string method_1(int int_0)
  {
    if (int_0 == 0)
      return string.Empty;
    return new UTF8Encoding(false, true).GetString(this.ReadBytes(int_0));
  }

  public object method_2()
  {
    return this.method_3((Enum2) this.ReadByte());
  }

  private object method_3(Enum2 enum2_0)
  {
    switch (enum2_0)
    {
      case Enum2.Undefined:
      case Enum2.Null:
        return (object) null;
      case Enum2.Booleanfalse:
        return (object) false;
      case Enum2.Booleantrue:
        return (object) true;
      case Enum2.Integer:
        return (object) this.method_4();
      case Enum2.Number:
        return (object) this.ReadDouble();
      case Enum2.String:
        return (object) this.method_6();
      case Enum2.OldXml:
      case Enum2.Xml:
        return (object) this.method_7();
      case Enum2.Datetime:
        return (object) this.method_5();
      case Enum2.Array:
        return this.method_9();
      case Enum2.Object:
        return this.method_12();
      case Enum2.Bytearray:
        return (object) this.method_8();
      default:
        throw new Exception("unknown amf3 tag " + (object) enum2_0);
    }
  }

  private int method_4()
  {
    int num1 = (int) this.ReadByte();
    if (num1 < 128)
      return num1;
    int num2 = (num1 & (int) sbyte.MaxValue) << 7;
    int num3 = (int) this.ReadByte();
    int num4;
    if (num3 < 128)
    {
      num4 = num2 | num3;
    }
    else
    {
      int num5 = (num2 | num3 & (int) sbyte.MaxValue) << 7;
      int num6 = (int) this.ReadByte();
      num4 = num6 >= 128 ? (num5 | num6 & (int) sbyte.MaxValue) << 8 | (int) this.ReadByte() : num5 | num6;
    }
    return -(num4 & 268435456) | num4;
  }

  private DateTime method_5()
  {
    int num1 = this.method_4();
    bool flag = (num1 & 1) != 0;
    int index = num1 >> 1;
    if (!flag)
      return (DateTime) this.arrayList_0[index];
    double num2 = this.ReadDouble();
    DateTime dateTime = Class118.dateTime_0.AddMilliseconds(num2);
    this.arrayList_0.Add((object) dateTime);
    return dateTime;
  }

  private string method_6()
  {
    int num = this.method_4();
    bool flag = (num & 1) != 0;
    int index = num >> 1;
    if (!flag)
      return this.list_1[index];
    int int_0 = index;
    if (int_0 == 0)
      return string.Empty;
    string str = this.method_1(int_0);
    this.list_1.Add(str);
    return str;
  }

  private XmlDocument method_7()
  {
    int num = this.method_4();
    bool flag = (num & 1) != 0;
    int int_0 = num >> 1;
    string xml = string.Empty;
    if (flag)
    {
      if (int_0 > 0)
        xml = this.method_1(int_0);
      this.arrayList_0.Add((object) xml);
    }
    else
      xml = this.arrayList_0[int_0] as string;
    XmlDocument xmlDocument = new XmlDocument();
    if (!string.IsNullOrEmpty(xml))
      xmlDocument.LoadXml(xml);
    return xmlDocument;
  }

  private byte[] method_8()
  {
    int num = this.method_4();
    bool flag = (num & 1) != 0;
    int count = num >> 1;
    if (!flag)
      return this.arrayList_0[count] as byte[];
    byte[] numArray = this.ReadBytes(count);
    this.arrayList_0.Add((object) numArray);
    return numArray;
  }

  private object method_9()
  {
    int num1 = this.method_4();
    bool flag = (num1 & 1) != 0;
    int length = num1 >> 1;
    if (!flag)
      return this.arrayList_0[length];
    Hashtable hashtable = (Hashtable) null;
    for (string str = this.method_6(); str != string.Empty; str = this.method_6())
    {
      if (hashtable == null)
      {
        hashtable = new Hashtable();
        this.arrayList_0.Add((object) hashtable);
      }
      object obj = this.method_2();
      hashtable.Add((object) str, obj);
    }
    if (hashtable == null)
    {
      object[] objArray = new object[length];
      this.arrayList_0.Add((object) objArray);
      for (int index = 0; index < length; ++index)
      {
        byte num2 = this.ReadByte();
        objArray[index] = this.method_3((Enum2) num2);
      }
      return (object) objArray;
    }
    for (int index = 0; index < length; ++index)
    {
      object obj = this.method_2();
      hashtable.Add((object) index.ToString(), obj);
    }
    return (object) hashtable;
  }

  private Class120 method_10(int int_0)
  {
    bool flag = (int_0 & 1) != 0;
    int_0 >>= 1;
    if (!flag)
      return this.list_0[int_0];
    string dotnetType = Configuration.Data.ToDotnetType("amf", this.method_6());
    bool bool_2 = (int_0 & 1) != 0;
    int_0 >>= 1;
    bool bool_3 = (int_0 & 1) != 0;
    int_0 >>= 1;
    string[] string_3 = new string[int_0];
    for (int index = 0; index < int_0; ++index)
      string_3[index] = this.method_6();
    Class120 class120 = new Class120(dotnetType, string_3, bool_2, bool_3);
    this.list_0.Add(class120);
    return class120;
  }

  private object method_11(Class120 class120_0)
  {
    object instance;
    if (class120_0.method_4())
      instance = (object) new AmfObject();
    else if (!Efx.Core.TypeHelper.GetTypeByName(class120_0.method_0()).TryCreateInstance(out instance))
      instance = (object) new AmfObject(class120_0.method_0());
    this.arrayList_0.Add(instance);
    if (class120_0.method_3())
    {
      IExternalizable externalizable = instance as IExternalizable;
      if (externalizable == null)
        throw new Exception("IExternalizable failed to unserialize");
      Class121 class121 = new Class121(this);
      externalizable.ReadExternal((IDataInput) class121);
    }
    else
    {
      AmfObject amfObject = instance as AmfObject;
      for (int index = 0; index < class120_0.method_1(); ++index)
      {
        string memberName = class120_0.method_2()[index];
        object obj = this.method_2();
        Type type = instance.GetType();
        Type memberType = type.GetMemberInfo(memberName, false).GetMemberType();
        string stringValue = obj as string;
        if (stringValue != null && memberType != typeof (string))
          obj = StringConverter.Convert(stringValue, memberType);
        if (amfObject == null)
          type.GetMemberInfo(memberName, false).SetValue(instance, obj.Convert(memberType));
        else
          amfObject[(object) memberName] = obj;
      }
      if (class120_0.method_4() && amfObject != null)
      {
        for (string str = this.method_6(); str != string.Empty; str = this.method_6())
        {
          object obj = this.method_2();
          amfObject.Add((object) str, obj);
        }
      }
    }
    return instance;
  }

  private object method_12()
  {
    int num = this.method_4();
    bool flag = (num & 1) != 0;
    int int_0 = num >> 1;
    if (flag)
      return this.method_11(this.method_10(int_0));
    return this.arrayList_0[int_0];
  }
}
