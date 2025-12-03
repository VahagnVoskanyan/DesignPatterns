namespace DesignPatterns.CreationalPatterns
{
    // Inferface for all king of chairs
    public interface IChair
    {
        public bool HasLegs();

        public void SitOn();
    }
    // Some kind of chair
    public class VictorianChair : IChair
    {
        public bool HasLegs()
        {
            return true;
        }

        public void SitOn()
        {
            Console.WriteLine("Soft sit");
        }
    }

    public class ModernChair : IChair
    {
        public bool HasLegs()
        {
            return false;
        }

        public void SitOn()
        {
            Console.WriteLine("Tough sit");
        }
    }

    public interface ISofa
    {
        public bool HasArmrests();

        public string LayOn();

        // The Abstract Factory makes sure that all products it creates are of
        // the same variant and thus, compatible.
        public string Collaborate(IChair chair);
    }

    public class VictorianSofa : ISofa
    {
        public bool HasArmrests()
        {
            return true;
        }

        public string LayOn()
        {
            return "Lay softly";
        }

        public string Collaborate(IChair chair)
        {
            var result = chair.HasLegs() ? "has legs" : "has no legs";

            return $"Victorian Chair Legs from Sofa class: {result}";
        }
    }

    public class ModernSofa : ISofa
    {
        public bool HasArmrests()
        {
            return false;
        }

        public string LayOn()
        {
            return "Lay toughly";
        }
        public string Collaborate(IChair chair)
        {
            var result = chair.HasLegs() ? "has legs" : "has no legs";

            return $"Modern Chair Legs from Sofa class: {result}";
        }

    }
    // Abstract factory interface that creates abstract products
    public interface IFurnitureFactory
    {
        public IChair CreateChair();

        public ISofa CreateSofa();
    }
    // Concrete factory that creates Victorian furniture.
    // Methods returns Victorian kind of pruducts.
    public class VictorianFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new VictorianChair();
        }

        public ISofa CreateSofa()
        {
            return new VictorianSofa();
        }
    }

    public class ModernFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new ModernChair();
        }

        public ISofa CreateSofa()
        {
            return new ModernSofa();
        }
    }

    // The client code works with factories and products only through abstract
    // types: AbstractFactory and AbstractProduct. This lets you pass any
    // factory or product subclass to the client code without breaking it.
    class AbsFactoryPatClient
    {
        public static void Main()
        {
            // The client code can work with any concrete factory class.
            Console.WriteLine("Client: Testing client code with the Victorian factory type...");
            ClientMethod(new VictorianFurnitureFactory());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the Modern factory type...");
            ClientMethod(new ModernFurnitureFactory());
        }

        public static void ClientMethod(IFurnitureFactory factory)
        {
            var chair = factory.CreateChair();
            var sofa = factory.CreateSofa();

            Console.WriteLine(sofa.LayOn());
            Console.WriteLine(sofa.Collaborate(chair));
        }
    }

    static class TestAbsFactoryMethodPat
    {
        public static void Run()
        {
            AbsFactoryPatClient.Main();
        }
    }
}