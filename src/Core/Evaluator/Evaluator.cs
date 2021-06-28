using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class Evaluator
    {
        private IObject NULL = new NullObj();
        private IObject TRUE = new BooleanObj(true);
        private IObject FALSE = new BooleanObj(false);


        public IObject Eval(INode node, Environment env)
        {
            switch (node.Type())
            {
                case NodeType.PROGRAM:
                    return EvalProgram((Program)node, env);
                case NodeType.BLOCK_STMT:
                    return EvalBlockStmt((BlockStatement)node, env);
                case NodeType.EXPR_STMT:
                    return Eval(((ExpressionStatement)node).Expression, env);
                case NodeType.VAR:
                    return EvalVarStmt((VarStatement)node, env);
                case NodeType.RETURN:
                    return EvalReturnStmt((ReturnStatement)node, env);
                case NodeType.INTEGER:
                    return new IntegerObj(((IntegerLiteral)node).Value);
                case NodeType.FLOAT:
                    return new FloatObj(((FloatLiteral)node).Value);
                case NodeType.STRING:
                    return new StringObj(((StringLiteral)node).Value);
                case NodeType.BOOLEAN:
                    return ((BooleanLiteral)node).Value ? TRUE : FALSE;
                case NodeType.NULL:
                    return NULL;
                default:
                    return NULL;
            }
            return null;
        }
        private IObject EvalProgram(Program node, Environment env){
            IObject result = null;
            foreach (var stmt in node.Statements){
                var evaluated = Eval(stmt, env);
                switch (evaluated.Type())
                {
                    case ObjectType.RETURN_OBJ:
                        return ((ReturnObj)evaluated).Value;
                    case ObjectType.ERROR_OBJ:
                        return evaluated;
                }
            }
            return result;
        }
        private IObject EvalBlockStmt(BlockStatement node, Environment env){
            IObject result = null;
            foreach (var stmt in node.Statements){
                var evaluated = Eval(stmt, env);
                if (evaluated != null){
                    var type = evaluated.Type();
                    if (type == ObjectType.RETURN_OBJ || type == ObjectType.ERROR_OBJ){
                        return evaluated;
                    }
                }
            }
            return result;
        }
        private IObject EvalVarStmt(VarStatement node, Environment env){
            var value = Eval(node.Value, env);
            if (IsError(value)){
                return value;
            }
            return env.Set(node.Name.Value, value);
        }
        private IObject EvalReturnStmt(ReturnStatement node, Environment env){
            var value = Eval(node.ReturnValue, env);

            if (IsError(value)){
                return value;
            }

            return new ReturnObj(value);
        }
        private bool IsError(IObject obj){
            return obj != null && obj.Type() == ObjectType.ERROR_OBJ;
        }
    }
}
