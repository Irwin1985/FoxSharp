# FoxSharp
Domain Specific Language for Visual FoxPro and C#

### Play with FoxSharp

You can play for a while with the language by lauching the FoxSharpREPL.app inside your Visual FoxPro IDE

```
cd c:\my-foxsharp\folder\
do FoxSharpREPL.app
```
![FoxSharp REPL](https://github.com/Irwin1985/FoxSharp/blob/main/repl.png)


### Setup FoxSharp inside a real Project
- Add **foxsharp.prg** to your project.
- Bring **foxsharp.prg** to scope with ```SET PROCEDURE TO foxsharp.prg ADDITIVE```
- Hit the command ```DO foxSharp.prg```

### Running code
FoxSharp will create a Screen Property Called **Foxsharp** which you can execute from.

```XBase
* Create FoxSharp class
do FoxSharp.prg
if _screen.foxsharp.hasErrors()
   _screen.foxsharp.printErrors()
endif
```
Put the code above in any place of your main PRG file. If there's no errors then the FoxSharp property will be created in you main Screen thus you can run code anywhere in your program.

```XBase
text to lcScript noshow
// sample code
var add = fn(x, y) { return x + y; };
add(10, 15);
endtext
lcResult = _screen.FoxSharp.runCode(lcScript)
if _screen.foxsharp.hasErrors()
   _screen.foxsharp.printErrors()
endif
if type('lcResult') == 'C'
   messagebox("Output from FoxSharp: " + lcResult)
endif
```

### The very basics of FoxSharp
```Javascript
// single line comment
/*
* Multiline comment
*/

// primitive data types
12345; // integer
3.14159265358979; // double
"Hello everybody"; // strings
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

### Data Types
```Javascript
// integer
info(1985);

// double
info(35.1985);

// string
info("Hello word\nBye world!");

// arrays
var languages = ["Visual FoxPro", "C#", "FoxSharp"];

// hash tables or dictionaries
data := {"firstName": "John", "lastName": "Doe", "age": 35, "married": false};
```

### While and For Loops
```Javascript
// while loop
a := 0;
while (a < 5){
   a += 1;
   info(format("Value of a = {0}", a));
}

// for loop
languages := ["Visual FoxPro", "Java", "C#", "Haskell", "Python", "FoxSharp"]

// iterate through array values
for (i in languages){
   info(format("I like \"{0}\" programming language", i));
}

// iterate through index and value of array
for (i, j in languages){
   info(format("languages[{0}] = \"{1}\"", i, j));
}
```

### Functions
```Javascript
/*
* Functions in FoxSharp are High Order and FirstClass Citizen
* that means you can thread them like you threat variables.
*/

// basic function declaration using pascal notation
add := fn(x, y) {
   return x + y;
}

// function declaration using 'var' keyword.
var circleArea = fn(radio) {
   PI := 3.14159265358979;
   return PI * radio ^ 2;
}

// calling functions
info(add(5, 10));

radio := 7;
info(circleArea(radio));
```

### Closures
```Javascript
/*
* a closure is a function inside of another function that
* could use the parent function symbols (lexical scope).
*/
var makeName = fn(firstName) {
   makeFullName := fn(lastName) {
      return info(format("Your fullname is: {0} {1}", firstName, lastName));
   };
   return makeFullName;
};

// make name
myName := makeName("John");

// make full name
myName("Doe");
```

### High Order Functions
```Javascript
// a function doubleMe that double the given number
var doubleMe = fn(x) { return x + x; };
info(doubleMe(4));


// define function trippleMe which accepts a function as argument.
var trippleMe = fn(number, fnDouble) {
   return fnDouble(number) + number;
};

info(trippleMe(4, doubleMe));

// note you just pass the function name instead of calling it with '(' and ')'
```

### Sending Emails
```Javascript
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
```

### Sending Email with Attachments
```Javascript
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
   "body": "it is kinda cool",
};

// use the "files" property and add one or more files to your email

// send the email by using the function send()
if (send(myEmail)) {
   info("message sent");
} else {
   error("error while sending the message");
}
```
### Sending Email with HTML body
```Javascript
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
```
### Input and Output
```Javascript
info("this is an info message");
warning("this is a warning message");
error("this is an error message");

// input data from user
name := inputbox("What's your name?");
if (len(name) > 0) {
   info(format("nice to meet you {0}!", name));
} else {
   info("you dont have any name");
}

// question message returns boolean
resp := question("Do you like FoxSharp?");
if (resp){
   info("Welcome aboard!");
} else {
   error("Get out my boat please :)");
}
```
### Variadic Functions
```Javascript
// NOTE: remember pass the last argument as variadic.
sumAll := fn(numbers...){
   total := 0
   for (x in numbers){
      total += x;
   }
   return total;
};

sumAll(1, 2, 3, 4, 5);

// example 2
add := fn(x, y) { return x + y; };
sub := fn(x, y) { return x - y; };
mul := fn(x, y) { return x * y; };
div := fn(x, y) { return x / y; };

compute := fn(x, y, functions...){
   for (fun in functions){
      info(fun(x, y));
   };
};

compute(10, 2, add, sub, mul, div);
```
### Downloading from internet
```Javascript
/*
* Download any file from internet
*/
if (download("https://github.com/Irwin1985/FoxSharp/edit/main/README.md", "c:\\my-folder\\README.md")){
   info("success!");
} else {
   error("something went wrong!");
}
```
