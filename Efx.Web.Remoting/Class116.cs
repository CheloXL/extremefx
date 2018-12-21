// Decompiled with JetBrains decompiler
// Type: Class116
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Crypto;
using Efx.Web;
using Efx.Web.Remoting;
using Efx.Web.Remoting.Serializers;
using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security.Cryptography.X509Certificates;

internal sealed class Class116 : RealProxy
{
  private readonly string string_0;
  private readonly Uri uri_0;
  private readonly IRemoteSerializer iremoteSerializer_0;
  private readonly ICredentials icredentials_0;
  private readonly X509CertificateCollection x509CertificateCollection_0;
  private readonly CookieContainer cookieContainer_0;
  private byte[] byte_0;

  public Class116(
    Type type_0,
    string string_1,
    IRemoteSerializer iremoteSerializer_1,
    ICredentials icredentials_1,
    X509CertificateCollection x509CertificateCollection_1)
    : base(type_0)
  {
    this.string_0 = string_1;
    this.uri_0 = new Uri(string_1);
    this.iremoteSerializer_0 = iremoteSerializer_1;
    this.icredentials_0 = icredentials_1;
    this.x509CertificateCollection_0 = x509CertificateCollection_1;
    this.cookieContainer_0 = new CookieContainer();
  }

  private void method_0()
  {
    DiffieHellman diffieHellman = new DiffieHellman(Configuration.Data.KeyLength);
    StandardWebRequest standardWebRequest1 = new StandardWebRequest();
    standardWebRequest1.Method = HttpFetchMethod.Post;
    StandardWebRequest standardWebRequest2 = standardWebRequest1;
    standardWebRequest2.Headers.Add("X-Crypto-Key", diffieHellman.GenerateRequest());
    standardWebRequest2.Headers.Add("X-Crypto-Length", Configuration.Data.KeyLength.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    standardWebRequest2.Cookies = this.cookieContainer_0;
    standardWebRequest2.FetchData(this.string_0);
    diffieHellman.HandleResponse(standardWebRequest2.Content);
    this.byte_0 = diffieHellman.Key;
  }

  public override IMessage Invoke(IMessage msg)
  {
    IMethodCallMessage mcm = msg as IMethodCallMessage;
    if (mcm == null)
      throw new NotImplementedException();
    IRemoteSerializer iremoteSerializer_1 = this.iremoteSerializer_0 ?? Class117.smethod_0(Configuration.Data.DefaultSerializer) ?? (IRemoteSerializer) new DotNetSerializer();
    if (iremoteSerializer_1 == null)
      throw new RemotingException("The serializer can not be null");
    MethodInfo methodBase = (MethodInfo) mcm.MethodBase;
    string str = (string) null;
    if (this.cookieContainer_0 != null)
    {
      Cookie cookie = this.cookieContainer_0.GetCookies(this.uri_0)["X-Crypto-SecretKey"];
      if (cookie != null)
        str = cookie.Value;
    }
    if (Configuration.Data.Encrypt && !"1".Equals(str, StringComparison.Ordinal))
      this.method_0();
    Class67 class67_1 = new Class67(this.string_0, iremoteSerializer_1, this.byte_0);
    class67_1.Cookies = this.cookieContainer_0;
    Class67 class67_2 = class67_1;
    if (this.x509CertificateCollection_0 != null)
      class67_2.ClientCertificates.AddRange(this.x509CertificateCollection_0);
    if (this.icredentials_0 != null)
      class67_2.Credentials = this.icredentials_0;
    return (IMessage) new ReturnMessage(class67_2.method_0(string.Format("{0}.{1}", (object) methodBase.DeclaringType.FullName, (object) mcm.MethodName), methodBase.ReturnType, mcm.Args), new object[0], 0, mcm.LogicalCallContext, mcm);
  }
}
