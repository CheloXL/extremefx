// Decompiled with JetBrains decompiler
// Type: Efx.Core.ExtensionMethods.ByteArrayExtensions
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Efx.Core.ExtensionMethods
{
  public static class ByteArrayExtensions
  {
    private static readonly Decoder _utf8Decode = Encoding.UTF8.GetDecoder();

    public static byte[] Compress(this byte[] pBytes, CompressionMethod pMethod)
    {
      if (pBytes == null)
        throw new ArgumentNullException(nameof (pBytes));
      using (MemoryStream memoryStream = new MemoryStream())
      {
        Stream stream;
        switch (pMethod)
        {
          case CompressionMethod.GZip:
            stream = (Stream) new GZipStream((Stream) memoryStream, CompressionMode.Compress, true);
            break;
          case CompressionMethod.Deflate:
            stream = (Stream) new DeflateStream((Stream) memoryStream, CompressionMode.Compress, true);
            break;
          default:
            return pBytes;
        }
        using (stream)
        {
          stream.Write(pBytes, 0, pBytes.Length);
          stream.Close();
          return memoryStream.ToArray();
        }
      }
    }

    public static byte[] Decompress(this byte[] pBytes, CompressionMethod pMethod)
    {
      if (pBytes == null)
        throw new ArgumentNullException(nameof (pBytes));
      using (MemoryStream memoryStream1 = new MemoryStream(pBytes))
      {
        Stream stream;
        switch (pMethod)
        {
          case CompressionMethod.GZip:
            stream = (Stream) new GZipStream((Stream) memoryStream1, CompressionMode.Decompress, true);
            break;
          case CompressionMethod.Deflate:
            stream = (Stream) new DeflateStream((Stream) memoryStream1, CompressionMode.Decompress, true);
            break;
          default:
            return pBytes;
        }
        using (stream)
        {
          using (MemoryStream memoryStream2 = new MemoryStream())
          {
            byte[] buffer = new byte[4096];
            int count;
            while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
              memoryStream2.Write(buffer, 0, count);
            stream.Close();
            return memoryStream2.ToArray();
          }
        }
      }
    }

    public static string ToEncodedString(this byte[] pBytes, Encoding pEncoding)
    {
      Decoder decoder = pEncoding.GetDecoder();
      char[] chars = new char[decoder.GetCharCount(pBytes, 0, pBytes.Length)];
      decoder.GetChars(pBytes, 0, pBytes.Length, chars, 0);
      return new string(chars);
    }

    public static string ToHexadecimal(this IEnumerable<byte> pBytes)
    {
      if (pBytes == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte pByte in pBytes)
        stringBuilder.AppendFormat("{0:x2}", (object) pByte);
      return stringBuilder.ToString();
    }

    public static string ToUtf8String(this byte[] pBytes)
    {
      if (pBytes.Length > 3 && pBytes[0] == (byte) 239 && (pBytes[1] == (byte) 187 && pBytes[1] == (byte) 191))
      {
        byte[] numArray = new byte[pBytes.Length - 3];
        Array.Copy((Array) pBytes, 3, (Array) numArray, 0, pBytes.Length - 3);
        pBytes = numArray;
      }
      char[] chars = new char[ByteArrayExtensions._utf8Decode.GetCharCount(pBytes, 0, pBytes.Length)];
      ByteArrayExtensions._utf8Decode.GetChars(pBytes, 0, pBytes.Length, chars, 0);
      return new string(chars);
    }
  }
}
