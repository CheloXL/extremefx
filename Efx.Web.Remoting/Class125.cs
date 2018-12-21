// Decompiled with JetBrains decompiler
// Type: Class125
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Reflection;
using Efx.Web.Remoting;
using Efx.Web.Remoting.Serialization.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

internal sealed class Class125
{
  private readonly StringBuilder stringBuilder_0 = new StringBuilder();
  private readonly bool bool_0 = true;
  private int int_0;

  internal Class125(bool bool_1)
  {
    this.bool_0 = bool_1;
  }

  internal string method_0(object object_0)
  {
    this.method_1(object_0);
    return this.stringBuilder_0.ToString();
  }

  private void method_1(object object_0)
  {
    if (object_0 != null && !(object_0 is DBNull))
    {
      if (!(object_0 is string) && !(object_0 is char))
      {
        if (object_0 is Guid)
          this.method_12(object_0.ToString());
        else if (object_0 is bool)
          this.stringBuilder_0.Append((bool) object_0 ? "true" : "false");
        else if (!(object_0 is int) && !(object_0 is long) && (!(object_0 is double) && !(object_0 is Decimal)) && (!(object_0 is float) && !(object_0 is byte) && (!(object_0 is short) && !(object_0 is sbyte))) && (!(object_0 is ushort) && !(object_0 is uint) && !(object_0 is ulong)))
        {
          if (object_0 is DateTime)
            this.method_5((DateTime) object_0);
          else if (object_0 is IDictionary<string, string>)
            this.method_10((IDictionary) object_0);
          else if (object_0 is IDictionary)
            this.method_11((IDictionary) object_0);
          else if (object_0 is byte[])
            this.method_4((byte[]) object_0);
          else if (!(object_0 is Array) && !(object_0 is IList) && !(object_0 is ICollection))
          {
            if (object_0 is Enum)
            {
              this.method_3((Enum) object_0);
            }
            else
            {
              Type type = object_0.GetType();
              if (JsonMapper.smethod_16(type))
                this.method_2(object_0, type);
              else
                this.method_6(object_0);
            }
          }
          else
            this.method_9((IEnumerable) object_0);
        }
        else
          this.stringBuilder_0.Append(((IConvertible) object_0).ToString((IFormatProvider) NumberFormatInfo.InvariantInfo));
      }
      else
        this.method_13((string) object_0);
    }
    else
      this.stringBuilder_0.Append("null");
  }

  private void method_2(object object_0, Type type_0)
  {
    Func<object, string> func;
    if (!JsonMapper.syncedDictionary_0.TryGetValue(type_0, out func))
      return;
    this.method_12(func(object_0));
  }

  private void method_3(Enum enum_0)
  {
    this.method_12(enum_0.ToString());
  }

  private void method_4(byte[] byte_0)
  {
    this.method_12(Convert.ToBase64String(byte_0, 0, byte_0.Length, Base64FormattingOptions.None));
  }

  private void method_5(DateTime dateTime_0)
  {
    this.stringBuilder_0.Append('"');
    this.stringBuilder_0.Append(dateTime_0.ToString("s"));
    this.stringBuilder_0.Append('"');
  }

  private void method_6(object object_0)
  {
    ++this.int_0;
    if (this.int_0 > 10)
      throw new Exception("Serializer encountered maximum depth of " + (object) 10);
    this.stringBuilder_0.Append('{');
    Type type = object_0.GetType();
    bool flag = false;
    if (this.bool_0)
    {
      this.method_7("$type", Configuration.Data.ToRemoteType("json", type.GetQualifiedName()));
      flag = true;
    }
    foreach (Class122 class122 in JsonMapper.smethod_20(type))
    {
      if (flag)
        this.stringBuilder_0.Append(',');
      object object_0_1 = class122.func_0(object_0);
      this.method_8(class122.string_0, object_0_1);
      flag = true;
    }
    this.stringBuilder_0.Append('}');
    --this.int_0;
  }

  private void method_7(string string_0, string string_1)
  {
    this.method_12(string_0);
    this.stringBuilder_0.Append(':');
    this.method_12(string_1);
  }

  private void method_8(string string_0, object object_0)
  {
    this.method_12(string_0);
    this.stringBuilder_0.Append(':');
    this.method_1(object_0);
  }

  private void method_9(IEnumerable ienumerable_0)
  {
    this.stringBuilder_0.Append('[');
    bool flag = false;
    foreach (object object_0 in ienumerable_0)
    {
      if (flag)
        this.stringBuilder_0.Append(',');
      this.method_1(object_0);
      flag = true;
    }
    this.stringBuilder_0.Append(']');
  }

  private void method_10(IDictionary idictionary_0)
  {
    this.stringBuilder_0.Append('{');
    bool flag = false;
    foreach (DictionaryEntry dictionaryEntry in idictionary_0)
    {
      if (flag)
        this.stringBuilder_0.Append(',');
      this.method_7((string) dictionaryEntry.Key, (string) dictionaryEntry.Value);
      flag = true;
    }
    this.stringBuilder_0.Append('}');
  }

  private void method_11(IDictionary idictionary_0)
  {
    this.stringBuilder_0.Append('[');
    bool flag = false;
    foreach (DictionaryEntry dictionaryEntry in idictionary_0)
    {
      if (flag)
        this.stringBuilder_0.Append(',');
      this.stringBuilder_0.Append('{');
      this.method_8("k", dictionaryEntry.Key);
      this.stringBuilder_0.Append(',');
      this.method_8("v", dictionaryEntry.Value);
      this.stringBuilder_0.Append('}');
      flag = true;
    }
    this.stringBuilder_0.Append(']');
  }

  private void method_12(string string_0)
  {
    this.stringBuilder_0.Append('"');
    this.stringBuilder_0.Append(string_0);
    this.stringBuilder_0.Append('"');
  }

  private void method_13(string string_0)
  {
    this.stringBuilder_0.Append('"');
    int startIndex = -1;
    for (int index = 0; index < string_0.Length; ++index)
    {
      char ch = string_0[index];
      if (ch >= ' ' && ch < '\x0080' && (ch != '"' && ch != '\\'))
      {
        if (startIndex == -1)
          startIndex = index;
      }
      else
      {
        if (startIndex != -1)
        {
          this.stringBuilder_0.Append(string_0, startIndex, index - startIndex);
          startIndex = -1;
        }
        switch (ch)
        {
          case '\t':
            this.stringBuilder_0.Append("\\t");
            continue;
          case '\n':
            this.stringBuilder_0.Append("\\n");
            continue;
          case '\r':
            this.stringBuilder_0.Append("\\r");
            continue;
          case '"':
          case '\\':
            this.stringBuilder_0.Append('\\');
            this.stringBuilder_0.Append(ch);
            continue;
          default:
            this.stringBuilder_0.Append("\\u");
            this.stringBuilder_0.Append(((int) ch).ToString("X4", (IFormatProvider) NumberFormatInfo.InvariantInfo));
            continue;
        }
      }
    }
    if (startIndex != -1)
      this.stringBuilder_0.Append(string_0, startIndex, string_0.Length - startIndex);
    this.stringBuilder_0.Append('"');
  }
}
