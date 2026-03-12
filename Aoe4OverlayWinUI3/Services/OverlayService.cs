using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;
using Aoe4OverlayWinUI3.Views;
using Aoe4OverlayWinUI3.Contracts.Services;

namespace Aoe4OverlayWinUI3.Services;

public class OverlayService:IOverlayService
{
    private Window _overlayWindow;
    private IntPtr _hWnd;



    // --- Win32 扩展样式常量 ---
    private const int GWL_EXSTYLE = -20;          // 扩展样式表
    private const uint WS_EX_TOPMOST = 0x00000008;   // 始终置顶
    private const uint WS_EX_LAYERED = 0x00080000;   // 支持层级透明
    private const uint WS_EX_TRANSPARENT = 0x00000020; // 鼠标穿透
    private const uint WS_EX_TOOLWINDOW = 0x00000080;  // 隐藏任务栏图标
    private const uint WS_EX_NOACTIVATE = 0x08000000;  // 窗口显示时不夺取焦点

    // --- ShowWindow 参数 ---
    private const int SW_HIDE = 0; // 隐藏窗口

    // --- SetWindowPos 标志位 ---
    // 将窗口置于所有窗口的最顶层
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOSIZE = 0x0001;       // 维持当前大小（忽略 cx, cy 参数）
    private const uint SWP_NOMOVE = 0x0002;       // 维持当前位置（忽略 x, y 参数）
    private const uint SWP_NOACTIVATE = 0x0010;   // 显示时不激活窗口
    private const uint SWP_SHOWWINDOW = 0x0040;   // 显示窗口

    // 构造函数
    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    // 设置新的扩展样式
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    // 设置窗口位置和显示
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    // 显示窗口
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    // 切换覆盖层显示状态的方法
    public void ToggleOverlay(bool enable)
    {
        if (enable)
        {
            // 如果窗口还没有创建，就创建并设置样式
            if (_overlayWindow == null)
            {
                // 创建窗口实例
                _overlayWindow = new OverlayWindow();

                // 获取该窗口的 HWND 句柄
                _hWnd = WindowNative.GetWindowHandle(_overlayWindow);

                // 应用 Win32 样式
                ApplyOverlayStyles(_hWnd);

                // 去掉 WinUI 3 默认的标题栏
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(_hWnd);
                AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

                // 不显示标题栏和边框
                if (appWindow.Presenter is OverlappedPresenter presenter)
                {
                    presenter.SetBorderAndTitleBar(false, false);
                }
            }

            // 使用 SWP_NOACTIVATE 确保窗口显示但不抢占焦点
            SetWindowPos(_hWnd, HWND_TOPMOST, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
        }
        else
        {
            // 隐藏但不销毁
            if (_hWnd != IntPtr.Zero)
            {
                ShowWindow(_hWnd, SW_HIDE);
            }
        }
    }

    private void ApplyOverlayStyles(IntPtr hWnd)
    {
        // 获取窗口当前的扩展样式
        IntPtr currentExStyle = GetWindowLongPtr(hWnd, GWL_EXSTYLE);

        // 合并 置顶、透明层、任务栏隐藏、不夺取焦点 
        uint newExStyle = (uint)currentExStyle.ToInt64()
                          | WS_EX_TOPMOST
                          | WS_EX_LAYERED
                          | WS_EX_TOOLWINDOW
                          | WS_EX_NOACTIVATE;

        // TODO: 加上 | WS_EX_TRANSPARENT

        // 将修改后的样式写回窗口
        SetWindowLongPtr(hWnd, GWL_EXSTYLE, (IntPtr)newExStyle);
    }

}
