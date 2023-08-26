using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using STAGGI_Budget_API.Models;
using System.Text;

namespace STAGGI_Budget_API.Services
{
    public class BUserService : IBUserService
    {
        private readonly IBUserRepository _buserRepository;
        private readonly IAuthService _authService;
        private readonly UserManager<BUser> _userManager;

        public BUserService(IBUserRepository buserRepository, IAuthService authService, UserManager<BUser> userManager)
        {
            _buserRepository = buserRepository;
            _authService = authService;
            _userManager = userManager;
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

        public void RecoverPassword(string email) //async Task
        {
            var bUser = _buserRepository.FindByEmail(email);
            if (bUser == null)
            {
                throw new ApplicationException("no se encontró el usuario"); //TODO - Corregir
            }

            string recipient = email;
            string subject = "Renovación de Contraseña Perdida";

            string newPassword = GenerateRandomPassword();

            _userManager.RemovePasswordAsync(bUser); //await
            _userManager.AddPasswordAsync(bUser, newPassword); //await


            string body = "La nueva contraseña es: " + newPassword;

            _authService.ForgotPasswordEmailSender(recipient, subject, body);

        }

        public string GenerateRandomPassword()
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            int passwordLength = 12; // Longitud de la contraseña
            StringBuilder password = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = random.Next(allowedChars.Length);
                password.Append(allowedChars[randomIndex]);
            }

            return password.ToString();
        }

    }
}
