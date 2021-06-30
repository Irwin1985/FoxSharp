using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Parser
    {
        // precedence order
        public int LOWEST = 0;       // lowest priority
        public int LOGIC_OR = 1;     // logic or
        public int LOGIC_AND = 2;    // logic and
        public int EQUALS = 3;       // a == b
        public int COMPARISON = 4;   // a <, <=, >, >= b
        public int TERM = 5;         // a +, - b
        public int FACTOR = 6;       // a *, / b
        public int PREFIX = 7;       // -a, !b
        public int POWER = 8;        // a^b
        public int CALL = 9;         // foo()
        public int INDEX = 10;       // bar[1]

        // precedence dictionary
        private Dictionary<TokenType, int> precedence = new Dictionary<TokenType, int>();
        private Dictionary<TokenType, IParsePrefix> PrefixParseFn = new Dictionary<TokenType, IParsePrefix>();
        private Dictionary<TokenType, IParseInfix> InfixParseFn = new Dictionary<TokenType, IParseInfix>();

        public Token curToken;
        public Token peekToken;
        private Scanner sc;
        public List<String> errors = new List<String>();

        // prefixParseFn
        private ParsePrefix prefixFn = new ParsePrefix();
        // infixParseFn
        private ParseInfix infixParseFn = new ParseInfix();
        // parseLiteralFn
        private ParseLiterals parseLiteralFn = new ParseLiterals();

        public bool panicMode = false;
        public Parser(Scanner sc)
        {
            this.sc = sc;
            // logic arithmetic
            precedence.Add(TokenType.OR, LOGIC_OR);
            precedence.Add(TokenType.AND, LOGIC_AND);
            // equality
            precedence.Add(TokenType.EQ, EQUALS);
            precedence.Add(TokenType.NOT_EQ, EQUALS);
            // comparison
            precedence.Add(TokenType.LT, COMPARISON);
            precedence.Add(TokenType.LT_EQ, COMPARISON);
            precedence.Add(TokenType.GT, COMPARISON);
            precedence.Add(TokenType.GT_EQ, COMPARISON);
            // term
            precedence.Add(TokenType.PLUS, TERM);
            precedence.Add(TokenType.PLUS_EQ, TERM);
            precedence.Add(TokenType.MINUS, TERM);
            precedence.Add(TokenType.MINUS_EQ, TERM);
            // factor
            precedence.Add(TokenType.MUL, FACTOR);
            precedence.Add(TokenType.MUL_EQ, FACTOR);
            precedence.Add(TokenType.DIV, FACTOR);
            precedence.Add(TokenType.DIV_EQ, FACTOR);
            // power
            precedence.Add(TokenType.POW, POWER);
            // call
            precedence.Add(TokenType.LPAREN, CALL);
            precedence.Add(TokenType.DOT, CALL);

            // primary
            precedence.Add(TokenType.LBRACKET, INDEX);

            // Register prefix tokens with it semantic code
            PrefixParseFn.Add(TokenType.NUMBER, parseLiteralFn);
            PrefixParseFn.Add(TokenType.IDENT, parseLiteralFn);
            PrefixParseFn.Add(TokenType.STRING, parseLiteralFn);
            PrefixParseFn.Add(TokenType.NULL, parseLiteralFn);
            PrefixParseFn.Add(TokenType.MINUS, prefixFn);
            PrefixParseFn.Add(TokenType.NOT, prefixFn);
            PrefixParseFn.Add(TokenType.TRUE, parseLiteralFn);
            PrefixParseFn.Add(TokenType.FALSE, parseLiteralFn);
            PrefixParseFn.Add(TokenType.LPAREN, new ParseGroupedExpression());
            PrefixParseFn.Add(TokenType.LBRACKET, parseLiteralFn);
            PrefixParseFn.Add(TokenType.LBRACE, parseLiteralFn);
            PrefixParseFn.Add(TokenType.FUNCTION, parseLiteralFn);
            PrefixParseFn.Add(TokenType.IF, parseLiteralFn);
            PrefixParseFn.Add(TokenType.SMTP, parseLiteralFn);

            // Register infix tokens with its semantic code
            InfixParseFn.Add(TokenType.PLUS, infixParseFn);
            InfixParseFn.Add(TokenType.PLUS_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.MINUS, infixParseFn);
            InfixParseFn.Add(TokenType.MINUS_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.MUL, infixParseFn);
            InfixParseFn.Add(TokenType.MUL_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.DIV, infixParseFn);
            InfixParseFn.Add(TokenType.DIV_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.POW, infixParseFn);
            InfixParseFn.Add(TokenType.OR, infixParseFn);
            InfixParseFn.Add(TokenType.AND, infixParseFn);
            InfixParseFn.Add(TokenType.LT, infixParseFn);
            InfixParseFn.Add(TokenType.LT_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.GT, infixParseFn);
            InfixParseFn.Add(TokenType.GT_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.EQ, infixParseFn);
            InfixParseFn.Add(TokenType.NOT_EQ, infixParseFn);
            InfixParseFn.Add(TokenType.LBRACKET, new ParseIndexExpression());
            InfixParseFn.Add(TokenType.LPAREN, new ParseCallExpression());
            InfixParseFn.Add(TokenType.DOT, infixParseFn);
            
            // advance tokens
            NextToken();
            NextToken();
        }
        public List<String> GetErrors(){
            return errors;
        }
        public int CurPrecedence(){
            switch (curToken.type){
                case TokenType.NUMBER:
                case TokenType.STRING:
                case TokenType.NULL:
                    errors.Add("Syntax Error: invalid infix operand: " + curToken.literal);
                    panicMode = true;
                    break;
            }
            if (precedence.ContainsKey(curToken.type)){
                return precedence[curToken.type];
            } else{
                return LOWEST;
            }
        }
        public void Expect(TokenType type){
            if (curToken.type == type){
                NextToken();
            } else{
                var msg = String.Format("expected token to be: {0}, got {1} instead.", type, curToken.type);
                errors.Add(msg);
                panicMode = true;
            }
        }
        public void NextToken(){
            curToken = peekToken;
            peekToken = sc.NextToken();
        }
        public Program ParseProgram(){
            var program = new Program(curToken);
            program.Statements = new List<IStatement>();

            while (!CurTokenIs(TokenType.EOF)){
                var stmt = ParseStatement();
                if (stmt != null){
                    program.Statements.Add(stmt);
                }
                if (panicMode) StartErrorRecovery();
                if (CurTokenIs(TokenType.SEMICOLON)) NextToken();
            }

            return program;
        }
        void StartErrorRecovery(){
            // try recover the syntax
            while (!CurTokenIs(TokenType.EOF)){
                if (CurTokenIs(TokenType.SEMICOLON)) return;
                switch (curToken.type){
                    case TokenType.VAR:
                    case TokenType.RETURN:
                        return;
                    default:
                        if (IsPascalBinding()) return;
                        NextToken();
                        break;
                }
            }
        }
        bool IsPascalBinding(){
            return CurTokenIs(TokenType.IDENT) && peekToken.type == TokenType.BINDING;
        }
        IStatement ParseStatement(){
            switch(curToken.type){
                case TokenType.VAR:
                    return ParseVarStatement();
                case TokenType.RETURN:
                    return ParseReturnStatement();
                case TokenType.FOR:
                    return ParseForStatement();
                case TokenType.WHILE:
                    return ParseWhileStatement();
                default:
                    if (IsPascalBinding()){
                        return ParsePascalBinding();
                    }
                    return ParseExpressionStatement();
            }
        }
        IStatement ParseVarStatement(){
            var stmt = new VarStatement(curToken);
            NextToken(); // advance 'var'
            if (!CurTokenIs(TokenType.IDENT)){
                return null;
            }
            stmt.Name = new Identifier(curToken, curToken.literal);
            NextToken(); // advance 'ident'
            Expect(TokenType.ASSIGN);

            stmt.Value = ParseExpression(LOWEST);

            if (stmt.Value.GetType() == typeof(FunctionLiteral)){
                var fun = (FunctionLiteral)stmt.Value;
                fun.Name = stmt.Name.Value;
            }

            return stmt;
        }
        IStatement ParsePascalBinding()
        {
            var tok = new Token(TokenType.VAR, curToken.lineNumber, curToken.columnNumber);
            var ident = new Identifier(curToken, curToken.literal);
            NextToken(); // advance 'ident'
            
            var stmt = new VarStatement(tok);
            stmt.Name = ident;

            Expect(TokenType.BINDING);

            stmt.Value = ParseExpression(LOWEST);

            return stmt;
        }
        IStatement ParseReturnStatement(){
            var stmt = new ReturnStatement(curToken);
            NextToken(); // advance 'return'
            stmt.ReturnValue = ParseExpression(LOWEST);

            return stmt;
        }
        IStatement ParseForStatement(){
            var stmt = new ForStatement(curToken);
            NextToken(); // skip 'for'
            Expect(TokenType.LPAREN);
            if (PeekTokenIs(TokenType.COMMA)){
                stmt.IndexOrKey = ParseIdentifier();
                Expect(TokenType.COMMA);
            }
            stmt.Value = ParseIdentifier();
            Expect(TokenType.IN);
            stmt.Source = ParseExpression(LOWEST);
            Expect(TokenType.RPAREN);
            stmt.Body = ParseBlockStatement();

            return stmt;
        }
        IStatement ParseWhileStatement(){
            var stmt = new WhileStatement(curToken);
            NextToken(); // skip 'while'
            
            Expect(TokenType.LPAREN);
            stmt.Condition = ParseExpression(LOWEST);
            Expect(TokenType.RPAREN);

            stmt.Body = ParseBlockStatement();

            return stmt;
        }
        ExpressionStatement ParseExpressionStatement(){
            var exp = new ExpressionStatement(curToken);
            exp.Expression = ParseExpression(LOWEST);
            return exp;
        }
        public IExpression ParseExpression(int precedence)
        {
            if (!PrefixParseFn.ContainsKey(curToken.type)){
                errors.Add(String.Format("no prefix parse function for {0} found.", curToken.type));
                panicMode = true;
                return null;
            }
            var prefix = PrefixParseFn[curToken.type];
            // calling the prefixparseExpression
            var left = prefix.ParsePrefixExpression(this);

            while (!panicMode && !CurTokenIs(TokenType.EOF) && precedence < CurPrecedence()){
                if (!InfixParseFn.ContainsKey(curToken.type)){
                    return left;
                }
                var infix = InfixParseFn[curToken.type];
                left = infix.ParseInfixExpression(this, left);
            }

            return left;
        }
        public List<IExpression> ParseExpressionList(TokenType closingToken){
            var exp = new List<IExpression>();
            exp.Add(ParseExpression(LOWEST));

            while (CurTokenIs(TokenType.COMMA)){
                NextToken(); // skip ','
                if (!CurTokenIs(closingToken)){
                    exp.Add(ParseExpression(LOWEST));
                }
            }
            return exp;
        }
        public bool CurTokenIs(TokenType type)
        {
            return curToken.type == type;
        }
        public bool PeekTokenIs(TokenType type)
        {
            return peekToken.type == type;
        }
        public Identifier ParseIdentifier()
        {
            var indent = new Identifier(curToken, curToken.literal);
            Expect(TokenType.IDENT);

            return indent;
        }
        public BlockStatement ParseBlockStatement()
        {
            var block = new BlockStatement(curToken);
            block.Statements = new List<IStatement>();
            Expect(TokenType.LBRACE);

            while (!CurTokenIs(TokenType.RBRACE)){
                var stmt = ParseStatement();
                if (stmt != null){
                    block.Statements.Add(stmt);
                }
                if (panicMode) StartErrorRecovery();
                if (CurTokenIs(TokenType.SEMICOLON)) NextToken();
            }
            Expect(TokenType.RBRACE);

            return block;
        }
    }
}
