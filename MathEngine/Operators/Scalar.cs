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

        public string ToText()
        {
            return Value.ToString();
        }

        public string ToLatex()
        {
            return Value.ToString();
        }
    }
}