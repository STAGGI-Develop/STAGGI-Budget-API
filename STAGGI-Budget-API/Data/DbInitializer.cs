using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Enums;
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

                var newUsers = new RequestUserDTO[]
                {
                new RequestUserDTO { FirstName="Sebastian", LastName="F", Email = "sf@mail.com", Password = "Pass-123"},
                new RequestUserDTO { FirstName="Tatiana", LastName="Q", Email = "tq@mail.com", Password = "Pass-123"},
                new RequestUserDTO { FirstName="Andres", LastName="R", Email = "ar@mail.com", Password = "Pass-123"},
                new RequestUserDTO { FirstName="Gonzalo", LastName="C", Email = "gc@mail.com", Password = "Pass-123"},
                new RequestUserDTO { FirstName="Gaston", LastName="R", Email = "gr@mail.com", Password = "Pass-123"},
                new RequestUserDTO { FirstName="Ignacio", LastName="D", Email = "id@mail.com", Password = "Pass-123"}
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


            // Set Categories
            if (!context.Categories.Any())
            {
                var allUsers = context.BUsers.ToList();
                var defaultCategories = new List<CategoryDTO>
                {
                    new CategoryDTO { Name = "Groceries", Image = "groceries" },
                    new CategoryDTO { Name = "Transportation", Image = "transportation" },
                    new CategoryDTO { Name = "Entertainment", Image = "entertainment" },
                    new CategoryDTO { Name = "Miscellaneous", Image = "miscellaneous" },
                    new CategoryDTO { Name = "Home", Image = "home" },
                    new CategoryDTO { Name = "Salary", Image = "salary" },
                    new CategoryDTO { Name = "Savings", Image = "savings" }
                };
                allUsers.ForEach(user =>
                {
                    defaultCategories.ForEach(cat =>
                    {
                        Category newCategory = new() { Name = cat.Name, Image = cat.Image, BUser = user };
                        context.Categories.Add(newCategory);
                    });
                });
                context.SaveChanges();
            }

            //// Set Accounts
            //if (!context.Accounts.Any())
            //{
            //    var allUsers = context.BUsers.ToList();
            //    allUsers.ForEach(user =>
            //    {
            //        Account newAccount = new() { BUser = user, Balance = 0 };
            //        context.Accounts.Add(newAccount);
            //    });
            //    context.SaveChanges();
            //}

            // Set Savings
            if (!context.Savings.Any())
            {
                var allUsers = context.BUsers.ToList();
                var newSavings = new List<SavingDTO>
                {
                    new SavingDTO { Name = "Vacaciones", TargetAmount = 1200 },
                    new SavingDTO { Name = "Regalo", TargetAmount = 250 },
                };
                allUsers.ForEach(user =>
                {
                    newSavings.ForEach(sav =>
                    {
                        Saving saving = new() { Name = sav.Name, TargetAmount = sav.TargetAmount,
                            DueDate = DateTime.Now.AddMonths(1), BUser = user };
                        context.Savings.Add(saving);
                    });
                });
                context.SaveChanges();
            }


            // Set Budgets
            if (!context.Budgets.Any())
            {
                var allUsers = context.BUsers.Include(u => u.Categories).ToList();
                var newBudgets = new List<RequestBudgetDTO>
                {
                    new RequestBudgetDTO { LimitAmount = 400, Period = 1, Category = "Groceries" },
                    new RequestBudgetDTO { LimitAmount = 50, Period = 0, Category = "Entertainment"},
                };
                allUsers.ForEach(user =>
                {
                    newBudgets.ForEach(bud =>
                    {
                        var userCategory = user.Categories.First(e => e.Name == bud.Category);
                        Budget budget = new()
                        {
                            LimitAmount = (double)bud.LimitAmount,
                            //Period = Enum.TryParse<BudgetPeriod>(bud.Period, out BudgetPeriod parsed)?parsed:,
                            Period = (BudgetPeriod)(bud.Period),
                            BUser = user,
                            Category = userCategory
                        };
                        context.Budgets.Add(budget);
                    });
                });
                context.SaveChanges();
            }

            // Set Transactions
            if (!context.Transactions.Any())
            {
                var allUsers = context.BUsers.Include(u => u.Categories)
                    .Include(u => u.Account)
                    .Include(u => u.Savings)
                    .Include(u => u.Budgets)
                    .ThenInclude(b => b.Category)
                    .ToList();

                var newTransactions = new List<RequestTransactionDTO>
                {
                    new RequestTransactionDTO { Title = "Sueldo ", Amount = 1000, Type = 0, Category = "Salary" },
                    new RequestTransactionDTO { Title = "Ahorro ", Amount = 100, Type = 0, Category = "Savings", Saving = "Vacaciones" },

                    new RequestTransactionDTO { Title = "Compra 1", Amount = -8, Type = 1, Category = "Entertainment"},
                    new RequestTransactionDTO { Title = "Compra 2", Amount = -25, Type = 1, Category = "Entertainment"},
                    new RequestTransactionDTO { Title = "Compra 3", Amount = -15, Type = 1, Category = "Groceries"},
                    new RequestTransactionDTO { Title = "Compra 4", Amount = -50, Type = 1, Category = "Groceries"},
                };

                allUsers.ForEach(user =>
                {
                    newTransactions.ForEach(tr =>
                    {
                        var category = user.Categories.First(e => e.Name == tr.Category);
                        var saving = user.Savings.FirstOrDefault(e => e.Name == tr.Saving);
                        var budget = user.Budgets.FirstOrDefault(e => e.Category.Name == tr.Category);

                        Transaction transaction = new()
                        {
                            Title = tr.Title,
                            Amount = (double)tr.Amount,
                            CreateDate = DateTime.Now,
                            Type = (TransactionType)(tr.Type),
                            Category = category,
                            Account = user.Account,
                            Saving = saving,
                            Budget = budget
                        };
                        context.Transactions.Add(transaction);

                        if (budget != null)
                        {
                            budget.Balance = (double)(budget.Balance + (tr.Amount * -1));
                        }

                        if(saving != null)
                        {
                            saving.Balance = (double)(saving.Balance + tr.Amount);
                        }
                        else
                        {
                            user.Account.Balance = (double)(user.Account.Balance + tr.Amount);
                        }
                    });
                });
                context.SaveChanges();


            }
        }
    }
}
