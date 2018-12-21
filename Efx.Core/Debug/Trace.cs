// Decompiled with JetBrains decompiler
// Type: Efx.Core.Debug.Trace
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Efx.Core.Debug
{
  public static class Trace
  {
    private static ITraceProvider _provider;

    public static void SetProvider(ITraceProvider pProvider)
    {
      if (pProvider == null)
        throw new ArgumentNullException(nameof (pProvider));
      Trace._provider = pProvider;
    }

    public static void Assert(bool pBoolExpression, string pMessage)
    {
      if (!pBoolExpression)
        return;
      Trace.LogError(pMessage);
    }

    public static void LogError(Exception pException)
    {
      Trace.LogError(Trace.getException(pException));
    }

    public static void LogError(string pMessage)
    {
      if (Trace._provider == null)
        return;
      Trace._provider.WriteLine(pMessage, (string) null);
    }

    public static void LogError(string pMessage, string pAdditionalInfo)
    {
      if (Trace._provider == null)
        return;
      Trace._provider.WriteLine(pMessage, pAdditionalInfo);
    }

    public static void LogError(string pMessage, Exception pException)
    {
      Trace.LogError(Trace.getException(pException), pMessage);
    }

    public static int GetErrors(
      int pPageIndex,
      int pPageSize,
      IList<ErrorLogEntry> pErrorEntryList)
    {
      if (Trace._provider != null)
        return Trace._provider.GetErrors(pPageIndex, pPageSize, pErrorEntryList);
      return 0;
    }

    public static ErrorLogEntry GetError(string pId)
    {
      if (Trace._provider != null)
        return Trace._provider.GetError(pId);
      return new ErrorLogEntry();
    }

    private static string getException(Exception pException)
    {
      string str = string.Format("{0} at source [{1}]<br />{2}", (object) Trace.filterText(pException.Message), (object) Trace.filterText(pException.Source), (object) Trace.filterText(pException.StackTrace).Replace("\r\n", "<br />"));
      if (pException.InnerException != null)
        str = str + "<br />in<br />" + Trace.getException(pException.InnerException);
      return str;
    }

    private static string filterText(string pIn)
    {
      return Regex.Replace(pIn, "([^\\w\\d:\\s\\\\!\\\"\\·\\#@\\$\\%\\&\\/\\(\\)\\=\\?¿\\|\\'¡`\\^\\[\\]\\+\\*\\´¨\\{\\}<>;\\,\\:\\.\\-_]+)", string.Empty);
    }
  }
}
