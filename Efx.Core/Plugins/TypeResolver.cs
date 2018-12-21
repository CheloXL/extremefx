// Decompiled with JetBrains decompiler
// Type: Efx.Core.Plugins.TypeResolver
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Debug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Efx.Core.Plugins
{
  [Serializable]
  public sealed class TypeResolver : MarshalByRefObject
  {
    private static readonly bool _fullTrust;

    static TypeResolver()
    {
      try
      {
        TypeResolver._fullTrust = SecurityManager.IsGranted((IPermission) new FileIOPermission(PermissionState.Unrestricted));
      }
      catch (Exception ex)
      {
      }
    }

    public static IEnumerable<string> GetAssignableFromType<T>(
      string pPath,
      string pFilePattern = "*.dll")
    {
      if (string.IsNullOrEmpty(pPath))
        throw new ArgumentNullException(nameof (pPath));
      if (string.IsNullOrEmpty(pFilePattern))
        throw new ArgumentNullException(nameof (pFilePattern));
      return TypeResolver.getAssignableFromType<T>((IEnumerable<string>) Array.ConvertAll<FileInfo, string>(Array.ConvertAll<string, FileInfo>(Directory.GetFiles(pPath, pFilePattern), (Converter<string, FileInfo>) (pS => new FileInfo(pS))), (Converter<FileInfo, string>) (pFi => pFi.FullName)));
    }

    private static IEnumerable<string> getAssignableFromType<T>(
      IEnumerable<string> pFiles)
    {
      AppDomain domain = AppDomain.CurrentDomain;
      if (TypeResolver._fullTrust)
        domain = AppDomain.CreateDomain("Sandbox", AppDomain.CurrentDomain.Evidence, new AppDomainSetup()
        {
          ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
          ApplicationName = "Efx_Sandbox_" + (object) Guid.NewGuid(),
          ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
          DynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase,
          LicenseFile = AppDomain.CurrentDomain.SetupInformation.LicenseFile,
          LoaderOptimization = AppDomain.CurrentDomain.SetupInformation.LoaderOptimization,
          PrivateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,
          PrivateBinPathProbe = AppDomain.CurrentDomain.SetupInformation.PrivateBinPathProbe,
          ShadowCopyFiles = "false"
        });
      try
      {
        Type type = typeof (TypeResolver);
        return ((TypeResolver) domain.CreateInstanceAndUnwrap(type.Assembly.GetName().Name, type.FullName)).getTypes(typeof (T), pFiles);
      }
      catch (Exception ex)
      {
        Trace.LogError(ex);
      }
      finally
      {
        if (TypeResolver._fullTrust)
          AppDomain.Unload(domain);
      }
      return (IEnumerable<string>) new string[0];
    }

    private IEnumerable<string> getTypes(
      Type pAssignTypeFrom,
      IEnumerable<string> pAssemblyFiles)
    {
      List<string> stringList = new List<string>();
      foreach (string pAssemblyFile in pAssemblyFiles)
      {
        if (File.Exists(pAssemblyFile))
        {
          try
          {
            foreach (Type type in Assembly.LoadFile(pAssemblyFile).GetTypes())
            {
              if (!type.IsAbstract && !type.IsInterface && pAssignTypeFrom.IsAssignableFrom(type))
                stringList.Add(type.AssemblyQualifiedName);
            }
          }
          catch (Exception ex)
          {
            Trace.LogError(string.Format("Error loading assembly: {0}", (object) pAssemblyFile), ex);
          }
        }
      }
      return (IEnumerable<string>) stringList;
    }
  }
}
