using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace STAGGI_Budget_API.Models
{
    public class BUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsPremium { get; set; } = false;


        //With MS doc for One to One
        public Account Account { get; set; } = new Account();
        public ICollection<Budget> Budgets { get; } = new List<Budget>();
        public ICollection<Category> Categories { get; } = new List<Category>();
        public ICollection<Saving> Savings { get; } = new List<Saving>();
        public Subscription Subscription { get; set; } = new Subscription();



        // // With DataAnotation
        //public ICollection<Category> Categories { get; set; }
        //public ICollection<Saving> Savings { get; set; }
        //public ICollection<Budget> Budgets { get; set; }
        //public Subscription? Subscription { get; set; }
        //public Account Account { get; set; }
    }
}
