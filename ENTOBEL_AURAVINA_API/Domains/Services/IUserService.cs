using ENTOBEL_AURAVINA_API.Domains.Models;
using ENTOBEL_AURAVINA_API.Resources;

namespace ENTOBEL_AURAVINA_API.Domains.Services
{
    public interface IUserService
    {
        public Task<bool> CreateUserAccount(AddUserViewModel userViewModel);
        public Task<List<User>> GetAllUser();
    }
}
