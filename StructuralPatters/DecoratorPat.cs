namespace DesignPatterns.StructuralPatters
{
    // Or Wrapper
    // You can easily substitute the linked “helper” object with another,
    // changing the behavior of the container at runtime. 


    // The base Component interface defines operations that can be altered by
    // decorators.
    public abstract class Notifier
    {
        public abstract string Send();
    }

    // Concrete Components provide default implementations of the operations.
    // There might be several variations of these classes.
    class OutlookNotifier : Notifier
    {
        public override string Send()
        {
            return " Send using Outlook Notifier";
        }
    }

    abstract class Decorator : Notifier
    {
        protected Notifier _notifier;

        public Decorator(Notifier notifier)
        {
            _notifier = notifier;
        }

        public void SetComponent(Notifier component)
        {
            _notifier = component;
        }

        public override string Send()
        {
            if (_notifier != null)
                return _notifier.Send();
            else
                return string.Empty;
        }
    }

    class SMSDecorator : Decorator
    {
        public SMSDecorator(Notifier notifier) : base(notifier)
        {
        }

        // Decorators may call parent implementation of the operation, instead
        // of calling the wrapped object directly. This approach simplifies
        // extension of decorator classes.
        public override string Send()
        {
            return $"SMS Decorator Send: ({base.Send()})";
        }
    }

    // Decorators can execute their behavior either before or after the call to
    // a wrapped object.
    class SlackDecorator : Decorator
    {
        public SlackDecorator(Notifier notifier) : base(notifier)
        {
        }

        public override string Send()
        {
            return $"Slack Decorator Send: ({base.Send()})";
        }
    }

    public class DecoratorClient
    {
        // The client code works with all objects using the Component interface.
        // This way it can stay independent of the concrete classes of
        // components it works with.
        public void ClientCode(Notifier notifier)
        {
            Console.WriteLine("RESULT: " + notifier.Send());
        }
    }

    class TestDecoratorPat
    {
        public static void Run()
        {
            DecoratorClient client = new();

            var simpleOutlookNot = new OutlookNotifier();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(simpleOutlookNot);
            Console.WriteLine();

            // ...as well as decorated ones.
            //
            // Note how decorators can wrap not only simple components but the
            // other decorators as well.
            SMSDecorator smsDecorator = new(simpleOutlookNot);
            SlackDecorator slackDecorator = new(smsDecorator);

            Console.WriteLine("Client: Now I've got a decorated component:");
            client.ClientCode(slackDecorator);
        }
    }
}
