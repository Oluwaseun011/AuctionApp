using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Events
{
    public interface IIntegrationEvent
    {
        public string EventName { get; }
    }
}
