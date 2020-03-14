using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{
    public class EfDavaRepository : IDavaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EfDavaRepository(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void AddDava(Dava dava)
        {   
            
            _dbContext.Davas.Add(dava);
            _dbContext.SaveChanges();
        }

        public void DeleteDava(int id)
        {
            var dava=_dbContext.Davas.FirstOrDefault(d=>d.Id==id);
            if (dava!=null)
            {   
                _dbContext.Davas.Remove(dava);
                _dbContext.SaveChanges();
            }
        }

        public Dava GetDava(int id)
        {
            return _dbContext.Davas.Where(d=>d.Id==id).Include(d=>d.Avukat).Include(d=>d.Muvekkil).Include(d=>d.Masrafs).Include(d=>d.DavaState).First();
        }

        public IQueryable<Dava> GetDavas()
        {
            return _dbContext.Davas.Include(d=>d.Avukat).Include(d=>d.Muvekkil).Include(d=>d.Masrafs).Include(d=>d.DavaState);
        }

        public Dava UpdateDava(Dava dava)
        {
            var updateDava=_dbContext.Davas.SingleOrDefault(d=>d.Id==dava.Id);
            if (updateDava!=null)
            {
                updateDava.Avukat=dava.Avukat;
                updateDava.Muvekkil=dava.Muvekkil;
                updateDava.Name=dava.Name;
                updateDava.DavaState=dava.DavaState;
                _dbContext.SaveChanges();               

            }

            return updateDava;

        }
    }
}