# FoxSharp
Glue programming language between Visual FoxPro and C#

### The very basics of FoxSharp
```Javascript
// single line comment
/*
* Multiline comment
*/

// primitive data types
12345; // integer (32 bit)
3.14159265358979; // float (64 bit)
"Hello everybody！大家好!"; // strings (utf-8)
true; // boolean true
false; // boolean false
null; // absence of value

// variable binding using var keyword
var a = 10;

// variable binding using pascal binding notation
b := 20; // note the colon (:) before equal sign.

// arithmetic expression
a + b;
a - b;
a * b;
a / b;
a ^ b; // exponent

// boolean comparison
a < b;
a > b;
a <= b;
a >= b;
a == b;
a != b;

// logical expression
a and b;
a or b;
a xor b;

// arrays
var fruits = ["apple", "orange", "strawberry"];
// arrays manipulation
fruits.size();
fruits.push("peach");

// hash or dictionaries
var data = {
  "name": "john",
  "age": 35,
  "salary": 123.45,
  "married": true,
  "has_children": false,
  "own_house": null,
};

```

# Examples
### Sending emails with smtp{} struct
```Javascript
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
```
