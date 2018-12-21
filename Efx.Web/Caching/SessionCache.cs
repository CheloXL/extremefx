// Decompiled with JetBrains decompiler
// Type: Efx.Web.Caching.SessionCache
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using Efx.Core.Caching;
using Efx.Core.ExtensionMethods;
using System;
using System.Web;
using System.Web.SessionState;

namespace Efx.Web.Caching
{
  public sealed class SessionCache : ICache<string, object>
  {
    private const string NO_SESSION_ERROR = "There is no Session on the current context";

    public void Set(string key, object value)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return;
      HttpSessionState session = current.Session;
      if (session == null)
        throw new ApplicationException("There is no Session on the current context");
      lock (session.SyncRoot)
        session[key] = value;
    }

    public object Get(string key)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return (object) null;
      HttpSessionState session = current.Session;
      if (session == null)
        throw new ApplicationException("There is no Session on the current context");
      return session[key];
    }

    public bool Contains(string key)
    {
      return !this.Get(key).IsNullOrDefault<object>();
    }

    public object GetOrAdd(string key, Func<string, object> valueCreator)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return (object) null;
      HttpSessionState session = current.Session;
      if (session == null)
        throw new ApplicationException("There is no Session on the current context");
      object obj = session[key];
      if (obj != null)
        return obj;
      lock (session.SyncRoot)
        return session[key] ?? (session[key] = valueCreator(key));
    }

    public T Get<T>(string pKey)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return default (T);
      HttpSessionState session = current.Session;
      if (session == null)
        throw new ApplicationException("There is no Session on the current context");
      object obj = session[pKey];
      if (obj != null)
        return (T) obj;
      return default (T);
    }

    public void Remove(string pKey)
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return;
      HttpSessionState session = current.Session;
      if (session == null)
        throw new ApplicationException("There is no Session on the current context");
      lock (session.SyncRoot)
      {
        if (session[pKey] == null)
          return;
        session.Remove(pKey);
      }
    }

    public void Clear()
    {
      HttpContext current = HttpContext.Current;
      if (current == null)
        return;
      HttpSessionState session = current.Session;
      if (session == null)
        throw new ApplicationException("There is no Session on the current context");
      lock (session.SyncRoot)
        session.RemoveAll();
    }
  }
}
