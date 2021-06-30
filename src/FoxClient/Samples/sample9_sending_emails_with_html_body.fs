/*
* FoxSharp v1.0
* Sample 9: sending emails with HTML body.
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/


// smpt structure
myEmail := smtp {
   "provider": "smtp.gmail.com",
   "port": 587,
   "user": "admin@any.com",
   "pass": "secret",
   "from": "john@any.com",
   "to": ["doe@any.com", "ana@any.com", "peter@any.com"],
   "subject": "have you seen this language before?",
   "files": ["c:\\my-folder\\myfile1.pdf", "c:\\my-folder\\myfile2.ppt"],
   "html": load("c:\\my-folder\\myHTMLTemplate.html"),
};

// use load() function to load any file in memory (like filetostr() in vfp)

// send the email by using the function send()
if (send(myEmail)) {
   info("message sent");
} else {
   error("error while sending the message");
}



