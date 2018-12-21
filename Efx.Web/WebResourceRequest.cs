// Decompiled with JetBrains decompiler
// Type: Efx.Web.WebResourceRequest
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Efx.Web
{
  public class WebResourceRequest
  {
    private readonly X509CertificateCollection _clientCertificates = new X509CertificateCollection();

    protected WebResourceRequest()
    {
      this.PostData = new Dictionary<string, string>();
      this.Headers = new WebHeaderCollection();
      this.Cookies = new CookieContainer();
      this.Method = HttpFetchMethod.Get;
      this.contentType = "application/x-www-form-urlencoded";
      this.Agent = "Opera/9.6 (Windows NT 5.1; U; en)";
      this.Referrer = "";
      this.ErrorMsg = "";
      this.HttpStatusCode = HttpStatusCode.OK;
      this.FetchTime = DateTime.MinValue;
    }

    public HttpStatusCode HttpStatusCode { get; private set; }

    public string ErrorMsg { get; private set; }

    public DateTime FetchTime { get; private set; }

    public string Referrer { get; private set; }

    public WebHeaderCollection Headers { get; private set; }

    public Dictionary<string, string> PostData { get; private set; }

    public HttpFetchMethod Method { get; set; }

    protected string contentType { get; set; }

    public string Agent { get; set; }

    public int Timeout { get; set; }

    public CookieContainer Cookies { get; set; }

    public ICredentials Credentials { get; set; }

    public X509CertificateCollection ClientCertificates
    {
      get
      {
        return this._clientCertificates;
      }
    }

    protected virtual byte[] GetPostData()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> keyValuePair in this.PostData)
        stringBuilder.AppendFormat("{0}={1}&", (object) keyValuePair.Key, (object) keyValuePair.Value);
      return new UTF8Encoding().GetBytes(stringBuilder.ToString());
    }

    protected byte[] fetchData(string pUrl)
    {
      if (string.IsNullOrEmpty(pUrl))
        throw new ArgumentException(nameof (pUrl));
      if (!pUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        pUrl = "http://" + pUrl;
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(pUrl);
      if (this.Credentials != null)
        httpWebRequest.Credentials = this.Credentials;
      if (this.Headers.Count != 0)
        httpWebRequest.Headers = this.Headers;
      httpWebRequest.CookieContainer = this.Cookies;
      httpWebRequest.AllowAutoRedirect = true;
      httpWebRequest.UserAgent = this.Agent ?? string.Empty;
      httpWebRequest.Referer = this.Referrer ?? string.Empty;
      foreach (X509Certificate clientCertificate in this._clientCertificates)
      {
        if (clientCertificate != null)
          httpWebRequest.ClientCertificates.Add(clientCertificate);
      }
      if (this.Timeout != 0)
        httpWebRequest.Timeout = this.Timeout;
      switch (this.Method)
      {
        case HttpFetchMethod.Post:
          httpWebRequest.Method = "POST";
          break;
        case HttpFetchMethod.Put:
          httpWebRequest.Method = "PUT";
          break;
        case HttpFetchMethod.Delete:
          httpWebRequest.Method = "DELETE";
          break;
        default:
          httpWebRequest.Method = "GET";
          break;
      }
      byte[] postData = this.GetPostData();
      if (postData != null && postData.Length > 0)
      {
        if (this.Method == HttpFetchMethod.Delete || this.Method == HttpFetchMethod.Get)
          throw new HttpUnhandledException("Post information can not be used with Get/Delete methods.");
        httpWebRequest.ContentType = this.contentType;
        httpWebRequest.ContentLength = (long) postData.Length;
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(postData, 0, postData.Length);
        requestStream.Close();
      }
      this.ErrorMsg = string.Empty;
      HttpWebResponse pResponse = (HttpWebResponse) null;
      try
      {
        this.FetchTime = DateTime.Now;
        pResponse = (HttpWebResponse) httpWebRequest.GetResponse();
      }
      catch (WebException ex)
      {
        string str = (string) null;
        byte[] webResponse = WebResourceRequest.GetWebResponse(ex.Response);
        if (webResponse != null)
        {
          Decoder decoder = new UTF8Encoding().GetDecoder();
          char[] chars = new char[decoder.GetCharCount(webResponse, 0, webResponse.Length)];
          decoder.GetChars(webResponse, 0, webResponse.Length, chars, 0);
          str = new string(chars);
        }
        this.ErrorMsg = string.IsNullOrEmpty(str) ? ex.Message : str;
      }
      catch (Exception ex)
      {
        return (byte[]) null;
      }
      finally
      {
        if (pResponse != null)
          this.HttpStatusCode = pResponse.StatusCode;
      }
      try
      {
        return WebResourceRequest.GetHttpResponse(pResponse);
      }
      catch (Exception ex)
      {
        return (byte[]) null;
      }
    }

    private static byte[] GetWebResponse(WebResponse pResponse)
    {
      if (pResponse == null)
        return (byte[]) null;
      Stream responseStream = pResponse.GetResponseStream();
      MemoryStream memoryStream = new MemoryStream();
      byte[] buffer = new byte[1024];
      int count;
      while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
        memoryStream.Write(buffer, 0, count);
      return memoryStream.ToArray();
    }

    private static byte[] GetHttpResponse(HttpWebResponse pResponse)
    {
      if (pResponse == null)
        return (byte[]) null;
      Stream responseStream = pResponse.GetResponseStream();
      Stream stream = pResponse.ContentEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) <= -1 ? (pResponse.ContentEncoding.IndexOf("deflate", StringComparison.OrdinalIgnoreCase) <= -1 ? responseStream : WebResourceRequest.DecompressDeflate(responseStream)) : WebResourceRequest.DecompressGzip(responseStream);
      try
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          byte[] buffer = new byte[1024];
          int count;
          while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
            memoryStream.Write(buffer, 0, count);
          return memoryStream.ToArray();
        }
      }
      finally
      {
        stream?.Dispose();
      }
    }

    private static Stream DecompressGzip(Stream pInInputStream)
    {
      try
      {
        Stream stream = (Stream) new MemoryStream();
        byte[] buffer = new byte[4096];
        using (GZipStream gzipStream = new GZipStream(pInInputStream, CompressionMode.Decompress))
        {
          int count;
          while ((count = gzipStream.Read(buffer, 0, buffer.Length)) != 0)
            stream.Write(buffer, 0, count);
        }
        stream.Seek(0L, SeekOrigin.Begin);
        return stream;
      }
      finally
      {
        pInInputStream.Dispose();
      }
    }

    private static Stream DecompressDeflate(Stream pInInputStream)
    {
      try
      {
        Stream stream = (Stream) new MemoryStream();
        byte[] buffer = new byte[4096];
        using (DeflateStream deflateStream = new DeflateStream(pInInputStream, CompressionMode.Decompress))
        {
          int count;
          while ((count = deflateStream.Read(buffer, 0, buffer.Length)) != 0)
            stream.Write(buffer, 0, count);
        }
        stream.Seek(0L, SeekOrigin.Begin);
        return stream;
      }
      finally
      {
        pInInputStream.Dispose();
      }
    }
  }
}
