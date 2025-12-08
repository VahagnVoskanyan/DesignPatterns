namespace DesignPatterns.CreationalPatterns
{
    // The Builder interface specifies methods for creating the different parts
    // of the Product objects.
    public interface IBuilder
    {
        void BuildWalls();

        void BuildDoors();

        void BuildRoof();
    }

    public class WoodenHouseBuilder : IBuilder
    {
        private House _house = new();

        // A fresh builder instance should contain a blank product object.
        public WoodenHouseBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _house = new House();
        }

        public void BuildDoors()
        {
            _house.Add("Wooden Doors");
        }

        public void BuildRoof()
        {
            _house.Add("Wooden Roof");
        }

        public void BuildWalls()
        {
            _house.Add("Wooden Walls");
        }

        public House GetHouse()
        {
            House result = _house;

            // Reset the builder so it can be used to build another product.
            Reset();

            return result;
        }
    }

    public class StoneHouseBuilder : IBuilder
    {
        private House _house = new();

        // A fresh builder instance should contain a blank product object.
        public StoneHouseBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _house = new House();
        }

        public void BuildDoors()
        {
            _house.Add("Stone Doors");
        }

        public void BuildRoof()
        {
            _house.Add("Stone Roof");
        }

        public void BuildWalls()
        {
            _house.Add("Stone Walls");
        }

        public House GetHouse()
        {
            House result = _house;

            // Reset the builder so it can be used to build another product.
            Reset();

            return result;
        }
    }

    public class House
    {
        private List<string> _parts = [];

        public void Add(string part)
        {
            _parts.Add(part);
        }

        public void ListParts()
        {
            Console.WriteLine("House parts: " + string.Join(", ", _parts));
        }
    }

    // Optional. The Director is only responsible for executing
    // the building steps in a particular sequence.
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        public void BuildMinimalViableHouse()
        {
            _builder.BuildWalls();
        }

        public void BuildFullFeaturedHouse()
        {
            _builder.BuildWalls();
            _builder.BuildDoors();
            _builder.BuildRoof();
        }
    }

    static class TestBuilderPat
    {
        public static void Run()
        {
            // Client code
            var director = new Director();
            var builderW = new WoodenHouseBuilder();
            director.Builder = builderW;

            Console.WriteLine("Standard basic house:");
            director.BuildMinimalViableHouse();
            builderW.GetHouse().ListParts();

            Console.WriteLine("Standard full featured house:");
            director.BuildFullFeaturedHouse();
            builderW.GetHouse().ListParts();

            // Custom house. Without Director class.
            Console.WriteLine("Custom wooden house:");
            builderW.BuildWalls();
            builderW.BuildRoof();
            builderW.GetHouse().ListParts();

            // Stone house
            Console.WriteLine("Stone house:");
            var builderS = new StoneHouseBuilder();
            builderS.BuildWalls();
            builderS.BuildRoof();
            builderS.GetHouse().ListParts();
        }
    }
}
