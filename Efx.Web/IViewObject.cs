// Decompiled with JetBrains decompiler
// Type: Efx.Web.IViewObject
// Assembly: Efx.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 9694B55A-6F04-4F0C-8780-19C4ED30C482
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Web.dll

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Efx.Web
{
  [Guid("0000010d-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [ComImport]
  public interface IViewObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int Draw(
      [MarshalAs(UnmanagedType.U4)] uint dwDrawAspect,
      int lindex,
      IntPtr pvAspect,
      [In] IntPtr ptd,
      IntPtr hdcTargetDev,
      IntPtr hdcDraw,
      [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcBounds,
      [MarshalAs(UnmanagedType.Struct)] ref Rectangle lprcWBounds,
      IntPtr pfnContinue,
      [MarshalAs(UnmanagedType.U4)] uint dwContinue);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetColorSet(
      [MarshalAs(UnmanagedType.U4), In] int dwDrawAspect,
      int lindex,
      IntPtr pvAspect,
      [In] IntPtr ptd,
      IntPtr hicTargetDev,
      [Out] IntPtr ppColorSet);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Freeze([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unfreeze([MarshalAs(UnmanagedType.U4), In] int dwFreeze);

    void SetAdvise([MarshalAs(UnmanagedType.U4), In] int aspects, [MarshalAs(UnmanagedType.U4), In] int advf, [MarshalAs(UnmanagedType.Interface), In] IAdviseSink pAdvSink);

    void GetAdvise([MarshalAs(UnmanagedType.LPArray), In, Out] int[] paspects, [MarshalAs(UnmanagedType.LPArray), In, Out] int[] advf, [MarshalAs(UnmanagedType.LPArray), In, Out] IAdviseSink[] pAdvSink);
  }
}
