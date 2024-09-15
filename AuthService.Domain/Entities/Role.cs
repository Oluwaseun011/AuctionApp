using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Role
    {
        private readonly List<UserRole> _userRoles;
        public Role(string name, string? description)
        {
            Name = name;
            Description = description;
            _userRoles = new List<UserRole>();
        }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        public IReadOnlyList<UserRole> UserRoles
        {
            get => _userRoles.AsReadOnly();
            set => _userRoles.AddRange(value);
        }
    }
}
