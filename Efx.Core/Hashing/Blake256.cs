// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.Blake256
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Efx.Core.Hashing
{
  public sealed class Blake256 : HashAlgorithm
  {
    private static readonly uint[] _gCst = new uint[16]{ 608135816U, 2242054355U, 320440878U, 57701188U, 2752067618U, 698298832U, 137296536U, 3964562569U, 1160258022U, 953160567U, 3193202383U, 887688300U, 3232508343U, 3380367581U, 1065670069U, 3041331479U };
    private static readonly byte[] _gPadding = new byte[64]{ (byte) 128, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0 };
    private static readonly int[] _gSigma = new int[224]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3, 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4, 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8, 9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13, 2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9, 12, 5, 1, 15, 14, 13, 4, 10, 0, 7, 6, 3, 9, 2, 8, 11, 13, 11, 7, 14, 12, 1, 3, 9, 5, 0, 15, 4, 8, 6, 2, 10, 6, 15, 14, 9, 11, 3, 0, 8, 12, 2, 13, 7, 1, 4, 10, 5, 10, 2, 8, 4, 7, 6, 1, 5, 15, 11, 9, 14, 3, 12, 13, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3, 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4, 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8 };
    private readonly byte[] _mBuf = new byte[64];
    private readonly uint[] _mH = new uint[8];
    private readonly uint[] _mM = new uint[16];
    private readonly uint[] _mS = new uint[4];
    private readonly uint[] _mV = new uint[16];
    private const int NB_ROUNDS = 14;
    private bool _mBNullT;
    private int _mNBufLen;
    private ulong _mT;

    public Blake256()
    {
      this.HashSizeValue = 256;
      this.Initialize();
    }

    public override void Initialize()
    {
      this._mH[0] = 1779033703U;
      this._mH[1] = 3144134277U;
      this._mH[2] = 1013904242U;
      this._mH[3] = 2773480762U;
      this._mH[4] = 1359893119U;
      this._mH[5] = 2600822924U;
      this._mH[6] = 528734635U;
      this._mH[7] = 1541459225U;
      Array.Clear((Array) this._mS, 0, this._mS.Length);
      this._mT = 0UL;
      this._mNBufLen = 0;
      this._mBNullT = false;
      Array.Clear((Array) this._mBuf, 0, this._mBuf.Length);
    }

    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
      int num = ibStart;
      int length = 64 - this._mNBufLen;
      if (this._mNBufLen > 0 && cbSize >= length)
      {
        Array.Copy((Array) array, num, (Array) this._mBuf, this._mNBufLen, length);
        this._mT += 512UL;
        this.Compress((IList<byte>) this._mBuf, 0);
        num += length;
        cbSize -= length;
        this._mNBufLen = 0;
      }
      for (; cbSize >= 64; cbSize -= 64)
      {
        this._mT += 512UL;
        this.Compress((IList<byte>) array, num);
        num += 64;
      }
      if (cbSize > 0)
      {
        Array.Copy((Array) array, num, (Array) this._mBuf, this._mNBufLen, cbSize);
        this._mNBufLen += cbSize;
      }
      else
        this._mNBufLen = 0;
    }

    protected override byte[] HashFinal()
    {
      byte[] array = new byte[8];
      ulong num = this._mT + ((ulong) this._mNBufLen << 3);
      Blake256.UInt32ToBytes((uint) (num >> 32 & (ulong) uint.MaxValue), (IList<byte>) array, 0);
      Blake256.UInt32ToBytes((uint) (num & (ulong) uint.MaxValue), (IList<byte>) array, 4);
      if (this._mNBufLen == 55)
      {
        this._mT -= 8UL;
        this.HashCore(new byte[1]{ (byte) 129 }, 0, 1);
      }
      else
      {
        if (this._mNBufLen < 55)
        {
          if (this._mNBufLen == 0)
            this._mBNullT = true;
          this._mT -= (ulong) (440L - ((long) this._mNBufLen << 3));
          this.HashCore(Blake256._gPadding, 0, 55 - this._mNBufLen);
        }
        else
        {
          this._mT -= (ulong) (512L - ((long) this._mNBufLen << 3));
          this.HashCore(Blake256._gPadding, 0, 64 - this._mNBufLen);
          this._mT -= 440UL;
          this.HashCore(Blake256._gPadding, 1, 55);
          this._mBNullT = true;
        }
        this.HashCore(new byte[1]{ (byte) 1 }, 0, 1);
        this._mT -= 8UL;
      }
      this._mT -= 64UL;
      this.HashCore(array, 0, 8);
      byte[] numArray = new byte[32];
      for (int index = 0; index < 8; ++index)
        Blake256.UInt32ToBytes(this._mH[index], (IList<byte>) numArray, index << 2);
      return numArray;
    }

    private static uint BytesToUInt32(IList<byte> pb, int iOffset)
    {
      return (uint) ((int) pb[iOffset + 3] | (int) pb[iOffset + 2] << 8 | (int) pb[iOffset + 1] << 16 | (int) pb[iOffset] << 24);
    }

    private static uint RotateRight(uint u, int nBits)
    {
      return u >> nBits | u << 32 - nBits;
    }

    private static void UInt32ToBytes(uint u, IList<byte> pbOut, int iOffset)
    {
      for (int index = 3; index >= 0; --index)
      {
        pbOut[iOffset + index] = (byte) (u & (uint) byte.MaxValue);
        u >>= 8;
      }
    }

    private void Compress(IList<byte> pbBlock, int iOffset)
    {
      for (int index = 0; index < 16; ++index)
        this._mM[index] = Blake256.BytesToUInt32(pbBlock, iOffset + (index << 2));
      Array.Copy((Array) this._mH, (Array) this._mV, 8);
      this._mV[8] = this._mS[0] ^ 608135816U;
      this._mV[9] = this._mS[1] ^ 2242054355U;
      this._mV[10] = this._mS[2] ^ 320440878U;
      this._mV[11] = this._mS[3] ^ 57701188U;
      this._mV[12] = 2752067618U;
      this._mV[13] = 698298832U;
      this._mV[14] = 137296536U;
      this._mV[15] = 3964562569U;
      if (!this._mBNullT)
      {
        uint num1 = (uint) (this._mT & (ulong) uint.MaxValue);
        this._mV[12] ^= num1;
        this._mV[13] ^= num1;
        uint num2 = (uint) (this._mT >> 32 & (ulong) uint.MaxValue);
        this._mV[14] ^= num2;
        this._mV[15] ^= num2;
      }
      for (int r = 0; r < 14; ++r)
      {
        this.G(0, 4, 8, 12, r, 0);
        this.G(1, 5, 9, 13, r, 2);
        this.G(2, 6, 10, 14, r, 4);
        this.G(3, 7, 11, 15, r, 6);
        this.G(3, 4, 9, 14, r, 14);
        this.G(2, 7, 8, 13, r, 12);
        this.G(0, 5, 10, 15, r, 8);
        this.G(1, 6, 11, 12, r, 10);
      }
      for (int index = 0; index < 8; ++index)
        this._mH[index] ^= this._mV[index];
      for (int index = 0; index < 8; ++index)
        this._mH[index] ^= this._mV[index + 8];
      for (int index = 0; index < 4; ++index)
        this._mH[index] ^= this._mS[index];
      for (int index = 0; index < 4; ++index)
        this._mH[index + 4] ^= this._mS[index];
    }

    private void G(int a, int b, int c, int d, int r, int i)
    {
      int index1 = (r << 4) + i;
      int index2 = Blake256._gSigma[index1];
      int index3 = Blake256._gSigma[index1 + 1];
      this._mV[a] += this._mV[b] + (this._mM[index2] ^ Blake256._gCst[index3]);
      this._mV[d] = Blake256.RotateRight(this._mV[d] ^ this._mV[a], 16);
      this._mV[c] += this._mV[d];
      this._mV[b] = Blake256.RotateRight(this._mV[b] ^ this._mV[c], 12);
      this._mV[a] += this._mV[b] + (this._mM[index3] ^ Blake256._gCst[index2]);
      this._mV[d] = Blake256.RotateRight(this._mV[d] ^ this._mV[a], 8);
      this._mV[c] += this._mV[d];
      this._mV[b] = Blake256.RotateRight(this._mV[b] ^ this._mV[c], 7);
    }
  }
}
