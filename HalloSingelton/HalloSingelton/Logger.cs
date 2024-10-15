namespace HalloSingelton
{
    internal class Logger
    {
        private static Logger instance;
        private static object sync = new object();
        public static Logger Instance
        {
            get
            {
                lock (sync)
                {
                    if (instance == null)
                        instance = new Logger();
                }

                return instance;
            }
        }

        private Logger()
        {
            Info("Neuer Logger");
        }

        public void Info(string message)
        {
            Console.WriteLine($"{DateTime.Now:g} [INFO] {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"{DateTime.Now:g} [ERROR] {message}");
        }
    }
}
