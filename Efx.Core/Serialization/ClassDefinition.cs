// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.ClassDefinition
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using Efx.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace Efx.Core.Serialization
{
  internal sealed class ClassDefinition
  {
    private static readonly MemoryLockedCache<string, IDictionary<string, MemberInfo>> _allPropertyCache = new MemoryLockedCache<string, IDictionary<string, MemberInfo>>();
    private static readonly Type _externalizableType = typeof (IExternalizable);
    private static readonly Dictionary<string, MemberInfo> _emptyDefinitions = new Dictionary<string, MemberInfo>();
    public readonly string ClassName;
    public readonly Type ClassType;
    public readonly bool Externalizable;
    public readonly IDictionary<string, MemberInfo> SerializableMembersInfo;
    public readonly string[] LoadedMembers;

    public ClassDefinition(Type pType, string[] loadedMembers = null)
    {
      this.ClassType = pType;
      this.ClassName = pType.GetQualifiedName();
      this.LoadedMembers = loadedMembers;
      this.Externalizable = ClassDefinition._externalizableType.IsAssignableFrom(pType);
      this.SerializableMembersInfo = this.Externalizable ? (IDictionary<string, MemberInfo>) ClassDefinition._emptyDefinitions : ClassDefinition.GetProperties((IReflect) pType, this.ClassName);
    }

    private static IDictionary<string, MemberInfo> GetProperties(
      IReflect type,
      string className)
    {
      return ClassDefinition._allPropertyCache.GetOrAdd(className, (Func<string, IDictionary<string, MemberInfo>>) (x =>
      {
        IDictionary<string, MemberInfo> dictionary = (IDictionary<string, MemberInfo>) new Dictionary<string, MemberInfo>();
        foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          if (!property.HasAttribute<XmlIgnoreAttribute>(true) && property.CanRead && property.CanWrite && (property.GetIndexParameters().Length <= 0 || property.PropertyType.IsArray))
            dictionary.Add(property.Name, (MemberInfo) property);
        }
        foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
          if (!field.HasAttribute<XmlIgnoreAttribute>(true) && !field.HasAttribute<NonSerializedAttribute>(true))
            dictionary.Add(field.Name, (MemberInfo) field);
        }
        return dictionary;
      }));
    }
  }
}
