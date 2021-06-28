using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    interface IParsePrefix
    {
        IExpression ParsePrefixExpression(Parser p);
    }
    interface IParseInfix
    {
        IExpression ParseInfixExpression(Parser p, IExpression left);
    }  
}
