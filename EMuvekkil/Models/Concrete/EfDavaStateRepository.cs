using System.Linq;
using EMuvekkil.Models.Abstract;

namespace EMuvekkil.Models.Concrete
{
    public class EfDavaStateRepository : IDavaStateRepository
    {

        private ApplicationDbContext _dbContext;
        public EfDavaStateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DavaState GetDavaState(int id)
        {
            return _dbContext.DavaStates.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<DavaState> GetDavaStates()
        {
            return _dbContext.DavaStates;
        }

        public DavaState GetNewDavaState()
        {
            return _dbContext.DavaStates.FirstOrDefault(s=>s.Id==1);
        }

                
    }
}