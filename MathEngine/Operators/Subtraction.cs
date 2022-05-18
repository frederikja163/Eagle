using MathEngine.Extensions;

namespace MathEngine.Operators
{
    public sealed class Subtraction : IBinaryOperation
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
        
        public Subtraction(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public IExpression Reduce()
        {
            IExpression left = Left.Reduce();
            IExpression right = Right.Reduce();
            Subtraction subtraction = new Subtraction(left, right);
            
            if (left is Scalar lValue && right is Scalar rValue)
            {
                Scalar scalar = lValue - rValue;
                Output.Intermediate(subtraction, scalar);
                return subtraction;
            }
            
            
            if (left != Left || right != Right)
            {
                Output.Intermediate(this, subtraction);
                return subtraction;
            }

            return this;
        }

        public override string ToString()
        {
            return $" {Left.ToString()} - {Right.ToString()} ";
        }
    }
}