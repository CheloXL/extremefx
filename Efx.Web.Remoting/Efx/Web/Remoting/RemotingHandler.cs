// Decompiled with JetBrains decompiler
// Type: Efx.Web.Remoting.RemotingHandler
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core.Conversion;
using Efx.Core.Crypto;
using Efx.Core.ExtensionMethods;
using Efx.Core.Reflection;
using Efx.Web.Remoting.Serializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

namespace Efx.Web.Remoting
{
  public sealed class RemotingHandler : IHttpHandler, IRequiresSessionState
  {
    private static readonly Regex regex_0 = new Regex("^[a-zA-Z_][a-zA-Z0-9_']*$", RegexOptions.IgnoreCase);
    private HttpContext httpContext_0;
    private byte[] byte_0;
    private HttpRequest httpRequest_0;
    private HttpResponse httpResponse_0;
    private IRemoteSerializer iremoteSerializer_0;
    private RemotingConfiguration remotingConfiguration_0;

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

    public void ProcessRequest(HttpContext context)
    {
      if (context == null)
        return;
      this.httpRequest_0 = context.Request;
      this.httpResponse_0 = context.Response;
      this.httpContext_0 = context;
      this.remotingConfiguration_0 = Configuration.Data;
      this.httpResponse_0.ContentEncoding = Encoding.UTF8;
      if (!this.httpRequest_0.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
      {
        this.method_7();
      }
      else
      {
        this.byte_0 = context.Session["X-Crypto-SecretKey"] as byte[];
        if (this.remotingConfiguration_0.Encrypt && this.byte_0 == null && this.method_3())
          return;
        string string_0 = this.method_0("X-Serializer");
        if (!this.method_2(string_0, out this.iremoteSerializer_0))
        {
          this.method_6(404, string.Format("The requested serializer [{0}] nor the default one [{1}] were found.", (object) string_0, (object) this.remotingConfiguration_0.DefaultSerializer));
        }
        else
        {
          byte[] numArray = this.method_1();
          if (this.remotingConfiguration_0.Encrypt)
          {
            if (this.byte_0 != null)
            {
              if (this.byte_0.Length > 0)
                numArray = XXTea.Decrypt(numArray, this.byte_0);
            }
          }
          RemotingRequest remotingRequest_0;
          try
          {
            remotingRequest_0 = (RemotingRequest) this.iremoteSerializer_0.FromByteArray(numArray, typeof (RemotingRequest));
          }
          catch (Exception ex)
          {
            this.method_5(-32700, "Parse error.", "Invalid serialized packed. An error occurred on the server while parsing the request data.", (RemotingRequest) null);
            return;
          }
          if (remotingRequest_0 == null)
          {
            this.method_5(-32600, "Invalid Request.", "The received request is not a valid RPC Request.", (RemotingRequest) null);
          }
          else
          {
            Class114 class114 = Class117.smethod_1(remotingRequest_0.Method.ToLowerInvariant());
            if (class114 == null)
            {
              this.method_5(-32601, "Method not found.", "The requested remote-procedure does not exist / is not available.", remotingRequest_0);
            }
            else
            {
              List<object> objectList = new List<object>((IEnumerable<object>) (remotingRequest_0.Params ?? new object[0]));
              if (objectList.Count != class114.parameterInfo_0.Length)
              {
                this.method_5(-32602, "Invalid params.", "Invalid method parameters.", remotingRequest_0);
              }
              else
              {
                int index = 0;
                foreach (ParameterInfo parameterInfo in class114.parameterInfo_0)
                {
                  object o = this.iremoteSerializer_0.TryCastTo(objectList[index], parameterInfo.ParameterType);
                  if (!parameterInfo.ParameterType.IsInstanceOfType(o))
                    o = o.Convert(parameterInfo.ParameterType);
                  objectList[index] = o;
                  ++index;
                }
                if (class114.bool_0 && !this.httpRequest_0.IsAuthenticated)
                {
                  this.method_6(401, "Unauthorized");
                }
                else
                {
                  object instance = Class117.smethod_2(class114.type_0);
                  if (instance == null)
                  {
                    this.method_5(-32603, "Can not instantiate the base class.", (string) null, remotingRequest_0);
                  }
                  else
                  {
                    RemotingResponse remotingResponse_0 = new RemotingResponse()
                    {
                      Id = remotingRequest_0.Id
                    };
                    try
                    {
                      object obj = instance.GetType().GetMethodInfo(class114.string_1, (ICollection<object>) objectList).Execute<object>(instance, (IEnumerable<object>) objectList);
                      if (string.IsNullOrEmpty(remotingRequest_0.Id))
                        return;
                      remotingResponse_0.Result = obj;
                      this.method_4(remotingResponse_0);
                    }
                    catch (Exception ex)
                    {
                      this.method_5(-32500, "Error trying to execute the requested method.", RemotingHandler.smethod_0(ex), remotingRequest_0);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    private string method_0(string string_0)
    {
      string[] values = this.httpRequest_0.Headers.GetValues(string_0);
      if (values != null && values.Length != 0)
        return values[0];
      return (string) null;
    }

    private byte[] method_1()
    {
      Stream inputStream = this.httpRequest_0.InputStream;
      inputStream.Seek(0L, SeekOrigin.Begin);
      int int32 = System.Convert.ToInt32(inputStream.Length);
      byte[] buffer = new byte[inputStream.Length];
      inputStream.Read(buffer, 0, int32);
      return buffer;
    }

    private static string smethod_0(Exception exception_0)
    {
      StackTrace stackTrace = new StackTrace(exception_0, true);
      StringBuilder stringBuilder1 = new StringBuilder(exception_0.Message);
      StackFrame[] frames = stackTrace.GetFrames();
      if (frames != null)
      {
        List<string> stringList = new List<string>();
        int length = frames.Length;
        for (int index = 0; index < length; ++index)
        {
          StackFrame stackFrame = frames[index];
          MethodBase method1 = stackFrame.GetMethod();
          string name = method1.Name;
          int fileLineNumber = stackFrame.GetFileLineNumber();
          if ((!method1.IsPrivate || fileLineNumber != 0) && (!(method1.DeclaringType != (Type) null) || method1.DeclaringType.FullName == null || !method1.DeclaringType.FullName.StartsWith("Efx.Core.Reflection", StringComparison.Ordinal)))
          {
            if (name.Equals("lambda_method", StringComparison.Ordinal) && index - 1 < length)
            {
              MethodBase method2 = frames[index + 1].GetMethod();
              if (method2.Name.Equals("Execute", StringComparison.Ordinal) && method2.DeclaringType.FullName.Equals("Efx.Core.Reflection.MethodInfoExtensions", StringComparison.Ordinal))
                break;
            }
            if (RemotingHandler.regex_0.IsMatch(name))
            {
              StringBuilder stringBuilder2 = new StringBuilder(name);
              string fileName = stackFrame.GetFileName();
              if (!string.IsNullOrEmpty(fileName))
              {
                FileInfo fileInfo = new FileInfo(fileName);
                stringBuilder2.AppendFormat("[{0}", (object) fileInfo.Name);
                if (fileLineNumber != 0)
                  stringBuilder2.AppendFormat("@{0}", (object) fileLineNumber);
                stringBuilder2.Append(']');
              }
              stringList.Add(stringBuilder2.ToString());
            }
          }
        }
        if (stringList.Count > 0)
        {
          stringBuilder1.Append(": ");
          stringBuilder1.Append(string.Join(" > ", stringList.ToArray()));
        }
      }
      return stringBuilder1.ToString();
    }

    private bool method_2(string string_0, out IRemoteSerializer iremoteSerializer_1)
    {
      string_0 = (string.IsNullOrEmpty(string_0) ? this.remotingConfiguration_0.DefaultSerializer : string_0).ToLowerInvariant();
      iremoteSerializer_1 = Class117.smethod_0(string_0);
      return iremoteSerializer_1 != null;
    }

    private bool method_3()
    {
      string pRequest = this.method_0("X-Crypto-Key");
      string s = this.method_0("X-Crypto-Length");
      if (this.httpContext_0.Session == null || string.IsNullOrEmpty(pRequest) || string.IsNullOrEmpty(s))
        return false;
      DiffieHellman diffieHellman = new DiffieHellman(int.Parse(s));
      this.httpResponse_0.Write(diffieHellman.GenerateResponse(pRequest));
      this.httpContext_0.Session["X-Crypto-SecretKey"] = (object) diffieHellman.Key;
      this.httpContext_0.Response.Cookies.Add(new HttpCookie("X-Crypto-SecretKey", "1"));
      return true;
    }

    private void method_4(RemotingResponse remotingResponse_0)
    {
      byte[] numArray1 = this.iremoteSerializer_0.ToByteArray((object) remotingResponse_0);
      if (this.remotingConfiguration_0.Encrypt && this.byte_0 != null && this.byte_0.Length > 0)
        numArray1 = XXTea.Encrypt(numArray1, this.byte_0);
      this.httpResponse_0.StatusCode = 200;
      this.httpResponse_0.ContentType = this.iremoteSerializer_0.EncodingType;
      CompressionMethod compressionMethod = WebUtilities.GetCompressionMethod(this.httpRequest_0);
      if (compressionMethod != CompressionMethod.None)
      {
        byte[] numArray2 = numArray1.Compress(compressionMethod);
        if (numArray2.Length < numArray1.Length)
        {
          this.httpResponse_0.AppendHeader("Content-Encoding", compressionMethod == CompressionMethod.Deflate ? "deflate" : "gzip");
          this.httpResponse_0.Cache.VaryByHeaders["Accept-Encoding"] = true;
          numArray1 = numArray2;
        }
      }
      if (numArray1 == null)
        return;
      this.httpResponse_0.AppendHeader("Content-Length", numArray1.Length.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      this.httpResponse_0.BinaryWrite(numArray1);
    }

    private void method_5(
      int int_0,
      string string_0,
      string string_1,
      RemotingRequest remotingRequest_0)
    {
      this.method_4(new RemotingResponse()
      {
        Id = remotingRequest_0 == null ? (string) null : remotingRequest_0.Id,
        Error = new RemotingError()
        {
          Code = int_0,
          Message = string_0,
          Data = string_1
        }
      });
    }

    private void method_6(int int_0, string string_0)
    {
      this.httpResponse_0.StatusCode = int_0;
      this.httpResponse_0.StatusDescription = (string_0.Length > 512 ? string_0.Substring(0, 500) : string_0).Replace('\r', ' ').Replace('\n', ' ');
      this.httpResponse_0.Write(string_0);
    }

    private void method_7()
    {
      this.httpResponse_0.ContentType = "text/html";
      StringBuilder stringBuilder = new StringBuilder("<html><body><h1>Available methods</h1><ul>");
      foreach (KeyValuePair<string, Class114> keyValuePair in Class117.smethod_4())
      {
        stringBuilder.AppendFormat("<li>{0} (", (object) keyValuePair.Value.string_0);
        ParameterInfo[] parameters = keyValuePair.Value.methodInfo_0.GetParameters();
        if (parameters.Length > 0)
        {
          foreach (ParameterInfo parameterInfo in parameters)
            stringBuilder.AppendFormat("{1} {0}, ", (object) parameterInfo.Name, (object) HttpUtility.HtmlEncode(RemotingHandler.smethod_1(parameterInfo.ParameterType)));
          stringBuilder.Remove(stringBuilder.Length - 2, 2);
        }
        stringBuilder.Append(")</li>");
      }
      stringBuilder.Append("</ul></body></html>");
      this.httpResponse_0.Write(stringBuilder.ToString());
    }

    private static string smethod_1(Type type_0)
    {
      string name = type_0.Name;
      if (!type_0.IsGenericType)
        return name;
      StringBuilder stringBuilder = new StringBuilder(name.Substring(0, name.IndexOf('`')));
      stringBuilder.Append('<');
      foreach (Type genericArgument in type_0.GetGenericArguments())
        stringBuilder.AppendFormat("{0}, ", (object) RemotingHandler.smethod_1(genericArgument));
      stringBuilder.Remove(stringBuilder.Length - 2, 2);
      stringBuilder.Append('>');
      return stringBuilder.ToString();
    }
  }
}
