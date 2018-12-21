// Decompiled with JetBrains decompiler
// Type: Efx.Core.Data.DataSource
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Efx.Core.Data
{
  public sealed class DataSource : IEnumerable, IEnumerable<DataSourceItem>
  {
    private string _dataKeyField;
    private List<DataSourceItem> _dataObject;
    private IEnumerable _dataSource;
    private string _dataTextField;
    private string _dataTextFieldFormat;
    private Dictionary<object, int> _datas;
    private bool _dirty;
    private Dictionary<string, int> _values;

    public DataSource(object pSource)
    {
      this.SetDataSource(pSource);
    }

    public string DataTextField
    {
      get
      {
        return this._dataTextField;
      }
      set
      {
        this._dirty = true;
        this._dataTextField = value;
      }
    }

    public string DataKeyField
    {
      get
      {
        return this._dataKeyField;
      }
      set
      {
        this._dirty = true;
        this._dataKeyField = value;
      }
    }

    public string DataTextFieldFormat
    {
      get
      {
        return this._dataTextFieldFormat;
      }
      set
      {
        this._dirty = true;
        this._dataTextFieldFormat = value;
      }
    }

    public bool IsValid { get; private set; }

    public int Count
    {
      get
      {
        this.dataBind();
        return this._dataObject.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    public object SyncRoot
    {
      get
      {
        return (object) this._dataObject;
      }
    }

    public DataSourceItem this[int pIndex]
    {
      get
      {
        this.dataBind();
        return this._dataObject[pIndex];
      }
    }

    public IEnumerator<DataSourceItem> GetEnumerator()
    {
      this.dataBind();
      return (IEnumerator<DataSourceItem>) this._dataObject.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      this.dataBind();
      return (IEnumerator) this._dataObject.GetEnumerator();
    }

    public void SetDataSource(object pSource)
    {
      this.IsValid = pSource != null;
      this._dirty = true;
      this._dataSource = pSource is IListSource ? (IEnumerable) ((IListSource) pSource).GetList() : (IEnumerable) pSource;
    }

    public string FindValueByData(object pData)
    {
      if (pData == null)
        return (string) null;
      this.dataBind();
      if (!this._datas.ContainsKey(pData))
        return (string) null;
      return this._dataObject[this._datas[pData]].Value;
    }

    public List<string> FindValuesByData(IEnumerable pData)
    {
      if (pData == null)
        return (List<string>) null;
      this.dataBind();
      List<string> stringList = new List<string>();
      foreach (object key in pData)
      {
        if (this._datas.ContainsKey(key))
          stringList.Add(this._dataObject[this._datas[key]].Value);
      }
      if (stringList.Count != 0)
        return stringList;
      return (List<string>) null;
    }

    public DataSourceItem FindItemByValue(string pValue)
    {
      if (string.IsNullOrEmpty(pValue))
        return (DataSourceItem) null;
      this.dataBind();
      if (!this._values.ContainsKey(pValue))
        return (DataSourceItem) null;
      return this._dataObject[this._values[pValue]];
    }

    public IEnumerable FindItemsByValues(IEnumerable<string> pValues)
    {
      if (pValues == null)
        return (IEnumerable) null;
      this.dataBind();
      ArrayList arrayList = new ArrayList();
      foreach (string index in pValues.Where<string>((Func<string, bool>) (pValue => this._values.ContainsKey(pValue))))
        arrayList.Add(this._dataObject[this._values[index]].Item);
      if (arrayList.Count != 0)
        return (IEnumerable) arrayList;
      return (IEnumerable) null;
    }

    private void dataBind()
    {
      if (!this._dirty)
        return;
      this._dirty = false;
      this._values = new Dictionary<string, int>();
      this._datas = new Dictionary<object, int>();
      this._dataObject = new List<DataSourceItem>();
      if (!this.IsValid)
        return;
      int num = 0;
      foreach (object obj1 in this._dataSource)
      {
        string str;
        string pText = str = (string) null;
        object obj2 = (object) null;
        if (!string.IsNullOrEmpty(this.DataTextField))
          pText = string.IsNullOrEmpty(this.DataTextFieldFormat) ? new Expression(this.DataTextField).Evaluate(obj1).ToString() : string.Format(this.DataTextFieldFormat, new Expression(this.DataTextField).Evaluate(obj1));
        if (!string.IsNullOrEmpty(this.DataKeyField))
        {
          obj2 = new Expression(this.DataKeyField).Evaluate(obj1);
          str = obj2 == null ? "null" : TypeDescriptor.GetConverter(obj2).ConvertToString(obj2);
        }
        else if (string.IsNullOrEmpty(this.DataTextField))
        {
          pText = str = obj1 == null ? "null" : TypeDescriptor.GetConverter(obj1).ConvertToString(obj1);
          obj2 = obj1;
          if (!string.IsNullOrEmpty(this.DataTextFieldFormat))
            pText = string.Format(this.DataTextFieldFormat, obj1);
        }
        else if (!string.IsNullOrEmpty(pText))
          str = pText;
        if (string.IsNullOrEmpty(pText))
          pText = str;
        this._dataObject.Add(new DataSourceItem(pText, str, obj1));
        if (str != null && !this._values.ContainsKey(str))
          this._values.Add(str, num);
        if (obj2 != null && !this._datas.ContainsKey(obj2))
          this._datas.Add(obj2, num);
        ++num;
      }
    }

    public void CopyTo(Array pArray, int pIndex)
    {
      throw new NotImplementedException();
    }
  }
}
