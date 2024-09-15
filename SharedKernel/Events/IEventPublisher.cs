using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : IIntegrationEvent;
    }
}
