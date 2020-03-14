using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{

    public class EfMasrafRepository : IMasrafRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EfMasrafRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Masraf AddMasraf(Masraf masraf)
        {
            _dbContext.Masrafs.Add(masraf);
            _dbContext.SaveChanges();
            return masraf;
        }

        public void DeleteMasraf(int id)
        {
            var masraf = _dbContext.Masrafs.SingleOrDefault(m => m.Id == id);
            if (masraf != null)
            {
                _dbContext.Masrafs.Remove(masraf);
                _dbContext.SaveChanges();
            }
        }

        public Masraf GetMasraf(int id)
        {
            return _dbContext.Masrafs.Include(m=>m.Owner).Include(m=>m.Dava).Where(m=>m.Id==id).First();
        }

        public IQueryable<Masraf> GetMasrafs(int davaId)
        {
            return _dbContext.Masrafs.Where(m=>m.DavaId==davaId).Include(m=>m.Owner).Include(m=>m.Dava);
        }

        public IQueryable<Masraf> GetMasrafs()
        {
            return _dbContext.Masrafs.Include(m=>m.Owner).Include(m=>m.Dava);
        }

    }
}