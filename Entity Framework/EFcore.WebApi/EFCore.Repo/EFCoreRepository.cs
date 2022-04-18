using System.Threading.Tasks;

namespace EFCore.Repo
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContexto _contexto;

        public EFCoreRepository(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }
    }
}
