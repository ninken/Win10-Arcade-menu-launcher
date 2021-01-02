#NoEnv 
;https://stackoverflow.com/questions/51260810/can-you-bind-a-key-to-minimize-all-open-windows-with-autohotkey
try{
   MinimizeAll()
}

MinimizeAll(){
    DetectHiddenWindows Off
    WinGet, id, list
    Loop, %id%
    {
        this_ID := id%A_Index%
        If NOT IsWindow(WinExist("ahk_id" . this_ID))
            continue
        WinGet, WinState, MinMax, ahk_id %this_ID%
        If (WinState = -1)
            continue
        WinGetTitle, title, ahk_id %this_ID%
        If (title = "")
            continue
        WinMinimize, ahk_id %this_ID%
    }
}

RestoreAll(){
    DetectHiddenWindows Off
    WinGet, id, list
    Loop, %id%
    {
        this_ID := id%A_Index%
        If NOT IsWindow(WinExist("ahk_id" . this_ID))
            continue
        WinGet, WinState, MinMax, ahk_id %this_ID%
        If (WinState != -1)
            continue
        WinGetTitle, title, ahk_id %this_ID%
        If (title = "")
            continue
        WinRestore, ahk_id %this_ID%
    }
}

; Check whether the target window is activation target
IsWindow(hWnd){
    WinGet, dwStyle, Style, ahk_id %hWnd%
    if ((dwStyle&0x08000000) || !(dwStyle&0x10000000)) {
        return false
    }
    WinGet, dwExStyle, ExStyle, ahk_id %hWnd%
    if (dwExStyle & 0x00000080) {
        return false
    }
    WinGetClass, szClass, ahk_id %hWnd%
    if (szClass = "TApplication") {
        return false
    }
    return true
}

ExitApp