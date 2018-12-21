// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.FastBinaryFormatter
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System.IO;

namespace Efx.Core.Serialization
{
  public static class FastBinaryFormatter
  {
    public static void Serialize(Stream stream, object @object)
    {
      new BinaryDataOutput((BinaryWriter) new CompactBinaryWriter(stream)).WriteObject(@object);
    }

    public static object Deserialize(Stream stream)
    {
      return new BinaryDataInput((BinaryReader) new CompactBinaryReader(stream)).ReadObject();
    }

    public static T Deserialize<T>(Stream stream)
    {
      return (T) new BinaryDataInput((BinaryReader) new CompactBinaryReader(stream)).ReadObject();
    }
  }
}
