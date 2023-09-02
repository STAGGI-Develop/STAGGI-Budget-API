using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        void Save(Subscription subscription);
    }
}
