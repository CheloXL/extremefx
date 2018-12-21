// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.Hash
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Efx.Core.Hashing
{
  public static class Hash
  {
    private static readonly UTF8Encoding _encoder = new UTF8Encoding();

    public static string GetHexHash(this byte[] pByteArray, HashType pHashType)
    {
      return Hash.ToHex((IEnumerable<byte>) pByteArray.GetHash(pHashType));
    }

    public static string GetHexHash(this string pInput, HashType pHashType)
    {
      if (!string.IsNullOrEmpty(pInput))
        return Hash.ToHex((IEnumerable<byte>) Hash._encoder.GetBytes(pInput).GetHash(pHashType));
      return string.Empty;
    }

    public static byte[] GetHash(this string pInput, HashType pHashType)
    {
      if (!string.IsNullOrEmpty(pInput))
        return Hash._encoder.GetBytes(pInput).GetHash(pHashType);
      return (byte[]) null;
    }

    private static string ToHex(IEnumerable<byte> pByteArray)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte pByte in pByteArray)
        stringBuilder.AppendFormat("{0:x2}", (object) pByte);
      return stringBuilder.ToString();
    }

    public static byte[] GetHash(this byte[] pByteArray, HashType pHashType)
    {
      if (pByteArray == null)
        throw new ArgumentNullException(nameof (pByteArray));
      switch (pHashType)
      {
        case HashType.Md5:
          return new MD5CryptoServiceProvider().ComputeHash(pByteArray);
        case HashType.Sha1:
          return new SHA1Managed().ComputeHash(pByteArray);
        case HashType.Sha256:
          return new SHA256Managed().ComputeHash(pByteArray);
        case HashType.Sha384:
          return new SHA384Managed().ComputeHash(pByteArray);
        case HashType.Sha512:
          return new SHA512Managed().ComputeHash(pByteArray);
        case HashType.Blake256:
          return new Blake256().ComputeHash(pByteArray);
        case HashType.Blake512:
          return new Blake512().ComputeHash(pByteArray);
        default:
          throw new InvalidEnumArgumentException("Invalid HashType");
      }
    }
  }
}
