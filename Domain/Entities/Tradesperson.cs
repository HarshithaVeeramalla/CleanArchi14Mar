using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Tradesperson : BaseEntity
    {
        public Tradesperson(Guid id) : base(id)
        {
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string LoginCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}