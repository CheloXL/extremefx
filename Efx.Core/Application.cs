// Decompiled with JetBrains decompiler
// Type: Efx.Core.Application
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Efx.Core
{
  public static class Application
  {
    private static readonly string _singleSeparator = Path.DirectorySeparatorChar.ToString();
    private static readonly string _doubleSeparator = ((int) Path.DirectorySeparatorChar).ToString() + Path.DirectorySeparatorChar.ToString();
    private static string _appPath;
    private static readonly char _search;
    private static readonly char _replace;
    private static readonly string _rootitem;

    public static string RootPath
    {
      get
      {
        if (!string.IsNullOrEmpty(Application._appPath))
          return Application._appPath;
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        Assembly assembly = Assembly.GetAssembly(typeof (Application));
        string directoryName = Path.GetDirectoryName(string.IsNullOrEmpty(executingAssembly.CodeBase) ? assembly.CodeBase : executingAssembly.CodeBase);
        if (string.IsNullOrEmpty(directoryName))
          throw new DirectoryNotFoundException("Can't find the base path of the executing assembly");
        string path = !directoryName.StartsWith("file:\\", StringComparison.OrdinalIgnoreCase) ? new Uri(directoryName).LocalPath : directoryName.Substring(6);
        if (path.EndsWith(((int) Path.DirectorySeparatorChar).ToString() + "bin", StringComparison.OrdinalIgnoreCase))
          path = path.Substring(0, path.Length - 4);
        if (!Directory.Exists(path))
          throw new DirectoryNotFoundException(string.Format("The Application path can not be found. Application path set at:[{0}]", (object) path));
        Application._appPath = path.EnsureTrailingDirectorySeparatorChar();
        return Application._appPath;
      }
    }

    public static string GetDirectoryPath(params string[] paths)
    {
      if (paths != null && paths.Length != 0)
        return Application.CombinePaths(paths).EnsureTrailingDirectorySeparatorChar();
      return string.Empty;
    }

    public static string GetFilePath(params string[] paths)
    {
      if (paths == null || paths.Length == 0)
        throw new ArgumentNullException(nameof (paths));
      return Application.CombinePaths(paths);
    }

    private static string FixPath(string pPath)
    {
      return pPath.Replace(Application._doubleSeparator, Application._singleSeparator);
    }

    private static string CombinePaths(params string[] pPaths)
    {
      if (pPaths == null)
        return string.Empty;
      List<string> stringList = new List<string>();
      int index1 = pPaths.Length - 1;
      for (int index2 = 0; index2 < index1; ++index2)
      {
        string pPath = pPaths[index2];
        if (!string.IsNullOrEmpty(pPath))
          stringList.Add(Application.NormalizePath(pPath).EnsureTrailingDirectorySeparatorChar());
      }
      stringList.Add(Application.NormalizePath(pPaths[index1]));
      return Application.FixPath(string.Join(Path.DirectorySeparatorChar.ToString(), stringList.ToArray()));
    }

    static Application()
    {
      if (Path.DirectorySeparatorChar.Equals('/'))
      {
        Application._search = '\\';
        Application._replace = '/';
      }
      else
      {
        Application._search = '/';
        Application._replace = '\\';
      }
      Application._rootitem = "~" + (object) Application._replace;
    }

    private static string NormalizePath(string pPath)
    {
      if (string.IsNullOrEmpty(pPath))
        return string.Empty;
      pPath = pPath.Replace(Application._search, Application._replace).Trim(Application._replace);
      if (pPath.StartsWith(Application._rootitem))
        pPath = Application.RootPath + pPath.Substring(2);
      else if (pPath.StartsWith("~"))
        pPath = Application.RootPath + pPath.Substring(1);
      return pPath;
    }
  }
}
