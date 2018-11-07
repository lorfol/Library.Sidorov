using Library.Domain.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;

namespace Library.Infrastructure.Data
{
    public class ApplicationUserManager : UserManager<User>
    {
        public static RoleManager<IdentityRole> RoleManager;

        public ApplicationUserManager(IUserStore<User> store)
                : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            LibraryDbContext db = context.Get<LibraryDbContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            return manager;
        }
    }
}
