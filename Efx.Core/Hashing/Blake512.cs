// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.Blake512
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Efx.Core.Hashing
{
  public sealed class Blake512 : HashAlgorithm
  {
    private static readonly ulong[] _gCst = new ulong[16]{ 2611923443488327891UL, 1376283091369227076UL, 11820040416388919760UL, 589684135938649225UL, 4983270260364809079UL, 13714699805381954668UL, 13883517620612518109UL, 4577018097722394903UL, 10526836309316205339UL, 15073842237943035308UL, 3458046377305235383UL, 13322122606961655446UL, 13437774018240085913UL, 2639559389850201335UL, 577009281997405206UL, 7163292796296056425UL };
    private static readonly byte[] _gPadding = new byte[128]{ (byte) 128, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0 };
    private static readonly int[] _gSigma = new int[256]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3, 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4, 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8, 9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13, 2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9, 12, 5, 1, 15, 14, 13, 4, 10, 0, 7, 6, 3, 9, 2, 8, 11, 13, 11, 7, 14, 12, 1, 3, 9, 5, 0, 15, 4, 8, 6, 2, 10, 6, 15, 14, 9, 11, 3, 0, 8, 12, 2, 13, 7, 1, 4, 10, 5, 10, 2, 8, 4, 7, 6, 1, 5, 15, 11, 9, 14, 3, 12, 13, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3, 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4, 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8, 9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13, 2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9 };
    private readonly byte[] _mBuf = new byte[128];
    private readonly ulong[] _mH = new ulong[8];
    private readonly ulong[] _mM = new ulong[16];
    private readonly ulong[] _mS = new ulong[4];
    private readonly ulong[] _mV = new ulong[16];
    private const int NB_ROUNDS = 16;
    private bool _mBNullT;
    private int _mNBufLen;
    private ulong _mT;

    public Blake512()
    {
      this.HashSizeValue = 512;
      this.Initialize();
    }

    public override void Initialize()
    {
      this._mH[0] = 7640891576956012808UL;
      this._mH[1] = 13503953896175478587UL;
      this._mH[2] = 4354685564936845355UL;
      this._mH[3] = 11912009170470909681UL;
      this._mH[4] = 5840696475078001361UL;
      this._mH[5] = 11170449401992604703UL;
      this._mH[6] = 2270897969802886507UL;
      this._mH[7] = 6620516959819538809UL;
      Array.Clear((Array) this._mS, 0, this._mS.Length);
      this._mT = 0UL;
      this._mNBufLen = 0;
      this._mBNullT = false;
      Array.Clear((Array) this._mBuf, 0, this._mBuf.Length);
    }

    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
      int num = ibStart;
      int length = 128 - this._mNBufLen;
      if (this._mNBufLen > 0 && cbSize >= length)
      {
        Array.Copy((Array) array, num, (Array) this._mBuf, this._mNBufLen, length);
        this._mT += 1024UL;
        this.Compress((IList<byte>) this._mBuf, 0);
        num += length;
        cbSize -= length;
        this._mNBufLen = 0;
      }
      for (; cbSize >= 128; cbSize -= 128)
      {
        this._mT += 1024UL;
        this.Compress((IList<byte>) array, num);
        num += 128;
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
      byte[] array = new byte[16];
      Blake512.UInt64ToBytes(this._mT + ((ulong) this._mNBufLen << 3), (IList<byte>) array, 8);
      if (this._mNBufLen == 111)
      {
        this._mT -= 8UL;
        this.HashCore(new byte[1]{ (byte) 129 }, 0, 1);
      }
      else
      {
        if (this._mNBufLen < 111)
        {
          if (this._mNBufLen == 0)
            this._mBNullT = true;
          this._mT -= (ulong) (888L - ((long) this._mNBufLen << 3));
          this.HashCore(Blake512._gPadding, 0, 111 - this._mNBufLen);
        }
        else
        {
          this._mT -= (ulong) (1024L - ((long) this._mNBufLen << 3));
          this.HashCore(Blake512._gPadding, 0, 128 - this._mNBufLen);
          this._mT -= 888UL;
          this.HashCore(Blake512._gPadding, 1, 111);
          this._mBNullT = true;
        }
        this.HashCore(new byte[1]{ (byte) 1 }, 0, 1);
        this._mT -= 8UL;
      }
      this._mT -= 128UL;
      this.HashCore(array, 0, 16);
      byte[] numArray = new byte[64];
      for (int index = 0; index < 8; ++index)
        Blake512.UInt64ToBytes(this._mH[index], (IList<byte>) numArray, index << 3);
      return numArray;
    }

    private static ulong BytesToUInt64(IList<byte> pb, int iOffset)
    {
      return (ulong) ((long) pb[iOffset + 7] | (long) pb[iOffset + 6] << 8 | (long) pb[iOffset + 5] << 16 | (long) pb[iOffset + 4] << 24 | (long) pb[iOffset + 3] << 32 | (long) pb[iOffset + 2] << 40 | (long) pb[iOffset + 1] << 48 | (long) pb[iOffset] << 56);
    }

    private static ulong RotateRight(ulong u, int nBits)
    {
      return u >> nBits | u << 64 - nBits;
    }

    private static void UInt64ToBytes(ulong u, IList<byte> pbOut, int iOffset)
    {
      for (int index = 7; index >= 0; --index)
      {
        pbOut[iOffset + index] = (byte) (u & (ulong) byte.MaxValue);
        u >>= 8;
      }
    }

    private void Compress(IList<byte> pbBlock, int iOffset)
    {
      for (int index = 0; index < 16; ++index)
        this._mM[index] = Blake512.BytesToUInt64(pbBlock, iOffset + (index << 3));
      Array.Copy((Array) this._mH, (Array) this._mV, 8);
      this._mV[8] = this._mS[0] ^ 2611923443488327891UL;
      this._mV[9] = this._mS[1] ^ 1376283091369227076UL;
      this._mV[10] = this._mS[2] ^ 11820040416388919760UL;
      this._mV[11] = this._mS[3] ^ 589684135938649225UL;
      this._mV[12] = 4983270260364809079UL;
      this._mV[13] = 13714699805381954668UL;
      this._mV[14] = 13883517620612518109UL;
      this._mV[15] = 4577018097722394903UL;
      if (!this._mBNullT)
      {
        this._mV[12] ^= this._mT;
        this._mV[13] ^= this._mT;
      }
      for (int r = 0; r < 16; ++r)
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
      int index2 = Blake512._gSigma[index1];
      int index3 = Blake512._gSigma[index1 + 1];
      this._mV[a] += this._mV[b] + (this._mM[index2] ^ Blake512._gCst[index3]);
      this._mV[d] = Blake512.RotateRight(this._mV[d] ^ this._mV[a], 32);
      this._mV[c] += this._mV[d];
      this._mV[b] = Blake512.RotateRight(this._mV[b] ^ this._mV[c], 25);
      this._mV[a] += this._mV[b] + (this._mM[index3] ^ Blake512._gCst[index2]);
      this._mV[d] = Blake512.RotateRight(this._mV[d] ^ this._mV[a], 16);
      this._mV[c] += this._mV[d];
      this._mV[b] = Blake512.RotateRight(this._mV[b] ^ this._mV[c], 11);
    }
  }
}
