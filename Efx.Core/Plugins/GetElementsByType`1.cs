// Decompiled with JetBrains decompiler
// Type: Efx.Core.Plugins.GetElementsByType`1
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Efx.Core.Plugins
{
  public sealed class GetElementsByType<T> where T : class, IPlugin
  {
    private Dictionary<string, T> _elementTypes;

    public T Get(IApplication pApplication, string pDllLocation, string pPluginsName)
    {
      if (string.IsNullOrEmpty(pPluginsName))
        return default (T);
      if (this._elementTypes == null)
      {
        this._elementTypes = new Dictionary<string, T>();
        foreach (T obj in TypeResolver.GetAssignableFromType<T>(pDllLocation, "*.dll").Select<string, Type>(new Func<string, Type>(Type.GetType)).Select<Type, object>(new Func<Type, object>(Activator.CreateInstance)).OfType<T>())
        {
          obj.Start(pApplication);
          this._elementTypes.Add(obj.PluginName, obj);
        }
      }
      if (!this._elementTypes.ContainsKey(pPluginsName))
        return default (T);
      return this._elementTypes[pPluginsName];
    }
  }
}
