using MathEngine.Extensions;

namespace MathEngine.Operators
{
    public sealed class Addition : IBinaryOperation
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        
        public Addition(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public IExpression Reduce()
        {
            var left = Left.Reduce();
            var right = Right.Reduce();

            if (left is Scalar lValue && right is Scalar rValue)
            {
                return lValue + rValue;
            }

            return new Addition(left, right);
        }

        public string ToText()
        {
            return $" {Left.ToText()} + {Right.ToText()} ".CleanExpressionString();
        }

        public string ToLatex()
        {
            return $" {Left.ToLatex()} + {Right.ToLatex()} ".CleanExpressionString();
        }
    }
}