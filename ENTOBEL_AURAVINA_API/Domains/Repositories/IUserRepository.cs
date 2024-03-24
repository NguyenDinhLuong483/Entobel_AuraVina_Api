using ENTOBEL_AURAVINA_API.Domains.Models;
using ENTOBEL_AURAVINA_API.Resources;
using Microsoft.AspNetCore.Mvc;

namespace ENTOBEL_AURAVINA_API.Domains.Repositories
{
    public interface IUserRepository
    {
        public User CreateUser(User area);
        public Task<List<User>> GetAllUserAsync();
    }
}
