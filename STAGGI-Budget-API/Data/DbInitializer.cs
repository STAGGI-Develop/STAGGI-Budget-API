using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(BudgetContext context,
            UserManager<BUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            // Set Users
            if (!_userManager.Users.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Premium" });

                var newUsers = new RegisterRequestDTO[]
                {
                new RegisterRequestDTO { FirstName="Sebastian", LastName="F", Email = "sf@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Tatiana", LastName="Q", Email = "tq@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Andres", LastName="R", Email = "ar@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Gonzalo", LastName="C", Email = "gc@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Gaston", LastName="R", Email = "gr@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Ignacio", LastName="D", Email = "id@mail.com", Password = "Pass-123"}
                };
                foreach (var user in newUsers)
                {
                    var newUser = new BUser
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.Email
                    };
                    await _userManager.CreateAsync(newUser, user.Password);
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                    await _userManager.AddToRoleAsync(newUser, "User");
                }
            }

            // Set Accounts
        }
    }
}
