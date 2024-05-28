using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace JoBoard.Models
{
    public class ApplicationUser: IdentityUser
    {
       public string Name { get; set; }

    }
}