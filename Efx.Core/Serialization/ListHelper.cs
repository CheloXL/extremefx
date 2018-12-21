// Decompiled with JetBrains decompiler
// Type: Efx.Core.Serialization.ListHelper
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Caching;
using Efx.Core.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Efx.Core.Serialization
{
  internal static class ListHelper
  {
    private static readonly MemoryLockedCache<Type, Action<object, object[]>> _addMethods = new MemoryLockedCache<Type, Action<object, object[]>>();
    private static readonly MemoryLockedCache<Type, MethodInfo> _seemsIList = new MemoryLockedCache<Type, MethodInfo>();

    internal static Action<object, object[]> GetAddMethodDelegate(Type listType)
    {
      return ListHelper._addMethods.GetOrAdd(listType, (Func<Type, Action<object, object[]>>) (lt =>
      {
        MethodInfo listAddMethod = ListHelper.GetListAddMethod(lt);
        if (listAddMethod.ReturnType == typeof (void))
          return listAddMethod.CreateWithoutReturn();
        Func<object, object[], object> f = listAddMethod.CreateWithReturn();
        object obj;
        return (Action<object, object[]>) ((o, p) => obj = f(o, p));
      }));
    }

    internal static MethodInfo GetListAddMethod(Type listType)
    {
      return ListHelper._seemsIList.GetOrAdd(listType, new Func<Type, MethodInfo>(ListHelper.ResolveListAdd));
    }

    private static MethodInfo ResolveListAdd(Type listType)
    {
      if (!listType.IsGenericType)
        return (MethodInfo) null;
      Type[] types = new Type[1]{ listType.GetGenericArguments()[0] };
      MethodInfo method1 = listType.GetMethod("Add", types);
      if (method1 != (MethodInfo) null)
        return method1;
      Type type = typeof (ICollection<>).MakeGenericType(types);
      if (type.IsAssignableFrom(listType))
      {
        MethodInfo method2 = type.GetMethod("Add", types);
        if (method2 != (MethodInfo) null)
          return method2;
      }
      types[0] = typeof (object);
      MethodInfo method3 = listType.GetMethod("Add", types);
      if (method3 != (MethodInfo) null)
        return method3;
      if (!typeof (IList).IsAssignableFrom(listType))
        return (MethodInfo) null;
      return typeof (IList).GetMethod("Add", types);
    }
  }
}
