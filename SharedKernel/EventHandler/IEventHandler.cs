using RoomService.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.EventHandler
{
    public interface IEventHandler<TRequest> where TRequest : IIntegrationEvent
    {
        Task Handle(TRequest request);

    }
}
