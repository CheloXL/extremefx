// Decompiled with JetBrains decompiler
// Type: Efx.Core.Template.TemplateContext
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Efx.Core.Template
{
  public sealed class TemplateContext
  {
    private static readonly Type _typeOfObject = typeof (object);
    private readonly HashSet<Type> _types = new HashSet<Type>();
    private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();

    public IEnumerable<Type> Types
    {
      get
      {
        return (IEnumerable<Type>) this._types;
      }
    }

    public IEnumerable<KeyValuePair<string, object>> Variables
    {
      get
      {
        return (IEnumerable<KeyValuePair<string, object>>) this._variables;
      }
    }

    public void AddType<T>()
    {
      this.AddType(typeof (T));
    }

    public void AddType(Type pType)
    {
      if (pType == (Type) null || pType == TemplateContext._typeOfObject)
        return;
      Type pType1 = pType;
      do
      {
        this._types.Add(pType1);
        pType1 = pType1.BaseType;
        this.AddType(pType1);
      }
      while (pType1 != (Type) null);
      foreach (Type genericArgument in pType.GetGenericArguments())
      {
        if (!this._types.Contains(genericArgument))
          this.AddType(genericArgument);
      }
      foreach (Type pType2 in pType.GetInterfaces())
      {
        if (!this._types.Contains(pType2))
          this.AddType(pType2);
      }
    }

    public void AddVariable(string pName, object pValue)
    {
      this._variables[pName] = pValue;
    }

    [Browsable(false)]
    public object GetVariable(string pName)
    {
      object obj;
      if (!this._variables.TryGetValue(pName, out obj))
        return (object) null;
      return obj;
    }
  }
}
