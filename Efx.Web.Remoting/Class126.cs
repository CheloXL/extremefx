// Decompiled with JetBrains decompiler
// Type: Class126
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

internal static class Class126
{
  private static Class126.Class129 class129_0 = new Class126.Class129();

  internal static long smethod_0()
  {
    if ((object) Assembly.GetCallingAssembly() != (object) typeof (Class126).Assembly || !Class126.smethod_1())
      return 0;
    lock (Class126.class129_0)
    {
      long long_0 = Class126.class129_0.method_0();
      if (long_0 == 0L)
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        List<byte> byteList = new List<byte>();
        AssemblyName assemblyName;
        try
        {
          assemblyName = executingAssembly.GetName();
        }
        catch
        {
          assemblyName = new AssemblyName(executingAssembly.FullName);
        }
        byte[] numArray = assemblyName.GetPublicKeyToken();
        if (numArray != null && numArray.Length == 0)
          numArray = (byte[]) null;
        if (numArray != null)
          byteList.AddRange((IEnumerable<byte>) numArray);
        byteList.AddRange((IEnumerable<byte>) Encoding.Unicode.GetBytes(assemblyName.Name));
        int num1 = Class126.smethod_3(typeof (Class126));
        int num2 = Class126.Class132.smethod_0();
        byteList.Add((byte) (num1 >> 24));
        byteList.Add((byte) (num2 >> 16));
        byteList.Add((byte) (num1 >> 8));
        byteList.Add((byte) num2);
        byteList.Add((byte) (num1 >> 16));
        byteList.Add((byte) (num2 >> 8));
        byteList.Add((byte) num1);
        byteList.Add((byte) (num2 >> 24));
        int count = byteList.Count;
        ulong num3 = 0;
        for (int index = 0; index != count; ++index)
        {
          ulong num4 = num3 + (ulong) byteList[index];
          ulong num5 = num4 + (num4 << 20);
          num3 = num5 ^ num5 >> 12;
          byteList[index] = (byte) 0;
        }
        ulong num6 = num3 + (num3 << 6);
        ulong num7 = num6 ^ num6 >> 22;
        long_0 = (long) (num7 + (num7 << 30)) ^ -5270735751521719443L;
        Class126.class129_0.method_1(long_0);
      }
      return long_0;
    }
  }

  private static bool smethod_1()
  {
    return Class126.smethod_2();
  }

  private static bool smethod_2()
  {
    StackFrame frame = new StackTrace().GetFrame(3);
    MethodBase methodBase = frame == null ? (MethodBase) null : frame.GetMethod();
    Type type = (object) methodBase == null ? (Type) null : methodBase.DeclaringType;
    return (object) type != (object) typeof (RuntimeMethodHandle) && (object) type != null && (object) type.Assembly == (object) typeof (Class126).Assembly;
  }

  private static int smethod_3(Type type_0)
  {
    return type_0.MetadataToken;
  }

  private sealed class Class127
  {
    internal static int smethod_0()
    {
      return Class126.Class133.smethod_1(Class126.Class133.smethod_1(Class126.Class131.smethod_0(), Class126.Class133.smethod_0(Class126.smethod_3(typeof (Class126.Class127)), Class126.Class130.smethod_0())), Class126.smethod_3(typeof (Class126.Class134)));
    }
  }

  private sealed class Class128
  {
    internal static int smethod_0()
    {
      return Class126.Class133.smethod_2(Class126.smethod_3(typeof (Class126.Class128)), Class126.Class133.smethod_0(Class126.smethod_3(typeof (Class126.Class132)), Class126.Class133.smethod_1(Class126.smethod_3(typeof (Class126.Class130)), Class126.Class133.smethod_2(Class126.smethod_3(typeof (Class126.Class131)), Class126.Class133.smethod_0(Class126.smethod_3(typeof (Class126.Class127)), Class126.smethod_3(typeof (Class126.Class134)))))));
    }
  }

  private sealed class Class129
  {
    private int int_0;
    private int int_1;

    internal Class129()
    {
      this.method_1(0L);
    }

    internal long method_0()
    {
      if ((object) Assembly.GetCallingAssembly() != (object) typeof (Class126.Class129).Assembly || !Class126.smethod_1())
        return 2918384;
      int[] numArray = new int[4]{ 0, 0, 0, 1675230909 };
      numArray[1] = 835644423;
      numArray[2] = -1664138670;
      numArray[0] = 933739782;
      int int0 = this.int_0;
      int int1 = this.int_1;
      int num1 = -1640531527;
      int num2 = -957401312;
      for (int index = 0; index != 32; ++index)
      {
        int1 -= (int0 << 4 ^ int0 >> 5) + int0 ^ num2 + numArray[num2 >> 11 & 3];
        num2 -= num1;
        int0 -= (int1 << 4 ^ int1 >> 5) + int1 ^ num2 + numArray[num2 & 3];
      }
      for (int index = 0; index != 4; ++index)
        numArray[index] = 0;
      return (long) ((ulong) int1 << 32 | (ulong) (uint) int0);
    }

    internal void method_1(long long_0)
    {
      if ((object) Assembly.GetCallingAssembly() != (object) typeof (Class126.Class129).Assembly || !Class126.smethod_1())
        return;
      int[] numArray = new int[4]{ 0, 835644423, 0, 0 };
      numArray[0] = 933739782;
      numArray[2] = -1664138670;
      numArray[3] = 1675230909;
      int num1 = -1640531527;
      int num2 = (int) long_0;
      int num3 = (int) (long_0 >> 32);
      int num4 = 0;
      for (int index = 0; index != 32; ++index)
      {
        num2 += (num3 << 4 ^ num3 >> 5) + num3 ^ num4 + numArray[num4 & 3];
        num4 += num1;
        num3 += (num2 << 4 ^ num2 >> 5) + num2 ^ num4 + numArray[num4 >> 11 & 3];
      }
      for (int index = 0; index != 4; ++index)
        numArray[index] = 0;
      this.int_0 = num2;
      this.int_1 = num3;
    }
  }

  private sealed class Class130
  {
    internal static int smethod_0()
    {
      return Class126.Class133.smethod_0(Class126.smethod_3(typeof (Class126.Class131)), Class126.smethod_3(typeof (Class126.Class128)) ^ Class126.Class133.smethod_1(Class126.smethod_3(typeof (Class126.Class130)), Class126.Class133.smethod_2(Class126.smethod_3(typeof (Class126.Class134)), Class126.Class128.smethod_0())));
    }
  }

  private sealed class Class131
  {
    internal static int smethod_0()
    {
      return Class126.Class133.smethod_2(Class126.Class133.smethod_0(Class126.Class130.smethod_0() ^ 527758446, Class126.smethod_3(typeof (Class126.Class128))), Class126.Class133.smethod_1(Class126.smethod_3(typeof (Class126.Class132)) ^ Class126.smethod_3(typeof (Class126.Class134)), -176174282));
    }
  }

  private sealed class Class132
  {
    internal static int smethod_0()
    {
      return Class126.Class133.smethod_2(Class126.Class133.smethod_1(Class126.smethod_3(typeof (Class126.Class130)), Class126.Class133.smethod_2(Class126.smethod_3(typeof (Class126.Class132)), Class126.smethod_3(typeof (Class126.Class127)))), Class126.Class134.smethod_0());
    }
  }

  private static class Class133
  {
    internal static int smethod_0(int int_0, int int_1)
    {
      return int_0 ^ int_1 - -1450575816;
    }

    internal static int smethod_1(int int_0, int int_1)
    {
      return int_0 - -579468797 ^ int_1 - 1704053127;
    }

    internal static int smethod_2(int int_0, int int_1)
    {
      return int_0 ^ (int_1 - 637495578 ^ int_0 - int_1);
    }
  }

  private sealed class Class134
  {
    internal static int smethod_0()
    {
      return Class126.Class133.smethod_0(Class126.smethod_3(typeof (Class126.Class134)), Class126.Class133.smethod_2(Class126.Class133.smethod_1(Class126.smethod_3(typeof (Class126.Class127)), Class126.smethod_3(typeof (Class126.Class132))), Class126.Class133.smethod_2(Class126.smethod_3(typeof (Class126.Class131)) ^ -975673014, Class126.Class127.smethod_0())));
    }
  }
}
