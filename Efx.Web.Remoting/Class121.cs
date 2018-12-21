// Decompiled with JetBrains decompiler
// Type: Class121
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Web.Remoting.Serialization.Amf;

internal sealed class Class121 : IDataInput
{
  private readonly Class118 class118_0;

  public Class121(Class118 class118_1)
  {
    this.class118_0 = class118_1;
  }

  public bool ReadBoolean()
  {
    return this.class118_0.ReadBoolean();
  }

  public byte ReadByte()
  {
    return this.class118_0.ReadByte();
  }

  public void ReadBytes(byte[] pBytes, uint pOffset, uint pLength)
  {
    byte[] numArray = this.class118_0.ReadBytes((int) pLength);
    for (int index = 0; index < numArray.Length; ++index)
      pBytes[(long) index + (long) pOffset] = numArray[index];
  }

  public double ReadDouble()
  {
    return this.class118_0.ReadDouble();
  }

  public float ReadFloat()
  {
    return this.class118_0.method_0();
  }

  public int ReadInt()
  {
    return this.class118_0.ReadInt32();
  }

  public object ReadObject()
  {
    return this.class118_0.method_2();
  }

  public short ReadShort()
  {
    return this.class118_0.ReadInt16();
  }

  public byte ReadUnsignedByte()
  {
    return this.class118_0.ReadByte();
  }

  public uint ReadUnsignedInt()
  {
    return (uint) this.class118_0.ReadInt32();
  }

  public ushort ReadUnsignedShort()
  {
    return this.class118_0.ReadUInt16();
  }

  public string ReadUTF()
  {
    return this.class118_0.ReadString();
  }

  public string ReadUTFBytes(uint pLength)
  {
    return this.class118_0.method_1((int) pLength);
  }
}
