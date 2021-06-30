/*
* FoxSharp v1.0
* Sample 12: variadic function
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

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

