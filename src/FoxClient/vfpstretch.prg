*====================================================================
* VfpStretch Class
*====================================================================
define class VfpStretch as custom
	nOriginalHeight		= 0
	nOriginalWidth		= 0
	oForm				= .null.
	*========================================================================*
	* Function Init
	*========================================================================*
	function init
		if type('_screen.CurrentZoom') = 'U'
			=addproperty(_screen, 'CurrentZoom', 0)
		endif
	endfunc
	*========================================================================*
	* function Do
	*========================================================================*
	function do(toThisform)
		with this
			.oForm = toThisform

			if type(".oForm") != 'O'
				.oForm = iif(type('_Screen.ActiveForm') == 'O', _screen.activeform, .null.)
				if isnull(.oForm)
					messagebox("You must create VfpStretch object inside a form!", 48, "Warning!")
					return .f.
				endif
			endif

			.oForm.minheight = .oForm.height
			.oForm.minwidth  = .oForm.width

			.nOriginalHeight = .oForm.height
			.nOriginalWidth  = .oForm.width
			.SaveContainer(.oForm)

			=bindevent(.oForm, "Resize", this, "Stretch", 1)
			.zoom()
		endwith
	endfunc
	*========================================================================*
	* Function ResetSize
	*========================================================================*
	function ResetSize
		with this.oForm
			.height = this.nOriginalHeight
			.width  = this.nOriginalWidth
		endwith
	endfunc
	*========================================================================*
	* Function SaveContainer
	*========================================================================*
	function SaveContainer
		lparameters oContainer
		with this
			local oThis
			.SaveOriginalSize(m.oContainer)
			for each m.oThis in m.oContainer.controls
				if m.oThis.tag <> "NOSTRETCH"
					if !m.oThis.baseclass == 'Custom'
						.SaveOriginalSize(m.oThis)
					endif

					if type("m.oThis.Anchor") = "N" and m.oThis.anchor > 0
						m.oThis.anchor = 0
					endif

					do case
					case m.oThis.baseclass == 'Container'
						.SaveContainer(m.oThis)
					case m.oThis.baseclass == 'Pageframe'
						local oPage
						for each oPage in m.oThis.pages
							.SaveContainer(m.oPage)
						endfor
					case m.oThis.baseclass == 'Grid'
						local oColumn
						for each oColumn in m.oThis.columns
							.SaveOriginalSize(m.oColumn)
						endfor
					case m.oThis.baseclass $ 'Commandgroup,Optiongroup'
						local oButton
						for each oButton in m.oThis.buttons
							.SaveOriginalSize(m.oButton)
						endfor
					endcase
				endif
			endfor
		endwith
	endfunc
	*========================================================================*
	* Function SaveOriginalSize
	*========================================================================*
	function SaveOriginalSize
		lparameters oObject
		if pemstatus(m.oObject, 'Width', 5)
			if !pemstatus(m.oObject, '_nOriginalWidth', 5)
				=addproperty(m.oObject, "_nOriginalWidth", m.oObject.width)
			endif

			if pemstatus(m.oObject, 'Height', 5)
				if !pemstatus(m.oObject, '_nOriginalHeight', 5)
					=addproperty(m.oObject, "_nOriginalHeight", m.oObject.height)
				endif
				if !pemstatus(m.oObject, '_nOriginalLeft', 5)
					=addproperty(m.oObject, "_nOriginalLeft", m.oObject.left)
				endif
				if !pemstatus(m.oObject, '_nOriginalTop', 5)
					=addproperty(m.oObject, "_nOriginalTop", m.oObject.top)
				endif
			endif
		endif

		if pemstatus(m.oObject, 'Fontsize', 5)
			=addproperty(m.oObject, "_nOriginalFontsize", m.oObject.fontsize)
		endif

		if pemstatus(m.oObject, 'RowHeight', 5)
			=addproperty(m.oObject, "_nOriginalRowheight", m.oObject.rowheight)
		endif
	endfunc
	*========================================================================*
	* Function Stretch
	*========================================================================*
	function stretch
		lparameters oContainer
		with this
			if type("oContainer") != "O"
				oContainer = .oForm
			endif

			local oThis
			if m.oContainer.baseclass == 'Form'
				m.oContainer.lockscreen = .t.
			else
				.AdjustSize(m.oContainer)
			endif

			for each m.oThis in m.oContainer.controls
				if !m.oThis.baseclass == 'Custom'
					.AdjustSize(m.oThis)
				endif
				do case
				case m.oThis.baseclass == 'Container'
					.stretch(m.oThis)
				case m.oThis.baseclass == 'Pageframe'
					local oPage
					for each oPage in m.oThis.pages
						.stretch(m.oPage)
					endfor
				case m.oThis.baseclass == 'Grid'
					local oColumn
					for each oColumn in m.oThis.columns
						.AdjustSize(m.oColumn)
					endfor
				case m.oThis.baseclass $ 'Commandgroup,Optiongroup'
					local oButton
					for each oButton in m.oThis.buttons
						.AdjustSize(m.oButton)
					endfor
				endcase
			endfor
			if m.oContainer.baseclass == 'Form'
				m.oContainer.lockscreen = .f.
			endif
		endwith
	endfunc
	*========================================================================*
	* Function AdjustSize
	*========================================================================*
	function AdjustSize
		lparameters oObject
		local nHeightRatio, nWidthRatio
		m.nHeightRatio 	= this.oForm.height / this.nOriginalHeight
		m.nWidthRatio 	= this.oForm.width / this.nOriginalWidth

		with m.oObject
			if pemstatus(m.oObject, '_nOriginalWidth', 5)
				.width  = ._nOriginalWidth * m.nWidthRatio
				if pemstatus(m.oObject, '_nOriginalHeight', 5)
					.height = ._nOriginalHeight * m.nHeightRatio
					.top    = ._nOriginalTop * m.nHeightRatio
					.left   = ._nOriginalLeft * m.nWidthRatio
				endif
			endif

			if pemstatus(m.oObject, '_nOriginalFontsize', 5)
				.fontsize = max(4, round(._nOriginalFontsize * ;
					iif(abs(m.nHeightRatio) < abs(m.nWidthRatio), m.nHeightRatio, m.nWidthRatio), 0))
			endif

			if pemstatus(m.oObject, '_nOriginalRowheight', 5)
				.rowheight = ._nOriginalRowheight * m.nHeightRatio
			endif

			if .baseclass == 'Control' and pemstatus(m.oObject, 'RepositionContents', 5)
				.RepositionContents()
			endif
		endwith
	endfunc
	*========================================================================*
	* Function Zoom
	*========================================================================*
	function zoom
		if type('_screen.CurrentZoom') == 'N' and type('_Screen.ActiveForm') = 'O'
			do case
			case _screen.CurrentZoom > 0 and _screen.CurrentZoom < 100
				*-- Width
				nDisp 	= _screen.width - _screen.activeform.minwidth
				nWidth 	= int(nDisp * (_screen.CurrentZoom / 100))
				_screen.activeform.width = _screen.activeform.minwidth + nWidth
				*-- Height
				nScrHei = _screen.height - 25
				nDisp 	= nScrHei - _screen.activeform.minheight
				nHeight = int(nDisp * (_screen.CurrentZoom / 100))
				_screen.activeform.height = _screen.activeform.minheight + nHeight
			case _screen.CurrentZoom = 100
				_screen.activeform.windowstate = 2
			otherwise
				*-- Normal (Zoom = 0)
				_screen.activeform.width  = _screen.activeform.minwidth
				_screen.activeform.height = _screen.activeform.minheight
			endcase
			_screen.activeform.autocenter = .t.
		endif
	endfunc
enddefine