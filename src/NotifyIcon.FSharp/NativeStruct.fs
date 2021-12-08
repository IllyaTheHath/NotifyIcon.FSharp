module internal NativeStruct

open System
open System.Runtime.InteropServices

[<Flags>]
type ImageType =
    /// Loads a bitmap.
    | IMAGE_BITMAP = 0u

    /// Loads an icon.
    | IMAGE_ICON = 1u

    /// Loads a cursor.
    | IMAGE_CURSOR = 2u

    /// Loads an enhanced metafile.
    | IMAGE_ENHMETAFILE = 3u

[<Flags>]
type LoadImageFlags =
    /// <summary>
    /// When the uType parameter specifies PInvoke.User32.ImageType.IMAGE_BITMAP, causes
    /// the function to return a DIB section bitmap rather than a compatible bitmap.
    /// This flag is useful for loading a bitmap without mapping it to the colors of
    /// the display device.
    /// </summary>
    | LR_CREATEDIBSECTION = 0x2000u

    /// <summary>
    /// The default flag; it does nothing. All it means is "not PInvoke.User32.LoadImageFlags.LR_MONOCHROME".
    /// </summary>
    | LR_DEFAULTCOLOR = 0x0u

    /// <summary>
    /// Uses the width or height specified by the system metric values for cursors or
    /// icons, if the cxDesired or cyDesired values are set to zero. If this flag is
    /// not specified and cxDesired and cyDesired are set to zero, the function uses
    /// the actual resource size. If the resource contains multiple images, the function
    /// uses the size of the first image.
    /// </summary>
    | LR_DEFAULTSIZE = 0x40u

    /// <summary>
    /// Loads the stand-alone image from the file specified by lpszName (icon, cursor,
    /// or bitmap file).
    /// </summary>
    | LR_LOADFROMFILE = 0x10u

    /// <summary>
    /// Searches the color table for the image and replaces the following shades of gray
    /// with the corresponding 3-D color.
    ///  • Dk Gray, RGB(128,128,128) with COLOR_3DSHADOW
    ///  • Gray, RGB(192,192,192) with COLOR_3DFACE
    ///  • Lt Gray, RGB(223,223,223) with COLOR_3DLIGHT
    /// Do not use this option if you are loading a bitmap with a color depth greater
    /// than 8bpp.
    /// </summary>
    | LR_LOADMAP3DCOLORS = 0x1000u

    /// <summary>
    /// Retrieves the color value of the first pixel in the image and replaces the corresponding
    /// entry in the color table with the default window color (COLOR_WINDOW). All pixels
    /// in the image that use that entry become the default window color. This value
    /// applies only to images that have corresponding color tables. Do not use this
    /// option if you are loading a bitmap with a color depth greater than 8bpp. If fuLoad
    /// includes both the PInvoke.User32.LoadImageFlags.LR_LOADTRANSPARENT and PInvoke.User32.LoadImageFlags.LR_LOADMAP3DCOLORS
    /// values, PInvoke.User32.LoadImageFlags.LR_LOADTRANSPARENT takes precedence. However,
    /// the color table entry is replaced with COLOR_3DFACE rather than COLOR_WINDOW.
    /// </summary>
    | LR_LOADTRANSPARENT = 0x20u

    /// <summary>
    /// Loads the image in black and white.
    /// </summary>
    | LR_MONOCHROME = 0x1u

    /// <summary>
    /// Shares the image handle if the image is loaded multiple times. If PInvoke.User32.LoadImageFlags.LR_SHARED
    /// is not set, a second call to LoadImage for the same resource will load the image
    /// again and return a different handle. When you use this flag, the system will
    /// destroy the resource when it is no longer needed. Do not use PInvoke.User32.LoadImageFlags.LR_SHARED
    /// for images that have non-standard sizes, that may change after loading, or that
    /// are loaded from a file. When loading a system icon or cursor, you must use PInvoke.User32.LoadImageFlags.LR_SHARED
    /// or the function will fail to load the resource. This function finds the first
    /// image in the cache with the requested resource name, regardless of the size requested.
    /// </summary>
    | LR_SHARED = 0x8000u

    /// <summary>
    /// Uses true VGA colors.
    /// </summary>
    | LR_VGACOLOR = 0x80u

[<Flags>]
type WindowsMessage =
    /// Sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.
    | WM_COMMAND = 0x0111u
    /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    | WM_LBUTTONDOWN = 0x0201u

    /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    | WM_LBUTTONUP = 0x0202u

    /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    | WM_LBUTTONDBLCLK = 0x0203u

    /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    | WM_RBUTTONDOWN = 0x0204u

    /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    | WM_RBUTTONUP = 0x0205u

    /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    | WM_RBUTTONDBLCLK = 0x0206u

    /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value.
    | WM_USER = 0x0400u
    /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    /// </summary>
    | WM_MBUTTONDOWN = 0x0207u
    /// <summary>
    /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    /// </summary>
    | WM_MBUTTONUP = 0x0208u
    /// <summary>
    /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
    /// </summary>
    | WM_MBUTTONDBLCLK = 0x0209u

[<Flags>]
type WindowStylesEx =
    /// <summary>Specifies a window that accepts drag-drop files.</summary>
    | WS_EX_ACCEPTFILES = 0x00000010u

    /// <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
    | WS_EX_APPWINDOW = 0x00040000u

    /// <summary>Specifies a window that has a border with a sunken edge.</summary>
    | WS_EX_CLIENTEDGE = 0x00000200u

    /// <summary>
    /// Specifies a window that paints all descendants in bottom-to-top painting order using double-buffering.
    /// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. This style is not supported in Windows 2000.
    /// </summary>
    /// <remarks>
    /// With WS_EX_COMPOSITED set, all descendants of a window get bottom-to-top painting order using double-buffering.
    /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
    /// but only if the descendent window also has the WS_EX_TRANSPARENT bit set.
    /// Double-buffering allows the window and its descendents to be painted without flicker.
    /// </remarks>
    | WS_EX_COMPOSITED = 0x02000000u

    /// <summary>
    /// Specifies a window that includes a question mark in the title bar. When the user clicks the question mark,
    /// the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message.
    /// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
    /// The Help application displays a pop-up window that typically contains help for the child window.
    /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
    /// </summary>
    | WS_EX_CONTEXTHELP = 0x00000400u

    /// <summary>
    /// Specifies a window which contains child windows that should take part in dialog box navigation.
    /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations
    /// such as handling the TAB key, an arrow key, or a keyboard mnemonic.
    /// </summary>
    | WS_EX_CONTROLPARENT = 0x00010000u

    /// <summary>Specifies a window that has a double border.</summary>
    | WS_EX_DLGMODALFRAME = 0x00000001u

    /// <summary>
    /// Specifies a window that is a layered window.
    /// This cannot be used for child windows or if the window has a class style of either CS_OWNDC or CS_CLASSDC.
    /// </summary>
    | WS_EX_LAYERED = 0x00080000u

    /// <summary>
    /// Specifies a window with the horizontal origin on the right edge. Increasing horizontal values advance to the left.
    /// The shell language must support reading-order alignment for this to take effect.
    /// </summary>
    | WS_EX_LAYOUTRTL = 0x00400000u

    /// <summary>Specifies a window that has generic left-aligned properties. This is the default.</summary>
    | WS_EX_LEFT = 0x00000000u

    /// <summary>
    /// Specifies a window with the vertical scroll bar (if present) to the left of the client area.
    /// The shell language must support reading-order alignment for this to take effect.
    /// </summary>
    | WS_EX_LEFTSCROLLBAR = 0x00004000u

    /// <summary>
    /// Specifies a window that displays text using left-to-right reading-order properties. This is the default.
    /// </summary>
    | WS_EX_LTRREADING = 0x00000000u

    /// <summary>
    /// Specifies a multiple-document interface (MDI) child window.
    /// </summary>
    | WS_EX_MDICHILD = 0x00000040u

    /// <summary>
    /// Specifies a top-level window created with this style does not become the foreground window when the user clicks it.
    /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
    /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
    /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
    /// </summary>
    | WS_EX_NOACTIVATE = 0x08000000u

    /// <summary>
    /// Specifies a window which does not pass its window layout to its child windows.
    /// </summary>
    | WS_EX_NOINHERITLAYOUT = 0x00100000u

    /// <summary>
    /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
    /// </summary>
    | WS_EX_NOPARENTNOTIFY = 0x00000004u

    /// <summary>
    /// The window does not render to a redirection surface.
    /// This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
    /// </summary>
    | WS_EX_NOREDIRECTIONBITMAP = 0x00200000u

    /// <summary>Specifies an overlapped window.</summary>
    | WS_EX_OVERLAPPEDWINDOW = 0x00000300u // WS_EX_WINDOWEDGE ||| WS_EX_CLIENTEDGE

    /// <summary>Specifies a palette window, which is a modeless dialog box that presents an array of commands.</summary>
    | WS_EX_PALETTEWINDOW = 0x00000188u // WS_EX_WINDOWEDGE ||| WS_EX_TOOLWINDOW ||| WS_EX_TOPMOST

    /// <summary>
    /// Specifies a window that has generic "right-aligned" properties. This depends on the window class.
    /// The shell language must support reading-order alignment for this to take effect.
    /// Using the WS_EX_RIGHT style has the same effect as using the SS_RIGHT (static), ES_RIGHT (edit), and BS_RIGHT/BS_RIGHTBUTTON (button) control styles.
    /// </summary>
    | WS_EX_RIGHT = 0x00001000u

    /// <summary>Specifies a window with the vertical scroll bar (if present) to the right of the client area. This is the default.</summary>
    | WS_EX_RIGHTSCROLLBAR = 0x00000000u

    /// <summary>
    /// Specifies a window that displays text using right-to-left reading-order properties.
    /// The shell language must support reading-order alignment for this to take effect.
    /// </summary>
    | WS_EX_RTLREADING = 0x00002000u

    /// <summary>Specifies a window with a three-dimensional border style intended to be used for items that do not accept user input.</summary>
    | WS_EX_STATICEDGE = 0x00020000u

    /// <summary>
    /// Specifies a window that is intended to be used as a floating toolbar.
    /// A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
    /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
    /// If a tool window has a system menu, its icon is not displayed on the title bar.
    /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
    /// </summary>
    | WS_EX_TOOLWINDOW = 0x00000080u

    /// <summary>
    /// Specifies a window that should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
    /// To add or remove this style, use the SetWindowPos function.
    /// </summary>
    | WS_EX_TOPMOST = 0x00000008u

    /// <summary>
    /// Specifies a window that should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
    /// The window appears transparent because the bits of underlying sibling windows have already been painted.
    /// To achieve transparency without these restrictions, use the SetWindowRgn function.
    /// </summary>
    | WS_EX_TRANSPARENT = 0x00000020u

    /// <summary>Specifies a window that has a border with a raised edge.</summary>
    | WS_EX_WINDOWEDGE = 0x00000100u

[<Flags>]
type WindowStyles =
    /// <summary>The window has a thin-line border.</summary>
    | WS_BORDER = 0x800000u

    /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
    | WS_CAPTION = 0xc00000u

    /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
    | WS_CHILD = 0x40000000u

    /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
    | WS_CLIPCHILDREN = 0x2000000u

    /// <summary>
    /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
    /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
    /// </summary>
    | WS_CLIPSIBLINGS = 0x4000000u

    /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
    | WS_DISABLED = 0x8000000u

    /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
    | WS_DLGFRAME = 0x400000u

    /// <summary>
    /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
    /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
    /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
    /// </summary>
    | WS_GROUP = 0x20000u

    /// <summary>The window has a horizontal scroll bar.</summary>
    | WS_HSCROLL = 0x100000u

    /// <summary>The window is initially maximized.</summary>
    | WS_MAXIMIZE = 0x1000000u

    /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
    | WS_MAXIMIZEBOX = 0x10000u

    /// <summary>The window is initially minimized.</summary>
    | WS_MINIMIZE = 0x20000000u

    /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
    | WS_MINIMIZEBOX = 0x20000u

    /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
    | WS_OVERLAPPED = 0x0u

    /// <summary>The window is an overlapped window.</summary>
    | WS_OVERLAPPEDWINDOW = 0xcf0000u // WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX

    /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
    | WS_POPUP = 0x80000000u

    /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
    | WS_POPUPWINDOW = 0x80880000u // WS_POPUP ||| WS_BORDER ||| WS_SYSMENU

    /// <summary>The window has a sizing border.</summary>
    | WS_SIZEFRAME = 0x40000u

    /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
    | WS_SYSMENU = 0x80000u

    /// <summary>
    /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
    /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.  
    /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
    /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
    /// </summary>
    | WS_TABSTOP = 0x10000u

    /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
    | WS_VISIBLE = 0x10000000u

    /// <summary>The window has a vertical scroll bar.</summary>
    | WS_VSCROLL = 0x200000u

type WndProc = delegate of hwnd:nativeint * msg:WindowsMessage * wParam:nativeint * lParam:nativeint -> nativeint

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type WndClass=
    val style: uint
    val lpfnWndProc: WndProc
    val cbClsExtra: int
    val cbWndExtra: int
    val hInstance: nativeint
    val hIcon: nativeint
    val hCursor: nativeint
    val hbrBackground: nativeint
    [<MarshalAs(UnmanagedType.LPWStr)>]
    val lpszMenuName: string
    [<MarshalAs(UnmanagedType.LPWStr)>]
    val lpszClassName: string

    new (lpfnWndProc, hInstance, lpszClassName) = {
        style = 0u
        lpfnWndProc = lpfnWndProc
        cbClsExtra = 0
        cbWndExtra = 0
        hInstance = hInstance
        hIcon = IntPtr.Zero
        hCursor = IntPtr.Zero
        hbrBackground = IntPtr.Zero
        lpszMenuName = String.Empty
        lpszClassName = lpszClassName
    }

[<Flags>]
type IconState =
    | Visible = 0x00
    | Hidden = 0x01

[<Flags>]
type IconDataMembers =
    /// <summary>
    /// The message ID is set.
    /// </summary>
    | Message = 0x01u

    /// <summary>
    /// The notification icon is set.
    /// </summary>
    | Icon = 0x02u

    /// <summary>
    /// The tooltip is set.
    /// </summary>
    | Tip = 0x04u

    /// <summary>
    /// State information (<see cref="IconState"/>) is set. This
    /// applies to both <see cref="NotifyIconData.IconState"/> and
    /// <see cref="NotifyIconData.StateMask"/>.
    /// </summary>
    | State = 0x08u

    /// <summary>
    /// The balloon ToolTip is set. Accordingly, the following
    /// members are set: <see cref="NotifyIconData.BalloonText"/>,
    /// <see cref="NotifyIconData.BalloonTitle"/>, <see cref="NotifyIconData.BalloonFlags"/>,
    /// and <see cref="NotifyIconData.VersionOrTimeout"/>.
    /// </summary>
    | Info = 0x10u

    // Internal identifier is set. Reserved, thus commented out.
    //Guid = 0x20,

    /// <summary>
    /// Windows Vista (Shell32.dll version 6.0.6) and later. If the ToolTip
    /// cannot be displayed immediately, discard it.<br/>
    /// Use this flag for ToolTips that represent real-time information which
    /// would be meaningless or misleading if displayed at a later time.
    /// For example, a message that states "Your telephone is ringing."<br/>
    /// This modifies and must be combined with the <see cref="Info"/> flag.
    /// </summary>
    | Realtime = 0x40u

    /// <summary>
    /// Windows Vista (Shell32.dll version 6.0.6) and later.
    /// Use the standard ToolTip. Normally, when uVersion is set
    /// to NOTIFYICON_VERSION_4, the standard ToolTip is replaced
    /// by the application-drawn pop-up user interface (UI).
    /// If the application wants to show the standard tooltip
    /// in that case, regardless of whether the on-hover UI is showing,
    /// it can specify NIF_SHOWTIP to indicate the standard tooltip
    /// should still be shown.<br/>
    /// Note that the NIF_SHOWTIP flag is effective until the next call 
    /// to Shell_NotifyIcon.
    /// </summary>
    | UseLegacyToolTips = 0x80u

[<Flags>]
type NotifyCommand =
    /// <summary>
    /// The taskbar icon is being created.
    /// </summary>
    | Add = 0x00u

    /// <summary>
    /// The settings of the taskbar icon are being updated.
    /// </summary>
    | Modify = 0x01u

    /// <summary>
    /// The taskbar icon is deleted.
    /// </summary>
    | Delete = 0x02u

    /// <summary>
    /// Focus is returned to the taskbar icon. Currently not in use.
    /// </summary>
    | SetFocus = 0x03u

    /// <summary>
    /// Shell32.dll version 5.0 and later only. Instructs the taskbar
    /// to behave according to the version number specified in the 
    /// uVersion member of the structure pointed to by lpdata.
    /// This message allows you to specify whether you want the version
    /// 5.0 behavior found on Microsoft Windows 2000 systems, or the
    /// behavior found on earlier Shell versions. The default value for
    /// uVersion is zero, indicating that the original Windows 95 notify
    /// icon behavior should be used.
    /// </summary>
    | SetVersion = 0x04u

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type NotifyIconData =
    val cbSize: int // DWORD
    val hWnd: nativeint // HWND
    val uID: uint // UINT
    val mutable uFlags: uint // UINT
    val uCallbackMessage: uint // UINT
    val mutable hIcon: nativeint // HICON
    [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)>]
    val mutable szTip: string // char[128]
    val dwState: IconState // DWORD
    val dwStateMask: IconState // DWORD
    [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)>]
    val szInfo: string // char[256]
    val uTimeoutOrVersion: uint // UINT
    [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)>]
    val szInfoTitle: string // char[64]
    val dwInfoFlags: int // DWORD

    new (handle, uCallbackMessage) = { 
        cbSize = Marshal.SizeOf(typeof<NotifyIconData>)
        hWnd = handle
        uID = 0x0u
        uFlags = 0x0u
        uCallbackMessage = uCallbackMessage
        hIcon = IntPtr.Zero
        szTip = "tooltip"
        dwState = IconState.Hidden
        dwStateMask = IconState.Hidden
        szInfo = String.Empty
        uTimeoutOrVersion = 0x4u
        szInfoTitle = String.Empty
        dwInfoFlags = 0
    }

[<Flags>]
type MenuFlags =
    | MF_STRING = 0u
    | MF_BYPOSITION = 0x400u
    | MF_SEPARATOR = 0x800u
    | MF_REMOVE = 0x1000u

[<Flags>]
type MenuItemInfoFMask =
    | MIIM_BITMAP = 0x00000080u
    | MIIM_CHECKMARKS = 0x00000008u
    | MIIM_DATA = 0x00000020u
    | MIIM_FTYPE = 0x00000100u
    | MIIM_ID = 0x00000002u
    | MIIM_STATE = 0x00000001u
    | MIIM_STRING = 0x00000040u
    | MIIM_SUBMENU = 0x00000004u
    | MIIM_TYPE = 0x00000010u

[<Flags>]
type MenuItemInfoFType =
    | MFT_BITMAP = 0x00000004u
    | MFT_MENUBARBREAK = 0x00000020u
    | MFT_MENUBREAK = 0x00000040u
    | MFT_OWNERDRAW = 0x00000100u
    | MFT_RADIOCHECK = 0x00000200u
    | MFT_RIGHTJUSTIFY = 0x00004000u
    | MFT_RIGHTORDER = 0x00002000u
    | MFT_SEPARATOR = 0x00000800u
    | MFT_STRING = 0x00000000u

[<Flags>]
type MenuItemInfoFState =
    | MFS_CHECKED = 0x00000008u
    | MFS_DEFAULT = 0x00001000u
    | MFS_DISABLED = 0x00000003u
    | MFS_ENABLED = 0x00000000u
    | MFS_GRAYED = 0x00000003u
    | MFS_HILITE = 0x00000080u
    | MFS_UNCHECKED = 0x00000000u
    | MFS_UNHILITE = 0x00000000u

[<Flags>]
type MenuItemInfoHBmpItem =
    | HBMMENU_CALLBACK = -1
    | HBMMENU_MBAR_CLOSE = 5
    | HBMMENU_MBAR_CLOSE_D = 6
    | HBMMENU_MBAR_MINIMIZE = 3
    | HBMMENU_MBAR_MINIMIZE_D = 7
    | HBMMENU_MBAR_RESTORE = 2
    | HBMMENU_POPUP_CLOSE = 8
    | HBMMENU_POPUP_MAXIMIZE = 10
    | HBMMENU_POPUP_MINIMIZE = 11
    | HBMMENU_POPUP_RESTORE = 9
    | HBMMENU_SYSTEM = 1

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type MenuItemInfo =
    val cbSize: uint //UINT
    val fMask: MenuItemInfoFMask//UINT
    val fType: MenuItemInfoFType //UINT
    val fState: MenuItemInfoFState //UINT
    val wID: uint //UINT
    val hSubMenu: nativeint //HMENU
    val hbmpChecked: nativeint //HBITMAP
    val hbmpUnchecked: nativeint //HBITMAP
    val dwItemData: nativeint //ULONG_PTR
    val dwTypeData: nativeint //LPSTR
    val cch: uint //UINT
    val hbmpItem: nativeint //HBITMAP

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type Rect =
    val left: int
    val top: int
    val right: int
    val bottom: int

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type Point =
    val x: int
    val y: int