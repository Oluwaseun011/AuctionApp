using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class UserRole
    {
        public UserRole(Guid userId, string roleName, string createdBy)
        {
            UserId = userId;
            RoleName = roleName;
            CreatedBy = createdBy;
        }

        public Guid UserId { get; private set; }
        public bool IsDeleted { get; set; }
        public string RoleName { get; private set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public User User { get; private set; } = default!;
        public Role Role { get; private set; } = default!;
    }
}
