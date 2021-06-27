using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class StringReader
    {
        int curPosition = 0;
        string input;
        private int lineNumber = 1;
        private int columnNumber = 0;

        public StringReader() { }
        public StringReader(string input)
        {
            this.input = input;
            this.curPosition = -1;
        }
        public char Read()
        {
            curPosition += 1;
            char chr = (char)0;
            if (curPosition < input.Length) {
                chr = input[curPosition];
                columnNumber += 1;
            }
            if (chr == '\n') {
                lineNumber += 1;
                columnNumber = 0;
            }
            return chr;
        }
        public char Peek()
        {
            int peekPos = curPosition + 1;
            if (peekPos >= input.Length)
            {
                return (char)0;
            } else
            {
                return input[peekPos];
            }
        }
        public bool IsAtEnd()
        {
            return curPosition + 1 >= input.Length;
        }
        public int GetLineNumber()
        {
            return lineNumber;
        }
        public int GetColumnNumber()
        {
            return columnNumber;
        }
    }
}
