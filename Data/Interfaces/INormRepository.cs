using Data.Models;

namespace Data.Interfaces;

public interface INormRepository
{
    IEnumerable<Norm> GetAll();

    bool Add(Norm norm);

    bool Update(Norm norm);
    
    public void UpdateRange(IEnumerable<Norm> norms) { }
    
    bool Save();
}