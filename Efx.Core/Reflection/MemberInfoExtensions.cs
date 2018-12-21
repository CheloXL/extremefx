// Decompiled with JetBrains decompiler
// Type: Efx.Core.Reflection.MemberInfoExtensions
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Efx.Core.Reflection
{
  public static class MemberInfoExtensions
  {
    private static readonly MemoryLockedCache<MemberInfo, Func<object, object>> _getCache = new MemoryLockedCache<MemberInfo, Func<object, object>>();
    private static readonly MemoryLockedCache<MemberInfo, Action<object, object>> _setCache = new MemoryLockedCache<MemberInfo, Action<object, object>>();
    private static readonly Type _objectType = typeof (object);
    private static readonly Type _voidType = typeof (void);

    public static Type GetMemberType(this MemberInfo memberInfo)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException(nameof (memberInfo));
      if (memberInfo.MemberType != MemberTypes.Property)
        return ((FieldInfo) memberInfo).FieldType;
      return ((PropertyInfo) memberInfo).PropertyType;
    }

    public static Type TryGetMemberType(this MemberInfo memberInfo)
    {
      if (memberInfo == (MemberInfo) null)
        return (Type) null;
      if (memberInfo.MemberType != MemberTypes.Property)
        return ((FieldInfo) memberInfo).FieldType;
      return ((PropertyInfo) memberInfo).PropertyType;
    }

    public static void TrySetValue(this MemberInfo memberInfo, object instance, object value)
    {
      if (memberInfo == (MemberInfo) null || instance == null)
        return;
      memberInfo.SetValueInternal(instance, value);
    }

    public static void SetValue(this MemberInfo memberInfo, object instance, object value)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException(nameof (memberInfo));
      if (instance == null)
        throw new ArgumentNullException(nameof (instance));
      memberInfo.SetValueInternal(instance, value);
    }

    private static void SetValueInternal(this MemberInfo memberInfo, object instance, object value)
    {
      MemberInfoExtensions._setCache.GetOrAdd(memberInfo, (Func<MemberInfo, Action<object, object>>) (arg =>
      {
        if (arg.MemberType != MemberTypes.Field)
          return MemberInfoExtensions.CreateSetMethod((PropertyInfo) arg);
        return MemberInfoExtensions.CreateSetField(arg.DeclaringType, (FieldInfo) arg);
      }))(instance, value);
    }

    public static object ReadValue(this MemberInfo memberInfo, object instance)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException(nameof (memberInfo));
      if (instance == null)
        throw new ArgumentNullException(nameof (instance));
      return MemberInfoExtensions.ReadValueInternal(memberInfo, instance);
    }

    public static object TryReadValue(this MemberInfo memberInfo, object instance)
    {
      if (!(memberInfo == (MemberInfo) null) && instance != null)
        return MemberInfoExtensions.ReadValueInternal(memberInfo, instance);
      return (object) null;
    }

    private static object ReadValueInternal(MemberInfo memberInfo, object instance)
    {
      return MemberInfoExtensions._getCache.GetOrAdd(memberInfo, new Func<MemberInfo, Func<object, object>>(MemberInfoExtensions.OnValueCreator))(instance);
    }

    private static Func<object, object> OnValueCreator(MemberInfo memberInfo)
    {
      Type declaringType = memberInfo.DeclaringType;
      if (declaringType.IsValueType)
      {
        if (memberInfo.MemberType == MemberTypes.Field)
          return new Func<object, object>((memberInfo as FieldInfo).GetValue);
        PropertyInfo prop = memberInfo as PropertyInfo;
        return (Func<object, object>) (o => prop.GetValue(o, (object[]) null));
      }
      DynamicMethod dynamicMethod = new DynamicMethod("_getter", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, MemberInfoExtensions._objectType, new Type[1]{ MemberInfoExtensions._objectType }, declaringType.IsArray ? declaringType.GetElementType() : declaringType, true);
      ILGenerator ilGenerator1 = dynamicMethod.GetILGenerator();
      ilGenerator1.Emit(OpCodes.Ldarg_0);
      ilGenerator1.Emit(OpCodes.Castclass, declaringType);
      if (memberInfo.MemberType == MemberTypes.Field)
      {
        FieldInfo field = memberInfo as FieldInfo;
        ilGenerator1.Emit(OpCodes.Ldfld, field);
        if (field.FieldType.IsValueType)
          ilGenerator1.Emit(OpCodes.Box, field.FieldType);
      }
      else
      {
        PropertyInfo propertyInfo = memberInfo as PropertyInfo;
        ILGenerator ilGenerator2 = ilGenerator1;
        OpCode opcode = declaringType.IsValueType ? OpCodes.Call : OpCodes.Callvirt;
        MethodInfo meth = propertyInfo.GetGetMethod();
        if ((object) meth == null)
          meth = declaringType.GetMethod("get_" + propertyInfo.Name);
        ilGenerator2.Emit(opcode, meth);
        if (propertyInfo.PropertyType.IsValueType)
          ilGenerator1.Emit(OpCodes.Box, propertyInfo.PropertyType);
      }
      ilGenerator1.Emit(OpCodes.Ret);
      return (Func<object, object>) dynamicMethod.CreateDelegate(typeof (Func<object, object>));
    }

    private static Action<object, object> CreateSetField(Type pType, FieldInfo fieldInfo)
    {
      DynamicMethod dynamicMethod = new DynamicMethod("_", MemberInfoExtensions._voidType, new Type[2]{ MemberInfoExtensions._objectType, MemberInfoExtensions._objectType }, pType, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      if (fieldInfo.FieldType.IsValueType)
        ilGenerator.Emit(OpCodes.Unbox_Any, fieldInfo.FieldType);
      ilGenerator.Emit(OpCodes.Stfld, fieldInfo);
      ilGenerator.Emit(OpCodes.Ret);
      return (Action<object, object>) dynamicMethod.CreateDelegate(typeof (Action<object, object>));
    }

    private static Action<object, object> CreateSetMethod(PropertyInfo pPropertyInfo)
    {
      MethodInfo setMethod = pPropertyInfo.GetSetMethod();
      if (setMethod == (MethodInfo) null)
        return (Action<object, object>) null;
      DynamicMethod dynamicMethod = new DynamicMethod("_", MemberInfoExtensions._voidType, new Type[2]{ MemberInfoExtensions._objectType, MemberInfoExtensions._objectType });
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Castclass, pPropertyInfo.DeclaringType);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(pPropertyInfo.PropertyType.IsClass ? OpCodes.Castclass : OpCodes.Unbox_Any, pPropertyInfo.PropertyType);
      ilGenerator.EmitCall(OpCodes.Callvirt, setMethod, (Type[]) null);
      ilGenerator.Emit(OpCodes.Ret);
      return (Action<object, object>) dynamicMethod.CreateDelegate(typeof (Action<object, object>));
    }
  }
}
