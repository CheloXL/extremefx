// Decompiled with JetBrains decompiler
// Type: Efx.Core.Data.MethodCall
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using Efx.Core.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Efx.Core.Data
{
  internal sealed class MethodCall : IValue
  {
    private readonly string _methodName;
    private readonly string _args;
    private readonly List<Expression> _parameters;

    public MethodCall(string pMethodName, string pExpression)
    {
      this._methodName = pMethodName;
      this._args = pExpression;
      this._parameters = MethodCall.GetArguments(pExpression);
    }

    public object Evaluate(object pObject)
    {
      if (pObject == null)
        return (object) null;
      object[] objArray = new object[this._parameters.Count];
      int index = 0;
      foreach (Expression parameter in this._parameters)
      {
        objArray[index] = parameter.Evaluate(pObject);
        ++index;
      }
      return pObject.GetType().GetMethodInfo(this._methodName, (ICollection<object>) objArray).Execute<object>(pObject, (IEnumerable<object>) objArray);
    }

    public override string ToString()
    {
      return string.Format("Method Call: {0}({1})", (object) this._methodName, (object) this._args);
    }

    private static List<Expression> GetArguments(string pString)
    {
      List<Expression> expressionList = new List<Expression>();
      int index = 0;
      int length = pString.Length;
label_3:
      if (index >= length)
        return expressionList;
      StringBuilder stringBuilder = new StringBuilder();
      do
      {
        int num = (int) pString[index];
        ++index;
      }
      while (index < length);
      goto label_3;
    }
  }
}
