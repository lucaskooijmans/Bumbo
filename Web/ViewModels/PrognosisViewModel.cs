namespace Web.ViewModels;

using Data.Models;

public class PrognosisViewModel
{
    public IEnumerable<DailyPrognosis> Prognosis { get; set; }
    public IEnumerable<DailyExpectations> Expectations { get; set; }
    public IEnumerable<Norm> Norms { get; set; }
    public Dictionary<int, IEnumerable<DailyExpectations>> DailyExpectationsLastEightWeeks { get; set; }
    public int WeekNumber { get; set; }
    public int CurrentWeekNumber { get; set; }
    public DateTime Date { get; set; }   
    public string SelectedWeeks { get; set; }
}