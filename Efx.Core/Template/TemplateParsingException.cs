// Decompiled with JetBrains decompiler
// Type: Efx.Core.Template.TemplateParsingException
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Runtime.Serialization;

namespace Efx.Core.Template
{
  [Serializable]
  public class TemplateParsingException : Exception
  {
    public TemplateParsingException()
    {
    }

    public TemplateParsingException(string pMessage)
      : base(pMessage)
    {
    }

    public TemplateParsingException(string pMessage, Exception pInnerException)
      : base(pMessage, pInnerException)
    {
    }

    public TemplateParsingException(SerializationInfo pInfo, StreamingContext pContext)
      : base(pInfo, pContext)
    {
    }
  }
}
