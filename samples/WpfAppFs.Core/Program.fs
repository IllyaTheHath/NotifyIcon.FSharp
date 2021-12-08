module Program

open Elmish.WPF
open NotifyIcon

type Model = { 
    Created: bool
    Icon: Icon option
    NotifyIcon: NotifyIcon option
}

let init () = {
    Created = false
    Icon = None
    NotifyIcon = None
}

type Msg =
  | CreateIcon
  | DestroyIcon


let update msg m =
    match msg with
    | CreateIcon ->
        let icon = Icon.create "yukibox.ico"
        match icon with
        | Error e -> raise e
        | Ok icon ->
            let notifyIcon = NotifyIcon.create()
            notifyIcon.setIcon icon
            notifyIcon.setTooltip "Wpf App FSharp Tooltip"
            
            notifyIcon.onMouseLeftButtonClick.Add (fun _ -> (
                System.Windows.MessageBox.Show ("clicked") |> ignore
            ))
            { m with Created = true; Icon = icon |> Some; NotifyIcon = notifyIcon |> Some }
    | DestroyIcon ->
        match m.NotifyIcon with | Some ni -> ni.Dispose() | None -> ()
        match m.Icon with | Some i -> i.Dispose() | None -> ()
        { m with Created = false; Icon = None; NotifyIcon = None }

let bindings () =
    [
        "Created" |> Binding.oneWay (fun m -> m.Created)
        "NotCreated" |> Binding.oneWay (fun m -> not m.Created)
        "CreateIcon" |> Binding.cmd (fun m -> CreateIcon)
        "DestroyIcon" |> Binding.cmd (fun m -> DestroyIcon)
    ]

let main window =
    WpfProgram.mkSimple init update bindings
    |> WpfProgram.startElmishLoop window
