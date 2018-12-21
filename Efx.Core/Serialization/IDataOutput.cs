// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.IDataOutput
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Serialization
{
  public interface IDataOutput
  {
    void Write(bool value);

    void Write(byte value);

    void Write(sbyte value);

    void Write(byte[] bytes);

    void Write(double value);

    void Write(float value);

    void Write(Decimal value);

    void Write(int value);

    void Write(uint value);

    void Write(short value);

    void Write(ushort value);

    void Write(long value);

    void Write(ulong value);

    void Write(char value);

    void Write(string value);

    void WriteObject(object value);
  }
}
