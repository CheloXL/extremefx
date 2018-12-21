// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.SaltRandomGenerator
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

namespace Efx.Core.Hashing
{
  public class SaltRandomGenerator : StringGenerator
  {
    protected override void defineSets()
    {
      this.charsLCase = "abcdefgijkmnopqrstwxyz";
      this.charsUCase = "ABCDEFGHJKLMNPQRSTWXYZ";
      this.charsNumeric = "23456789";
      this.charsSpecial = "./";
    }

    protected override string returnFinalString()
    {
      switch (this.final.Length)
      {
        case 8:
          return "_" + this.final;
        case 16:
          return "$1$" + this.final;
        case 22:
          return "$2$" + this.final;
        default:
          return (string) null;
      }
    }
  }
}
