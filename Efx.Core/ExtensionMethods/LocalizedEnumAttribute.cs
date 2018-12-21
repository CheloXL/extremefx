// Decompiled with JetBrains decompiler
// Type: Efx.Core.ExtensionMethods.LocalizedEnumAttribute
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace Efx.Core.ExtensionMethods
{
  public sealed class LocalizedEnumAttribute : DescriptionAttribute
  {
    private PropertyInfo _nameProperty;
    private Type _resourceType;

    public LocalizedEnumAttribute(string pDisplayNameKey)
      : base(pDisplayNameKey)
    {
    }

    public Type NameResourceType
    {
      get
      {
        return this._resourceType;
      }
      set
      {
        this._resourceType = value;
        this._nameProperty = this._resourceType.GetProperty(this.Description, BindingFlags.Static | BindingFlags.Public);
      }
    }

    public override string Description
    {
      get
      {
        if (this._nameProperty == (PropertyInfo) null)
          return base.Description;
        return (string) this._nameProperty.GetValue((object) this._nameProperty.DeclaringType, (object[]) null);
      }
    }
  }
}
