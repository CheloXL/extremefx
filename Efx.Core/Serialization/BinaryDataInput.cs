// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.BinaryDataInput
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Efx.Core.Serialization
{
  internal sealed class BinaryDataInput : IDataInput
  {
    private readonly List<ClassDefinition> _classDefinitions = new List<ClassDefinition>();
    private readonly List<object> _objectReferences = new List<object>();
    private readonly BinaryReader _reader;

    public BinaryDataInput(BinaryReader reader)
    {
      this._reader = reader;
    }

    public bool ReadBoolean()
    {
      return this._reader.ReadBoolean();
    }

    public byte ReadByte()
    {
      return this._reader.ReadByte();
    }

    public sbyte ReadSByte()
    {
      return this._reader.ReadSByte();
    }

    public byte[] ReadBytes()
    {
      return this._reader.ReadBytes(this._reader.ReadInt32());
    }

    public double ReadDouble()
    {
      return this._reader.ReadDouble();
    }

    public float ReadFloat()
    {
      return this._reader.ReadSingle();
    }

    public Decimal ReadDecimal()
    {
      return this._reader.ReadDecimal();
    }

    public int ReadInt()
    {
      return this._reader.ReadInt32();
    }

    public uint ReadUInt()
    {
      return this._reader.ReadUInt32();
    }

    public short ReadShort()
    {
      return this._reader.ReadInt16();
    }

    public ushort ReadUShort()
    {
      return this._reader.ReadUInt16();
    }

    public long ReadLong()
    {
      return this._reader.ReadInt64();
    }

    public ulong ReadULong()
    {
      return this._reader.ReadUInt64();
    }

    public char ReadChar()
    {
      return this._reader.ReadChar();
    }

    public string ReadString()
    {
      return this._reader.ReadString();
    }

    public object ReadObject()
    {
      TypeCode typeCode = (TypeCode) this._reader.ReadByte();
      switch (typeCode)
      {
        case TypeCode.Null:
          return (object) null;
        case TypeCode.BooleanFalse:
          return (object) false;
        case TypeCode.BooleanTrue:
          return (object) true;
        case TypeCode.Byte:
          return (object) this._reader.ReadByte();
        case TypeCode.SByte:
          return (object) this._reader.ReadSByte();
        case TypeCode.Char:
          return (object) this._reader.ReadChar();
        case TypeCode.Int:
          return (object) this._reader.ReadInt32();
        case TypeCode.UInt:
          return (object) this._reader.ReadUInt32();
        case TypeCode.Long:
          return (object) this._reader.ReadInt64();
        case TypeCode.ULong:
          return (object) this._reader.ReadUInt64();
        case TypeCode.UShort:
          return (object) this._reader.ReadUInt16();
        case TypeCode.Short:
          return (object) this._reader.ReadInt16();
        case TypeCode.Decimal:
          return (object) this._reader.ReadDecimal();
        case TypeCode.Double:
          return (object) this._reader.ReadDouble();
        case TypeCode.Float:
          return (object) this._reader.ReadSingle();
        case TypeCode.String:
          return (object) this._reader.ReadString();
        case TypeCode.Guid:
          return (object) new Guid(this._reader.ReadBytes(16));
        case TypeCode.DateTime:
          return (object) new DateTime(this._reader.ReadInt32(), this._reader.ReadInt32(), this._reader.ReadInt32(), this._reader.ReadInt32(), this._reader.ReadInt32(), this._reader.ReadInt32(), (DateTimeKind) this._reader.ReadInt32());
        case TypeCode.TimeSpan:
          return (object) new TimeSpan(this._reader.ReadInt64());
        case TypeCode.Enumerable:
          return this.ReadEnumerable();
        case TypeCode.Array:
          return this.ReadArray();
        case TypeCode.ByteArray:
          return this.ReadByteArray();
        case TypeCode.List:
          return this.ReadList();
        case TypeCode.Hashset:
          return this.ReadHashset();
        case TypeCode.Object:
          return this.ReadClass();
        case TypeCode.Reference:
          return this._objectReferences[this._reader.ReadInt32()];
        case TypeCode.Serializable:
          return this.ReadSerializable();
        default:
          throw new Exception("Unknown type code tag: " + (object) typeCode);
      }
    }

    private object ReadByteArray()
    {
      byte[] numArray = this._reader.ReadBytes(this._reader.ReadInt32());
      this._objectReferences.Add((object) numArray);
      return (object) numArray;
    }

    private object ReadArray()
    {
      Type typeByName = TypeHelper.GetTypeByName(this._reader.ReadString());
      int length = this._reader.ReadInt32();
      Array instance = Array.CreateInstance(typeByName, length);
      this._objectReferences.Add((object) instance);
      for (int index = 0; index < length; ++index)
        instance.SetValue(this.ReadObject(), index);
      return (object) instance;
    }

    private object ReadClass()
    {
      int num = this._reader.ReadInt32();
      bool flag = (num & 1) == 0;
      int length = num >> 1;
      ClassDefinition classDefinition;
      if (flag)
      {
        classDefinition = this._classDefinitions[length];
      }
      else
      {
        Type typeByName = TypeHelper.GetTypeByName(this._reader.ReadString());
        string[] loadedMembers = new string[length];
        for (int index = 0; index < length; ++index)
          loadedMembers[index] = this._reader.ReadString();
        classDefinition = new ClassDefinition(typeByName, loadedMembers);
        this._classDefinitions.Add(classDefinition);
      }
      object instance = classDefinition.ClassType.CreateInstance();
      this._objectReferences.Add(instance);
      if (classDefinition.Externalizable)
      {
        ((IExternalizable) instance).ReadExternal((IDataInput) this);
      }
      else
      {
        foreach (string loadedMember in classDefinition.LoadedMembers)
        {
          object obj = this.ReadObject();
          MemberInfo memberInfo;
          if (classDefinition.SerializableMembersInfo.TryGetValue(loadedMember, out memberInfo))
            memberInfo.SetValue(instance, obj);
        }
      }
      return instance;
    }

    private object ReadEnumerable()
    {
      Type typeByName = TypeHelper.GetTypeByName(this._reader.ReadString());
      object instance = typeByName.CreateInstance();
      this._objectReferences.Add(instance);
      Action<object, object[]> addMethodDelegate = ListHelper.GetAddMethodDelegate(typeByName);
      while (this._reader.ReadByte() == (byte) 20)
        addMethodDelegate(instance, new object[1]
        {
          this.ReadObject()
        });
      return instance;
    }

    private object ReadHashset()
    {
      IDictionary instance = TypeHelper.GetTypeByName(this._reader.ReadString()).CreateInstance<IDictionary>();
      this._objectReferences.Add((object) instance);
      int num = this._reader.ReadInt32();
      for (int index = 0; index < num; ++index)
        instance.Add(this.ReadObject(), this.ReadObject());
      return (object) instance;
    }

    private object ReadList()
    {
      IList instance = TypeHelper.GetTypeByName(this._reader.ReadString()).CreateInstance<IList>();
      this._objectReferences.Add((object) instance);
      int num = this._reader.ReadInt32();
      for (int index = 0; index < num; ++index)
        instance.Add(this.ReadObject());
      return (object) instance;
    }

    private object ReadSerializable()
    {
      object obj = new BinaryFormatter().Deserialize(this._reader.BaseStream);
      this._objectReferences.Add(obj);
      return obj;
    }
  }
}
