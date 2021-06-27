using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Scanner
    {
        private const char EOF_CHAR = (char)0;
        private int lineNumber = 0;
        private int columnNumber = 0;
        private char ch;
        private bool inMultipleComment = false;
        private StringReader stringReader;

        Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>();        
        public Scanner() { }
        public void ReadString(string input)
        {
            stringReader = new StringReader(input);
            StartScanner();
        }

        public void ReadFile(string fileName)
        {
            try
            {
                stringReader = new StringReader(System.IO.File.ReadAllText(fileName));
                StartScanner();
            } catch (System.IO.IOException)
            {
                throw;
            }
        }

        void StartScanner()
        {
            lineNumber = 1;
            columnNumber = 0;
            FillKeywords();
            NextChar();
        }

        void FillKeywords()
        {
            keywords.Add("fn", TokenType.FUNCTION);
            keywords.Add("var", TokenType.VAR);
            keywords.Add("true", TokenType.TRUE);
            keywords.Add("false", TokenType.FALSE);
            keywords.Add("null", TokenType.NULL);
            keywords.Add("and", TokenType.AND);
            keywords.Add("or", TokenType.OR);
            keywords.Add("xor", TokenType.XOR);
            keywords.Add("if", TokenType.IF);
            keywords.Add("else", TokenType.ELSE);
            keywords.Add("return", TokenType.RETURN);
            keywords.Add("break", TokenType.BREAK);
            keywords.Add("continue", TokenType.CONTINUE);
            keywords.Add("while", TokenType.WHILE);
            keywords.Add("for", TokenType.FOR);
            keywords.Add("smtp", TokenType.SMTP);
            keywords.Add("info", TokenType.INFO);
            keywords.Add("error", TokenType.ERROR);
            keywords.Add("warning", TokenType.WARNING);
            keywords.Add("panic", TokenType.PANIC);
        }

        TokenType LookupIdent(string key)
        {
            if (keywords.ContainsKey(key))
            {
                return keywords[key];
            }
            return TokenType.IDENT;
        }
        char ReadChar()
        {
            if (stringReader.IsAtEnd())
            {
                return EOF_CHAR;
            }

            columnNumber += 1; // increase column number
            return stringReader.Read();
        }
        char GetOSIndependentChar(){
            char ch = ReadChar();
            if (ch == '\r' || ch == '\n'){
                if (ch == '\r'){
                    ch = ReadChar();
                    if (ch == '\n'){
                        return ch;
                    } else{
                        throw new Exception("expecting line feed character.");
                    }
                }
            }
            return ch;
        }
        void NextChar()
        {
            ch = GetOSIndependentChar();
            while (ch == '\n')
            {
                lineNumber += 1;
                columnNumber = 0;
                ch = GetOSIndependentChar();
            }            
        }
        bool IsWhitespace(char ch)
        {
            return ch == ' ' || ch == '\t' || ch == '\r';
        }
        bool IsDigit(char ch)
        {
            return '0' <= ch && ch <= '9';
        }
        bool IsSpecial(char ch)
        {
            return "=+-*/!^<>,.:;(){}[]".Contains(ch);
        }
        bool IsLetter(char ch)
        {
            return 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || ch == '_';
        }
        void SkipBlanksAndComments()
        {
            while (IsWhitespace(ch) || ch == '/')
            {
                if (IsWhitespace(ch))
                {
                    NextChar();
                } else
                {
                    // we have a '/' here
                    if (stringReader.Peek() == '/')
                    {
                        SkipSingleComment();
                    } else if (stringReader.Peek() == '*')
                    {
                        SkipMultipleComment();
                    } else
                    {
                        break; // is not a comment nor a space thus keep lexing
                    }
                }
            }
        }
        void SkipSingleComment()
        {
            while (ch != EOF_CHAR && ch != '\n')
            {
                ch = GetOSIndependentChar();
            }
            if (ch != EOF_CHAR){
                NextChar(); // skip LF
            }
            lineNumber += 1;
            columnNumber = 0;
        }
        void SkipMultipleComment()
        {
            inMultipleComment = true;
            NextChar(); // skip the '/'
            while (true)
            {
                while (ch != EOF_CHAR && ch != '*')
                {
                    NextChar();
                }
                if (ch == EOF_CHAR) break; // we've reached EOF :(
                NextChar(); // advance the '*'
                if (ch == '/')
                {
                    NextChar();
                    lineNumber += 1;
                    inMultipleComment = false;
                    break;
                }

            }
        }
        Token ReadSpecial()
        {
            Token tok = new Token();
            tok.lineNumber = stringReader.GetLineNumber();
            tok.columnNumber = stringReader.GetColumnNumber();

            switch (ch)
            {
                case '=':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.EQ;
                        tok.literal = "==";
                    }else{
                        tok.type = TokenType.ASSIGN;
                        tok.literal = "=";
                    }
                    break;
                case '+':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.PLUS_EQ;
                        tok.literal = "+=";
                    }else{
                        tok.type = TokenType.PLUS;
                        tok.literal = "+";
                    }
                    break;
                case '-':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.MINUS_EQ;
                        tok.literal = "-=";
                    }else{
                        tok.type = TokenType.MINUS;
                        tok.literal = "-";
                    }
                    break;
                case '*':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.MUL_EQ;
                        tok.literal = "*=";
                    }else{
                        tok.type = TokenType.MUL;
                        tok.literal = "*";
                    }
                    break;
                case '/':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.DIV_EQ;
                        tok.literal = "/=";
                    }else{
                        tok.type = TokenType.DIV;
                        tok.literal = "/";
                    }
                    break;
                case '!':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.NOT_EQ;
                        tok.literal = "!=";
                    }else{
                        tok.type = TokenType.NOT;
                        tok.literal = "!";
                    }
                    break;
                case '^':
                    tok.type = TokenType.POW;
                    tok.literal = "^";
                    break;
                case '<':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.LT_EQ;
                        tok.literal = "<=";
                    }else{
                        tok.type = TokenType.LT;
                        tok.literal = "<";
                    }
                    break;
                case '>':
                    if (stringReader.Peek() == '='){
                        NextChar();
                        tok.type = TokenType.GT_EQ;
                        tok.literal = ">=";
                    }else{
                        tok.type = TokenType.GT;
                        tok.literal = ">";
                    }
                    break;
                case ',':
                    tok.type = TokenType.COMMA;
                    tok.literal = ",";
                    break;
                case '.':
                    tok.type = TokenType.DOT;
                    tok.literal = ".";
                    break;
                case ':':
                    tok.type = TokenType.COLON;
                    tok.literal = ":";
                    break;
                case ';':
                    tok.type = TokenType.SEMICOLON;
                    tok.literal = ";";
                    break;
                case '(':
                    tok.type = TokenType.LPAREN;
                    tok.literal = "(";
                    break;
                case ')':
                    tok.type = TokenType.RPAREN;
                    tok.literal = ")";
                    break;
                case '{':
                    tok.type = TokenType.LBRACE;
                    tok.literal = "{";
                    break;
                case '}':
                    tok.type = TokenType.RBRACE;
                    tok.literal = "}";
                    break;
                case '[':
                    tok.type = TokenType.LBRACKET;
                    tok.literal = "[";
                    break;
                case ']':
                    tok.type = TokenType.RBRACKET;
                    tok.literal = "]";
                    break;
                default:
                    break;
            }
            NextChar();
            return tok;
        }
        Token ReadNumber()
        {
            Token tok = new Token();
            tok.lineNumber = stringReader.GetLineNumber();
            tok.columnNumber = stringReader.GetColumnNumber();

            string lexeme = "";
            while (ch != EOF_CHAR && (IsDigit(ch) || ch == '.'))
            {
                lexeme += ch;
                NextChar();
            }            
            if (lexeme.Contains('.')){
                tok.type = TokenType.FLOAT;
            } else{
                tok.type = TokenType.INT;
            }
            tok.literal = lexeme;

            return tok;
        }
        Token ReadString()
        {
            Token tok = new Token();
            tok.type = TokenType.STRING;
            tok.lineNumber = stringReader.GetLineNumber();
            tok.columnNumber = stringReader.GetColumnNumber();

            char endStr = ch;
            NextChar(); // skip opening str
            string lexeme = "";
            while (ch != EOF_CHAR && ch != endStr){
                if (ch == '\\'){
                    NextChar(); // eat '\'
                    switch (ch){
                        case '\\':
                            lexeme += "\\";
                            break;
                        case 't':
                            lexeme += '\t';
                            break;
                        case 'r':
                            lexeme += '\r';
                            break;
                        case 'n':
                            lexeme += '\n';
                            break;
                        case '"':
                            lexeme += "\"";
                            break;
                        default:
                            lexeme += "\\";
                            break;
                    }
                    NextChar();
                } else{
                    lexeme += ch;
                    NextChar();
                }
            }
            if (ch != EOF_CHAR) NextChar(); // skip end str.
            tok.literal = lexeme;

            return tok;
        }
        Token ReadIdentifier()
        {
            Token tok = new Token();
            tok.lineNumber = stringReader.GetLineNumber();
            tok.columnNumber = stringReader.GetColumnNumber();

            string lexeme = "";
            while (ch != EOF_CHAR && IsLetter(ch))
            {
                lexeme += ch;
                NextChar();                
            }
            tok.type = LookupIdent(lexeme);
            tok.literal = lexeme;
            return tok;
        }
        public Token NextToken()
        {
            while (ch != EOF_CHAR)
            {
                SkipBlanksAndComments();
                
                if (IsDigit(ch) || ch == '.'){
                    return ReadNumber();
                }

                if (IsLetter(ch)){
                    return ReadIdentifier();
                }

                if (ch == '"' || ch == '\''){
                    return ReadString();
                }

                if (IsSpecial(ch)){
                    return ReadSpecial();
                }
                int lineNumber = stringReader.GetLineNumber();
                int columnNumber = stringReader.GetColumnNumber();
                var msg = String.Format("'{0}' at [{1}:{2}]", ch, lineNumber, columnNumber);
                throw new Exception("lexer: unknown character " + msg);
            }            
            if (inMultipleComment){
                throw new Exception("detected unterminated comment, expecting '*/'.");
            }

            return new Token(TokenType.EOF, "");
        }
    }
}
