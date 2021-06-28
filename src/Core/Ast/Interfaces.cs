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
        FLOAT,
        FUNCTION,
        HASH,
        IDENT,
        IF,
        INDEX,
        INFIX,
        INTEGER,
        NULL,
        PREFIX,
        PROGRAM,
        RETURN,
        SMTP,
        STRING,
        VAR,
    }
}
