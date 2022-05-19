using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MathEngine.Extensions;

namespace MathEngine
{
    public interface IOutputter
    {
        void Print(string message);
    }

    public sealed class ConsoleOutputter : IOutputter
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
    }

    public sealed class FileOutputter : IOutputter
    {
        private string _path;

        public FileOutputter(string path)
        {
            _path = path;
        }
    
        public void Print(string message)
        {
            using var sw = File.OpenWrite(_path);
            byte[] data = Encoding.UTF8.GetBytes(message);
            sw.Write(data);
            sw.WriteByte((byte)'\n');
        }
    }

    public static class Output
    {
        private static List<IOutputter> _outputters = new();

        public static void AddOutputter(IOutputter outputter)
        {
            _outputters.Add(outputter);
        }

        public static void Intermediate(IExpression left, IExpression right)
        {
            if (!Eagle.EnableIntermediate)
            {
                return;
            }
        
            Result(left, right);
        }
    
        public static void Result(IExpression left, IExpression right)
        {
            string message = $"{left} = {right}".CleanExpressionString();
            foreach (IOutputter outputter in _outputters)
            {
                outputter.Print(message);
            }
        }
    }
}