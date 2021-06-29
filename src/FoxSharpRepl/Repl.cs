using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoxSharp;

namespace FoxSharpRepl
{
    public class Repl
    {
        static string FOXSHARP_VERSION = "1.0";
        static void Main(string[] args)
        {
            FoxSharp.Environment globalEnv = new FoxSharp.Environment();
            DisplayWelcome();            
            while (true)
            {
                Console.Write(">>> ");
                var input = Console.ReadLine();
                
                if (input.Length == 0) continue;
                if (input == "quit") break;
                if (input.Substring(0,3) == "run"){
                    var fileName = input.Substring(3).TrimStart();
                    try
                    {
                        input = System.IO.File.ReadAllText(fileName);
                    }catch (Exception e){
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadString(input);
                FoxSharp.Parser parser = new FoxSharp.Parser(scanner);

                var program = parser.ParseProgram();
                if (!CheckParserErrors(parser)) continue;
                var evaluated = Evaluator.Eval(program, globalEnv);
                if (evaluated != null){
                    Console.WriteLine(evaluated.Inspect());
                }
            }
        }
        private static bool CheckParserErrors(FoxSharp.Parser p){
            var errors = p.GetErrors();
            if (errors.Count > 0){
                foreach (var msg in errors){
                    Console.WriteLine("Parsing Error: " + msg);
                }
                return false;
            }
            return true;
        }
        private static void DisplayWelcome()
        {
            Console.WriteLine(String.Format("Welcome to FoxSharp, Version {0}", FOXSHARP_VERSION));
            Console.WriteLine(String.Format("Data and Time: {0}", DateTime.Now));
            Console.WriteLine("type 'quit' to exit");
        }
    }
}
