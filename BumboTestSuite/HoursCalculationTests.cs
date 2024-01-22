using BusinessLogic.Services.HoursCalculationService;
using BusinessLogic.Services.HoursCalculationService.Factories;
using BusinessLogic.Services.HoursCalculationService.Interfaces;
using BusinessLogic.Services.HoursCalculationService.Policies;
using Data.Interfaces;
using Data.Models;

namespace BumboTestSuite;

public class HoursCalculationTests
{
    private HoursCalculationManager _hoursCalculationManager;
    private HoursPolicyFactory _hoursPolicyFactory;
    private IHourPolicy _saturdayHoursPolicy;
    private IHourPolicy _sundayHoursPolicy;
    private IHourPolicy _weekdayHoursPolicy;
    
    [SetUp]
    public void Setup()
    {
        // Initialize your factory for testing
        _hoursCalculationManager = new HoursCalculationManager(_hoursPolicyFactory);
        _hoursPolicyFactory = new HoursPolicyFactory();
        _saturdayHoursPolicy = new SaturdayHoursPolicy();
        _sundayHoursPolicy = new SundayHoursPolicy();
        _weekdayHoursPolicy = new WeekdayHoursPolicy();
    }
    
    [Test]
    public void HoursCalculationService_Shift_ShouldNotBeSplit()
    {
        // Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 8, 17, 00,00),
            End = new DateTime(2024, 1, 8, 21, 00,00),
        };

        // Act
        var result = _hoursCalculationManager.SplitShifts(shift);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void HoursCalculationService_Shift_ShouldBeSplit()
    {
        // Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 8, 17, 00,00),
            End = new DateTime(2024, 1, 9, 01, 00,00),
        };

        // Act
        var result = _hoursCalculationManager.SplitShifts(shift);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void GetHoursPolicy_Weekday_ShouldReturn_WeekdayHoursPolicy()
    {
        // Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 8, 17, 00,00),
            End = new DateTime(2024, 1, 8, 22, 00,00),
        };

        // Act
        var result = _hoursPolicyFactory.GetHourPolicy(shift);

        // Assert
        Assert.IsInstanceOf<WeekdayHoursPolicy>(result);
    }

    [Test]
    public void GetHoursPolicy_Saturday_ShouldReturn_SaturdayHoursPolicy()
    {
        // Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 13, 17, 00,00),
            End = new DateTime(2024, 1, 13, 22, 00,00),
        };
        // Act
        var result = _hoursPolicyFactory.GetHourPolicy(shift);

        // Assert
        Assert.IsInstanceOf<SaturdayHoursPolicy>(result);
    }
    
    
    [Test]
    public void GetHoursPolicy_Sunday_ShouldReturn_SundayHoursPolicy()
    {
        // Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 14, 17, 00,00),
            End = new DateTime(2024, 1, 14, 22, 00,00),
        };
        // Act
        var result = _hoursPolicyFactory.GetHourPolicy(shift);

        // Assert
        Assert.IsInstanceOf<SundayHoursPolicy>(result);
    }

    [Test]
    public void CalculateHours_WeekdayRoundHours_ShouldReturn_True()
    {
        //Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 10, 18, 00, 00),
            End = new DateTime(2024, 1, 11, 2, 00, 00),
        };

        var bonusHours = new Dictionary<int, double>
        {
            {0, 2.0 },
            {33, 1.0},
            {50, 5.0 }
        };

        //Act
        var result = _weekdayHoursPolicy.CalculateHours(shift);

        //Assert
        Assert.AreEqual(result, bonusHours);
    }

    [Test]
    public void CalculateHours_WeekdayHalfHours_ShouldReturn_True()
    {
        //Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 10, 18, 30, 00),
            End = new DateTime(2024, 1, 11, 2, 00, 00),
        };

        var bonusHours = new Dictionary<int, double>
        {
            {0, 1.5 },
            {33, 1.0},
            {50, 5.0 }
        };

        //Act
        var result = _weekdayHoursPolicy.CalculateHours(shift);

        //Assert
        Assert.AreEqual(result, bonusHours);
    }

    [Test]
    public void CalculateHours_SaturdayRoundHours_ShouldReturn_True()
    {
        //Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 13, 16, 00, 00),
            End = new DateTime(2024, 1, 13, 23, 00, 00),
        };

        var bonusHours = new Dictionary<int, double>
        {
            {0, 2.0 },
            {50, 5.0 }
        };

        //Act
        var result = _saturdayHoursPolicy.CalculateHours(shift);

        //Assert
        Assert.AreEqual(result, bonusHours);
    }

    [Test]
    public void CalculateHours_SaturdayHalfHours_ShouldReturn_True()
    {
        //Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 13, 16, 30, 00),
            End = new DateTime(2024, 1, 13, 23, 00, 00),
        };

        var bonusHours = new Dictionary<int, double>
        {
            {0, 1.5 },
            {50, 5.0 }
        };

        //Act
        var result = _saturdayHoursPolicy.CalculateHours(shift);

        //Assert
        Assert.AreEqual(result, bonusHours);
    }

    [Test]
    public void CalculateHours_SundayRoundHours_ShouldReturn_True()
    {
        //Arrange
        var shift = new Shift
        {
            Start = new DateTime(2024, 1, 14, 9, 00, 00),
            End = new DateTime(2024, 1, 14, 18, 00, 00),
        };

        var bonusHours = new Dictionary<int, double>
        {
            {50, 9.0 }
        };

        //Act
        var result = _sundayHoursPolicy.CalculateHours(shift);

        //Assert
        Assert.AreEqual(result, bonusHours);
    }



}