// Decompiled with JetBrains decompiler
// Type: Efx.Core.Debug.ITraceProvider
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System.Collections.Generic;

namespace Efx.Core.Debug
{
  public interface ITraceProvider
  {
    void WriteLine(string pText, string pAdditionalInfo);

    int GetErrors(int pPageIndex, int pPageSize, IList<ErrorLogEntry> pErrorEntryList);

    ErrorLogEntry GetError(string pId);

    bool Enabled { get; }
  }
}
