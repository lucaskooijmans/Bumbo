using Data.Models;

namespace Data.Interfaces;

public interface IPrognosisRepository
{
    IEnumerable<DailyPrognosis> GetWeeklyPrognosis(DateTime date);
    IEnumerable<DailyExpectations> GetWeeklyExpectations(DateTime date);
    bool Add(DailyPrognosis prognosis);
    void GeneratePrognosis(DateTime date, IEnumerable<Norm> norms);
    void AddRange(IEnumerable<DailyPrognosis> dailyPrognoses);
    List<DailyExpectations> GetLastEightWeeks(DateTime date);
    void AddExpectations(List<DailyExpectations> Expectations);
    bool Update(IEnumerable<DailyPrognosis> prognosis);
    bool UpdateExpectations(IEnumerable<DailyExpectations> dailyExpectations);
    bool Save();
    List<DailyPrognosis> GetDailyPrognosis(DateTime date);
}