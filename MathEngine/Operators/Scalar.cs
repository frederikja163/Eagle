using System.Diagnostics.CodeAnalysis;

namespace MathEngine.Operators
{
    public sealed class Scalar: IExpression
    {
        
        public long Value { get; set; }
        
        public Scalar(long value)
        {
            Value = value;
        }

        public IExpression Reduce()
        {
            return this;
        }
        
        public static Scalar operator +(Scalar left, Scalar right)
        {
            return new Scalar(left.Value + right.Value);
        }
        
        public static Scalar operator *(Scalar left, Scalar right)
        {
            return new Scalar(left.Value * right.Value);
        }
        
        public static Scalar operator -(Scalar left, Scalar right)
        {
            return new Scalar(left.Value - right.Value);
        }
        
        public static Scalar operator /(Scalar left, Scalar right)
        {
            return new Scalar(left.Value / right.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }


        [ExpressionParser(1)]
        public static bool TryParse(string str, [NotNullWhen(true)] out IExpression? expression)
        {
            if (long.TryParse(str, out long value))
            {
                expression = new Scalar(value);
                return true;
            }

            expression = null;
            return false;
        }
    }
}