// Decompiled with JetBrains decompiler
// Type: Efx.Core.Crypto.BigInteger
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;

namespace Efx.Core.Crypto
{
  public sealed class BigInteger
  {
    private static readonly int[] _primesBelow2000 = new int[303]{ 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, (int) sbyte.MaxValue, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997, 1999 };
    private const int MAX_LENGTH = 70;
    private readonly uint[] _data;
    private int _dataLength;

    public BigInteger()
    {
      this._data = new uint[70];
      this._dataLength = 1;
    }

    public BigInteger(long value)
    {
      this._data = new uint[70];
      long num = value;
      for (this._dataLength = 0; value != 0L && this._dataLength < 70; ++this._dataLength)
      {
        this._data[this._dataLength] = (uint) (value & (long) uint.MaxValue);
        value >>= 32;
      }
      if (num > 0L)
      {
        if (value != 0L || ((int) this._data[69] & int.MinValue) != 0)
          throw new ArithmeticException("Positive overflow in constructor.");
      }
      else if (num < 0L && (value != -1L || ((int) this._data[this._dataLength - 1] & int.MinValue) == 0))
        throw new ArithmeticException("Negative underflow in constructor.");
      if (this._dataLength != 0)
        return;
      this._dataLength = 1;
    }

    public BigInteger(ulong value)
    {
      this._data = new uint[70];
      for (this._dataLength = 0; value != 0UL && this._dataLength < 70; ++this._dataLength)
      {
        this._data[this._dataLength] = (uint) (value & (ulong) uint.MaxValue);
        value >>= 32;
      }
      if (value != 0UL || ((int) this._data[69] & int.MinValue) != 0)
        throw new ArithmeticException("Positive overflow in constructor.");
      if (this._dataLength != 0)
        return;
      this._dataLength = 1;
    }

    public BigInteger(BigInteger bigInteger)
    {
      this._data = new uint[70];
      this._dataLength = bigInteger._dataLength;
      for (int index = 0; index < this._dataLength; ++index)
        this._data[index] = bigInteger._data[index];
    }

    public BigInteger(string value, int radix)
    {
      BigInteger bigInteger1 = new BigInteger(1L);
      BigInteger bigInteger2 = new BigInteger();
      value = value.ToUpper().Trim();
      int num1 = 0;
      if (value[0] == '-')
        num1 = 1;
      for (int index = value.Length - 1; index >= num1; --index)
      {
        int num2 = (int) value[index];
        int num3 = num2 < 48 || num2 > 57 ? (num2 < 65 || num2 > 90 ? 9999999 : num2 - 65 + 10) : num2 - 48;
        if (num3 >= radix)
          throw new ArithmeticException("Invalid string in constructor.");
        if (value[0] == '-')
          num3 = -num3;
        bigInteger2 += bigInteger1 * (BigInteger) num3;
        if (index - 1 >= num1)
          bigInteger1 *= (BigInteger) radix;
      }
      if (value[0] == '-')
      {
        if (((int) bigInteger2._data[69] & int.MinValue) == 0)
          throw new ArithmeticException("Negative underflow in constructor.");
      }
      else if (((int) bigInteger2._data[69] & int.MinValue) != 0)
        throw new ArithmeticException("Positive overflow in constructor.");
      this._data = new uint[70];
      for (int index = 0; index < bigInteger2._dataLength; ++index)
        this._data[index] = bigInteger2._data[index];
      this._dataLength = bigInteger2._dataLength;
    }

    public BigInteger(IList<byte> inData)
    {
      this._dataLength = inData.Count >> 2;
      int num = inData.Count & 3;
      if (num != 0)
        ++this._dataLength;
      if (this._dataLength > 70)
        throw new ArithmeticException("Byte overflow in constructor.");
      this._data = new uint[70];
      int index1 = inData.Count - 1;
      int index2 = 0;
      while (index1 >= 3)
      {
        this._data[index2] = (uint) (((int) inData[index1 - 3] << 24) + ((int) inData[index1 - 2] << 16) + ((int) inData[index1 - 1] << 8)) + (uint) inData[index1];
        index1 -= 4;
        ++index2;
      }
      switch (num)
      {
        case 1:
          this._data[this._dataLength - 1] = (uint) inData[0];
          break;
        case 2:
          this._data[this._dataLength - 1] = ((uint) inData[0] << 8) + (uint) inData[1];
          break;
        case 3:
          this._data[this._dataLength - 1] = (uint) (((int) inData[0] << 16) + ((int) inData[1] << 8)) + (uint) inData[2];
          break;
      }
      while (this._dataLength > 1 && this._data[this._dataLength - 1] == 0U)
        --this._dataLength;
    }

    public BigInteger(IList<byte> inData, int pInLen)
    {
      this._dataLength = pInLen >> 2;
      int num = pInLen & 3;
      if (num != 0)
        ++this._dataLength;
      if (this._dataLength > 70 || pInLen > inData.Count)
        throw new ArithmeticException("Byte overflow in constructor.");
      this._data = new uint[70];
      int index1 = pInLen - 1;
      int index2 = 0;
      while (index1 >= 3)
      {
        this._data[index2] = (uint) (((int) inData[index1 - 3] << 24) + ((int) inData[index1 - 2] << 16) + ((int) inData[index1 - 1] << 8)) + (uint) inData[index1];
        index1 -= 4;
        ++index2;
      }
      switch (num)
      {
        case 1:
          this._data[this._dataLength - 1] = (uint) inData[0];
          break;
        case 2:
          this._data[this._dataLength - 1] = ((uint) inData[0] << 8) + (uint) inData[1];
          break;
        case 3:
          this._data[this._dataLength - 1] = (uint) (((int) inData[0] << 16) + ((int) inData[1] << 8)) + (uint) inData[2];
          break;
      }
      if (this._dataLength == 0)
        this._dataLength = 1;
      while (this._dataLength > 1 && this._data[this._dataLength - 1] == 0U)
        --this._dataLength;
    }

    public BigInteger(IList<uint> inData)
    {
      this._dataLength = inData.Count;
      if (this._dataLength > 70)
        throw new ArithmeticException("Byte overflow in constructor.");
      this._data = new uint[70];
      int index1 = this._dataLength - 1;
      int index2 = 0;
      while (index1 >= 0)
      {
        this._data[index2] = inData[index1];
        --index1;
        ++index2;
      }
      while (this._dataLength > 1 && this._data[this._dataLength - 1] == 0U)
        --this._dataLength;
    }

    public static implicit operator BigInteger(long value)
    {
      return new BigInteger(value);
    }

    public static implicit operator BigInteger(ulong value)
    {
      return new BigInteger(value);
    }

    public static implicit operator BigInteger(int value)
    {
      return new BigInteger((long) value);
    }

    public static implicit operator BigInteger(uint value)
    {
      return new BigInteger((ulong) value);
    }

    public static BigInteger operator +(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger bigInteger = new BigInteger();
      bigInteger._dataLength = pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength;
      long num1 = 0;
      for (int index = 0; index < bigInteger._dataLength; ++index)
      {
        long num2 = (long) pBi1._data[index] + (long) pBi2._data[index] + num1;
        num1 = num2 >> 32;
        bigInteger._data[index] = (uint) (num2 & (long) uint.MaxValue);
      }
      if (num1 != 0L && bigInteger._dataLength < 70)
      {
        bigInteger._data[bigInteger._dataLength] = (uint) num1;
        ++bigInteger._dataLength;
      }
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      if (((int) pBi1._data[69] & int.MinValue) == ((int) pBi2._data[69] & int.MinValue) && ((int) bigInteger._data[69] & int.MinValue) != ((int) pBi1._data[69] & int.MinValue))
        throw new ArithmeticException();
      return bigInteger;
    }

    public static BigInteger operator ++(BigInteger pBi1)
    {
      BigInteger bigInteger = new BigInteger(pBi1);
      long num1 = 1;
      int index;
      for (index = 0; num1 != 0L && index < 70; ++index)
      {
        long num2 = (long) bigInteger._data[index] + 1L;
        bigInteger._data[index] = (uint) (num2 & (long) uint.MaxValue);
        num1 = num2 >> 32;
      }
      if (index > bigInteger._dataLength)
      {
        bigInteger._dataLength = index;
      }
      else
      {
        while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
          --bigInteger._dataLength;
      }
      if (((int) pBi1._data[69] & int.MinValue) == 0 && ((int) bigInteger._data[69] & int.MinValue) != ((int) pBi1._data[69] & int.MinValue))
        throw new ArithmeticException("Overflow in ++.");
      return bigInteger;
    }

    public static BigInteger operator -(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger bigInteger = new BigInteger();
      bigInteger._dataLength = pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength;
      long num1 = 0;
      for (int index = 0; index < bigInteger._dataLength; ++index)
      {
        long num2 = (long) pBi1._data[index] - (long) pBi2._data[index] - num1;
        bigInteger._data[index] = (uint) (num2 & (long) uint.MaxValue);
        num1 = num2 < 0L ? 1L : 0L;
      }
      if (num1 != 0L)
      {
        for (int dataLength = bigInteger._dataLength; dataLength < 70; ++dataLength)
          bigInteger._data[dataLength] = uint.MaxValue;
        bigInteger._dataLength = 70;
      }
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      if (((int) pBi1._data[69] & int.MinValue) != ((int) pBi2._data[69] & int.MinValue) && ((int) bigInteger._data[69] & int.MinValue) != ((int) pBi1._data[69] & int.MinValue))
        throw new ArithmeticException();
      return bigInteger;
    }

    public static BigInteger operator --(BigInteger pBi1)
    {
      BigInteger bigInteger = new BigInteger(pBi1);
      bool flag = true;
      int index;
      for (index = 0; flag && index < 70; ++index)
      {
        long num = (long) bigInteger._data[index] - 1L;
        bigInteger._data[index] = (uint) (num & (long) uint.MaxValue);
        if (num >= 0L)
          flag = false;
      }
      if (index > bigInteger._dataLength)
        bigInteger._dataLength = index;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      if (((int) pBi1._data[69] & int.MinValue) != 0 && ((int) bigInteger._data[69] & int.MinValue) != ((int) pBi1._data[69] & int.MinValue))
        throw new ArithmeticException("Underflow in --.");
      return bigInteger;
    }

    public static BigInteger operator *(BigInteger pBi1, BigInteger pBi2)
    {
      bool flag1 = false;
      bool flag2 = false;
      try
      {
        if (((int) pBi1._data[69] & int.MinValue) != 0)
        {
          flag1 = true;
          pBi1 = -pBi1;
        }
        if (((int) pBi2._data[69] & int.MinValue) != 0)
        {
          flag2 = true;
          pBi2 = -pBi2;
        }
      }
      catch
      {
      }
      BigInteger bigInteger = new BigInteger();
      try
      {
        for (int index1 = 0; index1 < pBi1._dataLength; ++index1)
        {
          if (pBi1._data[index1] != 0U)
          {
            ulong num1 = 0;
            int index2 = 0;
            int index3 = index1;
            while (index2 < pBi2._dataLength)
            {
              ulong num2 = (ulong) pBi1._data[index1] * (ulong) pBi2._data[index2] + (ulong) bigInteger._data[index3] + num1;
              bigInteger._data[index3] = (uint) (num2 & (ulong) uint.MaxValue);
              num1 = num2 >> 32;
              ++index2;
              ++index3;
            }
            if (num1 != 0UL)
              bigInteger._data[index1 + pBi2._dataLength] = (uint) num1;
          }
        }
      }
      catch (Exception ex)
      {
        throw new ArithmeticException("Multiplication overflow.");
      }
      bigInteger._dataLength = pBi1._dataLength + pBi2._dataLength;
      if (bigInteger._dataLength > 70)
        bigInteger._dataLength = 70;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      if (((int) bigInteger._data[69] & int.MinValue) != 0)
      {
        if (flag1 != flag2 && bigInteger._data[69] == 2147483648U)
        {
          if (bigInteger._dataLength == 1)
            return bigInteger;
          bool flag3 = true;
          for (int index = 0; index < bigInteger._dataLength - 1 && flag3; ++index)
          {
            if (bigInteger._data[index] != 0U)
              flag3 = false;
          }
          if (flag3)
            return bigInteger;
        }
        throw new ArithmeticException("Multiplication overflow.");
      }
      if (flag1 != flag2)
        return -bigInteger;
      return bigInteger;
    }

    public static BigInteger operator <<(BigInteger pBi1, int pShiftVal)
    {
      BigInteger bigInteger = new BigInteger(pBi1);
      bigInteger._dataLength = BigInteger.shiftLeft((IList<uint>) bigInteger._data, pShiftVal);
      return bigInteger;
    }

    private static int shiftLeft(IList<uint> pBuffer, int pShiftVal)
    {
      int num1 = 32;
      int count = pBuffer.Count;
      while (count > 1 && pBuffer[count - 1] == 0U)
        --count;
      for (int index1 = pShiftVal; index1 > 0; index1 -= num1)
      {
        if (index1 < num1)
          num1 = index1;
        ulong num2 = 0;
        for (int index2 = 0; index2 < count; ++index2)
        {
          ulong num3 = (ulong) pBuffer[index2] << num1 | num2;
          pBuffer[index2] = (uint) (num3 & (ulong) uint.MaxValue);
          num2 = num3 >> 32;
        }
        if (num2 != 0UL && count + 1 <= pBuffer.Count)
        {
          pBuffer[count] = (uint) num2;
          ++count;
        }
      }
      return count;
    }

    public static BigInteger operator >>(BigInteger pBi1, int pShiftVal)
    {
      BigInteger bigInteger = new BigInteger(pBi1);
      bigInteger._dataLength = BigInteger.shiftRight((IList<uint>) bigInteger._data, pShiftVal);
      if (((int) pBi1._data[69] & int.MinValue) != 0)
      {
        for (int index = 69; index >= bigInteger._dataLength; --index)
          bigInteger._data[index] = uint.MaxValue;
        uint num = 2147483648;
        for (int index = 0; index < 32 && ((int) bigInteger._data[bigInteger._dataLength - 1] & (int) num) == 0; ++index)
        {
          bigInteger._data[bigInteger._dataLength - 1] |= num;
          num >>= 1;
        }
        bigInteger._dataLength = 70;
      }
      return bigInteger;
    }

    private static int shiftRight(IList<uint> pBuffer, int pShiftVal)
    {
      int num1 = 32;
      int num2 = 0;
      int count = pBuffer.Count;
      while (count > 1 && pBuffer[count - 1] == 0U)
        --count;
      for (int index1 = pShiftVal; index1 > 0; index1 -= num1)
      {
        if (index1 < num1)
        {
          num1 = index1;
          num2 = 32 - num1;
        }
        ulong num3 = 0;
        for (int index2 = count - 1; index2 >= 0; --index2)
        {
          ulong num4 = (ulong) pBuffer[index2] >> num1 | num3;
          num3 = (ulong) pBuffer[index2] << num2;
          pBuffer[index2] = (uint) num4;
        }
      }
      while (count > 1 && pBuffer[count - 1] == 0U)
        --count;
      return count;
    }

    public static BigInteger operator ~(BigInteger pBi1)
    {
      BigInteger bigInteger = new BigInteger(pBi1);
      for (int index = 0; index < 70; ++index)
        bigInteger._data[index] = ~pBi1._data[index];
      bigInteger._dataLength = 70;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      return bigInteger;
    }

    public static BigInteger operator -(BigInteger pBi1)
    {
      if (pBi1._dataLength == 1 && pBi1._data[0] == 0U)
        return new BigInteger();
      BigInteger bigInteger = new BigInteger(pBi1);
      for (int index = 0; index < 70; ++index)
        bigInteger._data[index] = ~pBi1._data[index];
      long num1 = 1;
      for (int index = 0; num1 != 0L && index < 70; ++index)
      {
        long num2 = (long) bigInteger._data[index] + 1L;
        bigInteger._data[index] = (uint) (num2 & (long) uint.MaxValue);
        num1 = num2 >> 32;
      }
      if (((int) pBi1._data[69] & int.MinValue) == ((int) bigInteger._data[69] & int.MinValue))
        throw new ArithmeticException("Overflow in negation.\n");
      bigInteger._dataLength = 70;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      return bigInteger;
    }

    public static bool operator ==(BigInteger pBi1, BigInteger pBi2)
    {
      return pBi1.Equals((object) pBi2);
    }

    public static bool operator !=(BigInteger pBi1, BigInteger pBi2)
    {
      return !pBi1.Equals((object) pBi2);
    }

    public override bool Equals(object o)
    {
      BigInteger bigInteger = (BigInteger) o;
      if (this._dataLength != bigInteger._dataLength)
        return false;
      for (int index = 0; index < this._dataLength; ++index)
      {
        if ((int) this._data[index] != (int) bigInteger._data[index])
          return false;
      }
      return true;
    }

    public override int GetHashCode()
    {
      return this.ToString().GetHashCode();
    }

    public static bool operator >(BigInteger pBi1, BigInteger pBi2)
    {
      int index1 = 69;
      if (((int) pBi1._data[69] & int.MinValue) != 0 && ((int) pBi2._data[index1] & int.MinValue) == 0)
        return false;
      if (((int) pBi1._data[index1] & int.MinValue) == 0 && ((int) pBi2._data[index1] & int.MinValue) != 0)
        return true;
      int index2 = (pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength) - 1;
      while (index2 >= 0 && (int) pBi1._data[index2] == (int) pBi2._data[index2])
        --index2;
      if (index2 >= 0)
        return pBi1._data[index2] > pBi2._data[index2];
      return false;
    }

    public static bool operator <(BigInteger pBi1, BigInteger pBi2)
    {
      int index1 = 69;
      if (((int) pBi1._data[69] & int.MinValue) != 0 && ((int) pBi2._data[index1] & int.MinValue) == 0)
        return true;
      if (((int) pBi1._data[index1] & int.MinValue) == 0 && ((int) pBi2._data[index1] & int.MinValue) != 0)
        return false;
      int index2 = (pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength) - 1;
      while (index2 >= 0 && (int) pBi1._data[index2] == (int) pBi2._data[index2])
        --index2;
      if (index2 >= 0)
        return pBi1._data[index2] < pBi2._data[index2];
      return false;
    }

    public static bool operator >=(BigInteger pBi1, BigInteger pBi2)
    {
      if (!(pBi1 == pBi2))
        return pBi1 > pBi2;
      return true;
    }

    public static bool operator <=(BigInteger pBi1, BigInteger pBi2)
    {
      if (!(pBi1 == pBi2))
        return pBi1 < pBi2;
      return true;
    }

    private static void multiByteDivide(
      BigInteger pBi1,
      BigInteger pBi2,
      BigInteger pOutQuotient,
      BigInteger pOutRemainder)
    {
      uint[] numArray1 = new uint[70];
      int length1 = pBi1._dataLength + 1;
      uint[] numArray2 = new uint[length1];
      uint num1 = 2147483648;
      uint num2 = pBi2._data[pBi2._dataLength - 1];
      int pShiftVal = 0;
      int num3 = 0;
      for (; num1 != 0U && ((int) num2 & (int) num1) == 0; num1 >>= 1)
        ++pShiftVal;
      for (int index = 0; index < pBi1._dataLength; ++index)
        numArray2[index] = pBi1._data[index];
      BigInteger.shiftLeft((IList<uint>) numArray2, pShiftVal);
      pBi2 <<= pShiftVal;
      int num4 = length1 - pBi2._dataLength;
      int index1 = length1 - 1;
      ulong num5 = (ulong) pBi2._data[pBi2._dataLength - 1];
      ulong num6 = (ulong) pBi2._data[pBi2._dataLength - 2];
      int length2 = pBi2._dataLength + 1;
      uint[] numArray3 = new uint[length2];
      for (; num4 > 0; --num4)
      {
        ulong num7 = ((ulong) numArray2[index1] << 32) + (ulong) numArray2[index1 - 1];
        ulong num8 = num7 / num5;
        ulong num9 = num7 % num5;
        bool flag = false;
        while (!flag)
        {
          flag = true;
          if (num8 == 4294967296UL || num8 * num6 > (num9 << 32) + (ulong) numArray2[index1 - 2])
          {
            --num8;
            num9 += num5;
            if (num9 < 4294967296UL)
              flag = false;
          }
        }
        for (int index2 = 0; index2 < length2; ++index2)
          numArray3[index2] = numArray2[index1 - index2];
        BigInteger bigInteger1 = new BigInteger((IList<uint>) numArray3);
        BigInteger bigInteger2 = pBi2 * (BigInteger) ((long) num8);
        while (bigInteger2 > bigInteger1)
        {
          --num8;
          bigInteger2 -= pBi2;
        }
        BigInteger bigInteger3 = bigInteger1 - bigInteger2;
        for (int index2 = 0; index2 < length2; ++index2)
          numArray2[index1 - index2] = bigInteger3._data[pBi2._dataLength - index2];
        numArray1[num3++] = (uint) num8;
        --index1;
      }
      pOutQuotient._dataLength = num3;
      int index3 = 0;
      int index4 = pOutQuotient._dataLength - 1;
      while (index4 >= 0)
      {
        pOutQuotient._data[index3] = numArray1[index4];
        --index4;
        ++index3;
      }
      for (; index3 < 70; ++index3)
        pOutQuotient._data[index3] = 0U;
      while (pOutQuotient._dataLength > 1 && pOutQuotient._data[pOutQuotient._dataLength - 1] == 0U)
        --pOutQuotient._dataLength;
      if (pOutQuotient._dataLength == 0)
        pOutQuotient._dataLength = 1;
      pOutRemainder._dataLength = BigInteger.shiftRight((IList<uint>) numArray2, pShiftVal);
      int index5;
      for (index5 = 0; index5 < pOutRemainder._dataLength; ++index5)
        pOutRemainder._data[index5] = numArray2[index5];
      for (; index5 < 70; ++index5)
        pOutRemainder._data[index5] = 0U;
    }

    private static void singleByteDivide(
      BigInteger pBi1,
      BigInteger pBi2,
      BigInteger pOutQuotient,
      BigInteger pOutRemainder)
    {
      uint[] numArray = new uint[70];
      int num1 = 0;
      for (int index = 0; index < 70; ++index)
        pOutRemainder._data[index] = pBi1._data[index];
      pOutRemainder._dataLength = pBi1._dataLength;
      while (pOutRemainder._dataLength > 1 && pOutRemainder._data[pOutRemainder._dataLength - 1] == 0U)
        --pOutRemainder._dataLength;
      ulong num2 = (ulong) pBi2._data[0];
      int index1 = pOutRemainder._dataLength - 1;
      ulong num3 = (ulong) pOutRemainder._data[index1];
      if (num3 >= num2)
      {
        ulong num4 = num3 / num2;
        numArray[num1++] = (uint) num4;
        pOutRemainder._data[index1] = (uint) (num3 % num2);
      }
      ulong num5;
      for (int index2 = index1 - 1; index2 >= 0; pOutRemainder._data[index2--] = (uint) (num5 % num2))
      {
        num5 = ((ulong) pOutRemainder._data[index2 + 1] << 32) + (ulong) pOutRemainder._data[index2];
        ulong num4 = num5 / num2;
        numArray[num1++] = (uint) num4;
        pOutRemainder._data[index2 + 1] = 0U;
      }
      pOutQuotient._dataLength = num1;
      int index3 = 0;
      int index4 = pOutQuotient._dataLength - 1;
      while (index4 >= 0)
      {
        pOutQuotient._data[index3] = numArray[index4];
        --index4;
        ++index3;
      }
      for (; index3 < 70; ++index3)
        pOutQuotient._data[index3] = 0U;
      while (pOutQuotient._dataLength > 1 && pOutQuotient._data[pOutQuotient._dataLength - 1] == 0U)
        --pOutQuotient._dataLength;
      if (pOutQuotient._dataLength == 0)
        pOutQuotient._dataLength = 1;
      while (pOutRemainder._dataLength > 1 && pOutRemainder._data[pOutRemainder._dataLength - 1] == 0U)
        --pOutRemainder._dataLength;
    }

    public static BigInteger operator /(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger pOutQuotient = new BigInteger();
      BigInteger pOutRemainder = new BigInteger();
      bool flag1 = false;
      bool flag2 = false;
      if (((int) pBi1._data[69] & int.MinValue) != 0)
      {
        pBi1 = -pBi1;
        flag2 = true;
      }
      if (((int) pBi2._data[69] & int.MinValue) != 0)
      {
        pBi2 = -pBi2;
        flag1 = true;
      }
      if (pBi1 < pBi2)
        return pOutQuotient;
      if (pBi2._dataLength == 1)
        BigInteger.singleByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
      else
        BigInteger.multiByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
      if (flag2 != flag1)
        return -pOutQuotient;
      return pOutQuotient;
    }

    public static BigInteger operator %(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger pOutQuotient = new BigInteger();
      BigInteger pOutRemainder = new BigInteger(pBi1);
      bool flag = false;
      if (((int) pBi1._data[69] & int.MinValue) != 0)
      {
        pBi1 = -pBi1;
        flag = true;
      }
      if (((int) pBi2._data[69] & int.MinValue) != 0)
        pBi2 = -pBi2;
      if (pBi1 < pBi2)
        return pOutRemainder;
      if (pBi2._dataLength == 1)
        BigInteger.singleByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
      else
        BigInteger.multiByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
      if (flag)
        return -pOutRemainder;
      return pOutRemainder;
    }

    public static BigInteger operator &(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger bigInteger = new BigInteger();
      int num1 = pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength;
      for (int index = 0; index < num1; ++index)
      {
        uint num2 = pBi1._data[index] & pBi2._data[index];
        bigInteger._data[index] = num2;
      }
      bigInteger._dataLength = 70;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      return bigInteger;
    }

    public static BigInteger operator |(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger bigInteger = new BigInteger();
      int num1 = pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength;
      for (int index = 0; index < num1; ++index)
      {
        uint num2 = pBi1._data[index] | pBi2._data[index];
        bigInteger._data[index] = num2;
      }
      bigInteger._dataLength = 70;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      return bigInteger;
    }

    public static BigInteger operator ^(BigInteger pBi1, BigInteger pBi2)
    {
      BigInteger bigInteger = new BigInteger();
      int num1 = pBi1._dataLength > pBi2._dataLength ? pBi1._dataLength : pBi2._dataLength;
      for (int index = 0; index < num1; ++index)
      {
        uint num2 = pBi1._data[index] ^ pBi2._data[index];
        bigInteger._data[index] = num2;
      }
      bigInteger._dataLength = 70;
      while (bigInteger._dataLength > 1 && bigInteger._data[bigInteger._dataLength - 1] == 0U)
        --bigInteger._dataLength;
      return bigInteger;
    }

    public BigInteger Max(BigInteger bigInteger)
    {
      if (!(this > bigInteger))
        return new BigInteger(bigInteger);
      return new BigInteger(this);
    }

    public BigInteger Min(BigInteger bigInteger)
    {
      if (!(this < bigInteger))
        return new BigInteger(bigInteger);
      return new BigInteger(this);
    }

    public BigInteger Abs()
    {
      if (((int) this._data[69] & int.MinValue) != 0)
        return -this;
      return new BigInteger(this);
    }

    public override string ToString()
    {
      return this.ToString(10);
    }

    public string ToString(int radix)
    {
      if (radix < 2 || radix > 36)
        throw new ArgumentException("Radix must be >= 2 and <= 36");
      string str = "";
      BigInteger pBi1 = this;
      bool flag = false;
      if (((int) pBi1._data[69] & int.MinValue) != 0)
      {
        flag = true;
        try
        {
          pBi1 = -pBi1;
        }
        catch (Exception ex)
        {
        }
      }
      BigInteger pOutQuotient = new BigInteger();
      BigInteger pOutRemainder = new BigInteger();
      BigInteger pBi2 = new BigInteger((long) radix);
      if (pBi1._dataLength == 1 && pBi1._data[0] == 0U)
      {
        str = "0";
      }
      else
      {
        for (; pBi1._dataLength > 1 || pBi1._dataLength == 1 && pBi1._data[0] != 0U; pBi1 = pOutQuotient)
        {
          BigInteger.singleByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
          str = pOutRemainder._data[0] >= 10U ? ((int) "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(int) pOutRemainder._data[0] - 10]).ToString() + str : ((int) pOutRemainder._data[0]).ToString() + str;
        }
        if (flag)
          str = "-" + str;
      }
      return str;
    }

    public string ToHexString()
    {
      string str = this._data[this._dataLength - 1].ToString("X");
      for (int index = this._dataLength - 2; index >= 0; --index)
        str += this._data[index].ToString("X8");
      return str;
    }

    public BigInteger ModPow(BigInteger pExp, BigInteger pN)
    {
      if (((int) pExp._data[69] & int.MinValue) != 0)
        throw new ArithmeticException("Positive exponents only.");
      BigInteger bigInteger1 = (BigInteger) 1;
      bool flag = false;
      BigInteger bigInteger2;
      if (((int) this._data[69] & int.MinValue) != 0)
      {
        bigInteger2 = -this % pN;
        flag = true;
      }
      else
        bigInteger2 = this % pN;
      if (((int) pN._data[69] & int.MinValue) != 0)
        pN = -pN;
      BigInteger bigInteger3 = new BigInteger();
      int index1 = pN._dataLength << 1;
      bigInteger3._data[index1] = 1U;
      bigInteger3._dataLength = index1 + 1;
      BigInteger pConstant = bigInteger3 / pN;
      int num1 = pExp.bitCount();
      int num2 = 0;
      for (int index2 = 0; index2 < pExp._dataLength; ++index2)
      {
        uint num3 = 1;
        for (int index3 = 0; index3 < 32; ++index3)
        {
          if (((int) pExp._data[index2] & (int) num3) != 0)
            bigInteger1 = this.barrettReduction(bigInteger1 * bigInteger2, pN, pConstant);
          num3 <<= 1;
          bigInteger2 = this.barrettReduction(bigInteger2 * bigInteger2, pN, pConstant);
          if (bigInteger2._dataLength != 1 || bigInteger2._data[0] != 1U)
          {
            ++num2;
            if (num2 == num1)
              break;
          }
          else
          {
            if (flag && ((int) pExp._data[0] & 1) != 0)
              return -bigInteger1;
            return bigInteger1;
          }
        }
      }
      if (flag && ((int) pExp._data[0] & 1) != 0)
        return -bigInteger1;
      return bigInteger1;
    }

    private BigInteger barrettReduction(
      BigInteger pX,
      BigInteger pN,
      BigInteger pConstant)
    {
      int dataLength = pN._dataLength;
      int index1 = dataLength + 1;
      int num1 = dataLength - 1;
      BigInteger bigInteger1 = new BigInteger();
      int index2 = num1;
      int index3 = 0;
      while (index2 < pX._dataLength)
      {
        bigInteger1._data[index3] = pX._data[index2];
        ++index2;
        ++index3;
      }
      bigInteger1._dataLength = pX._dataLength - num1;
      if (bigInteger1._dataLength <= 0)
        bigInteger1._dataLength = 1;
      BigInteger bigInteger2 = bigInteger1 * pConstant;
      BigInteger bigInteger3 = new BigInteger();
      int index4 = index1;
      int index5 = 0;
      while (index4 < bigInteger2._dataLength)
      {
        bigInteger3._data[index5] = bigInteger2._data[index4];
        ++index4;
        ++index5;
      }
      bigInteger3._dataLength = bigInteger2._dataLength - index1;
      if (bigInteger3._dataLength <= 0)
        bigInteger3._dataLength = 1;
      BigInteger bigInteger4 = new BigInteger();
      int num2 = pX._dataLength > index1 ? index1 : pX._dataLength;
      for (int index6 = 0; index6 < num2; ++index6)
        bigInteger4._data[index6] = pX._data[index6];
      bigInteger4._dataLength = num2;
      BigInteger bigInteger5 = new BigInteger();
      for (int index6 = 0; index6 < bigInteger3._dataLength; ++index6)
      {
        if (bigInteger3._data[index6] != 0U)
        {
          ulong num3 = 0;
          int index7 = index6;
          for (int index8 = 0; index8 < pN._dataLength && index7 < index1; ++index7)
          {
            ulong num4 = (ulong) bigInteger3._data[index6] * (ulong) pN._data[index8] + (ulong) bigInteger5._data[index7] + num3;
            bigInteger5._data[index7] = (uint) (num4 & (ulong) uint.MaxValue);
            num3 = num4 >> 32;
            ++index8;
          }
          if (index7 < index1)
            bigInteger5._data[index7] = (uint) num3;
        }
      }
      bigInteger5._dataLength = index1;
      while (bigInteger5._dataLength > 1 && bigInteger5._data[bigInteger5._dataLength - 1] == 0U)
        --bigInteger5._dataLength;
      BigInteger bigInteger6 = bigInteger4 - bigInteger5;
      if (((int) bigInteger6._data[69] & int.MinValue) != 0)
      {
        BigInteger bigInteger7 = new BigInteger();
        bigInteger7._data[index1] = 1U;
        bigInteger7._dataLength = index1 + 1;
        bigInteger6 += bigInteger7;
      }
      while (bigInteger6 >= pN)
        bigInteger6 -= pN;
      return bigInteger6;
    }

    public BigInteger Gcd(BigInteger bigInteger)
    {
      BigInteger bigInteger1 = ((int) this._data[69] & int.MinValue) == 0 ? this : -this;
      BigInteger bigInteger2 = ((int) bigInteger._data[69] & int.MinValue) == 0 ? bigInteger : -bigInteger;
      BigInteger bigInteger3 = bigInteger2;
      while (bigInteger1._dataLength > 1 || bigInteger1._dataLength == 1 && bigInteger1._data[0] != 0U)
      {
        bigInteger3 = bigInteger1;
        bigInteger1 = bigInteger2 % bigInteger1;
        bigInteger2 = bigInteger3;
      }
      return bigInteger3;
    }

    public void GenRandomBits(int pBits, Random pRand)
    {
      int num1 = pBits >> 5;
      int num2 = pBits & 31;
      if (num2 != 0)
        ++num1;
      if (num1 > 70)
        throw new ArithmeticException("Number of required bits > maxLength.");
      for (int index = 0; index < num1; ++index)
        this._data[index] = (uint) (pRand.NextDouble() * 4294967296.0);
      for (int index = num1; index < 70; ++index)
        this._data[index] = 0U;
      if (num2 != 0)
      {
        uint num3 = (uint) (1 << num2 - 1);
        this._data[num1 - 1] |= num3;
        uint num4 = uint.MaxValue >> 32 - num2;
        this._data[num1 - 1] &= num4;
      }
      else
        this._data[num1 - 1] |= 2147483648U;
      this._dataLength = num1;
      if (this._dataLength != 0)
        return;
      this._dataLength = 1;
    }

    private int bitCount()
    {
      while (this._dataLength > 1 && this._data[this._dataLength - 1] == 0U)
        --this._dataLength;
      uint num1 = this._data[this._dataLength - 1];
      uint num2 = 2147483648;
      int num3;
      for (num3 = 32; num3 > 0 && ((int) num1 & (int) num2) == 0; num2 >>= 1)
        --num3;
      return num3 + (this._dataLength - 1 << 5);
    }

    private bool rabinMillerTest(int pConfidence)
    {
      BigInteger bigInteger1 = ((int) this._data[69] & int.MinValue) == 0 ? this : -this;
      if (bigInteger1._dataLength == 1)
      {
        switch (bigInteger1._data[0])
        {
          case 0:
          case 1:
            return false;
          case 2:
          case 3:
            return true;
        }
      }
      if (((int) bigInteger1._data[0] & 1) == 0)
        return false;
      BigInteger bigInteger2 = bigInteger1 - new BigInteger(1L);
      int num1 = 0;
      for (int index1 = 0; index1 < bigInteger2._dataLength; ++index1)
      {
        uint num2 = 1;
        for (int index2 = 0; index2 < 32; ++index2)
        {
          if (((int) bigInteger2._data[index1] & (int) num2) == 0)
          {
            num2 <<= 1;
            ++num1;
          }
          else
          {
            index1 = bigInteger2._dataLength;
            break;
          }
        }
      }
      BigInteger pExp = bigInteger2 >> num1;
      int num3 = bigInteger1.bitCount();
      BigInteger bigInteger3 = new BigInteger();
      Random pRand = new Random();
      for (int index1 = 0; index1 < pConfidence; ++index1)
      {
        bool flag1 = false;
        while (!flag1)
        {
          int pBits = 0;
          while (pBits < 2)
            pBits = (int) (pRand.NextDouble() * (double) num3);
          bigInteger3.GenRandomBits(pBits, pRand);
          int dataLength = bigInteger3._dataLength;
          if (dataLength > 1 || dataLength == 1 && bigInteger3._data[0] != 1U)
            flag1 = true;
        }
        BigInteger bigInteger4 = bigInteger3.Gcd(bigInteger1);
        if (bigInteger4._dataLength == 1 && bigInteger4._data[0] != 1U)
          return false;
        BigInteger bigInteger5 = ModPow(pExp, bigInteger1);
        bool flag2 = false;
        if (bigInteger5._dataLength == 1 && bigInteger5._data[0] == 1U)
          flag2 = true;
        for (int index2 = 0; !flag2 && index2 < num1; ++index2)
        {
          if (!(bigInteger5 == bigInteger2))
          {
            bigInteger5 = bigInteger5 * bigInteger5 % bigInteger1;
          }
          else
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
          return false;
      }
      return true;
    }

    private bool lucasStrongTestHelper(BigInteger pThisVal)
    {
      long num1 = 5;
      long num2 = -1;
      long num3 = 0;
      bool flag1 = false;
      while (!flag1)
      {
        switch (BigInteger.jacobi((BigInteger) num1, pThisVal))
        {
          case -1:
            flag1 = true;
            break;
          case 0:
            if ((BigInteger) Math.Abs(num1) < pThisVal)
              return false;
            goto default;
          default:
            if (num3 == 20L)
            {
              BigInteger bigInteger = pThisVal.Sqrt();
              if (bigInteger * bigInteger == pThisVal)
                return false;
            }
            num1 = (Math.Abs(num1) + 2L) * num2;
            num2 = -num2;
            break;
        }
        ++num3;
      }
      long num4 = 1L - num1 >> 2;
      BigInteger bigInteger1 = pThisVal + (BigInteger) 1;
      int num5 = 0;
      for (int index1 = 0; index1 < bigInteger1._dataLength; ++index1)
      {
        uint num6 = 1;
        for (int index2 = 0; index2 < 32; ++index2)
        {
          if (((int) bigInteger1._data[index1] & (int) num6) == 0)
          {
            num6 <<= 1;
            ++num5;
          }
          else
          {
            index1 = bigInteger1._dataLength;
            break;
          }
        }
      }
      BigInteger pK = bigInteger1 >> num5;
      BigInteger bigInteger2 = new BigInteger();
      int index3 = pThisVal._dataLength << 1;
      bigInteger2._data[index3] = 1U;
      bigInteger2._dataLength = index3 + 1;
      BigInteger pConstant = bigInteger2 / pThisVal;
      BigInteger[] bigIntegerArray1 = BigInteger.lucasSequenceHelper((BigInteger) 1, (BigInteger) num4, pK, pThisVal, pConstant, 0);
      bool flag2 = false;
      if (bigIntegerArray1[0]._dataLength == 1 && bigIntegerArray1[0]._data[0] == 0U || bigIntegerArray1[1]._dataLength == 1 && bigIntegerArray1[1]._data[0] == 0U)
        flag2 = true;
      for (int index1 = 1; index1 < num5; ++index1)
      {
        if (!flag2)
        {
          bigIntegerArray1[1] = pThisVal.barrettReduction(bigIntegerArray1[1] * bigIntegerArray1[1], pThisVal, pConstant);
          bigIntegerArray1[1] = (bigIntegerArray1[1] - (bigIntegerArray1[2] << 1)) % pThisVal;
          if (bigIntegerArray1[1]._dataLength == 1 && bigIntegerArray1[1]._data[0] == 0U)
            flag2 = true;
        }
        bigIntegerArray1[2] = pThisVal.barrettReduction(bigIntegerArray1[2] * bigIntegerArray1[2], pThisVal, pConstant);
      }
      if (flag2)
      {
        BigInteger bigInteger3 = pThisVal.Gcd((BigInteger) num4);
        if (bigInteger3._dataLength == 1 && bigInteger3._data[0] == 1U)
        {
          if (((int) bigIntegerArray1[2]._data[69] & int.MinValue) != 0)
          {
            BigInteger[] bigIntegerArray2;
            (bigIntegerArray2 = bigIntegerArray1)[2] = bigIntegerArray2[2] + pThisVal;
          }
          BigInteger bigInteger4 = (BigInteger) (num4 * (long) BigInteger.jacobi((BigInteger) num4, pThisVal)) % pThisVal;
          if (((int) bigInteger4._data[69] & int.MinValue) != 0)
            bigInteger4 += pThisVal;
          if (bigIntegerArray1[2] != bigInteger4)
            flag2 = false;
        }
      }
      return flag2;
    }

    public bool IsProbablePrime(int pConfidence)
    {
      BigInteger bigInteger1 = ((int) this._data[69] & int.MinValue) == 0 ? this : -this;
      foreach (int num in BigInteger._primesBelow2000)
      {
        BigInteger bigInteger2 = (BigInteger) num;
        if (!(bigInteger2 >= bigInteger1))
        {
          if ((bigInteger1 % bigInteger2).IntValue() == 0)
            return false;
        }
        else
          break;
      }
      return bigInteger1.rabinMillerTest(pConfidence);
    }

    public bool IsProbablePrime()
    {
      BigInteger bigInteger1 = ((int) this._data[69] & int.MinValue) == 0 ? this : -this;
      if (bigInteger1._dataLength == 1)
      {
        switch (bigInteger1._data[0])
        {
          case 0:
          case 1:
            return false;
          case 2:
          case 3:
            return true;
        }
      }
      if (((int) bigInteger1._data[0] & 1) == 0)
        return false;
      foreach (int num in BigInteger._primesBelow2000)
      {
        BigInteger bigInteger2 = (BigInteger) num;
        if (!(bigInteger2 >= bigInteger1))
        {
          if ((bigInteger1 % bigInteger2).IntValue() == 0)
            return false;
        }
        else
          break;
      }
      BigInteger bigInteger3 = bigInteger1 - new BigInteger(1L);
      int num1 = 0;
      for (int index1 = 0; index1 < bigInteger3._dataLength; ++index1)
      {
        uint num2 = 1;
        for (int index2 = 0; index2 < 32; ++index2)
        {
          if (((int) bigInteger3._data[index1] & (int) num2) == 0)
          {
            num2 <<= 1;
            ++num1;
          }
          else
          {
            index1 = bigInteger3._dataLength;
            break;
          }
        }
      }
      BigInteger bigInteger4 = ModPow(bigInteger3 >> num1, bigInteger1);
      bool flag = false;
      if (bigInteger4._dataLength == 1 && bigInteger4._data[0] == 1U)
        flag = true;
      for (int index = 0; !flag && index < num1; ++index)
      {
        if (!(bigInteger4 == bigInteger3))
        {
          bigInteger4 = bigInteger4 * bigInteger4 % bigInteger1;
        }
        else
        {
          flag = true;
          break;
        }
      }
      if (flag)
        flag = this.lucasStrongTestHelper(bigInteger1);
      return flag;
    }

    public int IntValue()
    {
      return (int) this._data[0];
    }

    public long LongValue()
    {
      long num = (long) this._data[0];
      try
      {
        num |= (long) this._data[1] << 32;
      }
      catch (Exception ex)
      {
        if (((int) this._data[0] & int.MinValue) != 0)
          num = (long) (int) this._data[0];
      }
      return num;
    }

    private static int jacobi(BigInteger pA, BigInteger pB)
    {
      if (((int) pB._data[0] & 1) == 0)
        throw new ArgumentException("Jacobi defined only for odd integers.");
      if (pA >= pB)
        pA %= pB;
      if (pA._dataLength == 1 && pA._data[0] == 0U)
        return 0;
      if (pA._dataLength == 1 && pA._data[0] == 1U)
        return 1;
      if (pA < (BigInteger) 0)
      {
        if (((int) (pB - (BigInteger) 1)._data[0] & 2) == 0)
          return BigInteger.jacobi(-pA, pB);
        return -BigInteger.jacobi(-pA, pB);
      }
      int num1 = 0;
      for (int index1 = 0; index1 < pA._dataLength; ++index1)
      {
        uint num2 = 1;
        for (int index2 = 0; index2 < 32; ++index2)
        {
          if (((int) pA._data[index1] & (int) num2) == 0)
          {
            num2 <<= 1;
            ++num1;
          }
          else
          {
            index1 = pA._dataLength;
            break;
          }
        }
      }
      BigInteger pB1 = pA >> num1;
      int num3 = 1;
      if ((num1 & 1) != 0 && (((int) pB._data[0] & 7) == 3 || ((int) pB._data[0] & 7) == 5))
        num3 = -1;
      if (((int) pB._data[0] & 3) == 3 && ((int) pB1._data[0] & 3) == 3)
        num3 = -num3;
      if (pB1._dataLength == 1 && pB1._data[0] == 1U)
        return num3;
      return num3 * BigInteger.jacobi(pB % pB1, pB1);
    }

    public static BigInteger GenPseudoPrime(int pBits, int pConfidence, Random pRand)
    {
      BigInteger bigInteger = new BigInteger();
      for (bool flag = false; !flag; flag = bigInteger.IsProbablePrime(pConfidence))
      {
        bigInteger.GenRandomBits(pBits, pRand);
        bigInteger._data[0] |= 1U;
      }
      return bigInteger;
    }

    public BigInteger GenCoPrime(int pBits, Random pRand)
    {
      bool flag = false;
      BigInteger bigInteger1 = new BigInteger();
      while (!flag)
      {
        bigInteger1.GenRandomBits(pBits, pRand);
        BigInteger bigInteger2 = bigInteger1.Gcd(this);
        if (bigInteger2._dataLength == 1 && bigInteger2._data[0] == 1U)
          flag = true;
      }
      return bigInteger1;
    }

    public BigInteger ModInverse(BigInteger pModulus)
    {
      BigInteger[] bigIntegerArray1 = new BigInteger[2]{ (BigInteger) 0, (BigInteger) 1 };
      BigInteger[] bigIntegerArray2 = new BigInteger[2];
      BigInteger[] bigIntegerArray3 = new BigInteger[2]{ (BigInteger) 0, (BigInteger) 0 };
      int num = 0;
      BigInteger pBi1 = pModulus;
      BigInteger pBi2 = this;
      while (pBi2._dataLength > 1 || pBi2._dataLength == 1 && pBi2._data[0] != 0U)
      {
        BigInteger pOutQuotient = new BigInteger();
        BigInteger pOutRemainder = new BigInteger();
        if (num > 1)
        {
          BigInteger bigInteger = (bigIntegerArray1[0] - bigIntegerArray1[1] * bigIntegerArray2[0]) % pModulus;
          bigIntegerArray1[0] = bigIntegerArray1[1];
          bigIntegerArray1[1] = bigInteger;
        }
        if (pBi2._dataLength == 1)
          BigInteger.singleByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
        else
          BigInteger.multiByteDivide(pBi1, pBi2, pOutQuotient, pOutRemainder);
        bigIntegerArray2[0] = bigIntegerArray2[1];
        bigIntegerArray3[0] = bigIntegerArray3[1];
        bigIntegerArray2[1] = pOutQuotient;
        bigIntegerArray3[1] = pOutRemainder;
        pBi1 = pBi2;
        pBi2 = pOutRemainder;
        ++num;
      }
      if (bigIntegerArray3[0]._dataLength > 1 || bigIntegerArray3[0]._dataLength == 1 && bigIntegerArray3[0]._data[0] != 1U)
        throw new ArithmeticException("No inverse!");
      BigInteger bigInteger1 = (bigIntegerArray1[0] - bigIntegerArray1[1] * bigIntegerArray2[0]) % pModulus;
      if (((int) bigInteger1._data[69] & int.MinValue) != 0)
        bigInteger1 += pModulus;
      return bigInteger1;
    }

    public byte[] GetBytes()
    {
      int num1 = this.bitCount();
      int length = num1 >> 3;
      if ((num1 & 7) != 0)
        ++length;
      byte[] numArray = new byte[length];
      int index1 = 0;
      uint num2 = this._data[this._dataLength - 1];
      uint num3;
      if ((num3 = num2 >> 24 & (uint) byte.MaxValue) != 0U)
        numArray[index1++] = (byte) num3;
      uint num4;
      if ((num4 = num2 >> 16 & (uint) byte.MaxValue) != 0U || index1 != 0)
        numArray[index1++] = (byte) num4;
      uint num5;
      if ((num5 = num2 >> 8 & (uint) byte.MaxValue) != 0U || index1 != 0)
        numArray[index1++] = (byte) num5;
      uint num6;
      if ((num6 = num2 & (uint) byte.MaxValue) != 0U || index1 != 0)
        numArray[index1++] = (byte) num6;
      int index2 = this._dataLength - 2;
      while (index2 >= 0)
      {
        uint num7 = this._data[index2];
        numArray[index1 + 3] = (byte) (num7 & (uint) byte.MaxValue);
        uint num8 = num7 >> 8;
        numArray[index1 + 2] = (byte) (num8 & (uint) byte.MaxValue);
        uint num9 = num8 >> 8;
        numArray[index1 + 1] = (byte) (num9 & (uint) byte.MaxValue);
        uint num10 = num9 >> 8;
        numArray[index1] = (byte) (num10 & (uint) byte.MaxValue);
        --index2;
        index1 += 4;
      }
      return numArray;
    }

    public BigInteger Sqrt()
    {
      uint num1 = (uint) this.bitCount();
      uint num2 = ((int) num1 & 1) == 0 ? num1 >> 1 : (num1 >> 1) + 1U;
      uint num3 = num2 >> 5;
      byte num4 = (byte) (num2 & 31U);
      BigInteger bigInteger = new BigInteger();
      uint num5;
      if (num4 == (byte) 0)
      {
        num5 = 2147483648U;
      }
      else
      {
        num5 = 1U << (int) num4;
        ++num3;
      }
      bigInteger._dataLength = (int) num3;
      for (int index = (int) num3 - 1; index >= 0; --index)
      {
        for (; num5 != 0U; num5 >>= 1)
        {
          bigInteger._data[index] ^= num5;
          if (bigInteger * bigInteger > this)
            bigInteger._data[index] ^= num5;
        }
        num5 = 2147483648U;
      }
      return bigInteger;
    }

    private static BigInteger[] lucasSequenceHelper(
      BigInteger pP,
      BigInteger pQ,
      BigInteger pK,
      BigInteger pN,
      BigInteger pConstant,
      int pS)
    {
      BigInteger[] bigIntegerArray = new BigInteger[3];
      if (((int) pK._data[0] & 1) == 0)
        throw new ArgumentException("Argument k must be odd.");
      uint num = (uint) (1 << (pK.bitCount() & 31) - 1);
      BigInteger bigInteger1 = (BigInteger) 2 % pN;
      BigInteger bigInteger2 = (BigInteger) 1 % pN;
      BigInteger bigInteger3 = pP % pN;
      BigInteger bigInteger4 = bigInteger2;
      bool flag = true;
      for (int index = pK._dataLength - 1; index >= 0; --index)
      {
        for (; num != 0U && (index != 0 || num != 1U); num >>= 1)
        {
          if (((int) pK._data[index] & (int) num) != 0)
          {
            bigInteger4 = bigInteger4 * bigInteger3 % pN;
            bigInteger1 = (bigInteger1 * bigInteger3 - pP * bigInteger2) % pN;
            bigInteger3 = (pN.barrettReduction(bigInteger3 * bigInteger3, pN, pConstant) - (bigInteger2 * pQ << 1)) % pN;
            if (flag)
              flag = false;
            else
              bigInteger2 = pN.barrettReduction(bigInteger2 * bigInteger2, pN, pConstant);
            bigInteger2 = bigInteger2 * pQ % pN;
          }
          else
          {
            bigInteger4 = (bigInteger4 * bigInteger1 - bigInteger2) % pN;
            bigInteger3 = (bigInteger1 * bigInteger3 - pP * bigInteger2) % pN;
            bigInteger1 = (pN.barrettReduction(bigInteger1 * bigInteger1, pN, pConstant) - (bigInteger2 << 1)) % pN;
            if (flag)
            {
              bigInteger2 = pQ % pN;
              flag = false;
            }
            else
              bigInteger2 = pN.barrettReduction(bigInteger2 * bigInteger2, pN, pConstant);
          }
        }
        num = 2147483648U;
      }
      BigInteger bigInteger5 = (bigInteger4 * bigInteger1 - bigInteger2) % pN;
      BigInteger bigInteger6 = (bigInteger1 * bigInteger3 - pP * bigInteger2) % pN;
      if (!flag)
        bigInteger2 = pN.barrettReduction(bigInteger2 * bigInteger2, pN, pConstant);
      BigInteger bigInteger7 = bigInteger2 * pQ % pN;
      for (int index = 0; index < pS; ++index)
      {
        bigInteger5 = bigInteger5 * bigInteger6 % pN;
        bigInteger6 = (bigInteger6 * bigInteger6 - (bigInteger7 << 1)) % pN;
        bigInteger7 = pN.barrettReduction(bigInteger7 * bigInteger7, pN, pConstant);
      }
      bigIntegerArray[0] = bigInteger5;
      bigIntegerArray[1] = bigInteger6;
      bigIntegerArray[2] = bigInteger7;
      return bigIntegerArray;
    }
  }
}
