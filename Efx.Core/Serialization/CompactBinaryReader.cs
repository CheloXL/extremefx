// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.CompactBinaryReader
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.IO;

namespace Efx.Core.Serialization
{
  public sealed class CompactBinaryReader : BinaryReader
  {
    public CompactBinaryReader(Stream input)
      : base(input)
    {
    }

    public override byte ReadByte()
    {
      return (byte) this.BaseStream.ReadByte();
    }

    public override char ReadChar()
    {
      return (char) this.ReadUInt32();
    }

    public override short ReadInt16()
    {
      return (short) CompactBinaryReader.DecodeZigZag32(this.ReadUInt32());
    }

    public override int ReadInt32()
    {
      return CompactBinaryReader.DecodeZigZag32(this.ReadUInt32());
    }

    public override long ReadInt64()
    {
      return CompactBinaryReader.DecodeZigZag64(this.ReadUInt64());
    }

    public override ushort ReadUInt16()
    {
      return (ushort) this.ReadUInt32();
    }

    public override uint ReadUInt32()
    {
      int num1 = 0;
      for (int index = 0; index < 32; index += 7)
      {
        int num2 = this.BaseStream.ReadByte();
        if (num2 == -1)
          throw new Exception();
        num1 |= (num2 & (int) sbyte.MaxValue) << index;
        if ((num2 & 128) == 0)
          return (uint) num1;
      }
      throw new Exception();
    }

    public override ulong ReadUInt64()
    {
      long num1 = 0;
      for (int index = 0; index < 64; index += 7)
      {
        int num2 = this.BaseStream.ReadByte();
        if (num2 == -1)
          throw new Exception();
        num1 |= (long) (num2 & (int) sbyte.MaxValue) << index;
        if ((num2 & 128) == 0)
          return (ulong) num1;
      }
      throw new Exception();
    }

    private static int DecodeZigZag32(uint n)
    {
      return (int) (n >> 1) ^ -((int) n & 1);
    }

    private static long DecodeZigZag64(ulong n)
    {
      return (long) (n >> 1) ^ -((long) n & 1L);
    }
  }
}
