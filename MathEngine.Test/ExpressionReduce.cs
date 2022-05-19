using System;
using MathEngine.Operators;
using NUnit.Framework;

namespace MathEngine.Test
{
    public class ExpressionReduce
    {
        [TestCase("1+2+3+4+5", 15)]
        [TestCase("1+2+3+4-5", 5)]
        [TestCase("-5+4+3+2+1", 5)]
        [TestCase("-5-4-3-2-1", -15)]
        public void SummationParse(string equation, int result)
        {
            IExpression expression = IExpression.Parse(equation).Reduce();
            Assert.IsInstanceOf<Scalar>(expression);
            Assert.AreEqual(result, ((Scalar)expression).Value);
        }
    }
}
