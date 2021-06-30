/*
* FoxSharp v1.0
* Sample 5: high order functions
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

// a function doubleMe that double the given number
var doubleMe = fn(x) { return x + x; };
info(doubleMe(4));


// define function trippleMe which accepts a function as argument.
var trippleMe = fn(number, fnDouble) {
   return fnDouble(number) + number;
};

info(trippleMe(4, doubleMe));

// note you just pass the function name instead of calling it with '(' and ')'