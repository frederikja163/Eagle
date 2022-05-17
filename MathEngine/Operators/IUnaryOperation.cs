namespace MathEngine.Operators
{
    public interface IUnaryOperation : IOperation
    {
        public IExpression Value { get; }
    }
}