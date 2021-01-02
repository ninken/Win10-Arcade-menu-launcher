#NoEnv
for process in ComObjGet("winmgmts:").ExecQuery("Select * from Win32_Process Where ExecutablePath = ""C:\\Games\\PinballY\\PinballY.exe"" ")
{
;Process Found Maximize it...
 if (process.ExecutablePath =  "C:\Games\PinballY\PinballY.exe")
    PID := ""
    PID := process.Handle
    WinActivate, ahk_pid %PID% 
    WinMaximize, ahk_pid %PID%
	ExitApp
}

; Process not found run it...
try{
Run, C:\Games\PinballY\PinballY.exe
}
ExitApp