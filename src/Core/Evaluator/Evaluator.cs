using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public static class Evaluator
    {
        public static IObject NULL = new NullObj();
        public static IObject TRUE = new BooleanObj(true);
        public static IObject FALSE = new BooleanObj(false);
        private static Builtins Builtin = new Builtins();


        public static IObject Eval(INode node, Environment env)
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
                case NodeType.IF:
                    return EvalIfExpression((IfExpression)node, env);
                case NodeType.FUNCTION:
                    return EvalFunctionLiteral((FunctionLiteral)node, env);
                case NodeType.CALL:
                    return EvalCallExpression((CallExpression)node, env);
                case NodeType.ARRAY:
                    return EvalArrayLiteral((ArrayLiteral)node, env);
                case NodeType.HASH:
                    return EvalHashLiteral((HashLiteral)node, env);
                case NodeType.SMTP:
                    return EvalSmtpLiteral((SmtpLiteral)node, env);
                case NodeType.INDEX:
                    return EvalIndexExpression((IndexExpression)node, env);
                case NodeType.IDENT:
                    return EvalIdentifier((Identifier)node, env);
                case NodeType.NUMBER:
                    return new NumberObj(((NumberLiteral)node).Value);
                case NodeType.STRING:
                    return new StringObj(((StringLiteral)node).Value);
                case NodeType.BOOLEAN:
                    return ((BooleanLiteral)node).Value ? TRUE : FALSE;
                case NodeType.NULL:
                    return NULL;
                case NodeType.PREFIX:
                    return EvalPrefixExpression((PrefixExpression)node, env);
                case NodeType.INFIX:
                    return EvalInfixExpression((InfixExpression)node, env);
                default:
                    return NULL;
            }
        }
        private static IObject EvalProgram(Program node, Environment env){
            IObject result = null;
            foreach (var stmt in node.Statements){
                result = Eval(stmt, env);
                switch (result.Type())
                {
                    case ObjectType.RETURN:
                        return ((ReturnObj)result).Value;
                    case ObjectType.ERROR:
                        return result;
                }
            }
            return result;
        }
        private static IObject EvalBlockStmt(BlockStatement node, Environment env){
            IObject result = null;
            foreach (var stmt in node.Statements){
                result = Eval(stmt, env);
                if (result != null){
                    var type = result.Type();
                    if (type == ObjectType.RETURN || type == ObjectType.ERROR){
                        return result;
                    }
                }
            }
            return result;
        }
        private static IObject EvalVarStmt(VarStatement node, Environment env){
            var value = Eval(node.Value, env);
            if (IsError(value)){
                return value;
            }
            return env.Set(node.Name.Value, value);
        }
        private static IObject EvalReturnStmt(ReturnStatement node, Environment env){
            var value = Eval(node.ReturnValue, env);

            if (IsError(value)){
                return value;
            }

            return new ReturnObj(value);
        }
        private static IObject EvalIdentifier(Identifier node, Environment env){
            var name = node.Value;
            var result = env.Get(name);
            if (result == null){
                // try find it in builtins
                if (Builtin.Pool.ContainsKey(name)){
                    return Builtin.Pool[name];
                }
                return NewError(String.Format("variable not found: {0}.", name));
            }
            return result;
        }
        private static IObject EvalIfExpression(IfExpression node, Environment env){
            var condition = Eval(node.Condition, env);
            if (IsError(condition)){
                return condition;
            }
            if (condition.Type() != ObjectType.BOOLEAN){
                return NewError("the if condition must evaluate to BOOLEAN.");
            }
            if (((BooleanObj)condition) == TRUE){
                return Eval(node.Consequence, env);
            } else {
                if (node.Alternative != null){
                    return Eval(node.Alternative, env);
                }
                return NULL;
            }
        }
        private static IObject EvalFunctionLiteral(FunctionLiteral node, Environment env){
            var funObj = new FunctionObj();
            funObj.Parameters = node.Parameters;
            funObj.Env = env;
            funObj.Body = node.Body;

            return funObj;
        }
        private static IObject EvalCallExpression(CallExpression node, Environment env){
            var function = Eval(node.Function, env);
            if (IsError(function)){
                return function;
            }
            if (function.Type() != ObjectType.FUNCTION && function.Type() != ObjectType.BUILTIN){
                return NewError("not a function.");
            }
            List<IObject> args = EvalExpressions(node.Arguments, env);
            if (args.Count == 1 && IsError(args[0])){
                return args[0];
            }
            return ApplyFunction(function, args);
        }
        private static IObject EvalArrayLiteral(ArrayLiteral node, Environment env){
            var array = new ArrayObj();
            array.Elements = new List<IObject>();
            foreach(var elem in node.Elements){
                var item = Eval(elem, env);
                if (IsError(item)){
                    return item;
                }
                array.Elements.Add(item);
            }
            return array;
        }
        private static IObject EvalHashLiteral(HashLiteral node, Environment env){
            var hash = new HashObj();
            hash.Pairs = new Dictionary<string, IObject>();
            
            foreach (KeyValuePair<IExpression, IExpression> kvp in node.Pairs){
                var key = Eval(kvp.Key, env);
                if (IsError(key)){
                    return key;
                }
                if (key.Type() != ObjectType.STRING){
                    return NewError("key value must be a string.");
                }
                var value = Eval(kvp.Value, env);
                if (IsError(value)){
                    return value;
                }                
                hash.Pairs.Add(key.Inspect(), value);
            }

            return hash;
        }
        private static IObject EvalSmtpLiteral(SmtpLiteral node, Environment env){
            var smtp = new SmtpObj();
            smtp.Properties = new Dictionary<string, IObject>();
            foreach(KeyValuePair<IExpression, IExpression> kvp in node.Properties)
            {
                var keyObj = Eval(kvp.Key, env);
                if (IsError(keyObj)){
                    return keyObj;
                }
                if (keyObj.Type() != ObjectType.STRING){
                    return NewError("key must be a STRING.");
                }
                var key = ((StringObj)keyObj).Value;
                if (!node.HasProperty(key)){
                    return NewError(String.Format("unknown property {0}", key));
                }
                var value = Eval(kvp.Value, env);
                if (IsError(value)){
                    return value;
                }
                smtp.Properties.Add(key, value);
            }
            return smtp;
        }
        private static IObject EvalIndexExpression(IndexExpression node, Environment env){
            var left = Eval(node.Left, env);
            if (IsError(left)){
                return left;
            }
            var type = left.Type();
            if (IsIndexable(type)){
                var index = Eval(node.Index, env);
                if (IsError(index)){
                    return index;
                }
                if ((type == ObjectType.ARRAY || type == ObjectType.HASH) && node.Top != null){
                    return NewError("invalid index expression");
                }
                switch(type)
                {
                    case ObjectType.ARRAY:
                        return EvalArrayIndex((ArrayObj)left, index);
                    case ObjectType.HASH:
                        return EvalHashIndex((HashObj)left, index);
                    case ObjectType.STRING:
                        IObject topObj = null;
                        if (node.Top != null){
                            topObj = Eval(node.Top, env);
                        }
                        if (IsError(topObj)){
                            return topObj;
                        }
                        return EvalStringIndex((StringObj)left, index, topObj);
                    default:
                        return NULL;
                        
                }
            }
            return NewError(String.Format("type is not indexable: {0}", type));
        }
        private static IObject EvalArrayIndex(ArrayObj node, IObject index){            
            if (index.Type() != ObjectType.NUMBER){
                return NewError("array index must be a NUMBER.");
            }
            var arrayIndex = (int)((NumberObj)index).Value;
            if (arrayIndex < 0 || arrayIndex >= node.Elements.Count){
                return NewError("index out of bounds.");
            }
            return node.Elements[arrayIndex];
        }
        private static IObject EvalStringIndex(StringObj node, IObject index, IObject top){
            if (index.Type() != ObjectType.NUMBER){
                return NewError("string index must be a NUMBER.");
            }
            var strIndex = (int)((NumberObj)index).Value;
            if (strIndex < 0 || strIndex >= node.Value.Length){
                return NewError("index out of bounds.");
            }
            try{
                if (top != null){
                    if (top.Type() != ObjectType.NUMBER){
                        return NewError("string index must be a NUMBER.");
                    }
                    var topIndex = (int)((NumberObj)top).Value;
                    if (topIndex == 0){
                        topIndex = node.Value.Length - strIndex;
                    }
                    string subStr = node.Value.Substring(strIndex, topIndex);
                    return new StringObj(subStr);
                } else{
                    char subStr = node.Value[strIndex];
                    return new StringObj(subStr.ToString());
                }
            } catch(Exception e){
                return NewError(e.Message);
            }
        }
        private static IObject EvalHashIndex(HashObj node, IObject index){
            if (index.Type() != ObjectType.STRING){
                return NewError("hash index must be a STRING.");
            }
            var key = ((StringObj)index).Value;
            if (node.Pairs.ContainsKey(key)) {
                return node.Pairs[key];
            }
            return NULL;
        }
        private static bool IsIndexable(ObjectType type){
            return type == ObjectType.ARRAY || type == ObjectType.HASH || type == ObjectType.STRING;
        }
        private static List<IObject> EvalExpressions(List<IExpression> arguments, Environment env){
            var args = new List<IObject>();
            foreach(var arg in arguments){
                var result = Eval(arg, env);
                if (IsError(result)){        
                    var newArg = new List<IObject>();
                    newArg.Add(result);
                    return newArg;
                }
                args.Add(result);
            }
            return args;
        }
        private static IObject ApplyFunction(IObject function, List<IObject> args){
            switch (function.Type())
            {
                case ObjectType.FUNCTION:
                    var fn = (FunctionObj)function;
                    var extendedEnv = ExtendEnvironment(fn, args);
                    var evaluated = Eval(fn.Body, extendedEnv);
                    if (evaluated.Type() == ObjectType.RETURN){
                        return ((ReturnObj)evaluated).Value;
                    }
                    return evaluated;
                case ObjectType.BUILTIN:
                    var bt = (IBuiltin)function;
                    return bt.Run(args);
                default:
                    return NULL;
            }
        }
        private static Environment ExtendEnvironment(FunctionObj fn, List<IObject> args){
            var newEnv = new Environment(fn.Env);
            var idx = 0;
            foreach (var par in fn.Parameters)
            {
                newEnv.Set(par.Value, args[idx]);
                idx += 1;
            }
            return newEnv;
        }
        private static IObject EvalPrefixExpression(PrefixExpression node, Environment env){
            var rightObj = Eval(node.Right, env);
            if (IsError(rightObj)){
                return rightObj;
            }
            var type = rightObj.Type();
            switch (node.Operator){
                case "-":
                    if (type == ObjectType.NUMBER){
                        return new NumberObj(((NumberObj)rightObj).Value * -1);
                    } else{
                        return NewError("invalid operand for this operator.");
                    }
                case "!":
                    if (type == ObjectType.BOOLEAN){
                        return ((BooleanObj)rightObj).Value ? FALSE : TRUE;
                    }
                    return NewError("invalid operand for this operator.");
                default:
                    return NewError("invalid operator: " + node.Operator);
            }
        }
        private static IObject EvalInfixExpression(InfixExpression node, Environment env){
            var ope = node.Operator;
            
            // check for logical expression first
            if (ope == "or" || ope == "and"){
                return EvalLogicalExpression(node, env);
            }
            if (ope == "+=" || ope == "-=" || ope == "*=" || ope == "/="){
                return EvalShortHandAssignment(node, env);
            }
            var leftObj = Eval(node.Left, env);
            if (IsError(leftObj)){
                return leftObj;
            }
            var rightObj = Eval(node.Right, env);
            if (IsError(rightObj)){
                return rightObj;
            }
            var lType = leftObj.Type();
            var rType = rightObj.Type();

            if (IsNumber(lType) && IsNumber(rType)) {
                return EvalNumericExpression((NumberObj)leftObj, ope, (NumberObj)rightObj);

            }else if (lType == ObjectType.STRING && rType == ObjectType.STRING){
                return EvalStringExpression((StringObj)leftObj, ope, (StringObj)rightObj);

            }else if (lType == ObjectType.BOOLEAN && rType == ObjectType.BOOLEAN){
                return EvalBooleanExpression((BooleanObj)leftObj, ope, (BooleanObj)rightObj);
            }
            var msg = String.Format("invalid operands: {0}, {1}", lType, rType);
            return NewError(msg);
        }
        private static IObject EvalNumericExpression(NumberObj leftObj, string op, NumberObj rightObj)
        {
            switch (op)
            {
                case "+":
                    return new NumberObj(leftObj.Value + rightObj.Value);
                case "-":
                    return new NumberObj(leftObj.Value - rightObj.Value);
                case "*":
                    return new NumberObj(leftObj.Value * rightObj.Value);
                case "/":                    
                    if (rightObj.Value == 0){
                        return NewError("division by zero.");
                    }
                    return new NumberObj(leftObj.Value / rightObj.Value);
                case "^":
                    var result = Math.Pow(leftObj.Value, rightObj.Value);
                    return new NumberObj(result);
                // relational operator
                case "<":
                    return (leftObj.Value < rightObj.Value) ? TRUE : FALSE;
                case ">":
                    return (leftObj.Value > rightObj.Value) ? TRUE : FALSE;
                case "<=":
                    return (leftObj.Value <= rightObj.Value) ? TRUE : FALSE;
                case ">=":
                    return (leftObj.Value >= rightObj.Value) ? TRUE : FALSE;
                case "==":
                    return (leftObj.Value == rightObj.Value) ? TRUE : FALSE;
                case "!=":
                    return (leftObj.Value != rightObj.Value) ? TRUE : FALSE;
                default:
                    return NewError(String.Format("invalid operator for operands: {0}, {1}", leftObj.Type(), rightObj.Type()));
            }
        }
        private static IObject EvalStringExpression(StringObj leftObj, string op, StringObj rightObj){
            switch (op)
            {
                case "+":
                    return new StringObj(leftObj.Value + rightObj.Value);
                case "-":
                    return new StringObj(leftObj.Value.TrimEnd() + rightObj.Value);
                default:
                    return NewError("invalid operator for string expression: " + op);
            }
        }
        private static IObject EvalBooleanExpression(BooleanObj leftObj, string op, BooleanObj rightObj)
        {
            var leftInt = new NumberObj();
            var rightInt = new NumberObj();
            leftInt.Value = leftObj.Value ? 1 : 0;
            rightInt.Value = rightObj.Value ? 1 : 0;

            return EvalNumericExpression(leftInt, op, rightInt);
        }
        private static IObject EvalLogicalExpression(InfixExpression node, Environment env){
            var leftObj = Eval(node.Left, env);
            if (IsError(leftObj)){
                return leftObj;
            }
            if (leftObj.Type() != ObjectType.BOOLEAN){
                return NewError("invalid operand for logical expression: " + leftObj.Type());
            }
            BooleanObj leftOperand = (BooleanObj)leftObj;
            switch (node.Operator)
            {
                case "or":
                    if (leftOperand.Value){
                        return TRUE;
                    } else{
                        return EvaluateRightLogicalOperand(node.Right, env);
                    }
                case "and":
                    if (leftOperand.Value){
                        return EvaluateRightLogicalOperand(node.Right, env);
                    } else{
                        return FALSE;
                    }
                default:
                    return NewError("invalid operator: " + node.Operator);
            }
        }
        private static IObject EvalShortHandAssignment(InfixExpression node, Environment env){
            // left hand side must be a NUMBER or STRING.
            if (node.Left.Type() != NodeType.IDENT){
                return NewError("left hand side must be a variable name.");
            }
            var name = ((Identifier)node.Left).Value;
            var leftObj = Eval(node.Left, env);
            if (IsError(leftObj)){
                return leftObj;
            }
            var type = leftObj.Type();
            if (type != ObjectType.NUMBER && type != ObjectType.STRING){
                return NewError("left hand side must be a NUMBER or STRING.");
            }
            var valObj = Eval(node.Right, env);
            if (IsError(valObj)){
                return valObj;
            }
            IObject newObj = null;
            if (type == ObjectType.NUMBER){
                if (valObj.Type() != ObjectType.NUMBER){
                    return NewError("value must be a NUMBER");
                }
                var value = ((NumberObj)valObj).Value;
                var numObj = (NumberObj)leftObj;
                switch (node.Operator)
                {
                    case "+=":
                        numObj.Value += value;
                        break;
                    case "-=":
                        numObj.Value -= value;
                        break;
                    case "*=":
                        numObj.Value *= value;
                        break;
                    case "/=":
                        if (value == 0){
                            return NewError("division by zero.");
                        }
                        numObj.Value /= value;
                        break;
                    default:
                        break;
                }
                newObj = numObj;
            } else if (type == ObjectType.STRING){
                if (valObj.Type() != ObjectType.STRING)
                {
                    return NewError("value must be a STRING");
                }
                var value = ((StringObj)valObj).Value;
                var strObj = (StringObj)leftObj;
                switch (node.Operator)
                {
                    case "+=":
                        strObj.Value += value;
                        break;
                    case "-=":
                        strObj.Value = strObj.Value.TrimEnd() + value;
                        break;
                    case "*=":
                    case "/=":
                        return NewError("invalid operator for string types.");
                }
                newObj = strObj;
            }
            if (newObj != null){
                env.GetAndSet(name, newObj);
                return newObj;
            }
            return NULL;
        }
        private static IObject EvaluateRightLogicalOperand(INode node, Environment env){
            var rightObj = Eval(node, env);
            if (IsError(rightObj)){
                return rightObj;
            }
            if (rightObj.Type() != ObjectType.BOOLEAN){
                return NewError("invalid operand for logical expression: " + rightObj.Type());
            }
            return ((BooleanObj)rightObj).Value ? TRUE : FALSE;
        }
        private static bool IsNumber(ObjectType type){
            return type == ObjectType.NUMBER;
        }
        private static bool IsError(IObject obj){
            return obj != null && obj.Type() == ObjectType.ERROR;
        }
        public static IObject NewError(string msg){
            return new ErrorObj(String.Format("runtime error-> {0}", msg));
        }
    }
}
