// Decompiled with JetBrains decompiler
// Type: Efx.Web.BrowserCaps.DeviceInfo
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Text;

namespace Efx.Web.BrowserCaps
{
  public sealed class DeviceInfo
  {
    private int _maxPhysicalScreenWidth = -1;
    private int _maxWidthResolution = -1;
    private float _physicalResolutionWidthRatio = -1f;
    public string UserAgent;
    public string Id;
    public PointingMethod PointingMethod;
    public bool HasQwertyKeyboard;
    public int WidthResolution;
    public int HeightResolution;
    public int PhysicalScreenWidth;
    public int PhysicalScreenHeight;
    public bool DualOrientation;

    public int MaxPhysicalScreenWidth
    {
      get
      {
        if (this._maxPhysicalScreenWidth == -1)
          this._maxPhysicalScreenWidth = this.DualOrientation ? (this.PhysicalScreenWidth < this.PhysicalScreenHeight ? this.PhysicalScreenHeight : this.PhysicalScreenWidth) : this.PhysicalScreenWidth;
        return this._maxPhysicalScreenWidth;
      }
    }

    public int MaxWidthResolution
    {
      get
      {
        if (this._maxWidthResolution == -1)
          this._maxWidthResolution = this.DualOrientation ? (this.WidthResolution < this.HeightResolution ? this.HeightResolution : this.WidthResolution) : this.WidthResolution;
        return this._maxWidthResolution;
      }
    }

    public float PhysicalResolutionWidthRatio
    {
      get
      {
        if ((double) Math.Abs(this._physicalResolutionWidthRatio - -1f) < 0.5)
          this._physicalResolutionWidthRatio = (float) (this.MaxPhysicalScreenWidth * 75) / 25.4f / (float) this.MaxWidthResolution;
        return this._physicalResolutionWidthRatio;
      }
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder("DeviceInfo {");
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tId: {0}", (object) this.Id);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tUserAgent: {0}", (object) this.UserAgent);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tPointingMethod: {0}", (object) this.PointingMethod);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tHasQwertyKeyboard: {0}", (object) this.HasQwertyKeyboard);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tWidthResolution: {0}px", (object) this.WidthResolution);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tHeightResolution: {0}px", (object) this.HeightResolution);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tPhysicalScreenWidth: {0}mm", (object) this.PhysicalScreenWidth);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tPhysicalScreenHeight: {0}mm", (object) this.PhysicalScreenHeight);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tDualOrientation: {0}", (object) this.DualOrientation);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tMaxPhysicalScreenWidth: {0}", (object) this.MaxPhysicalScreenWidth);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tMaxWidthResolution: {0}", (object) this.MaxWidthResolution);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("\tPhysicalResolutionWidthRatio: {0}", (object) this.PhysicalResolutionWidthRatio);
      stringBuilder.AppendLine();
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
