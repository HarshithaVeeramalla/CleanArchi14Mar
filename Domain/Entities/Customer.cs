using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(Guid id) : base(id)
        {
        }
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LoginCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }
    }
}