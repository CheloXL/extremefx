// Decompiled with JetBrains decompiler
// Type: Efx.Core.Data.MemberAccess
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Reflection;

namespace Efx.Core.Data
{
  internal sealed class MemberAccess : IValue
  {
    private readonly string _name;

    public MemberAccess(string pMemberName)
    {
      this._name = pMemberName;
    }

    public object Evaluate(object pObject)
    {
      if (pObject != null)
        return pObject.GetType().GetMemberInfo(this._name, false).ReadValue(pObject);
      return (object) null;
    }

    public override string ToString()
    {
      return string.Format("Member Access: {0}", (object) this._name);
    }
  }
}
