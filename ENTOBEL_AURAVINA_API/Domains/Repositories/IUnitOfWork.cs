namespace ENTOBEL_AURAVINA_API.Domains.Repositories
{
    public interface IUnitOfWork
    {
        public Task<bool> CompleteAsync();
    }
}
