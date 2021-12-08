open System

open NotifyIcon

printfn "press any button to create notifyicon"

Console.ReadKey() |> ignore

let icon = Icon.create "yukibox.ico"
match icon with
| Error e -> raise e
| Ok icon ->
    let notifyIcon = NotifyIcon.create()
    notifyIcon.setIcon icon
    notifyIcon.setTooltip "Console App Tooltip"
    
    // not working in console app
    //notifyIcon.onMouseLeftButtonClick.Add (fun _ -> (
    //    printf "clicked"
    //))
    //notifyIcon.onMouseRightButtonClick.Add (fun _ -> (
    //    notifyIcon.Dispose()
    //    icon.Dispose()
    //))

    printf "right click or press any key to destroy tray icon"

    Console.ReadKey() |> ignore

    notifyIcon.Dispose()
    icon.Dispose()
