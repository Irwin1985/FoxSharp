# FoxSharp
Glue programming language between Visual FoxPro and C#

# Examples
```
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
if (err != nil){
   info("success!")
} else {
   panic(err);
}
```
