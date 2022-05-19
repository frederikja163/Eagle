using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MathEngine.Extensions;
using MathEngine.Operators;

namespace MathEngine
{
    public interface IExpression
    {
        IExpression Reduce();

        public delegate bool ExpressionParseDelegate(string str, [NotNullWhen(true)] out IExpression? expression);

        private static readonly ExpressionParseDelegate[] Parsers;

        static IExpression()
        {
            List<(float order, ExpressionParseDelegate parser)> parsers = new();

            IEnumerable<MethodInfo> methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttribute<ExpressionParserAttribute>() != null);
            
            foreach (MethodInfo method in methods)
            {
                ExpressionParserAttribute parserAttr = method.GetCustomAttribute<ExpressionParserAttribute>()!;

                try
                {
                    ExpressionParseDelegate parseDelegate = (method.CreateDelegate(typeof(ExpressionParseDelegate)) as ExpressionParseDelegate)!;
                    parsers.Add((parserAttr.Order, parseDelegate));
                }
                catch
                {
                    Log.Warning($"Expression parser {method.DeclaringType}.{method.Name} doesn't have correct signature. It needs to be of the signature 'public static bool TryParse(string str, [NotNullWhen(true)] out IExpression? expression)'.");
                }
            }

            Parsers = parsers.OrderBy(p => p.order)
                .Select(p => p.parser)
                .ToArray();
        }

        public static IExpression Parse(string str)
        {
            if (TryParse(str, out IExpression? expression))
            {
                return expression;
            }

            throw new Exception($"Failed to pass expression {str}");
        }

        public static bool TryParse(string str, [NotNullWhen(true)] out IExpression? expression)
        {
            foreach (ExpressionParseDelegate parser in Parsers)
            {
                if (parser.Invoke(str, out expression))
                {
                    return true;
                }
            }

            expression = null;
            return false;
        }
    }
}