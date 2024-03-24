namespace ENTOBEL_AURAVINA_API.Domains.Presistances.Repositories
{
    public class BaseRepository
    {
        public ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
    }
}
