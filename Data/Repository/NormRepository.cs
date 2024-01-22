using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class NormRepository : INormRepository
{
    private readonly BumboContext _context;
    
    public NormRepository(BumboContext context)
    {
        _context = context;
    }

    public IEnumerable<Norm> GetAll()
    {
        return _context.Norms.ToList();
    }

    public bool Add(Norm norm)
    {
        _context.Add(norm);
        return Save();    
    }

    public bool Update(Norm norm)
    {
        _context.Update(norm);
        return Save();
    }
    
    
    public void UpdateRange(IEnumerable<Norm> norms)
    {
        foreach (var norm in norms)
        {
            _context.Entry(norm).State = EntityState.Modified;
        }

        _context.SaveChanges();
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0;
    }
}