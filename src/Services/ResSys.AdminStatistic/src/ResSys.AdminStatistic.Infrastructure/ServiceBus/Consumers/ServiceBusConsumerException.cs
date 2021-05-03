using System;

namespace ResSys.AdminStatistic.Infrastructure.ServiceBus
{
    public class ServiceBusConsumerException : Exception
    {
        internal ServiceBusConsumerException(string message)
            : base(message)
        { }
    }
}
