// Decompiled with JetBrains decompiler
// Type: Class124
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

internal sealed class Class124
{
  private readonly StringBuilder stringBuilder_0 = new StringBuilder();
  private Class124.Enum3 enum3_0 = (Class124.Enum3) -1;
  private readonly char[] char_0;
  private int int_0;

  internal Class124(string string_0)
  {
    this.char_0 = string_0.ToCharArray();
  }

  public object method_0()
  {
    return this.method_3();
  }

  private Dictionary<string, object> method_1()
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    this.method_7();
    while (true)
    {
      switch (this.method_6())
      {
        case (Class124.Enum3) 1:
          goto label_6;
        case (Class124.Enum3) 5:
          this.method_7();
          continue;
        default:
          string index = this.method_4();
          if (this.method_8() == (Class124.Enum3) 4)
          {
            object obj = this.method_3();
            dictionary[index] = obj;
            continue;
          }
          goto label_5;
      }
    }
label_5:
    throw new Exception("Expected colon at index " + (object) this.int_0);
label_6:
    this.method_7();
    return dictionary;
  }

  private ArrayList method_2()
  {
    ArrayList arrayList = new ArrayList();
    this.method_7();
    while (true)
    {
      switch (this.method_6())
      {
        case (Class124.Enum3) 3:
          goto label_4;
        case (Class124.Enum3) 5:
          this.method_7();
          continue;
        default:
          arrayList.Add(this.method_3());
          continue;
      }
    }
label_4:
    this.method_7();
    return arrayList;
  }

  private object method_3()
  {
    switch (this.method_6())
    {
      case (Class124.Enum3) 0:
        return (object) this.method_1();
      case (Class124.Enum3) 2:
        return (object) this.method_2();
      case (Class124.Enum3) 6:
        return (object) this.method_4();
      case (Class124.Enum3) 7:
        return (object) this.method_5();
      case (Class124.Enum3) 8:
        this.method_7();
        return (object) true;
      case (Class124.Enum3) 9:
        this.method_7();
        return (object) false;
      case (Class124.Enum3) 10:
        this.method_7();
        return (object) null;
      default:
        throw new Exception("Unrecognized token at index" + (object) this.int_0);
    }
  }

  private string method_4()
  {
    this.method_7();
    this.stringBuilder_0.Length = 0;
    int startIndex = -1;
    while (this.int_0 < this.char_0.Length)
    {
      switch (this.char_0[this.int_0++])
      {
        case '"':
          if (startIndex != -1)
          {
            if (this.stringBuilder_0.Length == 0)
              return new string(this.char_0, startIndex, this.int_0 - startIndex - 1);
            this.stringBuilder_0.Append(this.char_0, startIndex, this.int_0 - startIndex - 1);
          }
          return this.stringBuilder_0.ToString();
        case '\\':
          if (this.int_0 != this.char_0.Length)
          {
            if (startIndex != -1)
            {
              this.stringBuilder_0.Append(this.char_0, startIndex, this.int_0 - startIndex - 1);
              startIndex = -1;
            }
            switch (this.char_0[this.int_0++])
            {
              case '"':
                this.stringBuilder_0.Append('"');
                continue;
              case '/':
                this.stringBuilder_0.Append('/');
                continue;
              case '\\':
                this.stringBuilder_0.Append('\\');
                continue;
              case 'b':
                this.stringBuilder_0.Append('\b');
                continue;
              case 'f':
                this.stringBuilder_0.Append('\f');
                continue;
              case 'n':
                this.stringBuilder_0.Append('\n');
                continue;
              case 'r':
                this.stringBuilder_0.Append('\r');
                continue;
              case 't':
                this.stringBuilder_0.Append('\t');
                continue;
              case 'u':
                if (this.char_0.Length - this.int_0 >= 4)
                {
                  this.stringBuilder_0.Append((char) Class124.smethod_1(this.char_0[this.int_0], this.char_0[this.int_0 + 1], this.char_0[this.int_0 + 2], this.char_0[this.int_0 + 3]));
                  this.int_0 += 4;
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
          else
            goto label_23;
        default:
          if (startIndex == -1)
          {
            startIndex = this.int_0 - 1;
            continue;
          }
          continue;
      }
    }
label_23:
    throw new Exception("Unexpectedly reached end of string");
  }

  private static uint smethod_0(char char_1, uint uint_0)
  {
    uint num = 0;
    if (char_1 >= '0' && char_1 <= '9')
      num = ((uint) char_1 - 48U) * uint_0;
    else if (char_1 >= 'A' && char_1 <= 'F')
      num = (uint) ((int) char_1 - 65 + 10) * uint_0;
    else if (char_1 >= 'a' && char_1 <= 'f')
      num = (uint) ((int) char_1 - 97 + 10) * uint_0;
    return num;
  }

  private static uint smethod_1(char char_1, char char_2, char char_3, char char_4)
  {
    return Class124.smethod_0(char_1, 4096U) + Class124.smethod_0(char_2, 256U) + Class124.smethod_0(char_3, 16U) + Class124.smethod_0(char_4, 1U);
  }

  private string method_5()
  {
    this.method_7();
    int startIndex = this.int_0 - 1;
    if (this.int_0 < this.char_0.Length)
    {
      do
      {
        char ch = this.char_0[this.int_0];
        if ((ch < '0' || ch > '9') && (ch != '.' && ch != '-' && (ch != '+' && ch != 'e')) && ch != 'E')
          goto label_4;
      }
      while (++this.int_0 != this.char_0.Length);
      throw new Exception("Unexpected end of string whilst parsing number");
    }
label_4:
    return new string(this.char_0, startIndex, this.int_0 - startIndex);
  }

  private Class124.Enum3 method_6()
  {
    if (this.enum3_0 != (Class124.Enum3) -1)
      return this.enum3_0;
    return this.enum3_0 = this.method_9();
  }

  private void method_7()
  {
    this.enum3_0 = (Class124.Enum3) -1;
  }

  private Class124.Enum3 method_8()
  {
    Class124.Enum3 enum3 = this.enum3_0 != (Class124.Enum3) -1 ? this.enum3_0 : this.method_9();
    this.enum3_0 = (Class124.Enum3) -1;
    return enum3;
  }

  private Class124.Enum3 method_9()
  {
    do
    {
      switch (this.char_0[this.int_0])
      {
        case '\t':
        case '\n':
        case '\r':
        case ' ':
          continue;
        default:
          goto label_2;
      }
    }
    while (++this.int_0 < this.char_0.Length);
label_2:
    if (this.int_0 == this.char_0.Length)
      throw new Exception("Reached end of string unexpectedly");
    char ch = this.char_0[this.int_0];
    ++this.int_0;
    switch (ch)
    {
      case '"':
        return (Class124.Enum3) 6;
      case '+':
      case '-':
      case '.':
      case '0':
      case '1':
      case '2':
      case '3':
      case '4':
      case '5':
      case '6':
      case '7':
      case '8':
      case '9':
        return (Class124.Enum3) 7;
      case ',':
        return (Class124.Enum3) 5;
      case ':':
        return (Class124.Enum3) 4;
      case '[':
        return (Class124.Enum3) 2;
      case ']':
        return (Class124.Enum3) 3;
      case 'f':
        if (this.char_0.Length - this.int_0 >= 4 && this.char_0[this.int_0] == 'a' && (this.char_0[this.int_0 + 1] == 'l' && this.char_0[this.int_0 + 2] == 's') && this.char_0[this.int_0 + 3] == 'e')
        {
          this.int_0 += 4;
          return (Class124.Enum3) 9;
        }
        break;
      case 'n':
        if (this.char_0.Length - this.int_0 >= 3 && this.char_0[this.int_0] == 'u' && (this.char_0[this.int_0 + 1] == 'l' && this.char_0[this.int_0 + 2] == 'l'))
        {
          this.int_0 += 3;
          return (Class124.Enum3) 10;
        }
        break;
      case 't':
        if (this.char_0.Length - this.int_0 >= 3 && this.char_0[this.int_0] == 'r' && (this.char_0[this.int_0 + 1] == 'u' && this.char_0[this.int_0 + 2] == 'e'))
        {
          this.int_0 += 3;
          return (Class124.Enum3) 8;
        }
        break;
      case '{':
        return (Class124.Enum3) 0;
      case '}':
        return (Class124.Enum3) 1;
    }
    throw new Exception("Could not find token at index " + (object) --this.int_0);
  }

  private enum Enum3
  {
  }
}
