// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.RemoteProxyFactory
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Web.Remoting.Serializers;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Efx.Web.Remoting
{
  public static class RemoteProxyFactory
  {
    public static T Create<T>(
      string endPoint,
      ICredentials credentials = null,
      IRemoteSerializer serializer = null,
      X509CertificateCollection clientCertificates = null)
      where T : IRemote
    {
      return (T) new Class116(typeof (T), endPoint, serializer, credentials, clientCertificates).GetTransparentProxy();
    }
  }
}
