namespace DesignPatterns.StructuralPatters
{
    // The Facade class provides a simple interface to the complex logic of one
    // or several subsystems.
    // Client code works with the Facade class methos instead of working
    // with all methods of subsystems directly.
    // Facades can work with multiple subsystems at the same time.
    public class Facade
    {
        protected Subsystem1 _subsystem1;

        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            _subsystem1 = subsystem1;
            _subsystem2 = subsystem2;
        }

        // Here might not be all methods of the Susbsystem
        public string Operation()
        {
            string result = "Facade initializes subsystems:\n";
            result += _subsystem1.Operation1();
            result += _subsystem2.Operation1();
            result += "Facade orders subsystems to perform the action:\n";
            result += _subsystem1.OperationN();
            result += _subsystem2.OperationZ();
            return result;
        }

        public string Sub1Operation()
        {
            string result = "Facade delegates to Subsystem1:\n";
            result += _subsystem1.Operation1();
            result += _subsystem1.OperationN();
            return result;
        }
    }

    public class Subsystem1
    {
        public string Operation1()
        {
            return "Subsystem1: Ready!\n";
        }

        public string OperationN()
        {
            return "Subsystem1: Go!\n";
        }

        public string OperationUnused()
        {
            return "Subsystem1: Unused!\n";
        }
    }

    public class Subsystem2
    {
        public string Operation1()
        {
            return "Subsystem2: Get ready!\n";
        }

        public string OperationZ()
        {
            return "Subsystem2: Fire!\n";
        }

        public string OperationUnused()
        {
            return "Subsystem2: Unused!\n";
        }
    }

    class FacadeClient
    {
        // The client code works with the Facade.
        public static void ClientCode(Facade facade)
        {
            Console.WriteLine(facade.Sub1Operation());
            Console.Write(facade.Operation());
        }
    }

    class TestFacadePat
    {
        public static void Run()
        {
            Subsystem1 subsystem1 = new();
            Subsystem2 subsystem2 = new();
            Facade facade = new(subsystem1, subsystem2);

            Console.WriteLine("Working with subsystem directly");
            Console.WriteLine(subsystem1.Operation1());

            FacadeClient.ClientCode(facade);
        }
    }
}
