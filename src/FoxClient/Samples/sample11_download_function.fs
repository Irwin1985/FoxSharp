/*
* Download any file from internet
*/
if (download("https://github.com/Irwin1985/FoxSharp/edit/main/README.md", "c:\\my-folder\\README.md")){
   info("success!");
} else {
   error("something went wrong!");
}