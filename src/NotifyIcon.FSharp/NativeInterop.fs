module internal NativeInterop 

open System
open System.Runtime.InteropServices

open NativeStruct

[<DllImport("user32.dll", EntryPoint = "LoadImage", SetLastError = true, CharSet = CharSet.Auto)>]
extern nativeint loadImage(
    nativeint hinst,
    string lpszName,
    ImageType uType,
    int cxDesired,
    int cyDesired,
    LoadImageFlags fuLoad)

[<DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)>]
extern bool destroyIcon(nativeint hIcon)

[<DllImport("user32.dll", EntryPoint = "DefWindowProc")>]
extern nativeint defWindowProc(nativeint hWnd, WindowsMessage uMsg, nativeint wParam, nativeint lParam)

[<DllImport("user32.dll", EntryPoint = "RegisterClassW", SetLastError = true)>]
extern uint16 registerClass(WndClass& lpWndClass)

[<DllImport("user32.dll", EntryPoint = "RegisterWindowMessage", SetLastError = true, CharSet = CharSet.Auto)>]
extern uint registerWindowMessage(string lpString)

[<DllImport("user32.dll", EntryPoint = "CreateWindowEx", SetLastError = true, CharSet = CharSet.Unicode)>]
extern nativeint createWindowEx(
    WindowStylesEx dwExStyle,
    uint16 regClassResult,
    string lpWindowName,
    WindowStyles dwStyle,
    int x,  int y,
    int nWidth, int nHeight,
    IntPtr hWndParent,
    IntPtr hMenu,
    IntPtr hInstance,
    IntPtr pvParam)

[<DllImport("user32.dll", EntryPoint = "DestroyWindow", SetLastError = true, CharSet = CharSet.Unicode)>]
extern [<MarshalAs(UnmanagedType.Bool)>] bool destroyWindow(nativeint hwnd)

[<DllImport("shell32.dll", EntryPoint = "Shell_NotifyIcon")>]
extern bool shellNotifyIcon(uint dwMessage, NotifyIconData& pnid)

//[<DllImport("user32.dll", EntryPoint = "GetDoubleClickTime", CharSet = CharSet.Auto, ExactSpelling = true)>]
//extern int getDoubleClickTime();

[<DllImport("user32.dll", EntryPoint = "CreatePopupMenu")>]
extern IntPtr createPopupMenu();

[<DllImport("user32.dll", EntryPoint = "InsertMenuItem", CharSet = CharSet.Auto, SetLastError = true)>]
extern bool insertMenuItem(IntPtr hMenu, uint uItem, bool fByPosition, MenuItemInfo& lpmii);

[<DllImport("user32.dll", EntryPoint = "TrackPopupMenu")>]
extern bool trackPopupMenu(IntPtr hMenu, int wFlags, int x, int y, int nReserved, IntPtr hwnd, Rect& lprc);

[<DllImport("user32.dll", EntryPoint = "GetCursorPos")>]
extern bool GetCursorPos([<Out>] Point& lpPoint);