using HalloSingelton;

Console.WriteLine("Hello, World!");

for (int i = 0; i < 10; i++)
    Task.Run(() => Logger.Instance.Info($"Hallo {i}"));

Logger.Instance.Info("App started");

Logger.Instance.Info("App is working");
Logger.Instance.Error("App crashed");

