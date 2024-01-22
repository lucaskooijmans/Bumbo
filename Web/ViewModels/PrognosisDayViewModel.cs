namespace Web.ViewModels;

using Data.Models;

public class PrognosisDayViewModel
{
    public IEnumerable<DailyPrognosis> Prognosis { get; set; }
    public IEnumerable<DailyExpectations> Expectations { get; set; }
    public Norm Norm { get; set; }
    public DateTime Date { get; set; }   
}
