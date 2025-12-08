namespace DesignPatterns.CreationalPatterns
{
    // Thread-safe Singleton
    class Singleton
    {
        private static Singleton? _instance;

        private Singleton() { }

        // lock object will be used to synchronize threads
        // during first access to the Singleton.
        private static readonly object _lock = new();

        public static Singleton GetInstance(string value)
        {
            // If intsance null => Create
            if (_instance == null)
            {
                // The first thread acquire lock, the rest will wait here.
                lock (_lock)
                {
                    // Once first thread leaves the lock block, a thread that
                    // might have been waiting for the lock release may then
                    // enter this section and get already created instance.
                    if (_instance == null)
                    {
                        _instance = new();
                        _instance.Value = value;
                    }
                }
            }

            return _instance;
        }

        // To Prove that works
        public string Value { get; set; } = string.Empty;
    }

    class TestSingletonPat
    {
        public static void Run()
        {
            // The client code.

            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );

            Thread process1 = new(() =>
            {
                TestSingleton("FOO");
            });

            Thread process2 = new(() =>
            {
                TestSingleton("BAR");
            });

            process1.Start();
            process2.Start();

            process1.Join(); // Main thread waits here until process1 finishes
            process2.Join();
        }

        public static void TestSingleton(string value)
        {
            Thread.Sleep(1000); // Simulate some work

            Singleton singleton = Singleton.GetInstance(value);
            Console.WriteLine(singleton.Value);
        }
    }
}
