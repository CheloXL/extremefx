// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.IExternalizable
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

namespace Efx.Core.Serialization
{
  public interface IExternalizable
  {
    void ReadExternal(IDataInput input);

    void WriteExternal(IDataOutput output);
  }
}
