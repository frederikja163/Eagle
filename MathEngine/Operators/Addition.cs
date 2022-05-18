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
            IExpression left = Left.Reduce();
            IExpression right = Right.Reduce();
            Addition addition = new Addition(left, right);
            
            if (left is Scalar lValue && right is Scalar rValue)
            {
                Scalar scalar = lValue + rValue;
                Output.Intermediate(addition, scalar);
                return scalar;
            }

            if (left != Left || right != Right)
            {
                Output.Intermediate(this, addition);
                return addition;
            }
            
            return this;
        }

        public override string ToString()
        {
            return $" {Left.ToString()} + {Right.ToString()} ";
        }
    }
}