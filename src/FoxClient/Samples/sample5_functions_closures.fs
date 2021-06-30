/*
* FoxSharp v1.0
* Sample 6: functions closures
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

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
