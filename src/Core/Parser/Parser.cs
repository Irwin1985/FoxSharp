using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class Parser
    {
        // precedence order
        const int LOWEST = 0;       // lowest priority
        const int LOGIC_OR = 1;     // logic or
        const int LOGIC_AND = 2;    // logic and
        const int EQUALS = 3;       // a == b
        const int COMPARISON = 4;   // a <, <=, >, >= b
        const int TERM = 5;         // a +, - b
        const int FACTOR = 6;       // a *, / b
        const int PREFIX = 7;       // -a, !b
        const int CALL = 8;         // foo()
        const int INDEX = 9;        // bar[1]

        // precedence dictionary
        private Dictionary<TokenType, int> precedence = new Dictionary<TokenType, int>();

        private Token curToken;
        private Token peekToken;
        public Parser()
        {
            precedence.Add(TokenType.OR, LOGIC_OR);
            precedence.Add(TokenType.AND, LOGIC_AND);
            precedence.Add(TokenType.EQ, EQUALS);
            precedence.Add(TokenType.LT, COMPARISON);
            precedence.Add(TokenType.LT_EQ, COMPARISON);
            precedence.Add(TokenType.GT, COMPARISON);
            precedence.Add(TokenType.GT_EQ, COMPARISON);
            precedence.Add(TokenType.PLUS, TERM);
            precedence.Add(TokenType.MINUS, TERM);
            precedence.Add(TokenType.MUL, FACTOR);
            precedence.Add(TokenType.DIV, FACTOR);
            precedence.Add(TokenType.LPAREN, CALL);
            precedence.Add(TokenType.LBRACKET, INDEX);
        }
        int CurPrecedence()
        {
            return precedence[curToken.type];
        }
    }
}
