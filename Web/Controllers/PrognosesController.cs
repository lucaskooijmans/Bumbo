using System.Globalization;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Extensions;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize]
public class PrognosesController : Controller
{
    private readonly INormRepository _normRepository;
    private readonly IPrognosisRepository _prognosisRepository;

    public PrognosesController(IPrognosisRepository prognosisRepository, INormRepository normRepository)
    {
        _prognosisRepository = prognosisRepository;
        _normRepository = normRepository;
    }

    [HttpGet]
    public IActionResult Index(DateTime date)
    {
        var lastEightWeeks =  LastEightWeeks(date);

        PrognosisViewModel prognosisViewModel = new PrognosisViewModel
        {
            Prognosis = _prognosisRepository.GetWeeklyPrognosis(date),
            Norms =  _normRepository.GetAll(),
            Expectations = _prognosisRepository.GetWeeklyExpectations(date),
            WeekNumber = date.Week(),
            CurrentWeekNumber = DateTime.Now.Week(),
            DailyExpectationsLastEightWeeks =
                lastEightWeeks.ToDictionary(group => group.Key, group => group.AsEnumerable()),
            Date = date
        };

        return View(prognosisViewModel);
    }

    private IEnumerable<IGrouping<int, DailyExpectations>> LastEightWeeks(DateTime date)
    {
        List<DailyExpectations> lastEightWeeks = _prognosisRepository.GetLastEightWeeks(date);

        var groupedExpectations = lastEightWeeks.GroupBy(expectation =>
        {
         
            int weekNumber = expectation.Date.Week();
            return weekNumber;
        });

        return groupedExpectations;
    }


    [HttpGet]
    public IActionResult Create(DateTime date, PrognosisViewModel prognosisViewModel)
    {
        var dailyExpectationsList = DailyExpectationsList(prognosisViewModel);

        DateTime dateUsed = DateTime.Now;

        if (date >= dateUsed)
        {
            dateUsed = date;
        }
        else
        {
            dateUsed = prognosisViewModel.Date;
        }
        
        var prognosisCreateViewModel = new PrognosisEditCreateViewModel
        {
            WeekNumber = dateUsed.Week(),
            Expectations = dailyExpectationsList,
            Date = dateUsed.StartOfWeek(),
            Norms = _normRepository.GetAll()
        }; 

        return View(prognosisCreateViewModel);
    }

    private List<DailyExpectations> DailyExpectationsList(PrognosisViewModel prognosisViewModel)
    {
        string[] selectedWeeks;
        int[] selectedWeeksInt = new int[0];
        if (prognosisViewModel.SelectedWeeks != null)
        {
            selectedWeeks = prognosisViewModel.SelectedWeeks.Split(",");

            selectedWeeksInt = new int[selectedWeeks.Length];

            for (int i = 0; i < selectedWeeks.Length; i++)
            {
                if (int.TryParse(selectedWeeks[i], out int weekValue))
                {
                    selectedWeeksInt[i] = weekValue;
                }
            }
        }

        List<DailyExpectations> dailyExpectationsList = new List<DailyExpectations>();

        if (selectedWeeksInt.Length == 0)
        {
            for (int i = 0; i < 7; i++)
            {
                dailyExpectationsList.Add(new DailyExpectations { ExpectedColli = null, ExpectedCustomers = null });
            }
        }
        else
        {
            var groupedExpectations =  LastEightWeeks(prognosisViewModel.Date);
            
            var weeksToAverage = groupedExpectations.Where(group => selectedWeeksInt.Contains(group.Key));

            var flattenedData = weeksToAverage.SelectMany(group => group);

            var summedData = flattenedData
                .GroupBy(data => data.Date.DayOfWeek)
                .Select(group =>
                {
                    var dayOfWeek = group.Key;
                    var totalExpectedColli = group.Average(data => data.ExpectedColli) ?? 0;
                    var totalExpectedCustomers = group.Average(data => data.ExpectedCustomers) ?? 0;

                    return new
                    {
                        DayOfWeek = dayOfWeek,
                        TotalExpectedColli = totalExpectedColli,
                        TotalExpectedCustomers = totalExpectedCustomers
                    };
                })
                .ToList();

            summedData.ForEach(item =>
            {
                dailyExpectationsList.Add(new DailyExpectations
                {
                    ExpectedColli = (int)item.TotalExpectedColli,
                    ExpectedCustomers = (int)item.TotalExpectedCustomers
                });
            });
        }

        return dailyExpectationsList;
    }

    [HttpPost]
    public IActionResult Create(PrognosisEditCreateViewModel prognosisEditCreateViewModel)
    {
        _prognosisRepository.AddExpectations(prognosisEditCreateViewModel.Expectations);

        _prognosisRepository.GeneratePrognosis(prognosisEditCreateViewModel.Date, _normRepository.GetAll());

        return RedirectToAction("Index", "ManagerDashboard");
    }

    [HttpGet]
    public IActionResult Edit(DateTime date)
    {
        var expectations =  _prognosisRepository.GetWeeklyExpectations(date);
        var dailyPrognoses =  _prognosisRepository.GetWeeklyPrognosis(date);

        var prognosisCreateViewModel = new PrognosisEditCreateViewModel
        {
            WeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday
            ),
            Prognosis = dailyPrognoses.ToList(),
            Expectations = expectations.ToList(),
            Date = date.StartOfWeek(),
            Norms = _normRepository.GetAll()
        };

        return View(prognosisCreateViewModel);
    }

    [HttpPost]
    public IActionResult Edit(PrognosisEditCreateViewModel prognosisEditCreateViewModel)
    {
        _prognosisRepository.UpdateExpectations(prognosisEditCreateViewModel.Expectations);
        return View(prognosisEditCreateViewModel);
    }


    [HttpGet]
    public IActionResult EditPrognosis(DateTime date)
    {
        var prognosis = _prognosisRepository.GetWeeklyPrognosis(date);

        var prognosisCreateViewModel = new PrognosisEditCreateViewModel
        {
            WeekNumber = date.Week(),
            Prognosis = prognosis.ToList(),
            Date = date.StartOfWeek(),
            Norms = _normRepository.GetAll()
        };

        return View(prognosisCreateViewModel);
    }


    [HttpPost]
    public IActionResult EditPrognosis(PrognosisEditCreateViewModel prognosisEditCreateViewModel)
    {
        _prognosisRepository.Update(prognosisEditCreateViewModel.Prognosis);
        return View(prognosisEditCreateViewModel);
    }
}