using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.DTOs;
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

                var newUsers = new RegisterRequestDTO[]
                {
                new RegisterRequestDTO { FirstName="Sebastian", LastName="F", Email = "sf@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Tatiana", LastName="Q", Email = "tq@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Andres", LastName="R", Email = "ar@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Gonzalo", LastName="C", Email = "gc@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Gaston", LastName="R", Email = "gr@mail.com", Password = "Pass-123"},
                new RegisterRequestDTO { FirstName="Ignacio2", LastName="DiBella2", Email = "78871@sistemas.frc.utn.edu.ar", Password = "Pass-123"},
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


            // Set Categories
            if (!context.Categories.Any())
            {
                var allUsers = context.BUsers.ToList();
                var defaultCategories = new List<CategoryDTO>
                {
                    new CategoryDTO { Name = "Groceries", ImageUrl = "groceries" },
                    new CategoryDTO { Name = "Transportation", ImageUrl = "transportation" },
                    new CategoryDTO { Name = "Entertainment", ImageUrl = "entertainment" },
                    new CategoryDTO { Name = "Miscellaneous", ImageUrl = "miscellaneous" },
                    new CategoryDTO { Name = "Home", ImageUrl = "home" },
                    new CategoryDTO { Name = "Salary", ImageUrl = "salary" },
                    new CategoryDTO { Name = "Savings", ImageUrl = "savings" }
                };
                allUsers.ForEach(user =>
                {
                    defaultCategories.ForEach(cat =>
                    {
                        Category newCategory = new() { Name = cat.Name, ImageUrl = cat.ImageUrl, BUser = user };
                        context.Categories.Add(newCategory);
                    });
                });
                context.SaveChanges();
            }

            // Set Accounts
            if (!context.Accounts.Any())
            {
                var allUsers = context.BUsers.ToList();
                allUsers.ForEach(user =>
                {
                    Account newAccount = new() { BUser = user, Balance = 0 };
                    context.Accounts.Add(newAccount);
                });
                context.SaveChanges();
            }

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
                var newBudgets = new List<CreateBudgetDTO>
                {
                    new CreateBudgetDTO { LimitAmount = 400, Period = "Monthly", Category = "Groceries" },
                    new CreateBudgetDTO { LimitAmount = 50, Period = "Weekly", Category = "Entertainment"},
                };
                allUsers.ForEach(user =>
                {
                    newBudgets.ForEach(bud =>
                    {
                        var userCategory = user.Categories.First(e => e.Name == bud.Category);
                        Budget budget = new()
                        {
                            LimitAmount = bud.LimitAmount,
                            //Period = Enum.TryParse<BudgetPeriod>(bud.Period, out BudgetPeriod parsed)?parsed:,
                            Period = (BudgetPeriod)Enum.Parse(typeof(BudgetPeriod), bud.Period),
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

                var newTransactions = new List<CreateTransactionDTO>
                {
                    new CreateTransactionDTO { Title = "Sueldo ", Amount = 1000, Type = "INCOME", Category = "Salary" },
                    new CreateTransactionDTO { Title = "Ahorro ", Amount = 100, Type = "INCOME", Category = "Savings", Saving = "Vacaciones" },

                    new CreateTransactionDTO { Title = "Compra 1", Amount = -8, Type = "OUTCOME", Category = "Entertainment"},
                    new CreateTransactionDTO { Title = "Compra 2", Amount = -25, Type = "OUTCOME", Category = "Entertainment"},
                    new CreateTransactionDTO { Title = "Compra 3", Amount = -15, Type = "OUTCOME", Category = "Groceries"},
                    new CreateTransactionDTO { Title = "Compra 4", Amount = -50, Type = "OUTCOME", Category = "Groceries"},
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
                            Amount = tr.Amount,
                            CreateDate = DateTime.Now,
                            Type = (TransactionType)Enum.Parse(typeof(TransactionType), tr.Type),
                            Category = category,
                            Account = user.Account,
                            Saving = saving,
                            Budget = budget
                        };
                        context.Transactions.Add(transaction);

                        if (budget != null)
                        {
                            budget.Balance = budget.Balance + (tr.Amount * -1);
                        }

                        if(saving != null)
                        {
                            saving.Balance = saving.Balance + tr.Amount;
                        }
                        else
                        {
                            user.Account.Balance = user.Account.Balance + tr.Amount;
                        }
                    });
                });
                context.SaveChanges();


            }
        }
    }
}
