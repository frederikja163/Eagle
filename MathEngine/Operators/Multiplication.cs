using MathEngine.Extensions;

namespace MathEngine.Operators
{
    public sealed class Multiplication : IBinaryOperation
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        
        public Multiplication(IExpression left, IExpression right)
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
                return lValue * rValue;
            }

            return new Multiplication(left, right);
        }

        public string ToText()
        {
            return $" {Left.ToText()} * {Right.ToText()} ".CleanExpressionString();
        }

        public string ToLatex()
        {
            return $" {Left.ToLatex()} \\cdot {Right.ToLatex()} ".CleanExpressionString();
        }
    }
}