// Decompiled with JetBrains decompiler
// Type: Class68
// Assembly: Efx.Web.Authorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 06C571D6-DC37-4657-A156-F8EB982998FF
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Authorization.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

internal static class Class68
{
  private static Class68.Class71 class71_0 = new Class68.Class71();

  internal static long smethod_0()
  {
    if ((object) Assembly.GetCallingAssembly() != (object) typeof (Class68).Assembly || !Class68.smethod_1())
      return 0;
    lock (Class68.class71_0)
    {
      long long_0 = Class68.class71_0.method_0();
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
        int num1 = Class68.smethod_3(typeof (Class68));
        int num2 = Class68.Class74.smethod_0();
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
        long_0 = (long) (num7 + (num7 << 30)) ^ 311771139397313649L;
        Class68.class71_0.method_1(long_0);
      }
      return long_0;
    }
  }

  private static bool smethod_1()
  {
    return Class68.smethod_2();
  }

  private static bool smethod_2()
  {
    StackFrame frame = new StackTrace().GetFrame(3);
    MethodBase methodBase = frame == null ? (MethodBase) null : frame.GetMethod();
    Type type = (object) methodBase == null ? (Type) null : methodBase.DeclaringType;
    return (object) type != (object) typeof (RuntimeMethodHandle) && (object) type != null && (object) type.Assembly == (object) typeof (Class68).Assembly;
  }

  private static int smethod_3(Type type_0)
  {
    return type_0.MetadataToken;
  }

  private sealed class Class69
  {
    internal static int smethod_0()
    {
      return Class68.Class75.smethod_1(Class68.Class75.smethod_1(Class68.Class73.smethod_0(), Class68.Class75.smethod_0(Class68.smethod_3(typeof (Class68.Class69)), Class68.Class72.smethod_0())), Class68.smethod_3(typeof (Class68.Class76)));
    }
  }

  private sealed class Class70
  {
    internal static int smethod_0()
    {
      return Class68.Class75.smethod_2(Class68.smethod_3(typeof (Class68.Class70)), Class68.Class75.smethod_0(Class68.smethod_3(typeof (Class68.Class74)), Class68.Class75.smethod_1(Class68.smethod_3(typeof (Class68.Class72)), Class68.Class75.smethod_2(Class68.smethod_3(typeof (Class68.Class73)), Class68.Class75.smethod_0(Class68.smethod_3(typeof (Class68.Class69)), Class68.smethod_3(typeof (Class68.Class76)))))));
    }
  }

  private sealed class Class71
  {
    private int int_0;
    private int int_1;

    internal Class71()
    {
      this.method_1(0L);
    }

    internal long method_0()
    {
      if ((object) Assembly.GetCallingAssembly() != (object) typeof (Class68.Class71).Assembly || !Class68.smethod_1())
        return 2918384;
      int[] numArray = new int[4]{ 0, 0, 0, -685731945 };
      numArray[1] = -879473329;
      numArray[2] = -287856721;
      numArray[0] = -928804952;
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
      if ((object) Assembly.GetCallingAssembly() != (object) typeof (Class68.Class71).Assembly || !Class68.smethod_1())
        return;
      int[] numArray = new int[4]{ 0, -879473329, 0, 0 };
      numArray[0] = -928804952;
      numArray[2] = -287856721;
      numArray[3] = -685731945;
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

  private sealed class Class72
  {
    internal static int smethod_0()
    {
      return Class68.Class75.smethod_0(Class68.smethod_3(typeof (Class68.Class73)), Class68.smethod_3(typeof (Class68.Class70)) ^ Class68.Class75.smethod_1(Class68.smethod_3(typeof (Class68.Class72)), Class68.Class75.smethod_2(Class68.smethod_3(typeof (Class68.Class76)), Class68.Class70.smethod_0())));
    }
  }

  private sealed class Class73
  {
    internal static int smethod_0()
    {
      return Class68.Class75.smethod_2(Class68.Class75.smethod_0(Class68.Class72.smethod_0() ^ 527758446, Class68.smethod_3(typeof (Class68.Class70))), Class68.Class75.smethod_1(Class68.smethod_3(typeof (Class68.Class74)) ^ Class68.smethod_3(typeof (Class68.Class76)), -2105741907));
    }
  }

  private sealed class Class74
  {
    internal static int smethod_0()
    {
      return Class68.Class75.smethod_2(Class68.Class75.smethod_1(Class68.smethod_3(typeof (Class68.Class72)), Class68.Class75.smethod_2(Class68.smethod_3(typeof (Class68.Class74)), Class68.smethod_3(typeof (Class68.Class69)))), Class68.Class76.smethod_0());
    }
  }

  private static class Class75
  {
    internal static int smethod_0(int int_0, int int_1)
    {
      return int_0 ^ int_1 - -1095721778;
    }

    internal static int smethod_1(int int_0, int int_1)
    {
      return int_0 - 1874359455 ^ int_1 - 448357591;
    }

    internal static int smethod_2(int int_0, int int_1)
    {
      return int_0 ^ (int_1 - 1438809996 ^ int_0 - int_1);
    }
  }

  private sealed class Class76
  {
    internal static int smethod_0()
    {
      return Class68.Class75.smethod_0(Class68.smethod_3(typeof (Class68.Class76)), Class68.Class75.smethod_2(Class68.Class75.smethod_1(Class68.smethod_3(typeof (Class68.Class69)), Class68.smethod_3(typeof (Class68.Class74))), Class68.Class75.smethod_2(Class68.smethod_3(typeof (Class68.Class73)) ^ -861875554, Class68.Class69.smethod_0())));
    }
  }
}
