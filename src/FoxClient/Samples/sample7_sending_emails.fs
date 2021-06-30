/*
* FoxSharp v1.0
* Sample 7: smtp data type for sending emails.
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/


// smpt structure
myEmail := smtp {
   "provider": "smtp.gmail.com",
   "port": 587,
   "user": "admin@any.com",
   "pass": "secret",
   "from": "john@any.com",
   "to": "doe@any.com",
   "subject": "have you seen this language before?",
   "body": "it is kinda cool",
};

// send the email by using the function send()
if (send(myEmail)) {
   info("message sent");
} else {
   error("error while sending the message");
}


