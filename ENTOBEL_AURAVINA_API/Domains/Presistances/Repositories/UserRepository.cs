using ENTOBEL_AURAVINA_API.Domains.Models;
using ENTOBEL_AURAVINA_API.Domains.Repositories;
using ENTOBEL_AURAVINA_API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace ENTOBEL_AURAVINA_API.Domains.Presistances.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User CreateUser(User area)
        {
            return _context.Add(area).Entity;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
