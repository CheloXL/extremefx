// Decompiled with JetBrains decompiler
// Type: Efx.Core.ExtensionMethods.StringExtensions
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Efx.Core.ExtensionMethods
{
  public static class StringExtensions
  {
    private static readonly Regex _killEntities = new Regex("&.+?;", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
    private static readonly Regex _killNonLatin = new Regex("[^a-z0-9\\s-.]", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
    private static readonly Regex _killExtraSpaces = new Regex("\\s+", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
    private static readonly Regex _killExtraMinus = new Regex("-+", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
    private static readonly Regex _rtp = new Regex("\\W+?$", RegexOptions.Singleline);

    public static int LevenshteinDistance(this string firstString, string secondString)
    {
      int length1 = firstString.Length;
      int length2 = secondString.Length;
      if (length1 == 0)
        return length2;
      if (length2 == 0)
        return length1;
      int[] numArray1 = new int[length1 + 1];
      int[] numArray2 = new int[length1 + 1];
      for (int index = 1; index <= length1; ++index)
        numArray1[index] = index;
      for (int index1 = 1; index1 <= length2; ++index1)
      {
        numArray2[0] = index1;
        char ch1 = secondString[index1 - 1];
        for (int index2 = 1; index2 <= length1; ++index2)
        {
          char ch2 = firstString[index2 - 1];
          int num1 = numArray1[index2] + 1;
          int num2 = numArray2[index2 - 1] + 1;
          int num3 = numArray1[index2 - 1] + (int) ch2 == (int) ch1 ? 0 : 1;
          if (num2 < num1)
            num1 = num2;
          if (num3 < num1)
            num1 = num3;
          numArray2[index2] = num1;
        }
        int[] numArray3 = numArray1;
        numArray1 = numArray2;
        numArray2 = numArray3;
      }
      return numArray1[length1];
    }

    public static string FormatInvariant(this string @string, params object[] arguments)
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, @string, arguments);
    }

    public static string ToStringInvariant(this int number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this byte number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this double number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this float number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this uint number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this long number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this ulong number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this short number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static string ToStringInvariant(this ushort number)
    {
      return number.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static byte[] HexToByteArray(this string value)
    {
      if (string.IsNullOrEmpty(value))
        return new byte[0];
      int length = value.Length;
      byte[] numArray = new byte[length / 2];
      for (int startIndex = 0; startIndex < length; startIndex += 2)
        numArray[startIndex / 2] = Convert.ToByte(value.Substring(startIndex, 2), 16);
      return numArray;
    }

    public static byte[] ToByteArray(this string value)
    {
      return Encoding.UTF8.GetBytes(value);
    }

    public static byte[] ToByteArray(this string value, Encoding encoding)
    {
      return encoding.GetBytes(value);
    }

    public static string EnsureTrailingDirectorySeparatorChar(this string path)
    {
      if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
        path += (string) (object) Path.DirectorySeparatorChar;
      return path;
    }

    public static string EnsureTrailingUrlSegmentSeparatorChar(this string path)
    {
      if (!path.EndsWith("/", StringComparison.Ordinal))
        path += "/";
      return path;
    }

    public static string SanitizeToTitle(this string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      value = Regex.Replace(value, "%([a-fA-F0-9][a-fA-F0-9])", "---$1---");
      value = Regex.Replace(value, "%", string.Empty);
      value = Regex.Replace(value, "---([a-fA-F0-9][a-fA-F0-9])---", "%$1");
      value = value.StripLatinDiacritics().ToLowerInvariant();
      value = StringExtensions._killEntities.Replace(value, string.Empty);
      value = Regex.Replace(value, "[^%a-z0-9 _-]", string.Empty);
      value = StringExtensions._killExtraSpaces.Replace(value, "-");
      value = StringExtensions._killExtraMinus.Replace(value, "-");
      return value.Trim('-');
    }

    public static string SanitizeToFilename(this string filename)
    {
      if (string.IsNullOrEmpty(filename))
        return string.Empty;
      filename = filename.StripLatinDiacritics().ToLowerInvariant();
      filename = StringExtensions._killEntities.Replace(filename, string.Empty);
      filename = filename.Replace('_', '-');
      filename = StringExtensions._killNonLatin.Replace(filename, string.Empty);
      filename = StringExtensions._killExtraSpaces.Replace(filename, "-");
      filename = StringExtensions._killExtraMinus.Replace(filename, "-");
      return filename.Trim('-');
    }

    public static string LeftOf(this string value, char characterDelimiter)
    {
      if (string.IsNullOrEmpty(value))
        return value;
      int length = value.IndexOf(characterDelimiter);
      if (length != -1)
        return value.Substring(0, length);
      return value;
    }

    public static string LeftOf(this string value, char characterDelimiter, int pN)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      int length = -1;
      for (; pN != 0; --pN)
      {
        length = value.IndexOf(characterDelimiter, length + 1);
        if (length == -1)
          return value;
      }
      return value.Substring(0, length);
    }

    public static string RightOf(this string pSrc, char pC)
    {
      if (string.IsNullOrEmpty(pSrc))
        return string.Empty;
      int num = pSrc.IndexOf(pC);
      if (num != -1)
        return pSrc.Substring(num + 1);
      return "";
    }

    public static string RightOf(this string pSrc, char pC, int pN)
    {
      if (string.IsNullOrEmpty(pSrc))
        return string.Empty;
      int num = -1;
      for (; pN != 0; --pN)
      {
        num = pSrc.IndexOf(pC, num + 1);
        if (num == -1)
          return "";
      }
      return pSrc.Substring(num + 1);
    }

    public static string Compress(this string pText)
    {
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      byte[] bytes = Encoding.UTF8.GetBytes(pText);
      MemoryStream memoryStream = new MemoryStream();
      using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Compress, true))
        deflateStream.Write(bytes, 0, bytes.Length);
      memoryStream.Position = 0L;
      byte[] buffer = new byte[memoryStream.Length];
      memoryStream.Read(buffer, 0, buffer.Length);
      byte[] inArray = new byte[buffer.Length + 4];
      Buffer.BlockCopy((Array) buffer, 0, (Array) inArray, 4, buffer.Length);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(bytes.Length), 0, (Array) inArray, 0, 4);
      return Convert.ToBase64String(inArray);
    }

    public static string Decompress(this string pCompressedText)
    {
      if (string.IsNullOrEmpty(pCompressedText))
        return string.Empty;
      byte[] buffer = Convert.FromBase64String(pCompressedText);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        int int32 = BitConverter.ToInt32(buffer, 0);
        memoryStream.Write(buffer, 4, buffer.Length - 4);
        byte[] numArray = new byte[int32];
        memoryStream.Position = 0L;
        using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Decompress))
          deflateStream.Read(numArray, 0, numArray.Length);
        return Encoding.UTF8.GetString(numArray);
      }
    }

    public static string Capitalize(this string pString)
    {
      if (!string.IsNullOrEmpty(pString))
        return Regex.Replace(pString, "[\\w']+", new MatchEvaluator(StringExtensions.CapitalizeString));
      return string.Empty;
    }

    private static string CapitalizeString(Match pMatchString)
    {
      string str = pMatchString.ToString();
      return ((int) char.ToUpper(str[0])).ToString() + str.Substring(1, str.Length - 1);
    }

    public static string SmartRightTrim(this string pText)
    {
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      int pMaxLength = pText.IndexOf('.') + 1;
      if (pMaxLength == 0)
        pMaxLength = pText.Length;
      return pText.SmartRightTrim(pMaxLength, " …");
    }

    public static string SmartRightTrim(this string pText, int pMaxLength, string pEllipsis = " …")
    {
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      pText = pText.Trim();
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      if (pText.Length <= pMaxLength)
        return pText;
      pText = pText.Substring(0, pMaxLength);
      int length = pText.LastIndexOf(' ');
      if (length != -1)
        pText = pText.Substring(0, length);
      return pText.RemoveTrailingPunctuation() + pEllipsis;
    }

    public static string SmartMiddleTrim(this string pText, int pMaxLength, string pEllipsis = " … ")
    {
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      pText = pText.Trim();
      if (string.IsNullOrEmpty(pText))
        return string.Empty;
      if (pText.Length < pMaxLength)
        return pText;
      int length = pMaxLength / 2;
      string str1;
      string str2;
      if (pText.IndexOf(' ') == -1)
      {
        str1 = pText.Substring(0, length);
        str2 = pText.Substring(pText.Length - length);
      }
      else
      {
        str2 = pText.Substring(pText.Length - length).Trim();
        int num = str2.LastIndexOf(' ');
        if (num != -1)
          str2 = str2.Substring(num + 1);
        str1 = pText.SmartRightTrim(pMaxLength - str2.Length, string.Empty);
      }
      return str1 + pEllipsis + str2;
    }

    public static string RemoveTrailingPunctuation(this string pText)
    {
      if (!string.IsNullOrEmpty(pText))
        return StringExtensions._rtp.Replace(pText, string.Empty);
      return string.Empty;
    }

    public static string StripLatinDiacritics(this string pString)
    {
      if (string.IsNullOrEmpty(pString))
        return string.Empty;
      string source = pString.Normalize(NormalizationForm.FormD);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char ch in source.Select(c => new{ c = c, uc = CharUnicodeInfo.GetUnicodeCategory(c) }).Where(_param0 => _param0.uc != UnicodeCategory.NonSpacingMark).Select(_param0 => _param0.c))
        stringBuilder.Append(ch);
      return stringBuilder.ToString();
    }

    public static string[] ToLines(this string pInputString)
    {
      if (string.IsNullOrEmpty(pInputString))
        return new string[0];
      return pInputString.Replace("\r\n", "\n").Replace("\r", "\n").Replace("舲", "\n").Replace("舳", "\n").Split(new char[1]{ '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }
  }
}
