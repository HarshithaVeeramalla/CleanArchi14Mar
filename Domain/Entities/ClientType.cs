using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class ClientType : BaseEntity
    {
        public ClientType(Guid id) : base(id)
        {
        }

        public string Name { get; set; } = string.Empty;
    }
}