namespace DesignPatterns.StructuralPatters
{
    // The base Component class declares common operations for both simple and
    // complex objects of a composition.
    abstract class OrgUnit
    {
        public OrgUnit() { }

        // The base Component may implement some default behavior or leave it to
        // concrete classes (by declaring the method containing the behavior as
        // "abstract").
        public virtual int MembersCount() { return 0; }

        public abstract string PrintName();

        // In some cases, it would be beneficial to define the child-management
        // operations right in the base Component class. This way, you won't
        // need to expose any concrete component classes to the client code,
        // even during the object tree assembly. The downside is that these
        // methods will be empty for the leaf-level components.
        public virtual void Add(OrgUnit component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(OrgUnit component)
        {
            throw new NotImplementedException();
        }

        // Lets the client code figure out whether a component can bear children.
        public virtual bool IsComposite()
        {
            return true;
        }
    }

    // The Leaf class represents the end objects of a composition. A leaf can't
    // have any children.
    //
    // Usually, it's the Leaf objects that do the actual work, whereas Composite
    // objects only delegate to their sub-components.
    class Section(int membersCount) : OrgUnit
    {
        private readonly int _membersCount = membersCount;

        public override int MembersCount()
        {
            return _membersCount;
        }

        public override string PrintName()
        {
            return "Section";
        }

        public override bool IsComposite()
        {
            return false;
        }
    }

    // The Composite class represents the complex components that may have
    // children. Usually, the Composite objects delegate the actual work to
    // their children and then "sum-up" the result.
    class Department(int innerMembersCount) : OrgUnit
    {
        protected List<OrgUnit> _children = [];
        private readonly int _innerMembersCount = innerMembersCount;

        public override void Add(OrgUnit component)
        {
            _children.Add(component);
        }

        public override void Remove(OrgUnit component)
        {
            _children.Remove(component);
        }

        // The Composite executes its primary logic in a particular way. It
        // traverses recursively through all its children, collecting and
        // summing their results. Since the composite's children pass these
        // calls to their children and so forth, the whole object tree is
        // traversed as a result.
        public override int MembersCount()
        {
            int result = _innerMembersCount;

            foreach (OrgUnit orgUnit in _children)
            {
                // May result in recursive calls to leaves and other composites.
                result += orgUnit.MembersCount();
            }

            return result;
        }

        public override string PrintName()
        {
            int i = 0;
            string result = "Department(";

            foreach (OrgUnit component in _children)
            {
                // May result in recursive calls to leaves and other composites.
                result += component.PrintName();
                // If it's not the last component.
                if (i != _children.Count - 1)
                {
                    result += "+";
                }
                i++;
            }

            return result + ")";
        }
    }

    class CompositeClient
    {
        // The client code works with all of the components via the base
        // interface.
        public void ClientCode(OrgUnit leaf)
        {
            Console.WriteLine($"RESULT: {leaf.PrintName()}\n");
            Console.WriteLine($"Total Members: {leaf.MembersCount()}\n");
        }

        // Thanks to the fact that the child-management operations are declared
        // in the base Component class, the client code can work with any
        // component, simple or complex, without depending on their concrete
        // classes.
        public void ClientCode2(OrgUnit component1, OrgUnit component2)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }

            Console.WriteLine($"RESULT: {component1.PrintName()}");
            Console.WriteLine($"Total Members: {component1.MembersCount()}\n");
        }
    }

    class TestCompositePat
    {
        public static void Run()
        {
            CompositeClient client = new();

            Console.WriteLine("Client: I've got a simple component:");
            Section section = new(4);
            client.ClientCode(section);

            Console.WriteLine("Client: Now I've got a composite component:");
            Department President = new(1);
            Department dep1 = new(2);
            // Add simple components to composite
            dep1.Add(new Section(4));
            dep1.Add(new Section(5));
            Department dep2 = new(2);
            dep2.Add(new Section(6));
            // Add composite to composite
            President.Add(dep1);
            President.Add(dep2);
            client.ClientCode(President);

            Console.WriteLine("Client: I don't need to check the components classes even when managing the tree:");
            client.ClientCode2(President, section);

            try
            {
                section.Add(new Section(3)); // This will throw exception
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("!! Unit isn't composite component");
            }
        }
    }
}
