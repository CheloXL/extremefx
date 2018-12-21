// Decompiled with JetBrains decompiler
// Type: Efx.Core.Data.Expression
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System.Text;

namespace Efx.Core.Data
{
  public sealed class Expression
  {
    private readonly Expression _childExpression;
    private readonly IValue _value;

    public Expression(string pExpression)
    {
      if (string.IsNullOrEmpty(pExpression))
      {
        this._value = (IValue) new ValueType((object) null);
      }
      else
      {
        int length = pExpression.Length;
        char ch1 = pExpression[0];
        char ch2 = pExpression[length - 1];
        if ((int) ch1 == (int) ch2 && (ch1 == '"' || ch1 == '\''))
        {
          this._value = (IValue) new ValueType((object) pExpression.Substring(1, length - 2).Trim());
        }
        else
        {
          bool flag = true;
          foreach (char c in pExpression)
          {
            if (!char.IsDigit(c))
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            this._value = (IValue) new ValueType((object) int.Parse(pExpression));
          }
          else
          {
            int pStart = 0;
            StringBuilder stringBuilder = new StringBuilder();
            while (pStart < length)
            {
              char ch3 = pExpression[pStart];
              ++pStart;
              switch (ch3)
              {
                case '(':
                  this._value = (IValue) new MethodCall(stringBuilder.ToString(), Expression.GetInBetween('(', ')', pExpression, ref pStart));
                  stringBuilder.Length = 0;
                  continue;
                case '.':
                  if (stringBuilder.Length != 0)
                  {
                    this._value = (IValue) new MemberAccess(stringBuilder.ToString());
                    stringBuilder.Length = 0;
                  }
                  this._childExpression = new Expression(pExpression.Substring(pStart));
                  return;
                case '[':
                  this._value = (IValue) new IndexedAccess(stringBuilder.ToString(), new Expression(Expression.GetInBetween('[', ']', pExpression, ref pStart)));
                  stringBuilder.Length = 0;
                  continue;
                default:
                  stringBuilder.Append(ch3);
                  continue;
              }
            }
            if (stringBuilder.Length == 0)
              return;
            this._value = (IValue) new MemberAccess(stringBuilder.ToString());
          }
        }
      }
    }

    public object Evaluate(object pContext)
    {
      object pContext1 = this._value.Evaluate(pContext);
      if (this._childExpression != null)
        return this._childExpression.Evaluate(pContext1);
      return pContext1;
    }

    private static string GetInBetween(char pLeft, char pRight, string pInString, ref int pStart)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int length = pInString.Length;
      if ((int) pLeft != (int) pRight)
      {
        int num = 1;
        while (pStart < length)
        {
          char ch = pInString[pStart];
          ++pStart;
          if ((int) ch == (int) pLeft)
            ++num;
          if ((int) ch == (int) pRight)
          {
            --num;
            if (num == 0)
              break;
          }
          stringBuilder.Append(ch);
        }
      }
      return stringBuilder.ToString();
    }

    public override string ToString()
    {
      return string.Format("Expression: {0}, Subexpression:{1}", (object) this._value, (object) this._childExpression);
    }
  }
}
