// Decompiled with JetBrains decompiler
// Type: Class67
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Conversion;
using Efx.Core.Crypto;
using Efx.Web;
using Efx.Web.Remoting;
using Efx.Web.Remoting.Serializers;
using System;
using System.Net;
using System.Net.Security;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;

internal sealed class Class67 : WebResourceRequest
{
  private readonly string string_0;
  private RemotingRequest remotingRequest_0;
  private readonly IRemoteSerializer iremoteSerializer_0;
  private readonly byte[] byte_0;

  public Class67(string string_1, IRemoteSerializer iremoteSerializer_1, byte[] byte_1)
  {
    this.string_0 = string_1;
    this.iremoteSerializer_0 = iremoteSerializer_1;
    this.byte_0 = byte_1;
  }

  public object method_0(string string_1, Type type_0, object[] object_0)
  {
    this.Method = HttpFetchMethod.Post;
    this.Headers.Add("Accept-Encoding", "deflate,gzip");
    this.Headers.Add("X-Serializer", this.iremoteSerializer_0.Name);
    string str = Guid.NewGuid().ToString("N");
    this.remotingRequest_0 = new RemotingRequest()
    {
      Id = str,
      Method = string_1,
      Params = object_0
    };
    byte[] numArray;
    try
    {
      ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(Class67.smethod_0);
      numArray = this.fetchData(this.string_0);
    }
    finally
    {
      ServicePointManager.ServerCertificateValidationCallback -= new RemoteCertificateValidationCallback(Class67.smethod_0);
    }
    if (numArray == null)
      throw new RemotingException(string.Format("{0}{1}", this.HttpStatusCode == HttpStatusCode.OK ? (object) string.Empty : (object) ("[" + (object) this.HttpStatusCode + "]: "), (object) this.ErrorMsg));
    if (!string.IsNullOrEmpty(this.ErrorMsg))
      throw new RemotingException(this.ErrorMsg);
    if (this.byte_0 != null && this.byte_0.Length > 0)
      numArray = XXTea.Decrypt(numArray, this.byte_0);
    RemotingResponse remotingResponse = (RemotingResponse) this.iremoteSerializer_0.FromByteArray(numArray, typeof (RemotingResponse));
    if (remotingResponse == null)
      return (object) null;
    if (remotingResponse.Error != null)
      throw new RemotingException(string.IsNullOrEmpty(remotingResponse.Error.Data) ? remotingResponse.Error.Message : remotingResponse.Error.Data);
    if (remotingResponse.Result == null)
      return (object) null;
    object o = this.iremoteSerializer_0.TryCastTo(remotingResponse.Result, type_0);
    if (o != null && !type_0.IsInstanceOfType(o))
      return o.Convert(type_0);
    return o;
  }

  private static bool smethod_0(
    object object_0,
    X509Certificate x509Certificate_0,
    X509Chain x509Chain_0,
    SslPolicyErrors sslPolicyErrors_0)
  {
    return true;
  }

  protected override byte[] GetPostData()
  {
    if (this.remotingRequest_0 == null)
      return (byte[]) null;
    byte[] pData = this.iremoteSerializer_0.ToByteArray((object) this.remotingRequest_0);
    if (this.byte_0 != null && this.byte_0.Length > 0)
      pData = XXTea.Encrypt(pData, this.byte_0);
    return pData;
  }
}
