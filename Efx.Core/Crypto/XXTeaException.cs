// Decompiled with JetBrains decompiler
// Type: Efx.Core.Crypto.XXTeaException
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Crypto
{
  [Serializable]
  public sealed class XXTeaException : Exception
  {
    public XXTeaException()
    {
    }

    public XXTeaException(string pMessage)
      : base(pMessage)
    {
    }

    public XXTeaException(string pMessage, Exception pInner)
      : base(pMessage, pInner)
    {
    }
  }
}
