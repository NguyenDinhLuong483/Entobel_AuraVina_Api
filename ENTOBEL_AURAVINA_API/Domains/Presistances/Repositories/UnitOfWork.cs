using ENTOBEL_AURAVINA_API.Domains.Repositories;

namespace ENTOBEL_AURAVINA_API.Domains.Presistances.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CompleteAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
