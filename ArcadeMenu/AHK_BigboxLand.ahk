#NoEnv
for process in ComObjGet("winmgmts:").ExecQuery("Select * from Win32_Process Where ExecutablePath = ""C:\\LaunchBox\\Core\\BigBox.exe"" ")
{
;Process Found Maximize it...
 if (process.ExecutablePath =  "C:\LaunchBox\Core\BigBox.exe")
    PID := ""
    PID := process.Handle
    WinActivate, ahk_pid %PID% 
    WinMaximize, ahk_pid %PID%
	ExitApp
}
for process in ComObjGet("winmgmts:").ExecQuery("Select * from Win32_Process Where ExecutablePath = ""C:\\LaunchBox-Vertical\\Core\\BigBox.exe"" ")
{
;Process Found Close it...
 if (process.ExecutablePath =  "C:\LaunchBox-Vertical\Core\BigBox.exe")
    PID := ""
    PID := process.Handle
	WinClose, ahk_pid %PID%
}

; Process not found run it...
try{
Run, C:\LaunchBox\BigBox.exe
}

ExitApp

