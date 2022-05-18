using System;
using MathEngine;
using MathEngine.Extensions;

namespace Sandbox
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Eagle.InitDefault();
            Eagle.EnableIntermediate = true;
            Eagle.EnableLatex = true;

            IExpression expression = IExpression.Parse("132489+2437289*234+1234/654");
            Log.Info(expression.ToString().CleanExpressionString());
            Output.Result(expression, expression.Reduce());
            
            Console.ReadKey();
        }
    }
}