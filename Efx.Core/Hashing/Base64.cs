// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.Base64
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Text;

namespace Efx.Core.Hashing
{
  public static class Base64
  {
    public static string Encode(string pData)
    {
      if (string.IsNullOrEmpty(pData))
        throw new ArgumentException(nameof (pData));
      try
      {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(pData));
      }
      catch (Exception ex)
      {
        throw new Exception("Error in base64Encode" + ex.Message);
      }
    }

    public static string Decode(string pData)
    {
      if (string.IsNullOrEmpty(pData))
        throw new ArgumentException(nameof (pData));
      try
      {
        Decoder decoder = new UTF8Encoding().GetDecoder();
        byte[] bytes = Convert.FromBase64String(pData);
        char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];
        decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
        return new string(chars);
      }
      catch (Exception ex)
      {
        throw new Exception("Error in base64Decode" + ex.Message);
      }
    }
  }
}
