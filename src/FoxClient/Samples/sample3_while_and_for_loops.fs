/*
* FoxSharp v1.0
* Sample 3: while | for loops
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

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
