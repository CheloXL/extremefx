// Decompiled with JetBrains decompiler
// Type: Efx.Core.Crypto.DiffieHellman
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Text;

namespace Efx.Core.Crypto
{
  public class DiffieHellman : IDisposable
  {
    private readonly int _bits;
    private BigInteger _g;
    private byte[] _key;
    private BigInteger _mine;
    private BigInteger _prime;

    public DiffieHellman()
      : this(256)
    {
    }

    public DiffieHellman(int pBits)
    {
      this._bits = pBits;
    }

    public byte[] Key
    {
      get
      {
        return this._key;
      }
    }

    public void Dispose()
    {
      this._prime = (BigInteger) null;
      this._mine = (BigInteger) null;
      this._g = (BigInteger) null;
      GC.Collect();
    }

    ~DiffieHellman()
    {
      this.Dispose();
    }

    public string GenerateRequest()
    {
      this._prime = BigInteger.GenPseudoPrime(this._bits, 30, new Random());
      this._mine = BigInteger.GenPseudoPrime(this._bits, 30, new Random());
      this._g = BigInteger.GenPseudoPrime(this._bits, 30, new Random());
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this._prime.ToString(36));
      stringBuilder.Append("|");
      stringBuilder.Append(this._g.ToString(36));
      stringBuilder.Append("|");
      BigInteger bigInteger = this._g.ModPow(this._mine, this._prime);
      stringBuilder.Append(bigInteger.ToString(36));
      return stringBuilder.ToString();
    }

    public string GenerateResponse(string pRequest)
    {
      string[] strArray = pRequest.Split('|');
      BigInteger pN = new BigInteger(strArray[0], 36);
      BigInteger bigInteger = new BigInteger(strArray[1], 36);
      BigInteger pExp = BigInteger.GenPseudoPrime(this._bits, 30, new Random());
      this._key = new BigInteger(strArray[2], 36).ModPow(pExp, pN).GetBytes();
      return bigInteger.ModPow(pExp, pN).ToString(36);
    }

    public void HandleResponse(string pResponse)
    {
      this._key = new BigInteger(pResponse, 36).ModPow(this._mine, this._prime).GetBytes();
      this.Dispose();
    }
  }
}
