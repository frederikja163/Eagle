using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using MathEngine.Extensions;

namespace MathEngine.Operators
{
    public sealed class Summation : IExpression
    {
        public record Term(IExpression Expression, bool IsPositive);
        
        private readonly Term[] _terms;

        public Summation(Term[] terms)
        {
            _terms = terms;
        }

        public IExpression Reduce()
        {
            Scalar sum = new(0);
            foreach ((IExpression expression, bool isPositive) in _terms)
            {
                IExpression reduced = expression.Reduce();
                if (reduced is Scalar reducedValue)
                {
                    if (isPositive)
                    {
                        sum += reducedValue;
                    }
                    else
                    {
                        sum -= reducedValue;
                    }
                }
            }
            // TODO: Fix this for non scalar expressions.
            
            return sum;
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            foreach ((IExpression expression, bool isPositive) in _terms)
            {
                if (builder.Length != 0)
                {
                    if (isPositive)
                    {
                        builder.Append(" + ");
                    }
                    else
                    {
                        builder.Append(" - ");
                    }
                }
                builder.Append($" {expression} ");
            }

            return builder.ToString();
        }
        
        [ExpressionParser(0)]
        public static bool TryParse(string str, [NotNullWhen(true)] out IExpression? expression)
        {
            if (str[0] != '+' && str[0] != '-')
            {
                str = '+' + str;
            }
            
            List<Term> expressions = new();
            int lastTermStart = 0;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '+' || str[i] == '-')
                {
                    if (!IExpression.TryParse(str[(lastTermStart + 1)..i], out IExpression? expr))
                    {
                        expression = null;
                        return false;
                    }

                    expressions.Add(new Term(expr, str[lastTermStart] == '+'));
                    lastTermStart = i;
                }
            }

            if (expressions.Count == 0)
            {
                expression = null;
                return false;
            }
            
            if (!IExpression.TryParse(str[(lastTermStart+1)..], out IExpression? e))
            {
                expression = null;
                return false;
            }
            expressions.Add(new Term(e, str[lastTermStart] == '+'));

            expression = new Summation(expressions.ToArray());
            return true;
        }
    }
}