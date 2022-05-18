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
            IExpression left = Left.Reduce();
            IExpression right = Right.Reduce();
            Multiplication multiplication = new Multiplication(left, right);

            if (left is Scalar lValue && right is Scalar rValue)
            {
                Scalar scalar = lValue * rValue;
                Output.Intermediate(multiplication, scalar);
                return scalar;
            }
            
            if (left != Left || right != Right)
            {
                Output.Intermediate(this, multiplication);
                return multiplication;
            }

            return this;
        }

        public override string ToString()
        {
            return Eagle.EnableLatex
                ? $" {Left.ToString()} \\cdot {Right.ToString()} "
                : $" {Left.ToString()} * {Right.ToString()} ";
        }
    }
}