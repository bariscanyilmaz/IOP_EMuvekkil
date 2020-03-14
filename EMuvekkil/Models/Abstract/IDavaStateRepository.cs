

using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    
    public interface IDavaStateRepository
    {
        DavaState GetDavaState(int id);

        DavaState GetNewDavaState();

        IQueryable<DavaState> GetDavaStates();
    }

}