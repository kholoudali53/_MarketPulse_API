using Microsoft.AspNetCore.Identity;
using Store.G01.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Repository.Identity
{
    public class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            var user = new AppUser()
            {
                Email = "kholoudali@gmail.com",
                DisplayName = "Kholoud Ali",
                UserName = "kholoud.ali",
                PhoneNumber = "1234567890",
                Address = new Address()
                {
                    FName = "Kholoud",
                    LName = "Ali",
                    City = "Tagamo3",
                    Country = "Egypt",
                    Street = "Elshbab",
                }
            };

            await _userManager.CreateAsync(user, "P@sswords");
        }
    }
}
