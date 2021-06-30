using FoxSharp;
using System;
namespace TestFoxSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestScanner();
            //TestParser();
            //TestEvaluator();
        }
        public static void TestEvaluator()
        {
            try
            {
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadString("true or irwin");
                //scanner.ReadFile(@"c:\a1\test.txt");
                FoxSharp.Parser parser = new FoxSharp.Parser(scanner);
                var program = parser.ParseProgram();
                var errors = parser.GetErrors();
                if (errors.Count > 0){
                    foreach (var msg in errors){
                        Console.WriteLine("Error: " + msg);
                    }
                }
                FoxSharp.Environment globalEnv = new FoxSharp.Environment();                
                var evaluated = Evaluator.Eval(program, globalEnv);
                if (evaluated != null){
                    Console.WriteLine(evaluated.Inspect());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void TestScanner()
        {
            try
            {
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadString("saludar := fn(data.name)");
                //scanner.ReadFile(@"c:\a1\test.txt");
                var tok = scanner.NextToken();
                while (tok.type != TokenType.EOF)
                {
                    Console.WriteLine(tok.ToString());
                    tok = scanner.NextToken();
                }
                Console.WriteLine(tok.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void TestParser()
        {
            try
            {
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadFile(@"c:\a1\test.txt");
                //scanner.ReadString("panic(err);");
                FoxSharp.Parser parser = new FoxSharp.Parser(scanner);
                var program = parser.ParseProgram();
                var errors = parser.GetErrors();
                if (errors.Count > 0)
                {
                    foreach (var msg in errors)
                    {
                        Console.WriteLine("Error: " + msg);
                    }
                }
                Console.WriteLine(program.Inspect());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
