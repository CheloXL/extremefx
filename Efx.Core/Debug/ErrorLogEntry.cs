// Decompiled with JetBrains decompiler
// Type: Efx.Core.Debug.ErrorLogEntry
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Debug
{
  public struct ErrorLogEntry
  {
    public string AdditionalInfo;
    public DateTime Date;
    public string Id;
    public string Message;

    public ErrorLogEntry(string pId, DateTime pDate, string pMessage, string pAdditionalInfo)
    {
      this.Id = pId;
      this.Date = pDate;
      this.Message = pMessage;
      this.AdditionalInfo = pAdditionalInfo;
    }
  }
}
