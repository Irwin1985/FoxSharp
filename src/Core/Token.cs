using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Token
    {
        public TokenType type;
        public string literal;
        public int lineNumber = 0;
        public int columnNumber = 0;

        public Token() { }
        public Token(int lineNumber, int columnNumber)
        {
            this.lineNumber = lineNumber;
            this.columnNumber = columnNumber;
        }
        public Token(TokenType type, string literal)
        {
            this.type = type;
            this.literal = literal;
        }

        public Token(TokenType type, char literal)
        {
            this.type = type;
            this.literal = literal.ToString();
        }

        public override string ToString()
        {
            return String.Format("type: {0}, literal: '{1}', line: {2}, col:{3}", type, literal, lineNumber, columnNumber);
        }
    }
    public enum TokenType
    {
        ILLEGAL,
        EOF,

        IDENT,
        INT,
        FLOAT,
        STRING,

        ASSIGN,
        PLUS,
        PLUS_EQ,
        MINUS,
        MINUS_EQ,
        NOT,
        MUL,
        MUL_EQ,
        DIV,
        DIV_EQ,
        POW,

        LT,
        LT_EQ,
        GT,
        GT_EQ,
        EQ,
        NOT_EQ,

        COMMA,
        SEMICOLON,
        DOT,
        COLON,

        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,
        LBRACKET,
        RBRACKET,

        // keywords
        FUNCTION,
        VAR,
        TRUE,
        FALSE,
        NULL,
        AND,
        OR,
        XOR,
        IF,
        ELSE,
        RETURN,
        BREAK,
        CONTINUE,
        WHILE,
        FOR,
        
        // output
        INFO,
        ERROR,
        WARNING,
        PANIC,

        // special NET assembly keywords
        SMTP,
    }
}
