// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.BinaryDataOutput
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using Efx.Core.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace Efx.Core.Serialization
{
  internal sealed class BinaryDataOutput : IDataOutput
  {
    private readonly Dictionary<Type, ClassDefinition> _classDefinition = new Dictionary<Type, ClassDefinition>();
    private readonly Dictionary<ClassDefinition, int> _classDefinitionReferences = new Dictionary<ClassDefinition, int>();
    private readonly Dictionary<object, int> _objectReferences = new Dictionary<object, int>();
    private static readonly MemoryLockedCache<Type, ClassDefinition> _classCache = new MemoryLockedCache<Type, ClassDefinition>();
    private static readonly Dictionary<Type, Action<BinaryDataOutput, object, Type>> _complexWriters = new Dictionary<Type, Action<BinaryDataOutput, object, Type>>();
    private static readonly Dictionary<Type, Action<BinaryWriter, object>> _primitiveWriters = new Dictionary<Type, Action<BinaryWriter, object>>();
    private static readonly Type _typeOfObject = typeof (object);
    private readonly BinaryWriter _writer;

    static BinaryDataOutput()
    {
      BinaryDataOutput._primitiveWriters.Add(typeof (bool), (Action<BinaryWriter, object>) ((writer, value) => writer.Write((bool) value ? (byte) 2 : (byte) 1)));
      BinaryDataOutput._primitiveWriters.Add(typeof (int), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 6);
        writer.Write((int) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (uint), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 7);
        writer.Write((uint) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (short), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 11);
        writer.Write((short) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (ushort), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 10);
        writer.Write((ushort) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (long), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 8);
        writer.Write((long) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (ulong), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 9);
        writer.Write((ulong) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (byte), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 3);
        writer.Write((byte) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (sbyte), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 4);
        writer.Write((sbyte) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (char), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 5);
        writer.Write((char) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (Decimal), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 12);
        writer.Write((Decimal) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (double), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 13);
        writer.Write((double) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (float), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 14);
        writer.Write((float) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (Enum), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 6);
        writer.Write((int) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (Guid), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 16);
        writer.Write(((Guid) value).ToByteArray());
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (string), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 15);
        writer.Write((string) value);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (DateTime), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        DateTime dateTime = (DateTime) value;
        writer.Write((byte) 17);
        writer.Write(dateTime.Year);
        writer.Write(dateTime.Month);
        writer.Write(dateTime.Day);
        writer.Write(dateTime.Hour);
        writer.Write(dateTime.Minute);
        writer.Write(dateTime.Second);
        writer.Write((int) dateTime.Kind);
      }));
      BinaryDataOutput._primitiveWriters.Add(typeof (TimeSpan), (Action<BinaryWriter, object>) ((writer, value) =>
      {
        writer.Write((byte) 18);
        writer.Write(((TimeSpan) value).Ticks);
      }));
    }

    internal BinaryDataOutput(BinaryWriter writer)
    {
      this._writer = writer;
    }

    public void Write(bool value)
    {
      this._writer.Write(value ? (byte) 2 : (byte) 1);
    }

    public void Write(byte value)
    {
      this._writer.Write(value);
    }

    public void Write(sbyte value)
    {
      this._writer.Write(value);
    }

    public void Write(byte[] value)
    {
      this._writer.Write(value.Length);
      this._writer.Write(value);
    }

    public void Write(double value)
    {
      this._writer.Write(value);
    }

    public void Write(float value)
    {
      this._writer.Write(value);
    }

    public void Write(Decimal value)
    {
      this._writer.Write(value);
    }

    public void Write(int value)
    {
      this._writer.Write(value);
    }

    public void Write(short value)
    {
      this._writer.Write(value);
    }

    public void Write(ushort value)
    {
      this._writer.Write(value);
    }

    public void Write(long value)
    {
      this._writer.Write(value);
    }

    public void Write(ulong value)
    {
      this._writer.Write(value);
    }

    public void Write(char value)
    {
      this._writer.Write(value);
    }

    public void Write(uint value)
    {
      this._writer.Write(value);
    }

    public void Write(string value)
    {
      this._writer.Write(value);
    }

    public void WriteObject(object value)
    {
      BinaryDataOutput.WriteObject(this, value);
    }

    private static bool TryGetWriter(Type valueType, out Action<BinaryWriter, object> writer)
    {
      writer = (Action<BinaryWriter, object>) null;
      if (valueType == (Type) null || BinaryDataOutput._primitiveWriters.TryGetValue(valueType, out writer))
        return true;
      if (valueType == BinaryDataOutput._typeOfObject)
        return false;
      Type baseType = valueType.BaseType;
      if (baseType != (Type) null)
        return BinaryDataOutput._primitiveWriters.TryGetValue(baseType, out writer);
      return !valueType.IsInterface;
    }

    private static void WriteArray(
      BinaryDataOutput binaryDataOutput,
      object o,
      Type typeIdentifier)
    {
      Type elementType = typeIdentifier.GetElementType();
      Array array = (Array) o;
      int length = array.Length;
      if (elementType == typeof (byte))
      {
        binaryDataOutput._writer.Write((byte) 22);
        binaryDataOutput._writer.Write(array.Length);
        binaryDataOutput._writer.Write((byte[]) o);
      }
      else
      {
        binaryDataOutput._writer.Write((byte) 21);
        binaryDataOutput._writer.Write(elementType.GetQualifiedName());
        binaryDataOutput._writer.Write(length);
        Action<BinaryWriter, object> writer;
        if (BinaryDataOutput.TryGetWriter(elementType, out writer))
        {
          for (int index = 0; index < length; ++index)
            writer(binaryDataOutput._writer, array.GetValue(index));
        }
        else
        {
          for (int index = 0; index < length; ++index)
            BinaryDataOutput.WriteObject(binaryDataOutput, array.GetValue(index));
        }
      }
    }

    private static void WriteClass(
      BinaryDataOutput binaryDataOutput,
      object instance,
      Type typeIdentifier)
    {
      ClassDefinition orAdd;
      if (binaryDataOutput._classDefinition.TryGetValue(typeIdentifier, out orAdd))
      {
        binaryDataOutput._writer.Write((byte) 25);
        binaryDataOutput._writer.Write(binaryDataOutput._classDefinitionReferences[orAdd] << 1);
      }
      else
      {
        orAdd = BinaryDataOutput._classCache.GetOrAdd(typeIdentifier, (Func<Type, ClassDefinition>) (x => new ClassDefinition(x, (string[]) null)));
        if (orAdd.SerializableMembersInfo.Count == 0 && !orAdd.Externalizable)
        {
          ISerializable serializable = instance as ISerializable;
          if (!typeIdentifier.IsSerializable && serializable == null)
            throw new SerializationException(string.Format("Can not serialize the type {0} ", (object) typeIdentifier.GetQualifiedName()));
          BinaryDataOutput.WriteSerializable(binaryDataOutput, instance);
          return;
        }
        binaryDataOutput._classDefinition.Add(typeIdentifier, orAdd);
        binaryDataOutput._classDefinitionReferences.Add(orAdd, binaryDataOutput._classDefinitionReferences.Count);
        binaryDataOutput._writer.Write((byte) 25);
        binaryDataOutput._writer.Write(orAdd.SerializableMembersInfo.Count << 1 | 1);
        binaryDataOutput._writer.Write(orAdd.ClassName);
        if (!orAdd.Externalizable)
        {
          foreach (KeyValuePair<string, MemberInfo> keyValuePair in (IEnumerable<KeyValuePair<string, MemberInfo>>) orAdd.SerializableMembersInfo)
            binaryDataOutput._writer.Write(keyValuePair.Key);
        }
      }
      if (orAdd.Externalizable)
      {
        ((IExternalizable) instance).WriteExternal((IDataOutput) binaryDataOutput);
      }
      else
      {
        foreach (KeyValuePair<string, MemberInfo> keyValuePair in (IEnumerable<KeyValuePair<string, MemberInfo>>) orAdd.SerializableMembersInfo)
          BinaryDataOutput.WriteObject(binaryDataOutput, keyValuePair.Value.ReadValue(instance));
      }
    }

    private static void WriteDictionary(
      BinaryDataOutput binaryDataOutput,
      object o,
      Type typeIdentifier)
    {
      IDictionary dictionary = (IDictionary) o;
      binaryDataOutput._writer.Write((byte) 24);
      binaryDataOutput._writer.Write(typeIdentifier.GetQualifiedName());
      binaryDataOutput._writer.Write(dictionary.Count);
      foreach (DictionaryEntry dictionaryEntry in dictionary)
      {
        BinaryDataOutput.WriteObject(binaryDataOutput, dictionaryEntry.Key);
        BinaryDataOutput.WriteObject(binaryDataOutput, dictionaryEntry.Value);
      }
    }

    private static void WriteIEnumerable(
      BinaryDataOutput binaryDataOutput,
      object o,
      Type typeIdentifier)
    {
      IEnumerable enumerable = (IEnumerable) o;
      binaryDataOutput._writer.Write((byte) 19);
      binaryDataOutput._writer.Write(typeIdentifier.GetQualifiedName());
      foreach (object obj in enumerable)
      {
        binaryDataOutput._writer.Write((byte) 20);
        BinaryDataOutput.WriteObject(binaryDataOutput, obj);
      }
      binaryDataOutput._writer.Write((byte) 0);
    }

    private static void WriteList(BinaryDataOutput binaryDataOutput, object o, Type typeIdentifier)
    {
      IList list = (IList) o;
      binaryDataOutput._writer.Write((byte) 23);
      binaryDataOutput._writer.Write(typeIdentifier.GetQualifiedName());
      binaryDataOutput._writer.Write(list.Count);
      foreach (object obj in (IEnumerable) list)
        BinaryDataOutput.WriteObject(binaryDataOutput, obj);
    }

    private static void WriteObject(BinaryDataOutput output, object value)
    {
      if (value != null && !(value is DBNull))
      {
        Type type = value.GetType();
        Action<BinaryWriter, object> writer;
        if (BinaryDataOutput.TryGetWriter(type, out writer))
        {
          if (writer != null)
            writer(output._writer, value);
          else
            output._writer.Write((byte) 0);
        }
        else
        {
          int num;
          if (output._objectReferences.TryGetValue(value, out num))
          {
            output._writer.Write((byte) 26);
            output._writer.Write(num);
          }
          else
          {
            if (!type.IsValueType)
              output._objectReferences.Add(value, output._objectReferences.Count);
            Action<BinaryDataOutput, object, Type> action;
            if (BinaryDataOutput._complexWriters.TryGetValue(type, out action))
            {
              action(output, value, type);
            }
            else
            {
              Array array = value as Array;
              if (array != null)
              {
                BinaryDataOutput._complexWriters.Add(type, new Action<BinaryDataOutput, object, Type>(BinaryDataOutput.WriteArray));
                BinaryDataOutput.WriteArray(output, (object) array, type);
              }
              else
              {
                IDictionary dictionary = value as IDictionary;
                if (dictionary != null)
                {
                  BinaryDataOutput._complexWriters.Add(type, new Action<BinaryDataOutput, object, Type>(BinaryDataOutput.WriteDictionary));
                  BinaryDataOutput.WriteDictionary(output, (object) dictionary, type);
                }
                else
                {
                  IList list = value as IList;
                  if (list != null)
                  {
                    BinaryDataOutput._complexWriters.Add(type, new Action<BinaryDataOutput, object, Type>(BinaryDataOutput.WriteList));
                    BinaryDataOutput.WriteList(output, (object) list, type);
                  }
                  else
                  {
                    IEnumerable enumerable = value as IEnumerable;
                    if (enumerable != null && ListHelper.GetListAddMethod(type) != (MethodInfo) null)
                    {
                      BinaryDataOutput._complexWriters.Add(type, new Action<BinaryDataOutput, object, Type>(BinaryDataOutput.WriteIEnumerable));
                      BinaryDataOutput.WriteIEnumerable(output, (object) enumerable, type);
                    }
                    else
                    {
                      BinaryDataOutput._complexWriters.Add(type, new Action<BinaryDataOutput, object, Type>(BinaryDataOutput.WriteClass));
                      BinaryDataOutput.WriteClass(output, value, type);
                    }
                  }
                }
              }
            }
          }
        }
      }
      else
        output._writer.Write((byte) 0);
    }

    private static void WriteSerializable(BinaryDataOutput binaryDataOutput, object value)
    {
      binaryDataOutput._writer.Write((byte) 27);
      new BinaryFormatter()
      {
        AssemblyFormat = FormatterAssemblyStyle.Simple
      }.Serialize(binaryDataOutput._writer.BaseStream, value);
    }
  }
}
