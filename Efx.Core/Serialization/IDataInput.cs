// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.IDataInput
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Serialization
{
  public interface IDataInput
  {
    bool ReadBoolean();

    byte ReadByte();

    sbyte ReadSByte();

    byte[] ReadBytes();

    double ReadDouble();

    float ReadFloat();

    Decimal ReadDecimal();

    int ReadInt();

    uint ReadUInt();

    short ReadShort();

    ushort ReadUShort();

    long ReadLong();

    ulong ReadULong();

    char ReadChar();

    string ReadString();

    object ReadObject();
  }
}
