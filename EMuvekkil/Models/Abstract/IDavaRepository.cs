using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface IDavaRepository
    {
        Dava GetDava(int id);
        Dava UpdateDava(Dava dava);
        void DeleteDava(int id);
        void AddDava(Dava dava);
        IQueryable<Dava> GetDavas();
        
    }

}