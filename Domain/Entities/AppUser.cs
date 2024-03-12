using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; } = null!; 
        public string? Bio { get; set; }
    }
}