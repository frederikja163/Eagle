using System;
using MathEngine.Extensions;

namespace MathEngine.Operators
{
    public class Division : IBinaryOperation
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        
        public Division(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public IExpression Reduce()
        {
            var left = Left.Reduce();
            var right = Right.Reduce();

            if (right is Scalar val && val.Value == 0)
            {
                throw new DivideByZeroException();
            }

            if (left is Scalar lValue && right is Scalar rValue)
            {
                return lValue / rValue;
            }

            return new Division(left, right);
        }

        public string ToText()
        {
            return $" {Left.ToText()} / {Right.ToText()} ".CleanExpressionString();
        }

        public string ToLatex()
        {
            return $" \\frac{{{Left.ToLatex()}}}{{{Right.ToLatex()}}} ".CleanExpressionString();
        }
    }
}