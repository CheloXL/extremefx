// Decompiled with JetBrains decompiler
// Type: Efx.Web.UI.Design.BaseContainerControlDesigner`1
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System.Web.UI;
using System.Web.UI.Design;

namespace Efx.Web.UI.Design
{
  public abstract class BaseContainerControlDesigner<T> : ContainerControlDesigner
    where T : Control
  {
    protected T Component
    {
      get
      {
        return (T) base.Component;
      }
    }

    protected abstract string Title { get; }

    protected abstract string Description { get; }

    public override string GetDesignTimeHtml(DesignerRegionCollection regions)
    {
      base.GetDesignTimeHtml(regions);
      return string.Format("<div style=\"background-color: #b0c4de; padding: 2px;\"><b style=\"display: block;padding: 2px 4px;\">{0}</b><div style=\"background-color: #fff; padding: 2px 4px;\">{1}<div {2}=0></div></div></div>", (object) this.Title, (object) this.Description, (object) DesignerRegion.DesignerRegionAttributeName);
    }
  }
}
