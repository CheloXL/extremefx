// Decompiled with JetBrains decompiler
// Type: Efx.Core.Crypto.XXTea
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;

namespace Efx.Core.Crypto
{
  public static class XXTea
  {
    public static byte[] Encrypt(byte[] pData, byte[] pKey)
    {
      if (pData != null && pData.Length != 0)
        return XXTea.toByteArray((IList<uint>) XXTea.encrypt(XXTea.toUInt32Array((IList<byte>) pData, true), XXTea.toUInt32Array((IList<byte>) pKey, false)), false);
      return pData;
    }

    public static byte[] Decrypt(byte[] pData, byte[] pKey)
    {
      if (pData != null && pData.Length != 0)
        return XXTea.toByteArray((IList<uint>) XXTea.decrypt(XXTea.toUInt32Array((IList<byte>) pData, false), XXTea.toUInt32Array((IList<byte>) pKey, false)), true);
      return pData;
    }

    private static uint[] encrypt(uint[] pV, uint[] pK)
    {
      int index1 = pV.Length - 1;
      if (index1 < 1)
        return pV;
      if (pK.Length < 4)
      {
        uint[] numArray = new uint[4];
        pK.CopyTo((Array) numArray, 0);
        pK = numArray;
      }
      uint num1 = pV[index1];
      uint num2 = 0;
      int num3 = 6 + 52 / (index1 + 1);
      while (0 < num3--)
      {
        num2 += 2654435769U;
        uint num4 = num2 >> 2 & 3U;
        int index2;
        for (index2 = 0; index2 < index1; ++index2)
        {
          uint num5 = pV[index2 + 1];
          num1 = (pV[index2] += (uint) (((int) (num1 >> 5) ^ (int) num5 << 2) + ((int) (num5 >> 3) ^ (int) num1 << 4) ^ ((int) num2 ^ (int) num5) + ((int) pK[(long) (index2 & 3) ^ (long) num4] ^ (int) num1)));
        }
        uint num6 = pV[0];
        num1 = (pV[index1] += (uint) (((int) (num1 >> 5) ^ (int) num6 << 2) + ((int) (num6 >> 3) ^ (int) num1 << 4) ^ ((int) num2 ^ (int) num6) + ((int) pK[(long) (index2 & 3) ^ (long) num4] ^ (int) num1)));
      }
      return pV;
    }

    private static uint[] decrypt(uint[] pV, uint[] pK)
    {
      int index1 = pV.Length - 1;
      if (index1 < 1)
        return pV;
      if (pK.Length < 4)
      {
        uint[] numArray = new uint[4];
        pK.CopyTo((Array) numArray, 0);
        pK = numArray;
      }
      uint num1 = pV[0];
      for (uint index2 = (uint) ((ulong) (6 + 52 / (index1 + 1)) * 2654435769UL); index2 != 0U; index2 -= 2654435769U)
      {
        uint num2 = index2 >> 2 & 3U;
        int index3;
        for (index3 = index1; index3 > 0; --index3)
        {
          uint num3 = pV[index3 - 1];
          num1 = (pV[index3] -= (uint) (((int) (num3 >> 5) ^ (int) num1 << 2) + ((int) (num1 >> 3) ^ (int) num3 << 4) ^ ((int) index2 ^ (int) num1) + ((int) pK[(long) (index3 & 3) ^ (long) num2] ^ (int) num3)));
        }
        uint num4 = pV[index1];
        num1 = (pV[0] -= (uint) (((int) (num4 >> 5) ^ (int) num1 << 2) + ((int) (num1 >> 3) ^ (int) num4 << 4) ^ ((int) index2 ^ (int) num1) + ((int) pK[(long) (index3 & 3) ^ (long) num2] ^ (int) num4)));
      }
      return pV;
    }

    private static uint[] toUInt32Array(IList<byte> pData, bool pIncludeLength)
    {
      int length = (pData.Count & 3) == 0 ? pData.Count >> 2 : (pData.Count >> 2) + 1;
      uint[] numArray;
      if (pIncludeLength)
      {
        numArray = new uint[length + 1];
        numArray[length] = (uint) pData.Count;
      }
      else
        numArray = new uint[length];
      int count = pData.Count;
      for (int index = 0; index < count; ++index)
        numArray[index >> 2] |= (uint) pData[index] << ((index & 3) << 3);
      return numArray;
    }

    private static byte[] toByteArray(IList<uint> pData, bool pIncludeLength)
    {
      int length = pData.Count << 2;
      if (pIncludeLength)
      {
        int num = (int) pData[pData.Count - 1];
        if (num > length)
          throw new XXTeaException("XxTea Decrypt Error: Wrong input data.");
        length = num;
      }
      byte[] numArray = new byte[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = (byte) (pData[index >> 2] >> ((index & 3) << 3));
      return numArray;
    }
  }
}
