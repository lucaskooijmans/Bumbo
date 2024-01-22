using Data.Models;

namespace Web.ViewModels;

public class PrognosisEditCreateViewModel
{
    public int WeekNumber { get; set; }
    public DateTime Date { get; set; }
    
    public List<DailyPrognosis> Prognosis { get; set; }
    
    public List<DailyExpectations> Expectations { get; set; }
    public IEnumerable<Norm> Norms { get; set; }
}