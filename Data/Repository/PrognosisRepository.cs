using Data.Enums;
using Data.Interfaces;
using Data.Models;
using Utility.Extensions;

namespace Data.Repository;

public class PrognosisRepository : IPrognosisRepository
{
    private readonly BumboContext _context;
    
    private int branchId = 1;

    public PrognosisRepository(BumboContext context)
    {
        _context = context;
    }

    public IEnumerable<DailyPrognosis> GetWeeklyPrognosis(DateTime date)
    {
        DateTime startOfWeek = date.StartOfWeek();
        DateTime endOfWeek = date.EndOfWeek();
        
        var weeklyPrognosis =  _context.DailyPrognosis
            .Where(dp => dp.Date >= startOfWeek && dp.Date <= endOfWeek)
            .ToList();

        return weeklyPrognosis;
    }
    
    public IEnumerable<DailyExpectations> GetWeeklyExpectations(DateTime date)
    {
        DateTime startOfWeek = date.StartOfWeek();
        DateTime endOfWeek = date.EndOfWeek();
        
        var weeklyExpectations =  _context.DailyExpectations
            .Where(dp => dp.Date >= startOfWeek && dp.Date <= endOfWeek)
            .ToList();

        return weeklyExpectations;
    }
    
    public List<DailyPrognosis> GetDailyPrognosis(DateTime date)
    {
        // Get prognosis of the date given
        var dailyPrognosis = _context.DailyPrognosis
            .Where(dp => dp.Date.Day == date.Date.Day)
            .ToList();
        
        return dailyPrognosis;
    }
    
    public bool Add(DailyPrognosis prognosis)
    {
        return true;
    }
    
    public void GeneratePrognosis(DateTime date, IEnumerable<Norm> norms)
    {
        Departments[] departments = (Departments[])Enum.GetValues(typeof(Departments));
        IEnumerable<DailyExpectations> dailyExpectations = GetWeeklyExpectations(date);
        List<DailyPrognosis> dailyPrognoses = new List<DailyPrognosis>();

        Dictionary<NormTypes, int> normsValues = new Dictionary<NormTypes, int>();
        
        foreach (Norm norm in norms)
        {
            normsValues.Add(norm.Type, norm.Value);
        }
        
        foreach (DailyExpectations dailyExpectation in dailyExpectations)
        {   
            foreach (Departments department in departments)
            {
                DailyPrognosis dailyPrognosis = new DailyPrognosis();
                dailyPrognosis.Date = dailyExpectation.Date;
                dailyPrognosis.BranchId = 1;

                if (department == Departments.Checkout)
                {
                    normsValues.TryGetValue(NormTypes.Checkout, out int value);
                    

                    int uren = (int)Math.Round(((double)dailyExpectation.ExpectedCustomers / value));
                    dailyPrognosis.Department = Departments.Checkout;
                    dailyPrognosis.NumberOfHours = uren;
                }
                else if (department == Departments.Fresh)
                {
                    normsValues.TryGetValue(NormTypes.Fresh, out int value);
                    int uren = (int)Math.Round(((double)dailyExpectation.ExpectedCustomers / value));
                    dailyPrognosis.Department = Departments.Fresh;
                    dailyPrognosis.NumberOfHours = uren;
                }
                else if (department == Departments.Dkw)
                {
                    normsValues.TryGetValue(NormTypes.Unpack, out int unpack);
                    normsValues.TryGetValue(NormTypes.Stock, out int stock);

                    int uren = (int)((dailyExpectation.ExpectedColli * unpack +
                                      dailyExpectation.ExpectedColli * stock) / 60);
                    dailyPrognosis.Department = Departments.Dkw;
                    dailyPrognosis.NumberOfHours = uren;
                }
                
                dailyPrognoses.Add(dailyPrognosis);
            }
        }
        
        AddRange(dailyPrognoses);
    }


    public void AddRange(IEnumerable<DailyPrognosis> dailyPrognoses)
    {
         _context.DailyPrognosis.AddRange(dailyPrognoses);
         _context.SaveChanges();
    }

    public bool Update(IEnumerable<DailyPrognosis> prognosis)
    {
        _context.UpdateRange(prognosis);
        _context.SaveChanges();
        return true;
    }

    public List<DailyExpectations> GetLastEightWeeks(DateTime date)
    {
        DateTime endOfLastWeek = date.StartOfWeek().AddDays(-1);
        DateTime startOfPeriod = endOfLastWeek.AddDays(-55);

        var lastEightWeeks = _context.DailyExpectations
            .Where(dp => dp.Date >= startOfPeriod && dp.Date <= endOfLastWeek)
            .OrderBy(dp => dp.Date)
            .ToList();

        return lastEightWeeks;
    }

    public bool Save()
    {
        return true;
    }
    
    public void AddExpectations(List<DailyExpectations> Expectations)
    {
        _context.AddRange(Expectations);
        _context.SaveChanges();
    }

    public bool UpdateExpectations(IEnumerable<DailyExpectations> dailyExpectations)
    {
        _context.UpdateRange(dailyExpectations);
        _context.SaveChanges();
        return true;
    }
}
