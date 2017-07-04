using System;
namespace ADND
{
    public interface IMessageChannel
    {
        void MessagePush(object message);
        string MessagePull();
    }
}
