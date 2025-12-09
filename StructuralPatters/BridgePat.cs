namespace DesignPatterns.StructuralPatters
{
    class Shape
    {
        protected IColor _implementation;

        public Shape(IColor implementation)
        {
            _implementation = implementation;
        }

        public virtual string BuildObjectOperation()
        {
            return "Shape: Base operation with:\n" +
                _implementation.PaintOparation();
        }
    }

    // Extending abstraction without changing the Implementation classes
    class CicleShape : Shape
    {
        public CicleShape(IColor implementation) : base(implementation)
        {
        }

        public override string BuildObjectOperation()
        {
            return "CicleShape: Cicle (Extended) operation with:\n" +
                _implementation.PaintOparation();
        }
    }

    class SquareShape : Shape
    {
        public SquareShape(IColor implementation) : base(implementation)
        {
        }

        public override string BuildObjectOperation()
        {
            return "SquareShape: Square (Extended) operation with:\n" +
                _implementation.PaintOparation();
        }
    }

    // Defines the interface for all implementation classes.
    // Provides only primitive operations, while the Abstraction
    // defines higher- level operations based on those primitives.
    public interface IColor
    {
        string PaintOparation();
    }

    // Two different implementations
    class RedColor : IColor
    {
        public string PaintOparation()
        {
            //return "ConcreteImplementationA: The result in platform A.\n";
            return "RedColor: Paints Red (platform A).\n";
        }
    }

    class BlueColor : IColor
    {
        public string PaintOparation()
        {
            return "BlueColor: Paints Blue (platform B).\n";
        }
    }

    class BridgeClient
    {
        // Client code only depend on the Abstraction class. This way the client code can
        // support any abstraction-implementation combination.
        public void ClientCode(Shape shape)
        {
            Console.Write(shape.BuildObjectOperation());
        }
    }

    class TestBridgePattern
    {
        public static void Run()
        {
            BridgeClient client = new();

            Shape shape;
            // The client code should be able to work with any pre-configured
            // abstraction-implementation combination.
            shape = new Shape(new RedColor());
            client.ClientCode(shape);

            Console.WriteLine();

            shape = new CicleShape(new BlueColor());
            client.ClientCode(shape);

            Console.WriteLine();

            shape = new CicleShape(new RedColor());
            client.ClientCode(shape);

            Console.WriteLine();

            shape = new SquareShape(new BlueColor());
            client.ClientCode(shape);
        }
    }
}
