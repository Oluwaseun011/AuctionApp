using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class User
    {
        private readonly List<UserRole> _userRoles = new();

        public User(string firstName, string lastName, string email, string? enumeratorCode, string phone, string organizationName, string passwordHash, string hashSalt, string createdBy)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EnumeratorCode = enumeratorCode;
            Phone = phone;
            OrganizationName = organizationName;
            PasswordHash = passwordHash;
            HashSalt = hashSalt;
            CreatedBy = createdBy;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string? EnumeratorCode { get; private set; }
        public string Phone { get; private set; }
        public string OrganizationName { get; private set; }
        public string PasswordHash { get; private set; }
        public string HashSalt { get; private set; }
        public string? ResetToken { get; private set; }
        public DateTime? ResetTokenExpiryTime { get; private set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        public IReadOnlyList<UserRole> UserRoles
        {
            get => _userRoles.AsReadOnly();
            private set => _userRoles.AddRange(value);
        }


        public void AddUserRoles(params UserRole[] userRoles)
        {
            foreach (var userRole in userRoles)
            {
                if (!_userRoles.Any(ur => ur.RoleName.Equals(userRole.RoleName, StringComparison.OrdinalIgnoreCase)))
                {
                    _userRoles.Add(userRole);
                }
            }
        }
        public void UpdateUserRoles(params UserRole[] userRoles)
        {
            foreach (var userRole in userRoles)
            {
                _userRoles.Remove(userRole);
                //userRole.IsDeleted = true;
            }
        }



        public User UpdateResetToken(string token)
        {
            ResetToken = token;
            ResetTokenExpiryTime = DateTime.UtcNow.AddMinutes(60);
            return this;
        }

        public User UpdatePassword(string passwordHash, string hashSalt)
        {
            PasswordHash = passwordHash;
            HashSalt = hashSalt;
            return this;
        }
        public void UpdateUser(string firstName, string lastName, string? enumeratorCode, string phone, string organizationName, string modifiedBy)
        {
            FirstName = firstName;
            LastName = lastName;
            EnumeratorCode = enumeratorCode;
            Phone = phone;
            OrganizationName = organizationName;
            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
