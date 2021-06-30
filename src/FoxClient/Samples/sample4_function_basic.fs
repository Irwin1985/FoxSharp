/*
* FoxSharp v1.0
* Sample 4: functions
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

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


