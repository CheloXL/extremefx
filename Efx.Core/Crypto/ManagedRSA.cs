// Decompiled with JetBrains decompiler
// Type: Efx.Core.Crypto.ManagedRSA
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace Efx.Core.Crypto
{
  public sealed class ManagedRSA : RSA
  {
    private static readonly byte[] _asn1HashMdx = new byte[18]{ (byte) 48, (byte) 32, (byte) 48, (byte) 12, (byte) 6, (byte) 8, (byte) 42, (byte) 134, (byte) 72, (byte) 134, (byte) 247, (byte) 13, (byte) 2, (byte) 0, (byte) 5, (byte) 0, (byte) 4, (byte) 16 };
    private static readonly byte[] _asn1HashSha1 = new byte[15]{ (byte) 48, (byte) 33, (byte) 48, (byte) 9, (byte) 6, (byte) 5, (byte) 43, (byte) 14, (byte) 3, (byte) 2, (byte) 26, (byte) 5, (byte) 0, (byte) 4, (byte) 20 };
    private static readonly byte[] _asn1HashSha256 = new byte[19]{ (byte) 48, (byte) 49, (byte) 48, (byte) 13, (byte) 6, (byte) 9, (byte) 96, (byte) 134, (byte) 72, (byte) 1, (byte) 101, (byte) 3, (byte) 4, (byte) 2, (byte) 1, (byte) 5, (byte) 0, (byte) 4, (byte) 32 };
    private const int RSA_SIGN = 1;
    private const int RSA_CRYPT = 2;
    private readonly int _keylen;
    private BigInteger _d;
    private BigInteger _dp;
    private BigInteger _dq;
    private BigInteger _e;
    private BigInteger _n;
    private BigInteger _p;
    private BigInteger _q;
    private BigInteger _qp;

    public ManagedRSA(int pKeylen)
    {
      this._keylen = pKeylen / 8;
      if (this._keylen * 8 != pKeylen)
        throw new ArgumentException("keylen must be a multiple of 8");
    }

    public override string KeyExchangeAlgorithm
    {
      get
      {
        return "RSA-PKCS1-KeyEx";
      }
    }

    public override string SignatureAlgorithm
    {
      get
      {
        return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
      }
    }

    public override int KeySize
    {
      get
      {
        return this._keylen * 8;
      }
      set
      {
        throw new ArgumentException("please do this in the constructor!");
      }
    }

    public override void FromXmlString(string pXmlString)
    {
      XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(pXmlString));
      this._n = this._e = this._p = this._q = this._dp = this._dq = this._qp = this._d = (BigInteger) null;
      while (true)
      {
        string name;
        do
        {
          switch (xmlTextReader.MoveToContent())
          {
            case XmlNodeType.None:
              goto label_5;
            case XmlNodeType.Element:
              name = xmlTextReader.Name;
              continue;
            case XmlNodeType.EndElement:
              goto label_3;
            default:
              goto label_6;
          }
        }
        while (this.checkXmlElement((XmlReader) xmlTextReader, name, "Modulus", ref this._n) || this.checkXmlElement((XmlReader) xmlTextReader, name, "Exponent", ref this._e) || (this.checkXmlElement((XmlReader) xmlTextReader, name, "P", ref this._p) || this.checkXmlElement((XmlReader) xmlTextReader, name, "Q", ref this._q)) || (this.checkXmlElement((XmlReader) xmlTextReader, name, "DP", ref this._dp) || this.checkXmlElement((XmlReader) xmlTextReader, name, "DQ", ref this._dq) || (this.checkXmlElement((XmlReader) xmlTextReader, name, "InverseQ", ref this._qp) || this.checkXmlElement((XmlReader) xmlTextReader, name, "D", ref this._d))));
        xmlTextReader.ReadString();
        continue;
label_3:
        xmlTextReader.ReadEndElement();
      }
label_5:
      return;
label_6:
      throw new ArgumentException("something unexpected in xmlString");
    }

    public override string ToXmlString(bool pIncludePrivateParameters)
    {
      string str = "<RSAKeyValue>\n    <Modulus>" + this.bigintToB64(this._n) + "</Modulus>\n    <Exponent>" + this.bigintToB64(this._e) + "</Exponent>\n";
      if (pIncludePrivateParameters)
        str = str + "    <P>" + this.bigintToB64(this._p) + "</P>\n    <Q>" + this.bigintToB64(this._q) + "</Q>\n    <DP>" + this.bigintToB64(this._dp) + "</DP>\n    <DQ>" + this.bigintToB64(this._dq) + "</DQ>\n    <InverseQ>" + this.bigintToB64(this._qp) + "</InverseQ>\n    <D>" + this.bigintToB64(this._d) + "</D>\n";
      return str + "</RSAKeyValue>\n";
    }

    public override void ImportParameters(RSAParameters pParameters)
    {
      this._n = new BigInteger((IList<byte>) pParameters.Modulus);
      this._e = new BigInteger((IList<byte>) pParameters.Exponent);
      this._p = !object.ReferenceEquals((object) pParameters.P, (object) null) ? new BigInteger((IList<byte>) pParameters.P) : (BigInteger) null;
      this._q = !object.ReferenceEquals((object) pParameters.Q, (object) null) ? new BigInteger((IList<byte>) pParameters.Q) : (BigInteger) null;
      this._dp = !object.ReferenceEquals((object) pParameters.DP, (object) null) ? new BigInteger((IList<byte>) pParameters.DP) : (BigInteger) null;
      this._dq = !object.ReferenceEquals((object) pParameters.DQ, (object) null) ? new BigInteger((IList<byte>) pParameters.DQ) : (BigInteger) null;
      this._qp = !object.ReferenceEquals((object) pParameters.InverseQ, (object) null) ? new BigInteger((IList<byte>) pParameters.InverseQ) : (BigInteger) null;
      this._d = !object.ReferenceEquals((object) pParameters.D, (object) null) ? new BigInteger((IList<byte>) pParameters.D) : (BigInteger) null;
    }

    public override RSAParameters ExportParameters(bool pIncludePrivateParameters)
    {
      RSAParameters rsaParameters = new RSAParameters();
      rsaParameters.Modulus = this._n.GetBytes();
      rsaParameters.Exponent = this._e.GetBytes();
      if (pIncludePrivateParameters)
      {
        rsaParameters.P = this._p.GetBytes();
        rsaParameters.Q = this._q.GetBytes();
        rsaParameters.DP = this._dp.GetBytes();
        rsaParameters.DQ = this._dq.GetBytes();
        rsaParameters.InverseQ = this._qp.GetBytes();
        rsaParameters.D = this._d.GetBytes();
      }
      return rsaParameters;
    }

    public void GenerateKeyPair(int pExponent)
    {
      byte[] pInput;
      byte[] numArray1;
      do
      {
        BigInteger bigInteger1 = (BigInteger) pExponent;
        Random pRand = new Random();
        int pBits = this._keylen * 8 / 2;
        BigInteger pN1 = (BigInteger) null;
        BigInteger pN2 = (BigInteger) null;
        BigInteger pModulus1 = (BigInteger) null;
        BigInteger bigInteger2 = (BigInteger) null;
        BigInteger pModulus2;
        BigInteger bigInteger3;
        do
        {
          pModulus2 = BigInteger.GenPseudoPrime(pBits, 20, pRand);
          bigInteger3 = BigInteger.GenPseudoPrime(pBits, 20, pRand);
          if (!(pModulus2 == bigInteger3))
            goto label_2;
label_1:
          continue;
label_2:
          if (pModulus2 < bigInteger3)
          {
            BigInteger bigInteger4 = pModulus2;
            pModulus2 = bigInteger3;
            bigInteger3 = bigInteger4;
          }
          pN1 = pModulus2 - (BigInteger) 1;
          pN2 = bigInteger3 - (BigInteger) 1;
          pModulus1 = pN1 * pN2;
          bigInteger2 = pModulus1.Gcd(bigInteger1);
          goto label_1;
        }
        while (bigInteger2 != (BigInteger) 1);
        this._n = pModulus2 * bigInteger3;
        this._e = bigInteger1;
        this._p = pModulus2;
        this._q = bigInteger3;
        this._d = bigInteger1.ModInverse(pModulus1);
        this._dp = this._d.ModPow((BigInteger) 1, pN1);
        this._dq = this._d.ModPow((BigInteger) 1, pN2);
        this._qp = bigInteger3.ModInverse(pModulus2);
        byte[] numArray2 = new byte[this._keylen - 1];
        new RNGCryptoServiceProvider().GetBytes(numArray2);
        pInput = this.padBytes(numArray2, this._keylen);
        numArray1 = this.DoPrivate(this.DoPublic(pInput));
      }
      while (!this.compareBytes((IList<byte>) pInput, 0, (IList<byte>) numArray1, 0, this._keylen));
    }

    public byte[] DoPublic(byte[] pInput)
    {
      if (pInput.Length != this._keylen)
        throw new ArgumentException("input.Length does not match keylen");
      if (object.ReferenceEquals((object) this._n, (object) null))
        throw new ArgumentException("no key set!");
      BigInteger bigInteger = new BigInteger((IList<byte>) pInput);
      if (bigInteger >= this._n)
        throw new ArgumentException("input exceeds modulus");
      return this.padBytes(bigInteger.ModPow(this._e, this._n).GetBytes(), this._keylen);
    }

    public byte[] DoPrivate(byte[] pInput)
    {
      if (pInput.Length != this._keylen)
        throw new ArgumentException("input.Length does not match keylen");
      if (object.ReferenceEquals((object) this._d, (object) null))
        throw new ArgumentException("no private key set!");
      BigInteger bigInteger = new BigInteger((IList<byte>) pInput);
      if (bigInteger >= this._n)
        throw new ArgumentException("input exceeds modulus");
      return this.padBytes(bigInteger.ModPow(this._d, this._n).GetBytes(), this._keylen);
    }

    public bool CheckPrivateKey()
    {
      if (this._p * this._q != this._n)
        return false;
      return this._e.Gcd((this._p - (BigInteger) 1) * (this._q - (BigInteger) 1)) == (BigInteger) 1;
    }

    public byte[] Encrypt(byte[] pInput, bool pFOaep)
    {
      if (pFOaep)
        throw new ArgumentException("OAEP padding not supported, sorry");
      int length1 = pInput.Length;
      int length2 = this._keylen - 3 - length1;
      if (length2 < 8)
        throw new ArgumentException("input too long");
      byte[] pInput1 = new byte[this._keylen];
      pInput1[0] = (byte) 0;
      pInput1[1] = (byte) 2;
      byte[] data = new byte[length2];
      new RNGCryptoServiceProvider().GetBytes(data);
      for (int index = 0; index < length2; ++index)
      {
        if (data[index] == (byte) 0)
          data[index] = (byte) index;
        if ((byte) index == (byte) 0)
          data[index] = (byte) 1;
      }
      Array.Copy((Array) data, 0, (Array) pInput1, 2, length2);
      pInput1[length2 + 2] = (byte) 0;
      Array.Copy((Array) pInput, 0, (Array) pInput1, length2 + 3, length1);
      return this.DoPublic(pInput1);
    }

    public byte[] Decrypt(byte[] pInput, bool pFOaep)
    {
      if (pFOaep)
        throw new ArgumentException("OAEP padding not supported, sorry");
      byte[] numArray1 = this.DoPrivate(pInput);
      if (numArray1[0] == (byte) 0 && numArray1[1] == (byte) 2)
      {
        int length1 = numArray1.Length;
        for (int index = 2; index < length1 - 1; ++index)
        {
          if (numArray1[index] == (byte) 0)
          {
            int sourceIndex = index + 1;
            int length2 = length1 - sourceIndex;
            byte[] numArray2 = new byte[length2];
            Array.Copy((Array) numArray1, sourceIndex, (Array) numArray2, 0, length2);
            return numArray2;
          }
        }
        throw new ArgumentException("invalid padding");
      }
      throw new ArgumentException("invalid signature bytes");
    }

    public ManagedRSA.HashAlgorith MapHashAlgorithmOid(string pHashAlgorithmOid)
    {
      if (string.Compare(pHashAlgorithmOid, CryptoConfig.MapNameToOID("MD5"), true) == 0)
        return ManagedRSA.HashAlgorith.Md5;
      if (string.Compare(pHashAlgorithmOid, CryptoConfig.MapNameToOID("SHA1"), true) == 0)
        return ManagedRSA.HashAlgorith.Sha1;
      if (string.Compare(pHashAlgorithmOid, CryptoConfig.MapNameToOID("SHA256"), true) == 0)
        return ManagedRSA.HashAlgorith.Sha256;
      throw new ArgumentException("unknown hash_algorithm_oid");
    }

    public byte[] SignHash(byte[] pSignMe, string pHashAlgorithmOid)
    {
      ManagedRSA.HashAlgorith pHashAlgorithm = this.MapHashAlgorithmOid(pHashAlgorithmOid);
      return this.SignHash(pSignMe, pHashAlgorithm);
    }

    public byte[] SignHash(byte[] pSignMe, ManagedRSA.HashAlgorith pHashAlgorithm)
    {
      int length = pSignMe.Length;
      int num = 0;
      switch (pHashAlgorithm)
      {
        case ManagedRSA.HashAlgorith.Raw:
          num = this._keylen - 3 - length;
          break;
        case ManagedRSA.HashAlgorith.Sha1:
          if (length != 20)
            throw new ArgumentException("SHA1 hashes must be 20 bytes long");
          num = this._keylen - 3 - 20 - 15;
          break;
        case ManagedRSA.HashAlgorith.Md2:
        case ManagedRSA.HashAlgorith.Md4:
        case ManagedRSA.HashAlgorith.Md5:
          if (length != 16)
            throw new ArgumentException("MDx hashes must be 16 bytes long");
          num = this._keylen - 3 - 16 - 18;
          break;
        case ManagedRSA.HashAlgorith.Sha256:
          if (length != 32)
            throw new ArgumentException("SHA256 hashes must be 32 bytes long");
          num = this._keylen - 3 - 32 - 19;
          break;
      }
      if (num < 8)
        throw new ArgumentException("input too long");
      byte[] pInput = new byte[this._keylen];
      pInput[0] = (byte) 0;
      pInput[1] = (byte) 1;
      for (int index = 0; index < num; ++index)
        pInput[index + 2] = byte.MaxValue;
      pInput[num + 2] = (byte) 0;
      switch (pHashAlgorithm)
      {
        case ManagedRSA.HashAlgorith.Raw:
          Array.Copy((Array) pSignMe, 0, (Array) pInput, num + 3, length);
          break;
        case ManagedRSA.HashAlgorith.Sha1:
          Array.Copy((Array) ManagedRSA._asn1HashSha1, 0, (Array) pInput, num + 3, 15);
          Array.Copy((Array) pSignMe, 0, (Array) pInput, num + 3 + 15, length);
          break;
        case ManagedRSA.HashAlgorith.Md2:
        case ManagedRSA.HashAlgorith.Md4:
        case ManagedRSA.HashAlgorith.Md5:
          Array.Copy((Array) ManagedRSA._asn1HashMdx, 0, (Array) pInput, num + 3, 18);
          pInput[num + 3 + 13] = (byte) pHashAlgorithm;
          Array.Copy((Array) pSignMe, 0, (Array) pInput, num + 3 + 18, length);
          break;
        case ManagedRSA.HashAlgorith.Sha256:
          Array.Copy((Array) ManagedRSA._asn1HashSha256, 0, (Array) pInput, num + 3, 19);
          Array.Copy((Array) pSignMe, 0, (Array) pInput, num + 3 + 19, length);
          break;
      }
      return this.DoPrivate(pInput);
    }

    public bool VerifyHash(byte[] pHash, string pHashAlgorithmOid, byte[] pSignature)
    {
      ManagedRSA.HashAlgorith pHashAlgorithm = this.MapHashAlgorithmOid(pHashAlgorithmOid);
      return this.VerifyHash(pHash, pSignature, pHashAlgorithm);
    }

    public bool VerifyHash(byte[] pHash, byte[] pSignature, ManagedRSA.HashAlgorith pHashAlgorithm)
    {
      if (pSignature.Length != this._keylen)
        return false;
      byte[] numArray = this.DoPublic(pSignature);
      if (numArray[0] != (byte) 0 || numArray[1] != (byte) 1)
        return false;
      int length = numArray.Length;
      for (int index = 2; index < length - 1; ++index)
      {
        switch (numArray[index])
        {
          case 0:
            int pI1 = index + 1;
            int pN = length - pI1;
            switch (pN)
            {
              case 34:
                if ((int) numArray[pI1 + 13] != (int) (byte) pHashAlgorithm)
                  return false;
                numArray[pI1 + 13] = (byte) 0;
                if (this.compareBytes((IList<byte>) numArray, pI1, (IList<byte>) ManagedRSA._asn1HashMdx, 0, 18))
                  return this.compareBytes((IList<byte>) numArray, pI1 + 18, (IList<byte>) pHash, 0, 16);
                return false;
              case 35:
                if (pHashAlgorithm == ManagedRSA.HashAlgorith.Sha1)
                {
                  if (this.compareBytes((IList<byte>) numArray, pI1, (IList<byte>) ManagedRSA._asn1HashSha1, 0, 15))
                    return this.compareBytes((IList<byte>) numArray, pI1 + 15, (IList<byte>) pHash, 0, 20);
                  return false;
                }
                break;
            }
            if (pN == pHash.Length && pHashAlgorithm == ManagedRSA.HashAlgorith.Raw)
              return this.compareBytes((IList<byte>) numArray, pI1, (IList<byte>) pHash, 0, pN);
            return false;
          case byte.MaxValue:
            continue;
          default:
            goto label_20;
        }
      }
label_20:
      return false;
    }

    public byte[] SignData(byte[] pData, HashAlgorithm pHasher)
    {
      ManagedRSA.HashAlgorith pHashAlgorithm = this.mapHashAlgorithm((IDisposable) pHasher);
      return this.SignHash(pHasher.ComputeHash(pData), pHashAlgorithm);
    }

    public bool VerifyData(byte[] pData, HashAlgorithm pHasher, byte[] pSignature)
    {
      ManagedRSA.HashAlgorith pHashAlgorithm = this.mapHashAlgorithm((IDisposable) pHasher);
      return this.VerifyHash(pHasher.ComputeHash(pData), pSignature, pHashAlgorithm);
    }

    public override byte[] EncryptValue(byte[] pRgb)
    {
      return this.DoPublic(pRgb);
    }

    public override byte[] DecryptValue(byte[] pRgb)
    {
      return this.DoPrivate(pRgb);
    }

    protected override void Dispose(bool pDisposing)
    {
      this._n = this._e = this._p = this._q = this._dp = this._dq = this._qp = this._d = (BigInteger) null;
    }

    private bool compareBytes(IList<byte> pB1, int pI1, IList<byte> pB2, int pI2, int pN)
    {
      for (int index = 0; index < pN; ++index)
      {
        if ((int) pB1[index + pI1] != (int) pB2[index + pI2])
          return false;
      }
      return true;
    }

    private byte[] padBytes(byte[] pB, int pN)
    {
      int length = pB.Length;
      if (length >= pN)
        return pB;
      byte[] numArray = new byte[pN];
      int destinationIndex = pN - length;
      for (int index = 0; index < destinationIndex; ++index)
        numArray[index] = (byte) 0;
      Array.Copy((Array) pB, 0, (Array) numArray, destinationIndex, length);
      return numArray;
    }

    private string bigintToB64(BigInteger pBi)
    {
      return Convert.ToBase64String(pBi.GetBytes());
    }

    private bool checkXmlElement(
      XmlReader pReader,
      string pElementName,
      string pElementNameRequired,
      ref BigInteger pBiOut)
    {
      if (string.Compare(pElementName, pElementNameRequired, true) != 0)
        return false;
      BigInteger bigInteger = new BigInteger((IList<byte>) Convert.FromBase64String(pReader.ReadString()));
      pBiOut = bigInteger;
      return true;
    }

    private ManagedRSA.HashAlgorith mapHashAlgorithm(IDisposable pHasher)
    {
      Type type = pHasher.GetType();
      if (object.ReferenceEquals((object) type, (object) typeof (MD5CryptoServiceProvider)))
        return ManagedRSA.HashAlgorith.Md5;
      if (object.ReferenceEquals((object) type, (object) typeof (SHA1CryptoServiceProvider)))
        return ManagedRSA.HashAlgorith.Sha1;
      if (!object.ReferenceEquals((object) type, (object) typeof (SHA256Managed)))
        throw new ArgumentException("unknown HashAlgorithm");
      return ManagedRSA.HashAlgorith.Sha256;
    }

    public enum HashAlgorith
    {
      Raw = 0,
      Sha1 = 1,
      Md2 = 2,
      Md4 = 4,
      Md5 = 5,
      Sha256 = 6,
    }
  }
}
