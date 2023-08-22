using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class BUserService : IBUserService
    {
        private readonly IBUserRepository _buserRepository;

        public BUserService(IBUserRepository buserRepository)
        {
            _buserRepository = buserRepository;
        }
        public Result<List<BUserDTO>> GetAll()
        {
            var result = _buserRepository.GetAll();

            var usersDTO = new List<BUserDTO>();
            foreach (var user in result)
            {
                usersDTO.Add(new BUserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsPremium = user.IsPremium,

                });
            }

            return Result<List<BUserDTO>>.Success(usersDTO);
        }

        public Result<BUserDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Result<BUserDTO> CreateAccountForCurrentClient()
        {
            throw new NotImplementedException();
        }

        public Result<List<BUserDTO>> GetCurrentClientAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
