using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Events
{
    public class EventEnvelope<TEvent> where TEvent : IIntegrationEvent
    {
        public EventEnvelope(Guid id, string name, TEvent payload, float version = 1.0f, DateTime? createdDate = null, IReadOnlyDictionary<string, object?>? metaData = null)
        {
            Id = id;
            Name = name;
            Payload = payload;
            Version = version;
            CreatedDate = createdDate ?? DateTime.UtcNow;
            MetaData = metaData ?? new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Version { get; set; }
        public TEvent Payload { get; set; }
        public DateTime CreatedDate { get; set; }
        public IReadOnlyDictionary<string, object?> MetaData { get; set; }
    }
}
