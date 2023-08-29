using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using System;

namespace STAGGI_Budget_API.Repositories
{
    public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(BudgetContext repositoryContext) : base(repositoryContext) { }
        public void Save(Subscription subscription)
        {
            if (subscription.Id == null)
            {
                Create(subscription);
            }
            else
            {
                Update(subscription);
            }
            SaveChanges();
        }
    }
}
