using System;
using System.ComponentModel;

namespace MathEngine
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExpressionParserAttribute : Attribute
    {
        public float Order { get; }
        
        public ExpressionParserAttribute(float order)
        {
            Order = order;
        }
    }
}