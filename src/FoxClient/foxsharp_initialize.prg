if type('_screen.foxsharp') != 'c'
	=removeproperty(_screen, 'foxsharp')
endif
=addproperty(_screen, 'foxsharp', createobject('foxSharpclass'))

define class foxSharpclass as Custom 
	errorIndex = 0
	dimension errors(1)
		
	function init				
		this.checkdependecy('wwDotNetBridge.prg')
		this.checkdependecy('wwDotnetBridge.dll')
		this.checkdependecy('wwIPStuff.dll')
		this.checkdependecy('foxSharp.dll')
		if !this.hasErrors()
			DO wwDotnetBridge
			InitializeDotnetVersion("V4")
		endif
	endfunc
	
	function checkdependecy(dependencyName)
		if !file(dependencyName)
			this.pushError('[' + dependencyName + '] not found in program path.')
		endif		
	endfunc
	
	function pushError(msg)
		this.errorIndex = this.errorIndex + 1
		dimension this.errors(this.errorIndex)
		this.errors[this.errorIndex] = msg
	endfunc
	
	function hasErrors
		if alen(this.errors, 1) > 0
			return type('this.errors[1]') == 'C'
		endif
		return .f.
	endfunc

	function printErrors
		if alen(this.errors, 1) > 0
			for each msg in this.errors
				if type('msg') == 'C'
					messagebox(msg, 48, 'foxSharp errors')
				endif
			endfor
		endif
	endfunc
	
	function run(source) 
		try
			loBridge = GetwwDotnetBridge()
			if loBridge.LoadAssembly("FoxSharp.dll")
				loFoxSharp = loBridge.CreateInstance("FoxSharp.Core")				
				if !isnull(loFoxSharp)
					loFoxSharp.Run(source)
				else
					this.pushError('could not create the foxSharp instance')	
				endif				
			else
				this.pushError('could not load the assembly FoxSharp.dll')
			endif
		catch to loEx
			this.pushError('try/catch error: ' + loEx.Message)
		finally
			store .null. to loFoxSharp
			release loFoxSharp
		endtry
	endfunc
	
enddefine