using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Library.Domain.Core.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        public User()
        {
            this.IsBanned = false;
        }
    }
}
