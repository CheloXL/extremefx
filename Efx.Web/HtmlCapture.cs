// Decompiled with JetBrains decompiler
// Type: Efx.Web.HtmlCapture
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Efx.Web
{
  public class HtmlCapture
  {
    private static readonly List<Thread> _threads = new List<Thread>();
    private string _url;
    private Thread _thread;

    public event Action<Uri, Bitmap> HtmlImageCapture;

    public int PaddingWidth { get; set; }

    public int PaddingHeight { get; set; }

    public int Delay { get; set; }

    public void Create(string pUrl)
    {
      this._url = pUrl;
      this._thread = new Thread(new ThreadStart(this.DoWork));
      HtmlCapture._threads.Add(this._thread);
      this._thread.SetApartmentState(ApartmentState.STA);
      this._thread.Start();
      this._thread.Join();
    }

    private void DoWork()
    {
      WebBrowser webBrowser1 = new WebBrowser();
      webBrowser1.Width = 640;
      webBrowser1.Height = 480;
      webBrowser1.ScriptErrorsSuppressed = true;
      webBrowser1.ScrollBarsEnabled = false;
      webBrowser1.AllowNavigation = false;
      using (WebBrowser webBrowser2 = webBrowser1)
      {
        webBrowser2.NewWindow += new CancelEventHandler(HtmlCapture.NewWindow);
        webBrowser2.Navigate(this._url);
        while (webBrowser2.ReadyState != WebBrowserReadyState.Complete)
          Application.DoEvents();
        if (this.Delay > 0)
          Thread.Sleep(this.Delay);
        if (webBrowser2.Document == (HtmlDocument) null || webBrowser2.Document.Body == (HtmlElement) null)
          return;
        HtmlElement body = webBrowser2.Document.Body;
        int attr1 = HtmlCapture.GetAttr(body, "scrollWidth");
        int num1 = attr1 > 0 ? attr1 : HtmlCapture.GetAttr(body, "offsetWidth") + HtmlCapture.GetAttr(body, "offsetLeft");
        HtmlCapture.GetAttr(body, "scrollHeight");
        int attr2 = HtmlCapture.GetAttr(body, "scrollHeight");
        int num2 = attr2 > 0 ? attr2 : HtmlCapture.GetAttr(body, "offsetHeight") + HtmlCapture.GetAttr(body, "offsetTop");
        if (num1 < 1)
          num1 = Screen.PrimaryScreen.Bounds.Width;
        if (num2 < 1)
          num2 = Screen.PrimaryScreen.Bounds.Height;
        int width = num1 + this.PaddingWidth;
        int height = num2 + this.PaddingHeight;
        webBrowser2.Width = width;
        webBrowser2.Height = height;
        Rectangle rectangle = new Rectangle(0, 0, width, height);
        using (Bitmap bitmap = new Bitmap(width, height))
        {
          IViewObject domDocument = webBrowser2.Document.DomDocument as IViewObject;
          using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          {
            IntPtr hdc = graphics.GetHdc();
            domDocument.Draw(1U, -1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, hdc, ref rectangle, ref rectangle, IntPtr.Zero, 0U);
            graphics.ReleaseHdc(hdc);
          }
          if (this.HtmlImageCapture != null)
            this.HtmlImageCapture(webBrowser2.Url, bitmap);
        }
      }
      HtmlCapture._threads.Remove(this._thread);
    }

    private static int GetAttr(HtmlElement pElement, string pAttrName)
    {
      int result;
      int.TryParse(pElement.GetAttribute(pAttrName), out result);
      return result;
    }

    private static void NewWindow(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
    }
  }
}
