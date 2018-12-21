// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.CompactBinaryWriter
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System.IO;

namespace Efx.Core.Serialization
{
  public sealed class CompactBinaryWriter : BinaryWriter
  {
    public CompactBinaryWriter(Stream stream)
      : base(stream)
    {
    }

    public override void Write(uint value)
    {
      for (; ((long) value & (long) sbyte.MinValue) != 0L; value >>= 7)
        this.BaseStream.WriteByte((byte) ((int) value & (int) sbyte.MaxValue | 128));
      this.BaseStream.WriteByte((byte) value);
    }

    public override void Write(ulong value)
    {
      for (; ((long) value & (long) sbyte.MinValue) != 0L; value >>= 7)
        this.BaseStream.WriteByte((byte) ((long) value & (long) sbyte.MaxValue | 128L));
      this.BaseStream.WriteByte((byte) value);
    }

    public override void Write(char ch)
    {
      this.Write((uint) ch);
    }

    public override void Write(ushort ch)
    {
      this.Write((uint) ch);
    }

    public override void Write(short ch)
    {
      this.Write(CompactBinaryWriter.EncodeZigZag32((int) ch));
    }

    public override void Write(int ch)
    {
      this.Write(CompactBinaryWriter.EncodeZigZag32(ch));
    }

    public override void Write(long ch)
    {
      this.Write(CompactBinaryWriter.EncodeZigZag64(ch));
    }

    private static uint EncodeZigZag32(int n)
    {
      return (uint) (n << 1 ^ n >> 31);
    }

    private static ulong EncodeZigZag64(long n)
    {
      return (ulong) (n << 1 ^ n >> 63);
    }
  }
}
