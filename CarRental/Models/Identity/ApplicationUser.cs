using Microsoft.AspNetCore.Identity;

namespace Projekt_strona.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
