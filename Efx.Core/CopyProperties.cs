// Decompiled with JetBrains decompiler
// Type: Efx.Core.CopyProperties
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Text;

namespace Efx.Core
{
  public static class CopyProperties
  {
    private static readonly Dictionary<string, Type> _comp = new Dictionary<string, Type>();

    public static CopyProperties.ICopyTarget From<TSource>(TSource source)
    {
      return (CopyProperties.ICopyTarget) new CopyProperties.CopyTargetInfo((object) source, typeof (TSource));
    }

    public interface ICopyTarget
    {
      void To<TTarget>(TTarget target, bool skipDefaultOrNull);
    }

    private sealed class CopyTargetInfo : CopyProperties.ICopyTarget
    {
      private static readonly Dictionary<string, string> _options = new Dictionary<string, string>()
      {
        {
          "CompilerVersion",
          "v3.5"
        },
        {
          "optimize",
          "true"
        },
        {
          "target",
          "library"
        }
      };
      private readonly object _source;
      private readonly Type _sourceType;
      private Type _targetType;

      public CopyTargetInfo(object pSource, Type pType)
      {
        this._sourceType = pType;
        this._source = pSource;
      }

      private string NormalizeType(Type type)
      {
        if (!(type == (Type) null))
          return type.FullName.Replace(".", "_").Replace("+", "_");
        return "NULL";
      }

      public void To<TTarget>(TTarget target, bool skipDefaultOrNull = false)
      {
        this._targetType = typeof (TTarget);
        string index = string.Format("Copy_{0}_{1}_{2}", (object) this.NormalizeType(this._sourceType), (object) this.NormalizeType(this._targetType), (object) skipDefaultOrNull);
        Type copyClass;
        if (!CopyProperties._comp.TryGetValue(index, out copyClass))
          CopyProperties._comp[index] = copyClass = CopyProperties.CopyTargetInfo.GenerateCopyClass(index, this._sourceType, this._targetType, skipDefaultOrNull);
        copyClass.InvokeMember("CopyProps", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, (object) null, new object[2]
        {
          this._source,
          (object) target
        });
      }

      [SecuritySafeCritical]
      private static Type GenerateCopyClass(
        string pClassName,
        Type pSourceType,
        Type pTargetType,
        bool pSkipDefaultOrNull)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("namespace EfxCopyProperties {\r\n");
        stringBuilder.Append("    public static class ");
        stringBuilder.Append(pClassName);
        stringBuilder.Append(" {\r\n");
        stringBuilder.Append("        public static void CopyProps(");
        stringBuilder.Append(pSourceType.FullName);
        stringBuilder.Append(" source, ");
        stringBuilder.Append(pTargetType.FullName);
        stringBuilder.Append(" target) {\r\n");
        foreach (MemberInfo member1 in pSourceType.GetMembers(BindingFlags.Instance | BindingFlags.Public))
        {
          PropertyInfo propertyInfo1 = member1 as PropertyInfo;
          FieldInfo fieldInfo1 = member1 as FieldInfo;
          if ((!(propertyInfo1 == (PropertyInfo) null) || !(fieldInfo1 == (FieldInfo) null)) && (!(propertyInfo1 != (PropertyInfo) null) || propertyInfo1.CanRead))
          {
            MemberInfo[] member2 = pTargetType.GetMember(member1.Name, BindingFlags.Instance | BindingFlags.Public);
            if (member2.Length == 1)
            {
              MemberInfo memberInfo = member2[0];
              PropertyInfo propertyInfo2 = memberInfo as PropertyInfo;
              FieldInfo fieldInfo2 = memberInfo as FieldInfo;
              if ((!(propertyInfo2 == (PropertyInfo) null) || !(fieldInfo2 == (FieldInfo) null)) && (!(propertyInfo2 != (PropertyInfo) null) || propertyInfo2.CanWrite))
              {
                Type c = propertyInfo1 == (PropertyInfo) null ? fieldInfo1.FieldType : propertyInfo1.PropertyType;
                if (!(propertyInfo2 == (PropertyInfo) null ? fieldInfo2.FieldType : propertyInfo2.PropertyType).IsAssignableFrom(c))
                  throw new ArgumentException("Property/Field " + member1.Name + " has an incompatible type in " + pTargetType.FullName);
                if (pSkipDefaultOrNull)
                {
                  if ("System.String".Equals(c.FullName, StringComparison.Ordinal))
                    stringBuilder.AppendFormat("           if (!string.IsNullOrEmpty(source.{0}))", (object) member1.Name);
                  else if (c.IsValueType)
                    stringBuilder.AppendFormat("           if (source.{0} != default({1}))", (object) member1.Name, (object) c.FullName);
                  else
                    stringBuilder.AppendFormat("           if (source.{0} != null)", (object) member1.Name);
                }
                stringBuilder.AppendFormat("            target.{0} = source.{1};\r\n", (object) memberInfo.Name, (object) member1.Name);
              }
            }
          }
        }
        stringBuilder.Append("        }\r\n   }\r\n}");
        CodeDomProvider codeDomProvider = (CodeDomProvider) new CSharpCodeProvider((IDictionary<string, string>) CopyProperties.CopyTargetInfo._options);
        CompilerParameters options = new CompilerParameters()
        {
          GenerateExecutable = false,
          GenerateInMemory = true,
          TreatWarningsAsErrors = false
        };
        List<string> stringList = new List<string>()
        {
          typeof (object).Assembly.Location,
          pSourceType.Assembly.Location,
          pTargetType.Assembly.Location
        };
        options.ReferencedAssemblies.AddRange(stringList.ToArray());
        return codeDomProvider.CompileAssemblyFromSource(options, stringBuilder.ToString()).CompiledAssembly.GetType("EfxCopyProperties." + pClassName);
      }
    }
  }
}
