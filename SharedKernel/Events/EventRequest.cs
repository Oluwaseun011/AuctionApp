using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Events
{
    public class EventRequest<T>
    {
        public string Name { get; init; }
        public T Payload { get; init; }
        public string TopicId { get; init; }
        public string TimeStamp { get; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}
