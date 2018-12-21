// Decompiled with JetBrains decompiler
// Type: Efx.Core.Hashing.StringGenerator
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Security.Cryptography;

namespace Efx.Core.Hashing
{
  public abstract class StringGenerator
  {
    private char[][] _charGroups;

    protected string charsLCase { get; set; }

    protected string charsUCase { get; set; }

    protected string charsNumeric { get; set; }

    protected string charsSpecial { get; set; }

    protected string final { get; set; }

    public string Create(int pMinLength, int pMaxLength)
    {
      if (StringGenerator.validateMinMax(pMinLength, pMaxLength))
      {
        this.defineSets();
        this.convertSetOfCharacters();
        this.generateRandomString(pMinLength, pMaxLength);
      }
      return this.returnFinalString();
    }

    private static bool validateMinMax(int pMinLength, int pMaxLength)
    {
      if (pMinLength > 0 && pMaxLength > 0)
        return pMinLength <= pMaxLength;
      return false;
    }

    protected abstract void defineSets();

    private void convertSetOfCharacters()
    {
      this._charGroups = new char[4][]
      {
        this.charsLCase.ToCharArray(),
        this.charsUCase.ToCharArray(),
        this.charsNumeric.ToCharArray(),
        this.charsSpecial.ToCharArray()
      };
    }

    private void generateRandomString(int pMinLength, int pMaxLength)
    {
      int[] numArray1 = new int[this._charGroups.Length];
      for (int index = 0; index < numArray1.Length; ++index)
        numArray1[index] = this._charGroups[index].Length;
      int[] numArray2 = new int[this._charGroups.Length];
      for (int index = 0; index < numArray2.Length; ++index)
        numArray2[index] = index;
      byte[] data = new byte[4];
      new RNGCryptoServiceProvider().GetBytes(data);
      Random random = new Random(((int) data[0] & (int) sbyte.MaxValue) << 24 | (int) data[1] << 16 | (int) data[2] << 8 | (int) data[3]);
      char[] chArray = pMinLength < pMaxLength ? new char[random.Next(pMinLength, pMaxLength + 1)] : new char[pMinLength];
      int maxValue = numArray2.Length - 1;
      for (int index1 = 0; index1 < chArray.Length; ++index1)
      {
        int index2 = maxValue == 0 ? 0 : random.Next(0, maxValue);
        int index3 = numArray2[index2];
        int index4 = numArray1[index3] - 1;
        int index5 = index4 == 0 ? 0 : random.Next(0, index4 + 1);
        chArray[index1] = this._charGroups[index3][index5];
        if (index4 == 0)
        {
          numArray1[index3] = this._charGroups[index3].Length;
        }
        else
        {
          if (index4 != index5)
          {
            char ch = this._charGroups[index3][index4];
            this._charGroups[index3][index4] = this._charGroups[index3][index5];
            this._charGroups[index3][index5] = ch;
          }
          --numArray1[index3];
        }
        if (maxValue == 0)
        {
          maxValue = numArray2.Length - 1;
        }
        else
        {
          if (maxValue != index2)
          {
            int num = numArray2[maxValue];
            numArray2[maxValue] = numArray2[index2];
            numArray2[index2] = num;
          }
          --maxValue;
        }
      }
      this.final = new string(chArray);
    }

    protected abstract string returnFinalString();
  }
}
