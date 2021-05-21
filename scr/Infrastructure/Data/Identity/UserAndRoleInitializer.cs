using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Identity
{
    public class UserAndRoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    FirstName = "Инна",
                    SecondName = "Белова",
                    MiddleName = "Ивановна",
                    DateOfBirth = new DateTime(1982, 1, 12),
                    Sex = "женский",
                    Address = "г. Пинск, ул. Светлова 23, кв. 31",
                    Phone = "652-34-23",
                    Email = adminEmail,
                    UserName = adminEmail
                };

                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
