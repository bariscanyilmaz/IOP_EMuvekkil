using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface IMasrafRepository
    {
        Masraf GetMasraf(int id);
        
        void DeleteMasraf(int id);
        Masraf AddMasraf(Masraf masraf);
        IQueryable<Masraf> GetMasrafs(int davaId);
        IQueryable<Masraf> GetMasrafs();
    }
}