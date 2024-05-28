using JoBoard.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JoBoard.Data
{
    public class JobsIdentityContext: IdentityDbContext<MyIdentityUser>
    {
        public JobsIdentityContext():base("Jobs_Connection")
        {

        }

    }

    public class MyIdentityUser : ApplicationUser
    {
         
    }
}