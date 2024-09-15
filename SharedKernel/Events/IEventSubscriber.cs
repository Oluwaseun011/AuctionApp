using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Events
{
    public interface IEventSubscriber
    {
        Task SubscribeAsync<T>(Func<T, Task> eventFactory, string eventName) where T : IIntegrationEvent;
    }
}
