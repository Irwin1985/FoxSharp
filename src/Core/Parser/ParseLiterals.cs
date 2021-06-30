using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class ParseLiterals : IParsePrefix{
        Parser p;
        public IExpression ParsePrefixExpression(Parser p)
        {
            this.p = p;
            switch (p.curToken.type){
                case TokenType.NUMBER:
                    return ParseNumberLiteral();
                case TokenType.STRING:
                    return ParseStringLiteral();
                case TokenType.NULL:
                    return ParseNullLiteral();
                case TokenType.TRUE:
                case TokenType.FALSE:
                    return ParseBoolean();
                case TokenType.IDENT:
                    return ParseIdentifier();
                case TokenType.LBRACKET:
                    return ParseArrayLiteral();
                case TokenType.LBRACE:
                    return ParseHashLiteral();
                case TokenType.FUNCTION:
                    return ParseFunctionLiteral();
                case TokenType.IF:
                    return ParseIfExpression();
                case TokenType.SMTP:
                    return ParseSmtpLiteral();
                default:
                    return null;
            }
        }
        IExpression ParseNumberLiteral(){
            var lit = new NumberLiteral(p.curToken, double.Parse(p.curToken.literal.Replace('.', ',')));
            p.Expect(TokenType.NUMBER);

            return lit;
        }
        IExpression ParseStringLiteral(){
            var str = new StringLiteral(p.curToken, p.curToken.literal);
            p.NextToken();

            return str;
        }
        IExpression ParseNullLiteral(){
            var exp = new NullLiteral();
            p.NextToken();

            return exp;
        }
        IExpression ParseIdentifier(){
            var ident = new Identifier(p.curToken, p.curToken.literal);
            p.Expect(TokenType.IDENT);
            return ident;
        }
        IExpression ParseBoolean()
        {
            var boolean = new BooleanLiteral(p.curToken, p.CurTokenIs(TokenType.TRUE));
            p.NextToken();
            return boolean;
        }
        IExpression ParseArrayLiteral(){
            var array = new ArrayLiteral(p.curToken);
            array.Elements = new List<IExpression>();
            p.NextToken(); // advance '['
            if (!p.CurTokenIs(TokenType.RBRACKET)){
                array.Elements = p.ParseExpressionList(TokenType.RBRACKET);
            }
            p.Expect(TokenType.RBRACKET);

            return array;
        }
        IExpression ParseHashLiteral(){
            var hash = new HashLiteral(p.curToken);
            hash.Pairs = new Dictionary<IExpression, IExpression>();
            p.NextToken(); // skip '{'

            if (!p.CurTokenIs(TokenType.RBRACE)){
                // parse key:value pairs
                var key = p.ParseExpression(p.LOWEST);
                p.Expect(TokenType.COLON);
                var value = p.ParseExpression(p.LOWEST);
                hash.Pairs.Add(key, value);

                while (p.CurTokenIs(TokenType.COMMA)){
                    p.NextToken(); // skip ','
                    if (!p.CurTokenIs(TokenType.RBRACE)){
                        key = p.ParseExpression(p.LOWEST);
                        p.Expect(TokenType.COLON);
                        value = p.ParseExpression(p.LOWEST);
                        hash.Pairs.Add(key, value);
                    }
                }
            }
            p.Expect(TokenType.RBRACE);

            return hash;
        }
        IExpression ParseFunctionLiteral(){
            var fun = new FunctionLiteral(p.curToken);
            fun.Parameters = new List<Identifier>();
            p.NextToken(); // skip 'fn'
            p.Expect(TokenType.LPAREN);

            if (!p.CurTokenIs(TokenType.RPAREN)){
                fun.Parameters.Add(p.ParseIdentifier());
                if (p.CurTokenIs(TokenType.DOT_PARAM)){
                    p.NextToken(); // skip '...'
                    fun.LastParamArray = true;
                }
                while (p.CurTokenIs(TokenType.COMMA)){
                    if (fun.LastParamArray){
                        p.errors.Add("variadic parameters must be the last parameter");
                        p.panicMode = true;
                        return null;
                    }
                    p.NextToken(); // skip ','
                    if (!p.CurTokenIs(TokenType.RPAREN)){
                        fun.Parameters.Add(p.ParseIdentifier());
                        if (p.CurTokenIs(TokenType.DOT_PARAM)){
                            p.NextToken(); // skip '...'
                            fun.LastParamArray = true;
                        }
                    }
                }
            }
            p.Expect(TokenType.RPAREN);

            fun.Body = p.ParseBlockStatement();

            return fun;
        }
        IExpression ParseIfExpression(){
            var ifExp = new IfExpression(p.curToken);
            p.NextToken(); // skip 'if'

            p.Expect(TokenType.LPAREN);
            ifExp.Condition = p.ParseExpression(p.LOWEST);
            p.Expect(TokenType.RPAREN);

            ifExp.Consequence = p.ParseBlockStatement();

            if (p.CurTokenIs(TokenType.ELSE)){
                p.NextToken(); // skip 'else'
                ifExp.Alternative = p.ParseBlockStatement();
            }

            return ifExp;
        }
        IExpression ParseSmtpLiteral(){
            var smtp = new SmtpLiteral(p.curToken);
            smtp.Properties = new Dictionary<IExpression, IExpression>();
            p.NextToken(); // skip 'smtp'
            p.Expect(TokenType.LBRACE);
            if (!p.CurTokenIs(TokenType.RBRACE)){
                
                var key = p.ParseExpression(p.LOWEST);
                p.Expect(TokenType.COLON);
                var value = p.ParseExpression(p.LOWEST);
                smtp.Properties.Add(key, value);

                while (p.CurTokenIs(TokenType.COMMA)){
                    p.NextToken(); // skip ','
                    if (!p.CurTokenIs(TokenType.RBRACE)){
                        key = p.ParseExpression(p.LOWEST);
                        p.Expect(TokenType.COLON);
                        value = p.ParseExpression(p.LOWEST);
                        smtp.Properties.Add(key, value);
                    }
                }

            }
            p.Expect(TokenType.RBRACE);
            return smtp;
        }
    }
}
