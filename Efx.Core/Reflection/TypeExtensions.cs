// Decompiled with JetBrains decompiler
// Type: Efx.Core.Reflection.TypeExtensions
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace Efx.Core.Reflection
{
  public static class TypeExtensions
  {
    private static readonly MemoryLockedCache<Type, string> _cachedCsNames = new MemoryLockedCache<Type, string>();
    private static readonly MemoryLockedCache<Type, string> _cachedNames = new MemoryLockedCache<Type, string>();
    private static readonly MemoryLockedCache<Type, Func<object>> _constructorCache = new MemoryLockedCache<Type, Func<object>>();
    private static readonly MemoryLockedCache<Type, MemoryLockedCache<string, MemberInfo>> _members = new MemoryLockedCache<Type, MemoryLockedCache<string, MemberInfo>>();
    private static readonly Regex _r1 = new Regex(", PublicKeyToken=\\w+", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex _r2 = new Regex(", Culture=\\w+", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex _r3 = new Regex(", Version=\\d+.\\d+.\\d+.\\d+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static bool CanBeNull(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type.IsValueType)
        return type.IsNullable();
      return true;
    }

    public static T CreateInstance<T>(this Type activator) where T : class
    {
      return activator.CreateInstance() as T;
    }

    public static object CreateInstance(this Type activator)
    {
      if (activator == (Type) null)
        throw new ArgumentNullException(nameof (activator));
      return TypeExtensions.CreateInstanceInternal(activator);
    }

    public static object DefaultValue(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (!type.CanBeNull())
        return Activator.CreateInstance(type);
      return (object) null;
    }

    public static T GetAttribute<T>(this Type type, bool pInherit) where T : Attribute
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      T[] customAttributes = (T[]) type.GetCustomAttributes(typeof (T), pInherit);
      if (customAttributes.Length <= 0)
        return default (T);
      return customAttributes[0];
    }

    public static T[] GetAttributes<T>(this Type type, bool pInherit) where T : Attribute
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return (T[]) type.GetCustomAttributes(typeof (T), pInherit);
    }

    public static string GetCSharpName(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return TypeExtensions._cachedCsNames.GetOrAdd(type, (Func<Type, string>) (x => TypeExtensions.ParseType(x.AssemblyQualifiedName)));
    }

    public static MemberInfo GetMemberInfo(
      this Type type,
      string memberName,
      bool isIndexer)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (string.IsNullOrEmpty(memberName))
        throw new ArgumentNullException(nameof (memberName));
      return TypeExtensions.GetMemberInfoInternal(type, memberName, isIndexer);
    }

    public static MethodInfo GetMethodInfo(
      this Type type,
      string methodName,
      ICollection<object> arguments = null)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (string.IsNullOrEmpty(methodName))
        throw new ArgumentNullException(nameof (methodName));
      return TypeExtensions.GetMethodInfoInternal((IReflect) type, methodName, arguments);
    }

    public static string GetQualifiedName(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return TypeExtensions._cachedNames.GetOrAdd(type, (Func<Type, string>) (x =>
      {
        byte[] publicKey = x.Assembly.GetName().GetPublicKey();
        string assemblyQualifiedName = x.AssemblyQualifiedName;
        int startIndex = assemblyQualifiedName.LastIndexOf(']');
        if (startIndex < 0)
          startIndex = 0;
        if (publicKey.Length != 0 && assemblyQualifiedName.IndexOf(", mscorlib,", startIndex, StringComparison.Ordinal) == -1)
          return assemblyQualifiedName;
        return TypeExtensions._r3.Replace(TypeExtensions._r2.Replace(TypeExtensions._r1.Replace(assemblyQualifiedName, string.Empty), string.Empty), string.Empty);
      }));
    }

    public static Type GetRealType(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (!type.IsNullable())
        return type;
      return type.GetGenericArguments()[0];
    }

    public static bool HasAttribute<T>(this Type type, bool pInherit) where T : Attribute
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return type.IsDefined(typeof (T), pInherit);
    }

    public static bool IsNullable(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type.IsGenericType)
        return type.GetGenericTypeDefinition() == typeof (Nullable<>);
      return false;
    }

    public static bool TryCreateInstance<T>(this Type activator, out T instance) where T : class
    {
      if (activator == (Type) null)
      {
        instance = default (T);
        return false;
      }
      try
      {
        instance = TypeExtensions.CreateInstanceInternal(activator) as T;
        return true;
      }
      catch (Exception ex)
      {
        instance = default (T);
        return false;
      }
    }

    public static bool TryCreateInstance(this Type activator, out object instance)
    {
      instance = (object) null;
      if (activator == (Type) null)
        return false;
      try
      {
        instance = TypeExtensions.CreateInstanceInternal(activator);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public static bool TryGetMemberInfo(
      this Type type,
      string memberName,
      bool isIndexer,
      out MemberInfo memberInfo)
    {
      memberInfo = (MemberInfo) null;
      if (!(type == (Type) null))
      {
        if (!string.IsNullOrEmpty(memberName))
        {
          try
          {
            memberInfo = TypeExtensions.GetMemberInfoInternal(type, memberName, isIndexer);
            return true;
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return false;
    }

    public static bool TryGetMethodInfo(
      this Type type,
      string methodName,
      ICollection<object> arguments,
      out MethodInfo methodInfo)
    {
      methodInfo = (MethodInfo) null;
      if (!(type == (Type) null))
      {
        if (!string.IsNullOrEmpty(methodName))
        {
          try
          {
            methodInfo = TypeExtensions.GetMethodInfoInternal((IReflect) type, methodName, arguments);
            return true;
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return false;
    }

    public static bool TryGetMethodInfo(
      this Type type,
      string methodName,
      Type[] arguments,
      Type[] genericTypes,
      out MethodInfo methodInfo)
    {
      methodInfo = (MethodInfo) null;
      if (!(type == (Type) null))
      {
        if (!string.IsNullOrEmpty(methodName))
        {
          try
          {
            methodInfo = TypeExtensions.GetMethodInfoInternal((IReflect) type, methodName, arguments, genericTypes);
            return true;
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return false;
    }

    private static object CreateInstanceInternal(Type activator)
    {
      return TypeExtensions._constructorCache.GetOrAdd(activator, (Func<Type, Func<object>>) (x =>
      {
        if (x.IsGenericType)
        {
          Type[] genericArguments = x.GetGenericArguments();
          x = x.GetGenericTypeDefinition();
          x = x.MakeGenericType(genericArguments);
        }
        DynamicMethod dynamicMethod = new DynamicMethod("_", x, (Type[]) null);
        ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
        ilGenerator.Emit(OpCodes.Newobj, x.GetConstructor(Type.EmptyTypes));
        ilGenerator.Emit(OpCodes.Ret);
        return (Func<object>) dynamicMethod.CreateDelegate(typeof (Func<object>));
      }))();
    }

    private static PropertyInfo GetFirstProperty(Type pType, string pMemberName)
    {
      try
      {
        return pType.GetProperty(pMemberName);
      }
      catch (AmbiguousMatchException ex)
      {
        foreach (PropertyInfo property in pType.GetProperties())
        {
          if (pMemberName.Equals(property.Name, StringComparison.Ordinal))
            return property;
        }
      }
      return (PropertyInfo) null;
    }

    private static MemberInfo GetMemberInfoInternal(
      Type type,
      string memberName,
      bool isIndexer)
    {
      return TypeExtensions._members.GetOrAdd(type, (Func<Type, MemoryLockedCache<string, MemberInfo>>) (x => new MemoryLockedCache<string, MemberInfo>())).GetOrAdd(memberName, (Func<string, MemberInfo>) (x =>
      {
        if (isIndexer)
        {
          object[] customAttributes = type.GetCustomAttributes(typeof (DefaultMemberAttribute), false);
          if (customAttributes.Length == 1)
            memberName = ((DefaultMemberAttribute) customAttributes[0]).MemberName;
        }
        PropertyInfo firstProperty = TypeExtensions.GetFirstProperty(type, memberName);
        if ((object) firstProperty != null)
          return (MemberInfo) firstProperty;
        return (MemberInfo) type.GetField(memberName);
      }));
    }

    private static MethodInfo GetMethodInfoInternal(
      IReflect type,
      string methodName,
      ICollection<object> arguments)
    {
      Type[] arguments1;
      if (arguments == null)
      {
        arguments1 = Type.EmptyTypes;
      }
      else
      {
        arguments1 = new Type[arguments.Count];
        int index = 0;
        foreach (object obj in (IEnumerable<object>) arguments)
        {
          arguments1[index] = obj == null ? typeof (void) : obj.GetType();
          ++index;
        }
      }
      return TypeExtensions.GetMethodInfoInternal(type, methodName, arguments1, (Type[]) null);
    }

    private static MethodInfo GetMethodInfoInternal(
      IReflect type,
      string methodName,
      Type[] arguments,
      Type[] genericTypes)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      if (string.IsNullOrEmpty(methodName))
        throw new ArgumentNullException(nameof (methodName));
      MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) new TypeExtensions.CustomBinder(), arguments ?? Type.EmptyTypes, (ParameterModifier[]) null);
      if (method != (MethodInfo) null && genericTypes != null && genericTypes.Length > 0)
        return method.MakeGenericMethod(genericTypes);
      return method;
    }

    private static string ParseType(string pInputType)
    {
      do
      {
        int length = pInputType.LastIndexOf(',');
        if (length != -1)
          pInputType = pInputType.Substring(0, length);
        else
          goto label_2;
      }
      while (pInputType[pInputType.Length - 1] != ']');
      goto label_3;
label_2:
      return pInputType;
label_3:
      StringBuilder stringBuilder = new StringBuilder();
      int length1 = pInputType.IndexOf('`');
      stringBuilder.Append(pInputType.Substring(0, length1));
      stringBuilder.Append("<");
      int startIndex = pInputType.IndexOf("[[", StringComparison.Ordinal) + 2;
      string str = pInputType.Substring(startIndex, pInputType.Length - startIndex - 2);
      string[] separator = new string[1]{ "],[" };
      foreach (string pInputType1 in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        string type = TypeExtensions.ParseType(pInputType1);
        stringBuilder.Append(type);
        stringBuilder.Append(",");
      }
      --stringBuilder.Length;
      stringBuilder.Append(">");
      return stringBuilder.ToString();
    }

    private sealed class CustomBinder : Binder
    {
      private static readonly Binder _defaultBinder = Type.DefaultBinder;

      public override FieldInfo BindToField(
        BindingFlags bindingAttr,
        FieldInfo[] match,
        object value,
        CultureInfo culture)
      {
        return TypeExtensions.CustomBinder._defaultBinder.BindToField(bindingAttr, match, value, culture);
      }

      public override MethodBase BindToMethod(
        BindingFlags bindingAttr,
        MethodBase[] match,
        ref object[] args,
        ParameterModifier[] modifiers,
        CultureInfo culture,
        string[] names,
        out object state)
      {
        return TypeExtensions.CustomBinder._defaultBinder.BindToMethod(bindingAttr, match, ref args, modifiers, culture, names, out state);
      }

      public override object ChangeType(object value, Type type, CultureInfo culture)
      {
        return TypeExtensions.CustomBinder._defaultBinder.ChangeType(value, type, culture);
      }

      public override void ReorderArgumentArray(ref object[] args, object state)
      {
        TypeExtensions.CustomBinder._defaultBinder.ReorderArgumentArray(ref args, state);
      }

      public override MethodBase SelectMethod(
        BindingFlags bindingAttr,
        MethodBase[] matches,
        Type[] types,
        ParameterModifier[] modifiers)
      {
        if (matches == null)
          throw new ArgumentNullException(nameof (matches));
        foreach (MethodBase match in matches)
        {
          if (TypeExtensions.CustomBinder.MethodMatches((IList<ParameterInfo>) match.GetParameters(), (IList<Type>) types))
            return match;
        }
        return TypeExtensions.CustomBinder._defaultBinder.SelectMethod(bindingAttr, matches, types, modifiers);
      }

      public override PropertyInfo SelectProperty(
        BindingFlags bindingAttr,
        PropertyInfo[] match,
        Type returnType,
        Type[] indexes,
        ParameterModifier[] modifiers)
      {
        return TypeExtensions.CustomBinder._defaultBinder.SelectProperty(bindingAttr, match, returnType, indexes, modifiers);
      }

      private static bool MethodMatches(IList<ParameterInfo> parameters, IList<Type> types)
      {
        if (types.Count != parameters.Count)
          return false;
        for (int index = types.Count - 1; index >= 0; --index)
        {
          if (!(types[index] == (Type) null) && !(types[index] == typeof (void)))
          {
            if (!parameters[index].ParameterType.IsAssignableFrom(types[index]))
              return false;
          }
          else if (parameters[index].ParameterType.IsValueType)
            return false;
        }
        return true;
      }
    }
  }
}
