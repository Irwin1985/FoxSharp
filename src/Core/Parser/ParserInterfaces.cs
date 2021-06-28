using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    interface IPrefixParse
    {
        IExpression ParsePrefixExpression(Parser p);
    }
    interface IInfixParse
    {
        IExpression ParseInfixExpression(Parser p, IExpression left);
    }  
}
