// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.PasswordRandomGenerator
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

namespace Efx.Core.Hashing
{
  public class PasswordRandomGenerator : StringGenerator
  {
    protected override void defineSets()
    {
      this.charsLCase = "abcdefgijkmnopqrstwxyz";
      this.charsUCase = "ABCDEFGHJKLMNPQRSTWXYZ";
      this.charsNumeric = "23456789";
      this.charsSpecial = "*$-+?_&=!%{}/";
    }

    protected override string returnFinalString()
    {
      return this.final;
    }
  }
}
