using System.Linq;
using MathEngine.Operators;

namespace MathEngine
{
    public interface IExpression
    {
        IExpression Reduce();

        public string ToText();
        public string ToLatex();

        public static IExpression Parse(string expression)
        {
            {
                int index = expression.IndexOf('+');
                if (index != -1)
                {
                    string left = expression[..index];
                    string right = expression[(index+1)..];
                    return new Addition(Parse(left), Parse(right));
                }
            }
            {
                int index = expression.IndexOf('-');
                if (index != -1)
                {
                    string left = expression[..index];
                    string right = expression[(index+1)..];
                    return new Subtraction(Parse(left), Parse(right));
                }
            }
            {
                int index = expression.IndexOf('*');
                if (index != -1)
                {
                    string left = expression[..index];
                    string right = expression[(index+1)..];
                    return new Multiplication(Parse(left), Parse(right));
                }
            }
            {
                int index = expression.IndexOf('/');
                if (index != -1)
                {
                    string left = expression[..index];
                    string right = expression[(index+1)..];
                    return new Division(Parse(left), Parse(right));
                }
            }
            {
                return new Scalar(long.Parse(expression));
            }
        }
    }
}