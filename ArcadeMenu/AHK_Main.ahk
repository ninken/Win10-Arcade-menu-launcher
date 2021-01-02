Run C:\Autohotkey\ArcadeMenu\ArcadeMenu.exe

; Main System Menu
F12::
Run C:\Autohotkey\ArcadeMenu\ArcadeMenu.exe
return 

;Coin1
;F9::
;return 

;Coin2
;F8::
;return 

;Arcade Menu
;F1::
;return 

;Pause 
$Pause::
Process, Exist, VPinballX.exe
If (ErrorLevel = 0) ; If it is not running
	{
	send Pause
	}
Else 
	{
	send Q ; Program is running
	}
return


;Pinball Start
1::
;return

;Pinball Coin = 3, S - Special
$S::
Process, Exist, VPinballX.exe
If (ErrorLevel = 0) ; If it is not running
	{
	send s
	}
Else 
	{
	send 3S ; Program is running
	}
return	


; Escape
;ESC::
;return 


;Alt+F4 Quit...
;VolUp
;VolDown