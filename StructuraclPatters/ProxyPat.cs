namespace DesignPatterns.StructuraclPatters
{
    // Doing tasks before and/or after forwarding the request to the RealSubject

    // Interface that will be implemented by both RealSubject and Proxy
    public interface ISubject
    {
        void Request();
    }

    // RealSubject class that implements the ISubject interface
    public class RealSubject : ISubject
    {
        public void Request()
        {
            // Actual implementation of the request
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }

    // Proxy class that also implements the ISubject interface
    public class Proxy : ISubject
    {
        private RealSubject _realSubject;

        public Proxy(RealSubject realSubject)
        {
            _realSubject = realSubject;
        }

        public void Request()
        {
            if (CheckAccess())
            {
                _realSubject.Request();

                LogAccess();
            }
        }

        public bool CheckAccess()
        {
            // Some real checks should go here.
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }

        public void LogAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request. " + DateTime.Now);
        }
    }

    // Client code
    public class ProxyClient
    {
        public void ClientCode(ISubject subject)
        {
            subject.Request();
        }
    }

    public class TestProxyPattern
    {
        public static void Run()
        {
            ProxyClient client = new();

            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealSubject realSubject = new();
            client.ClientCode(realSubject);

            Console.WriteLine();

            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new(realSubject); // one additional step
            client.ClientCode(proxy);
        }
    }
}
