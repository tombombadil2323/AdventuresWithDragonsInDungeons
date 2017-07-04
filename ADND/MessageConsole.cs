using System;
namespace ADND
{
    public class MessageConsole : IMessageChannel
    {
        public MessageConsole()
        {
        }
        public MessageConsole(object message)
        {
			MessagePush(message);
        }
        public void MessagePush(object message)
        {
            Console.WriteLine(message);
        }

        public string MessagePull()
        {
            return Console.ReadLine();
        }
    }
}
