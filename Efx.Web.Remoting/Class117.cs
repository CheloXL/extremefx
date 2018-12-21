// Decompiled with JetBrains decompiler
// Type: Class117
// Assembly: Efx.Web.Remoting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 11D5333A-8A85-4DAC-8B61-C8CAFAF3E798
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.Remoting.dll

using Efx.Core;
using Efx.Core.Plugins;
using Efx.Core.Reflection;
using Efx.Web.Remoting;
using Efx.Web.Remoting.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

internal static class Class117
{
  private static readonly object object_0 = new object();
  private static Dictionary<string, IRemoteSerializer> dictionary_0;
  private static Dictionary<string, Class114> dictionary_1;
  private static Dictionary<string, string> dictionary_2;
  private static Dictionary<Type, object> dictionary_3;
  private static volatile bool bool_0;

  public static IRemoteSerializer smethod_0(string string_0)
  {
    if (string.IsNullOrEmpty(string_0))
      return (IRemoteSerializer) null;
    Class117.smethod_3();
    IRemoteSerializer remoteSerializer;
    if (!Class117.dictionary_0.TryGetValue(string_0, out remoteSerializer))
      return (IRemoteSerializer) null;
    return remoteSerializer;
  }

  public static Class114 smethod_1(string string_0)
  {
    Class117.smethod_3();
    string str;
    if (Class117.dictionary_2.TryGetValue(string_0, out str))
      string_0 = str;
    Class114 class114;
    if (!Class117.dictionary_1.TryGetValue(string_0, out class114))
      return (Class114) null;
    return class114;
  }

  public static object smethod_2(Type type_0)
  {
    Class117.smethod_3();
    object instance;
    if (Class117.dictionary_3.TryGetValue(type_0, out instance))
      return instance;
    lock (Class117.object_0)
    {
      if (!Class117.dictionary_3.TryGetValue(type_0, out instance))
      {
        if (!type_0.TryCreateInstance(out instance))
          return (object) null;
        Class117.dictionary_3.Add(type_0, instance);
      }
      return instance;
    }
  }

  private static void smethod_3()
  {
    if (Class117.bool_0)
      return;
    lock (Class117.object_0)
    {
      if (Class117.bool_0)
        return;
      Class117.bool_0 = true;
      Class117.dictionary_0 = new Dictionary<string, IRemoteSerializer>();
      Class117.dictionary_1 = new Dictionary<string, Class114>();
      Class117.dictionary_2 = new Dictionary<string, string>();
      Class117.dictionary_3 = new Dictionary<Type, object>();
      string str = Application.GetDirectoryPath("~/bin");
      if (!Directory.Exists(str))
        str = Application.RootPath;
      foreach (string typeName in TypeResolver.GetAssignableFromType<IRemoteSerializer>(str, "*.dll"))
      {
        IRemoteSerializer instance;
        if (TypeHelper.GetTypeByName(typeName).TryCreateInstance<IRemoteSerializer>(out instance))
          Class117.dictionary_0.Add(instance.Name, instance);
      }
      foreach (string typeName in TypeResolver.GetAssignableFromType<IRemote>(str, "*.dll"))
      {
        Type typeByName = TypeHelper.GetTypeByName(typeName);
        object instance;
        if (typeByName.TryCreateInstance(out instance))
        {
          foreach (Type c in typeByName.GetInterfaces())
          {
            if (!c.Name.Equals("IRemote", StringComparison.Ordinal) && typeof (IRemote).IsAssignableFrom(c))
            {
              foreach (MethodInfo method in c.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod))
              {
                if (!(method == (MethodInfo) null))
                {
                  string string_2 = string.Format("{0}.{1}", (object) method.DeclaringType.FullName, (object) method.Name);
                  Class114 class114 = new Class114(typeByName, string_2, method.GetAttribute<RequiresAuthentication>(true) != null, method, method.GetParameters(), method.Name);
                  string lowerInvariant = string_2.ToLowerInvariant();
                  if (!Class117.dictionary_1.ContainsKey(lowerInvariant))
                  {
                    Class117.dictionary_1.Add(lowerInvariant, class114);
                    int num = lowerInvariant.LastIndexOf('.');
                    if (num != -1)
                    {
                      string key = lowerInvariant.Substring(num + 1);
                      if (!Class117.dictionary_2.ContainsKey(key))
                        Class117.dictionary_2.Add(key, lowerInvariant);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  public static Dictionary<string, Class114> smethod_4()
  {
    Class117.smethod_3();
    return Class117.dictionary_1;
  }
}
