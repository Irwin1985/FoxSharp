* test program
cd C:\Users\irwin.SUBIFOR\source\repos\FoxSharp\

set path to "src,src\FoxClient,src\Core,src\Core\obj\Debug;src\wwDotNetBridge" additive
do foxsharp_initialize.prg

if _screen.foxsharp.hasErrors()
	_screen.foxsharp.printErrors()
	return
endif

text to lcScript noshow
	// sending an email
	var email = smtp {
	   provider: "smtp.gmail.com",
	   port: 587,
	   user: "user_email@any.com",
	   pass: "user_secret",
	   from: "john@any.com",
	   to: "doe@any.com",
	   subject: "have you seen this language?",
	   body: "it's really handy"   
	};
	err := email.send();
	if (err != null){
	   panic(err);
	} else {
	   info("email sent!");
	}	
endtext
_screen.FoxSharp.run(lcScript)
_screen.foxsharp.printErrors()

clear all
release all

