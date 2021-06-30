/*
* FoxSharp v1.0
* Sample 12: variadic function
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

sumAll := fn(numbers...){
   total := 0
   for (x in numbers){
      total += x;
   }
   return total;
};

sumAll(1, 2, 3, 4, 5);