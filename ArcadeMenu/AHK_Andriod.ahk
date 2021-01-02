#NoEnv
for process in ComObjGet("winmgmts:").ExecQuery("Select * from Win32_Process Where ExecutablePath = ""C:\\Emulators\\Microvirt\\MEmu\\MEmu.exe"" ")
{
;Process Found Maximize it...
 if (process.ExecutablePath =  "C:\Emulators\Microvirt\MEmu\MEmu.exe")
    PID := ""
    PID := process.Handle
    WinActivate, ahk_pid %PID%
    ;WinMaximize, ahk_pid %PID%
	ExitApp
}

; Process not found run it...
try{
Run, C:\Emulators\Microvirt\MEmu\MEmuConsole.exe MEmu
}
;======= Send Full Screen...
sleep, 15000 
for process in ComObjGet("winmgmts:").ExecQuery("Select * from Win32_Process Where ExecutablePath = ""C:\\Emulators\\Microvirt\\MEmu\\MEmu.exe"" ")
{
 if (process.ExecutablePath =  "C:\Emulators\Microvirt\MEmu\MEmu.exe")
    PID := ""
    PID := process.Handle
    WinActivate, ahk_pid %PID%
    Send {F11}
    ;WinMaximize, ahk_pid %PID%
	ExitApp
 }

ExitApp