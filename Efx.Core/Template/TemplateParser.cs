// Decompiled with JetBrains decompiler
// Type: Efx.Core.Template.TemplateParser
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Efx.Core.Template
{
  internal sealed class TemplateParser
  {
    private static readonly Dictionary<char, char> _defaultExpressionOpeners = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' } };
    private static readonly Dictionary<string, HashSet<string>> _defaultKeywords = new Dictionary<string, HashSet<string>>() { { "var", (HashSet<string>) null }, { "if", new HashSet<string>() { "else" } }, { "do", new HashSet<string>() { "while" } }, { "try", new HashSet<string>() { "catch", "finally" } }, { "for", (HashSet<string>) null }, { "foreach", (HashSet<string>) null }, { "while", (HashSet<string>) null }, { "lock", (HashSet<string>) null }, { "using", (HashSet<string>) null }, { "helper", (HashSet<string>) null } };
    private static readonly Regex _removeSpaces = new Regex("\\s+", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
    private static readonly Regex _pattern = new Regex("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private const char ARROBA = '@';
    private const string TEMPLATE = "\r\nnamespace Efx.Template.TemplateProvider {{\r\n\t**EFX-USING-HERE**\r\n\tpublic class Template{0} {{\r\n\t\tpublic static string Render(TemplateContext pContext) {{\r\n\t\t\tStringBuilder builder = new StringBuilder();\r\n\t\t\t**EFX-CODE-HERE**\r\n\t\t\t{1}\r\n\t\t\treturn builder.ToString();\r\n\t\t}}\r\n\t\t{2}\r\n\t}}\r\n}}";
    private const string TEMPLATE_HELPER = "\r\n\t\tprivate static string {0}\r\n\t\t\tStringBuilder builder = new StringBuilder();\r\n\t\t\t{1}\r\n\t\t\treturn builder.ToString();\r\n\t\t}}\r\n";
    private StringBuilder _builder;
    private StringBuilder _helpers;
    private char _closeStatement;
    private TemplateParser.States _currentState;
    private LinkedList<HashSet<string>> _nextPossibleStatements;
    private char _openStatement;
    private char[] _template;
    private int _templateLength;
    private int _templateLengthMinusOne;
    private readonly bool _escapeExpressions;

    public TemplateParser(bool pEscapeExpressions)
    {
      this._escapeExpressions = pEscapeExpressions;
    }

    private char PeekNext(int pIndex)
    {
      if (pIndex < this._templateLengthMinusOne)
        return this._template[pIndex + 1];
      return char.MinValue;
    }

    public string Parse(string pInputTemplate, string pHash)
    {
      return string.Format("\r\nnamespace Efx.Template.TemplateProvider {{\r\n\t**EFX-USING-HERE**\r\n\tpublic class Template{0} {{\r\n\t\tpublic static string Render(TemplateContext pContext) {{\r\n\t\t\tStringBuilder builder = new StringBuilder();\r\n\t\t\t**EFX-CODE-HERE**\r\n\t\t\t{1}\r\n\t\t\treturn builder.ToString();\r\n\t\t}}\r\n\t\t{2}\r\n\t}}\r\n}}", (object) pHash, (object) this.Parse(pInputTemplate), (object) this._helpers);
    }

    private string Parse(string pInputTemplate)
    {
      if (string.IsNullOrEmpty(pInputTemplate))
        return string.Empty;
      this._nextPossibleStatements = new LinkedList<HashSet<string>>();
      this._builder = new StringBuilder();
      this._helpers = new StringBuilder();
      this._currentState = TemplateParser.States.TextBlock;
      this._template = pInputTemplate.ToCharArray();
      StringBuilder pTempText = new StringBuilder();
      this._templateLength = this._template.Length;
      this._templateLengthMinusOne = this._templateLength - 1;
      string key = string.Empty;
      for (int pCloseIndex = 0; pCloseIndex < this._templateLength; ++pCloseIndex)
      {
        char ch = this._template[pCloseIndex];
        switch (this._currentState)
        {
          case TemplateParser.States.TextBlock:
            if (ch != '@')
            {
              pTempText.Append(ch);
              break;
            }
            switch (this.PeekNext(pCloseIndex))
            {
              case '(':
                this._openStatement = '(';
                this._closeStatement = ')';
                this._currentState = TemplateParser.States.Expression;
                this.AddPreviousText(pTempText);
                continue;
              case '*':
                while (pCloseIndex < this._templateLengthMinusOne)
                {
                  if (this._template[++pCloseIndex] == '*' && this._template[pCloseIndex + 1] == '@')
                  {
                    ++pCloseIndex;
                    break;
                  }
                }
                continue;
              case ':':
                ++pCloseIndex;
                continue;
              case '@':
                ++pCloseIndex;
                pTempText.Append('@');
                continue;
              case '{':
                this._openStatement = '{';
                this._closeStatement = '}';
                this._currentState = TemplateParser.States.MultilineStatement;
                this.AddPreviousText(pTempText);
                continue;
              default:
                string pAfterArroba;
                int pNewIndex;
                if (TemplateParser.IsEmail(pInputTemplate, pCloseIndex, out pAfterArroba, out pNewIndex))
                {
                  pCloseIndex = pNewIndex;
                  pTempText.Append(ch);
                  pTempText.Append(pAfterArroba);
                  continue;
                }
                key = this.ReadKeyword(pCloseIndex + 1);
                if (TemplateParser._defaultKeywords.ContainsKey(key))
                {
                  this._openStatement = '{';
                  this._closeStatement = '}';
                  this._currentState = TemplateParser.States.Statement;
                  this._nextPossibleStatements.AddLast(TemplateParser._defaultKeywords[key]);
                  this.AddPreviousText(pTempText);
                  continue;
                }
                this._closeStatement = char.MinValue;
                this._openStatement = char.MinValue;
                this._currentState = TemplateParser.States.Expression;
                this.AddPreviousText(pTempText);
                continue;
            }
          case TemplateParser.States.Statement:
            string pText1 = this.GetBeforeOpen((IList<char>) this._template, pCloseIndex);
            bool flag;
            if (flag = "helper".Equals(key, StringComparison.Ordinal))
              pText1 = pText1.Replace("helper", string.Empty);
            string pText2 = new TemplateParser(this._escapeExpressions).Parse(this.FindEnclosedText((IList<char>) this._template, pCloseIndex, out pCloseIndex));
            if (flag)
            {
              this._helpers.Append(string.Format("\r\n\t\tprivate static string {0}\r\n\t\t\tStringBuilder builder = new StringBuilder();\r\n\t\t\t{1}\r\n\t\t\treturn builder.ToString();\r\n\t\t}}\r\n", (object) pText1, (object) pText2));
            }
            else
            {
              this.AddCode(pText1);
              this.AddCode(pText2);
              this.AddCode("}");
            }
            if (this.IsStatement(pCloseIndex))
            {
              if (!TemplateParser._removeSpaces.Replace(this.GetBeforeOpen((IList<char>) this._template, pCloseIndex + 1), string.Empty).StartsWith("elseif", StringComparison.Ordinal))
              {
                this._nextPossibleStatements.RemoveLast();
                break;
              }
              break;
            }
            this._currentState = TemplateParser.States.TextBlock;
            break;
          case TemplateParser.States.MultilineStatement:
            this.AddCode(this.GetBeforeOpen((IList<char>) this._template, pCloseIndex));
            this._builder.Append(this.FindEnclosedText((IList<char>) this._template, pCloseIndex, out pCloseIndex));
            this._currentState = TemplateParser.States.TextBlock;
            break;
          case TemplateParser.States.Expression:
            if ((int) this._openStatement == (int) this._closeStatement && this._openStatement == char.MinValue)
            {
              this.AddExpression(this.ReadExpression(ref pCloseIndex));
              this._currentState = TemplateParser.States.TextBlock;
              break;
            }
            this.AddExpression(this.FindEnclosedText((IList<char>) this._template, pCloseIndex, out pCloseIndex));
            this._currentState = TemplateParser.States.TextBlock;
            break;
        }
      }
      this.AddPreviousText(pTempText);
      return this._builder.ToString();
    }

    private string ReadExpression(ref int pIndex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      LinkedList<char> linkedList = new LinkedList<char>();
      while (pIndex < this._templateLength)
      {
        char ch1 = this._template[pIndex];
        if (!TemplateParser.IsLetter(ch1) && (ch1 != '.' || !TemplateParser.IsLetter(this.PeekNext(pIndex))))
        {
          char ch2;
          if (TemplateParser._defaultExpressionOpeners.TryGetValue(ch1, out ch2))
          {
            linkedList.AddLast(ch2);
            stringBuilder.Append(ch1);
          }
          else if (linkedList.Last != null && (int) linkedList.Last.Value == (int) ch1)
          {
            linkedList.RemoveLast();
            stringBuilder.Append(ch1);
          }
          else if (ch1 == ':' && linkedList.Last == null && TemplateParser.IsLetter(this.PeekNext(pIndex)))
            stringBuilder.Append(ch1);
          else if (linkedList.Count != 0)
          {
            stringBuilder.Append(ch1);
          }
          else
          {
            pIndex -= linkedList.Count + 1;
            using (LinkedList<char>.Enumerator enumerator = linkedList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                int current = (int) enumerator.Current;
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
              }
              break;
            }
          }
        }
        else
          stringBuilder.Append(ch1);
        ++pIndex;
      }
      return stringBuilder.ToString();
    }

    private string ReadKeyword(int pIndex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (pIndex < this._templateLength)
      {
        char pValue = this._template[pIndex];
        ++pIndex;
        if (TemplateParser.IsLetter(pValue))
          stringBuilder.Append(pValue);
        else
          break;
      }
      return stringBuilder.ToString();
    }

    private bool IsStatement(int pIndex)
    {
      if (this._nextPossibleStatements.Count == 0)
        return false;
      HashSet<string> stringSet = this._nextPossibleStatements.Last.Value;
      if (stringSet != null)
        return stringSet.Contains(this.ReadKeyword(this.SkipWhiteSpace(pIndex + 1)));
      return false;
    }

    private int SkipWhiteSpace(int pIndex)
    {
      while (pIndex < this._templateLength && char.IsWhiteSpace(this._template[pIndex]))
        ++pIndex;
      return pIndex;
    }

    private string GetBeforeOpen(IList<char> pTemplate, int pIndex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (pTemplate[pIndex] == '{')
        return string.Empty;
      for (; pIndex < pTemplate.Count; ++pIndex)
      {
        char ch = pTemplate[pIndex];
        stringBuilder.Append(ch);
        if ((int) ch == (int) this._openStatement)
          break;
      }
      return stringBuilder.ToString();
    }

    private string FindEnclosedText(IList<char> pTemplate, int pIndex, out int pCloseIndex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num = 0;
      while (pIndex < pTemplate.Count)
      {
        char ch = pTemplate[pIndex];
        ++pIndex;
        if ((int) ch == (int) this._openStatement)
          break;
      }
      while (pIndex < pTemplate.Count)
      {
        char ch = pTemplate[pIndex];
        ++pIndex;
        stringBuilder.Append(ch);
        if ((int) ch == (int) this._closeStatement)
        {
          if (num != 0)
          {
            --num;
          }
          else
          {
            pCloseIndex = pIndex - 1;
            --stringBuilder.Length;
            return stringBuilder.ToString();
          }
        }
        else if ((int) ch == (int) this._openStatement)
          ++num;
      }
      throw new TemplateParsingException("Missing ending " + (object) this._closeStatement);
    }

    private void AddPreviousText(StringBuilder pTempText)
    {
      if (pTempText.Length == 0)
        return;
      this.AddText(pTempText.ToString());
      pTempText.Length = 0;
    }

    private static bool IsEmail(string pLine, int pI, out string pAfterArroba, out int pNewIndex)
    {
      pAfterArroba = (string) null;
      pNewIndex = pI;
      StringBuilder stringBuilder1 = new StringBuilder("@");
      int index1 = pI;
      int num1 = 0;
      while (index1 > -1)
      {
        --index1;
        char pValue = pLine[index1];
        if (TemplateParser.IsValidEmailChar(pValue))
        {
          stringBuilder1.Insert(0, pValue);
          ++num1;
        }
        else
          break;
      }
      if (num1 > 64)
        return false;
      StringBuilder stringBuilder2 = new StringBuilder();
      int index2 = pI;
      int num2 = 0;
      while (index2 < pLine.Length)
      {
        ++index2;
        char pValue = pLine[index2];
        if (TemplateParser.IsValidEmailChar(pValue))
        {
          ++num2;
          stringBuilder1.Append(pValue);
          stringBuilder2.Append(pValue);
        }
        else
          break;
      }
      if (num2 > (int) byte.MaxValue)
        return false;
      pAfterArroba = stringBuilder2.ToString();
      pNewIndex = index2 - 1;
      return TemplateParser._pattern.IsMatch(stringBuilder1.ToString());
    }

    private static bool IsValidEmailChar(char pValue)
    {
      if (!TemplateParser.IsLetter(pValue) && !TemplateParser.IsDecimalDigit(pValue) && (pValue != '_' && pValue != '-'))
        return pValue == '.';
      return true;
    }

    private static bool IsDecimalDigit(char pValue)
    {
      return char.GetUnicodeCategory(pValue) == UnicodeCategory.DecimalDigitNumber;
    }

    private static bool IsLetter(char pValue)
    {
      UnicodeCategory unicodeCategory = char.GetUnicodeCategory(pValue);
      switch (unicodeCategory)
      {
        case UnicodeCategory.UppercaseLetter:
        case UnicodeCategory.LowercaseLetter:
        case UnicodeCategory.TitlecaseLetter:
        case UnicodeCategory.ModifierLetter:
        case UnicodeCategory.OtherLetter:
          return true;
        default:
          return unicodeCategory == UnicodeCategory.LetterNumber;
      }
    }

    private void AddText(string pText)
    {
      if (string.IsNullOrEmpty(pText))
        return;
      this._builder.AppendFormat("builder.Append(@\"{0}\" );\r\n", (object) pText.Replace("\"", "\"\""));
    }

    private void AddCode(string pText)
    {
      if (string.IsNullOrEmpty(pText))
        return;
      this._builder.AppendFormat("{0}\r\n", (object) pText);
    }

    private void AddExpression(string pText)
    {
      if (string.IsNullOrEmpty(pText))
        return;
      int length = pText.IndexOf(':');
      bool flag1;
      bool flag2 = (flag1 = pText[0] == '^') ? !this._escapeExpressions : this._escapeExpressions;
      if (flag1)
        pText = pText.Substring(1);
      string str = length < 0 ? pText : pText.Substring(0, length);
      this._builder.AppendFormat("if ({0} != null) {{", (object) str);
      if (length < 0)
        this._builder.AppendFormat(flag2 ? "builder.Append(System.Security.SecurityElement.Escape({0}.ToString()));\r\n" : "builder.Append({0});\r\n", (object) str);
      else
        this._builder.AppendFormat(flag2 ? "builder.Append(System.Security.SecurityElement.Escape(string.Format(\"{{0:{0}}}\", {1})));\r\n" : "builder.AppendFormat(\"{{0:{0}}}\", {1});\r\n", (object) pText.Substring(length + 1), (object) str);
      this._builder.Append("}");
    }

    private enum States
    {
      TextBlock,
      Statement,
      MultilineStatement,
      Expression,
    }
  }
}
