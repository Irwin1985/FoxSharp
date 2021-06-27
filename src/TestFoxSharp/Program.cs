using FoxSharp;
using System;
namespace TestFoxSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FoxSharp.Scanner scanner = new FoxSharp.Scanner();
                scanner.ReadFile(@"c:\a1\test.txt");

                var tok = scanner.NextToken();
                while (tok.type != TokenType.EOF)
                {
                    Console.WriteLine(tok.ToString());
                    tok = scanner.NextToken();
                }
                Console.WriteLine(tok.ToString());
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
