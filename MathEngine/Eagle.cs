namespace MathEngine
{
    public static class Eagle
    {
        public static bool EnableIntermediate { get; set; } = false;
        public static bool EnableLatex { get; set; } = false;
    
        public static void InitDefault()
        {
            Log.AddLogger(new ConsoleLogger());
            Log.AddLogger(new FileLogger("log.txt"));
        
            Output.AddOutputter(new ConsoleOutputter());
            Eagle.EnableLatex = false;
            Eagle.EnableIntermediate = false;
        
            Log.Info("Using default initialization configuration.");
        }
    }
}