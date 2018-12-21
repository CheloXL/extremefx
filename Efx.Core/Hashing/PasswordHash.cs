// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.PasswordHash
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Efx.Core.Hashing
{
  public sealed class PasswordHash
  {
    private static readonly UTF8Encoding _encoder = new UTF8Encoding();
    private static readonly char[] _ascii64 = new char[64]{ '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private const string BYTE0 = "*0";
    private const string BYTE1 = "*1";
    private const string BYTEP = "$P$";
    private readonly int _iters;

    public PasswordHash()
      : this(8)
    {
    }

    public PasswordHash(int pIterationCountLog2)
    {
      if (pIterationCountLog2 < 3 || pIterationCountLog2 > 30)
        throw new ArgumentOutOfRangeException(nameof (pIterationCountLog2));
      this._iters = pIterationCountLog2;
    }

    public string HashPassword(string pPassword)
    {
      byte[] data = new byte[6];
      new RNGCryptoServiceProvider().GetNonZeroBytes(data);
      StringBuilder pStrb = new StringBuilder("$P$");
      pStrb.Append(PasswordHash._ascii64[Math.Min(this._iters + 5, 30)]);
      PasswordHash.encode64(pStrb, (IList<byte>) data, 6);
      return PasswordHash.crypt(pPassword, pStrb.ToString());
    }

    private static string crypt(string pPassword, string pSalt)
    {
      if (string.IsNullOrEmpty(pPassword) || string.IsNullOrEmpty(pSalt))
        return string.Empty;
      string str = pSalt.StartsWith("*0", StringComparison.OrdinalIgnoreCase) ? "*1" : "*0";
      if (!pSalt.StartsWith("$P$", StringComparison.Ordinal))
        return str;
      try
      {
        int num1 = Array.IndexOf<char>(PasswordHash._ascii64, pSalt[3]);
        if (num1 < 7 || num1 > 30)
          return str;
        int num2 = 1 << num1;
        SHA1Managed shA1Managed = new SHA1Managed();
        byte[] bytes1 = PasswordHash._encoder.GetBytes(pPassword);
        int length1 = bytes1.Length;
        byte[] bytes2 = PasswordHash._encoder.GetBytes(pSalt.Substring(4, 8));
        int length2 = bytes2.Length;
        byte[] buffer1 = new byte[length2 + length1];
        bytes2.CopyTo((Array) buffer1, 0);
        bytes1.CopyTo((Array) buffer1, length2);
        byte[] hash = shA1Managed.ComputeHash(buffer1);
        byte[] buffer2 = new byte[20 + length1];
        while (num2-- != 0)
        {
          hash.CopyTo((Array) buffer2, 0);
          bytes1.CopyTo((Array) buffer2, 20);
          hash = shA1Managed.ComputeHash(buffer2);
        }
        StringBuilder pStrb = new StringBuilder(pSalt.Substring(0, 12));
        PasswordHash.encode64(pStrb, (IList<byte>) hash, 20);
        return pStrb.ToString();
      }
      catch (Exception ex)
      {
      }
      return str;
    }

    private static void encode64(StringBuilder pStrb, IList<byte> pRandom, int pLength)
    {
      int num1 = 0;
      do
      {
        IList<byte> byteList = pRandom;
        int index1 = num1;
        int index2 = index1 + 1;
        int num2 = (int) byteList[index1];
        pStrb.Append(PasswordHash._ascii64[num2 & 63]);
        if (index2 < pLength)
          goto label_6;
label_1:
        pStrb.Append(PasswordHash._ascii64[num2 >> 6 & 63]);
        int index3 = index2 + 1;
        if (index3 < pLength)
        {
          if (index3 < pLength)
            num2 |= (int) pRandom[index3] << 16;
          pStrb.Append(PasswordHash._ascii64[num2 >> 12 & 63]);
          num1 = index3 + 1;
          if (num1 < pLength)
          {
            pStrb.Append(PasswordHash._ascii64[num2 >> 18 & 63]);
            continue;
          }
          goto label_9;
        }
        else
          goto label_8;
label_6:
        num2 |= (int) pRandom[index2] << 8;
        goto label_1;
      }
      while (num1 < pLength);
      goto label_10;
label_8:
      return;
label_9:
      return;
label_10:;
    }

    public bool CheckPassword(string pPassword, string pHash)
    {
      string str = PasswordHash.crypt(pPassword, pHash);
      if (!str.StartsWith("*", StringComparison.OrdinalIgnoreCase))
        return str.Equals(pHash, StringComparison.Ordinal);
      return false;
    }
  }
}
