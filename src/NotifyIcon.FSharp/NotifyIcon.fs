module NotifyIcon

open System
open System.ComponentModel
open System.Diagnostics
open System.Runtime.InteropServices

open NativeStruct
open NativeInterop

type private MouseButtons =
    | LeftButton
    | MiddleButton
    | RightButton

type internal MouseButtonClick =
    | SingleClick
    | DoubleClick

type internal MouseEvent =
    | LeftButtonDown
    | LeftButtonUp
    | LeftButtonDoubleClick
    | RightButtonDown
    | RightButtonUp
    | RightButtonDoubleClick
    | MiddleButtonDown
    | MiddleButtonUp
    | MiddleButtonDoubleClick

type internal WindowMessageSink private (onMouseEvent) as x =
    let mutable disposed = false

    // The ID of messages that are received from the the taskbar icon
    let callbackMessageId = WindowsMessage.WM_USER

    /// Processes incoming system messages.
    let processWindowMessage msg (wParam:nativeint) (lParam:nativeint) =
        if msg = callbackMessageId then
            let message : WindowsMessage = lParam.ToInt32() |> uint 
                                           |> Microsoft.FSharp.Core.LanguagePrimitives.EnumOfValue
            match message with            
            | WindowsMessage.WM_LBUTTONDOWN ->
                onMouseEvent(MouseEvent.LeftButtonDown)
            | WindowsMessage.WM_LBUTTONUP ->
                onMouseEvent(MouseEvent.LeftButtonUp)
            | WindowsMessage.WM_LBUTTONDBLCLK ->
                onMouseEvent(MouseEvent.LeftButtonDoubleClick)
            
            | WindowsMessage.WM_RBUTTONDOWN -> 
                onMouseEvent(MouseEvent.RightButtonDown)
            | WindowsMessage.WM_RBUTTONUP ->
                onMouseEvent(MouseEvent.RightButtonUp)
            | WindowsMessage.WM_RBUTTONDBLCLK ->
                onMouseEvent(MouseEvent.RightButtonDoubleClick)

            | WindowsMessage.WM_MBUTTONDOWN -> 
                onMouseEvent(MouseEvent.MiddleButtonDown)
            | WindowsMessage.WM_MBUTTONUP ->
                onMouseEvent(MouseEvent.MiddleButtonUp)
            | WindowsMessage.WM_MBUTTONDBLCLK ->
                onMouseEvent(MouseEvent.MiddleButtonDoubleClick)

            | _ -> ignore()
        else if msg = WindowsMessage.WM_COMMAND then
            let cmd = wParam.ToInt32() |> uint
            Debug.WriteLine(msg)

    /// Callback method that receives messages from the taskbar area
    let onWindowMessageReceived hwnd msg wParam lParam =
        //forward message
        processWindowMessage msg wParam lParam
        // Pass the message to the default window procedure
        defWindowProc (hwnd, msg, wParam, lParam)
        
    //generate a unique ID for the window
    let moduleName = Process.GetCurrentProcess().ProcessName
    let windowId = moduleName + "_" + Guid.NewGuid().ToString()

    // register window message handler
    let messageHandler = WndProc(onWindowMessageReceived)

    // Create a simple window class which is reference through the messageHandler delegate
    let hInstance = Marshal.GetHINSTANCE (x.GetType().Module)
    let lpszClassName = windowId + "_class";
    let mutable wc = WndClass(messageHandler, hInstance, lpszClassName)

    // Register the window class
    let hwnd = registerClass(&wc)

    // Create the message window
    let messageWindowHandle = createWindowEx(
        WindowStylesEx.WS_EX_OVERLAPPEDWINDOW,
        hwnd,
        moduleName,
        WindowStyles.WS_CAPTION,
        0, 0, 0, 0,
        IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero)

    static member create(onMouseEvent) = new WindowMessageSink(onMouseEvent)
    member val CallbackMessageId = callbackMessageId
    member val MessageWindowHandle = messageWindowHandle

    member private x.dispose disposing = 
        if not disposed then
            if disposing then ()
            destroyWindow(messageWindowHandle) |> ignore
            disposed <- true
    interface IDisposable with
        member x.Dispose() =
            x.dispose true
            GC.SuppressFinalize(x)
    override x.Finalize() = 
        x.dispose false

type Icon private (handle) =
    let mutable disposed = false
    member val Handle : nativeint = handle

    static member create(filename) =
        let handle = loadImage (System.IntPtr.Zero, filename, ImageType.IMAGE_ICON, 32, 32, LoadImageFlags.LR_LOADFROMFILE)
        if handle = IntPtr.Zero then
            let exp = Marshal.GetLastWin32Error()
            exp |> Win32Exception |> Error
        else new Icon(handle) |> Ok

    member x.Dispose() =
        (x :> IDisposable).Dispose()
    member private x.dispose disposing = 
        if not disposed then
            if disposing then ()
            destroyIcon x.Handle |> ignore
            disposed <- true
    interface IDisposable with
        member x.Dispose() =
            x.dispose true
            GC.SuppressFinalize(x)
    override x.Finalize() = 
        x.dispose false

type NotifyIcon private () =
    let mutable disposed = false
    let mutable created = false
    let lockobj = obj()
    //let icon = icon
    
    let mouseLeftButtonClickEvent = new Event<obj>()
    let mouseLeftButtonDoubleClickEvent = new Event<obj>()
    let mouseRightButtonClickEvent = new Event<obj>()
    let mouseRightButtonDoubleClickEvent = new Event<obj>()
    let mouseMiddleButtonClickEvent = new Event<obj>()
    let mouseMiddleButtonDoubleClickEvent = new Event<obj>()

    let mutable doubleClick = false
    let wmMouseDown button click =
        match button, click with 
        | MouseButtons.LeftButton, MouseButtonClick.SingleClick -> ()
        | MouseButtons.LeftButton, MouseButtonClick.DoubleClick ->
            mouseLeftButtonDoubleClickEvent.Trigger ()
            doubleClick <- true
        | MouseButtons.RightButton, MouseButtonClick.SingleClick -> ()
        | MouseButtons.RightButton, MouseButtonClick.DoubleClick ->
            mouseRightButtonDoubleClickEvent.Trigger ()
            doubleClick <- true
        | MouseButtons.MiddleButton, MouseButtonClick.SingleClick -> ()
        | MouseButtons.MiddleButton, MouseButtonClick.DoubleClick ->
            mouseMiddleButtonDoubleClickEvent.Trigger ()
            doubleClick <- true

    let wmMouseUp button =
        match button with 
        | MouseButtons.LeftButton->
            if not doubleClick then
                mouseLeftButtonClickEvent.Trigger ()
            doubleClick <- false
        | MouseButtons.RightButton ->
            // context menu here
            if not doubleClick then
                mouseRightButtonClickEvent.Trigger ()
            doubleClick <- false
        | MouseButtons.MiddleButton ->
            if not doubleClick then
                mouseMiddleButtonClickEvent.Trigger ()
            doubleClick <- false

    let onMouseEvent event =
        match event with
        | MouseEvent.LeftButtonDown ->
            wmMouseDown MouseButtons.LeftButton MouseButtonClick.SingleClick
        | MouseEvent.LeftButtonUp ->
            wmMouseUp MouseButtons.LeftButton
        | MouseEvent.LeftButtonDoubleClick ->
            wmMouseDown MouseButtons.LeftButton MouseButtonClick.DoubleClick
            
        | MouseEvent.RightButtonDown ->
            wmMouseDown MouseButtons.RightButton MouseButtonClick.SingleClick
        | MouseEvent.RightButtonUp ->
            wmMouseUp MouseButtons.RightButton
        | MouseEvent.RightButtonDoubleClick ->
            wmMouseDown MouseButtons.RightButton MouseButtonClick.DoubleClick

        | MouseEvent.MiddleButtonDown ->
            wmMouseDown MouseButtons.MiddleButton MouseButtonClick.SingleClick
        | MouseEvent.MiddleButtonUp ->
            wmMouseUp MouseButtons.MiddleButton
        | MouseEvent.MiddleButtonDoubleClick ->
            wmMouseDown MouseButtons.MiddleButton MouseButtonClick.DoubleClick

    let messageSink = WindowMessageSink.create(onMouseEvent)
    let mutable notifyIconData = NotifyIconData(messageSink.MessageWindowHandle, messageSink.CallbackMessageId |> uint)

    let createTaskbarIcon() =
        lock lockobj (fun () ->
            if created then ignore()

            let members = IconDataMembers.Message ||| IconDataMembers.Icon ||| IconDataMembers.Tip
            notifyIconData.uFlags <- members |> uint
            let status = shellNotifyIcon(NotifyCommand.Add |> uint, &notifyIconData)
            created <- true
        )

    let removeTaskbarIcon() = 
        lock lockobj (fun () ->
            if not created then ignore()

            let status = shellNotifyIcon(NotifyCommand.Delete |> uint, &notifyIconData)
            created <- false
        )

    // Create Task Icon
    do createTaskbarIcon()

    [<CLIEvent>]
    member val onMouseLeftButtonClick = mouseLeftButtonClickEvent.Publish
    [<CLIEvent>]
    member val onMouseLeftButtonDoubleClick = mouseLeftButtonDoubleClickEvent.Publish
    [<CLIEvent>]
    member val onMouseRightButtonClick = mouseRightButtonClickEvent.Publish
    [<CLIEvent>]
    member val onMouseRightButtonDoubleClick = mouseRightButtonDoubleClickEvent.Publish
    [<CLIEvent>]
    member val onMouseMiddleButtonClick = mouseMiddleButtonClickEvent.Publish
    [<CLIEvent>]
    member val onMouseMiddleButtonDoubleClick = mouseMiddleButtonDoubleClickEvent.Publish

    static member create() = new NotifyIcon()

    member x.setIcon (icon:Icon) =
        if not created then ignore()

        notifyIconData.uFlags <- IconDataMembers.Icon |> uint
        notifyIconData.hIcon <- icon.Handle
        shellNotifyIcon(NotifyCommand.Modify |> uint, &notifyIconData) |> ignore

    member x.setTooltip (tooltip) =
        if not created then ignore()

        notifyIconData.uFlags <- IconDataMembers.Tip |> uint
        notifyIconData.szTip <- tooltip
        shellNotifyIcon(NotifyCommand.Modify |> uint, &notifyIconData) |> ignore

    member x.Dispose() =
        (x :> IDisposable).Dispose()
    member private x.dispose disposing = 
        lock lockobj (fun () ->
            if not disposed then
                if disposing then
                    (messageSink :> IDisposable).Dispose()
                removeTaskbarIcon()
                disposed <- true
        )
    interface IDisposable with
        member x.Dispose() = 
            x.dispose true
            GC.SuppressFinalize(x)
    override x.Finalize() = 
        x.dispose false 
