#NoEnv
SetTitleMatchMode, 2

sleep, 3000 
for process in ComObjGet("winmgmts:").ExecQuery("Select * from Win32_Process Where ExecutablePath = ""C:\\Emulators\\RPCS3-PS3\\rpcs3.exe"" ")
{
;Process Found Maximize it...
 if (process.ExecutablePath =  "C:\Emulators\RPCS3-PS3\rpcs3.exe")
    PID := ""
    PID := process.Handle
    WinActivate, ahk_pid %PID%
    WinMaximize, ahk_pid %PID%


}

WinWait, FPS:, , 15
if ErrorLevel
{
    return
}
else
     Send {Alt Down}{Enter}{Alt Up} ;
ExitApp