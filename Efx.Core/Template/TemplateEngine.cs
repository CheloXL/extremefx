// Decompiled with JetBrains decompiler
// Type: Efx.Core.Template.TemplateEngine
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Hashing;
using Efx.Core.Reflection;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Text;

namespace Efx.Core.Template
{
  public sealed class TemplateEngine
  {
    private static readonly Dictionary<string, Assembly> _preCompiled = new Dictionary<string, Assembly>();
    private static readonly Dictionary<string, string> _options = new Dictionary<string, string>() { { "CompilerVersion", "v3.5" }, { "optimize", "true" }, { "target", "library" } };

    public string Render(string pTemplate, TemplateContext pContext, bool pHtmlEscape = false)
    {
      pContext.AddType<TemplateContext>();
      string hexHash = pTemplate.GetHexHash(HashType.Sha1);
      Assembly pAsm;
      lock (TemplateEngine._preCompiled)
      {
        if (!TemplateEngine._preCompiled.TryGetValue(hexHash, out pAsm))
        {
          string str1 = new TemplateParser(pHtmlEscape).Parse(pTemplate, hexHash);
          StringBuilder stringBuilder1 = new StringBuilder();
          HashSet<string> pAssemblies = new HashSet<string>();
          HashSet<string> pNamespaces = new HashSet<string>();
          TemplateEngine.AddTypeInformation(typeof (object), pAssemblies, pNamespaces);
          TemplateEngine.AddTypeInformation(typeof (StringBuilder), pAssemblies, pNamespaces);
          TemplateEngine.AddTypeInformation(typeof (Action), pAssemblies, pNamespaces);
          foreach (KeyValuePair<string, object> variable in pContext.Variables)
          {
            if (variable.Value == null)
            {
              stringBuilder1.AppendFormat("var {0} = pContext.GetVariable(\"{0}\");", (object) variable.Key);
            }
            else
            {
              Type type = variable.Value.GetType();
              pContext.AddType(type);
              stringBuilder1.AppendFormat("var {0} = ({1})pContext.GetVariable(\"{0}\");", (object) variable.Key, (object) type.GetCSharpName());
            }
            stringBuilder1.AppendLine();
          }
          foreach (Type type in pContext.Types)
            TemplateEngine.AddTypeInformation(type, pAssemblies, pNamespaces);
          StringBuilder stringBuilder2 = new StringBuilder();
          foreach (string str2 in pNamespaces)
            stringBuilder2.AppendFormat("using {0};\r\n", (object) str2);
          pAsm = this.Compile(str1.Replace("**EFX-CODE-HERE**", stringBuilder1.ToString()).Replace("**EFX-USING-HERE**", stringBuilder2.ToString()), pAssemblies);
          TemplateEngine._preCompiled.Add(hexHash, pAsm);
        }
      }
      return TemplateEngine.Execute(pAsm, pContext);
    }

    private static void AddTypeInformation(
      Type pType,
      HashSet<string> pAssemblies,
      HashSet<string> pNamespaces)
    {
      pAssemblies.Add(pType.Assembly.Location);
      pNamespaces.Add(pType.Namespace);
      if (!pType.IsGenericType)
        return;
      foreach (Type genericArgument in pType.GetGenericArguments())
        TemplateEngine.AddTypeInformation(genericArgument, pAssemblies, pNamespaces);
    }

    private static string Execute(Assembly pAsm, TemplateContext pContext)
    {
      foreach (Type type in pAsm.GetModules(false)[0].GetTypes())
      {
        MethodInfo method = type.GetMethod("Render", BindingFlags.Static | BindingFlags.Public);
        if (!(method == (MethodInfo) null))
        {
          try
          {
            return (string) method.Invoke((object) null, (object[]) new TemplateContext[1]{ pContext });
          }
          catch (TargetInvocationException ex)
          {
            throw ex.InnerException;
          }
        }
      }
      return (string) null;
    }

    [SecuritySafeCritical]
    private Assembly Compile(string pTemplate, HashSet<string> pAssemblies)
    {
      CodeDomProvider codeDomProvider = (CodeDomProvider) new CSharpCodeProvider((IDictionary<string, string>) TemplateEngine._options);
      CompilerParameters options = new CompilerParameters() { GenerateExecutable = false, GenerateInMemory = true, TreatWarningsAsErrors = false };
      pAssemblies.Add(typeof (object).Assembly.Location);
      pAssemblies.Add(this.GetType().Assembly.Location);
      List<string> stringList = new List<string>();
      foreach (string pAssembly in pAssemblies)
        stringList.Add(pAssembly);
      options.ReferencedAssemblies.AddRange(stringList.ToArray());
      CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(options, pTemplate);
      if (compilerResults.Errors.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder("Compiler Errors :\r\n");
        foreach (CompilerError error in (CollectionBase) compilerResults.Errors)
          stringBuilder.AppendFormat("Line {0},{1}\t: {2}\n", (object) error.Line, (object) error.Column, (object) error.ErrorText);
        throw new TemplateParsingException(stringBuilder.ToString());
      }
      return compilerResults.CompiledAssembly;
    }
  }
}
