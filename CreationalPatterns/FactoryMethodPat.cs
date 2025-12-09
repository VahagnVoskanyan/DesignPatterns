namespace DesignPatterns.CreationalPatterns
{
    // The Creator class declares the factory method that is supposed to return
    // an object of a Transport class. The Creator's subclasses usually provide
    // the implementation of this method.
    abstract class Creator
    {
        // The Creator may also provide some default implementation of
        // the factory method.
        public abstract ITransport FactoryMethod();

        // The Creator's primary responsibility is not creating transports.
        // Usually, it contains some core business logic that relies on Transport objects,
        // returned by the factory method. Subclasses can indirectly change that business logic
        // by overriding the factory method and returning a different type of
        // product from it.
        public string DoPrimaryServiceBefore()
        {
            // Calling the factory method to create a Transport object.
            var someTransport = FactoryMethod();

            Console.WriteLine("Always doing some service before travel");

            // Some operation logic that depends on what Transport object
            // the factory method have returned.
            var result = "Creator: The same creator's code has just worked with "
                + someTransport.TravelsOn();

            return result;
        }
    }

    // Concrete Creators override the factory method and
    // return an instance of a concrete Transport class.
    class TruckCreator : Creator
    {
        // The signature of the method still uses the abstract transport type.
        // This way the Creator can stay independent of concrete transport classes.
        public override ITransport FactoryMethod()
        {
            return new TruckTransport();
        }
    }

    class ShipCreator : Creator
    {
        public override ITransport FactoryMethod()
        {
            return new ShipTransport();
        }
    }

    // The Transport interface declares the operations that all
    // transports must do.
    public interface ITransport
    {
        string TravelsOn();
    }

    // Concrete Transport travels on concrete ways.
    class TruckTransport : ITransport
    {
        public string TravelsOn()
        {
            return "'Truck travels on road'";
        }
    }

    class ShipTransport : ITransport
    {
        public string TravelsOn()
        {
            return "'Ship travels on water'";
        }
    }

    class FactoryClient
    {
        public static void Main()
        {
            // No core business logic in ITransport usage
            Console.WriteLine("Using only ITransport Interface");
            UsingOnlyITransport(new TruckTransport());
            Console.WriteLine();
            UsingOnlyITransport(new ShipTransport());
            Console.WriteLine("--");

            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new TruckCreator());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ShipCreator());
        }

        public static void UsingOnlyITransport(ITransport transport)
        {
            Console.WriteLine(transport.TravelsOn());
        }

        // The client argument is the base abstract 'Creator' class.
        public static void ClientCode(Creator creator)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.DoPrimaryServiceBefore());
            // ...
        }
    }

    class TestFactoryMethodPat
    {
        public static void Run()
        {
            FactoryClient.Main();
        }
    }
}
