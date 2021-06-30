/*
* FoxSharp v1.0
* Sample 10: builtins functions
* Author: Irwin Rodriguez <rodriguez.irwin@gmail.com>
*/

info("this is an info message");
warning("this is a warning message");
error("this is an error message");

// question message returns boolean
resp := question("Do you like FoxSharp?");
if (resp){
   info("Welcome aboard!");
} else {
   error("Get out my boat please :)");
}



