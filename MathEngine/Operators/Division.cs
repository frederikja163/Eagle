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
            IExpression left = Left.Reduce();
            IExpression right = Right.Reduce();
            Division division = new Division(left, right);

            if (right is Scalar val && val.Value == 0)
            {
                throw new DivideByZeroException();
            }

            if (left is Scalar lValue && right is Scalar rValue)
            {
                Scalar scalar = lValue / rValue;
                Output.Intermediate(division, scalar);
                return scalar;
            }
            
            if (left != Left || right != Right)
            {
                Output.Intermediate(this, division);
                return division;
            }

            return this;
        }

        public override string ToString()
        {
            return Eagle.EnableLatex ?
                $" \\frac{{{Left.ToString()}}}{{{Right.ToString()}}} " :
                $" {Left.ToString()} / {Right.ToString()} ".CleanExpressionString();
        }
    }
}