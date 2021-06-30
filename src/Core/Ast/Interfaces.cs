using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public interface INode{
        string Inspect();
        NodeType Type();
    }
    public interface IStatement : INode {}
    public interface IExpression : INode {}
    public enum NodeType { 
        ARRAY,
        BLOCK_STMT,
        BOOLEAN,
        CALL,
        EXPR_STMT,
        FUNCTION,
        HASH,
        IDENT,
        IF,
        INDEX,
        INFIX,
        NULL,
        NUMBER,
        PREFIX,
        PROGRAM,
        RETURN,
        SMTP,
        STRING,
        VAR,
        WHILE,
    }
}
