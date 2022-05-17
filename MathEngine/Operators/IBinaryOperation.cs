namespace MathEngine.Operators
{
    public interface IBinaryOperation : IOperation
    {
        public IExpression Left { get; }
        public IExpression Right { get; }
    }
}