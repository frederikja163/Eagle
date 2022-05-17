using System;
using MathEngine;

namespace Sandbox
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.AddLogger(new ConsoleLogger());
            Log.AddLogger(new FileLogger("log.txt"));
            Log.Trace("Starting sandbox.");

            Log.Info(IExpression.Parse("132489+2437289*234+1234/654").Reduce().ToText());
            Log.Info(IExpression.Parse("132489+2437289*234+1234/654").ToLatex());
        }
    }
}