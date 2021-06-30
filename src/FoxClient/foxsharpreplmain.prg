* Create instance of VfpStretch
set procedure to "VFPStretch.prg" additive
if type("_Screen.oVfpStretch") = "O"
	removeproperty(_screen, "oVfpStretch")
endif
addproperty(_screen, "oVfpStretch", createobject("vfpStretch"))

* Create FoxSharp class
do FoxSharp.prg
if _screen.foxsharp.hasErrors()
	_screen.foxsharp.printErrors()
endif

* Bring FoxSharp to scope
set procedure to "FoxSharp" additive

* Call the REPL
do form foxsharpRepl
