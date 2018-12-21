// Decompiled with JetBrains decompiler
// Type: Efx.Core.Data.IndexedAccess
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;

namespace Efx.Core.Data
{
  internal sealed class IndexedAccess : IValue
  {
    private readonly Expression _indexer;
    private readonly string _name;

    public IndexedAccess(string pName, Expression pIndexer)
    {
      this._indexer = pIndexer;
      this._name = pName;
    }

    public object Evaluate(object pObject)
    {
      throw new NotImplementedException("Datasource item: Indexer access is not implemented");
    }

    public override string ToString()
    {
      return string.Format("Indexer: {0}[{1}]", (object) this._name, (object) this._indexer);
    }
  }
}
