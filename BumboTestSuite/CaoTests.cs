using BusinessLogic.Services.CaoService.Factories;
using BusinessLogic.Services.CaoService.Interfaces;
using BusinessLogic.Services.CaoService.Rules;
using Data.Enums;
using Data.Models;

namespace BumboTestSuite
{
    public class CaoTests
    {
        private CaoServiceFactory _caoServiceFactory;
        private ICaoService _generalCaoService;
        private ICaoService _minorSixteenSeventeenCaoService;
        private ICaoService _underSixteenCaoService;

        [SetUp]
        public void Setup()
        {
            // Initialize your factory for testing
            _caoServiceFactory = new CaoServiceFactory();
            _generalCaoService = new GeneralCaoService();
            _minorSixteenSeventeenCaoService = new MinorSixteenSeventeenCaoService();
            _underSixteenCaoService = new UnderSixteenCaoService();
        }

        [Test]
        public void GetCaoService_UnderSixteen_ShouldReturnUnderSixteenCaoService()
        {
            var dateOfBirth = DateTime.Now.AddYears(-15); // Assuming the employee is 15 years old
            var employee = new Employee
            {
                DateOfBirth = dateOfBirth, // Age below 16
            };

            // Act
            var result = _caoServiceFactory.GetCaoService(employee);

            // Assert
            Assert.IsInstanceOf<UnderSixteenCaoService>(result);
        }

        [Test]
        public void GetCaoService_SixteenToSeventeen_ShouldReturnMinorSixteenSeventeenCaoService()
        {
            // Arrange
            var employee = new Employee
            {
                DateOfBirth = DateTime.Now.AddYears(-16), // Age 16/17
            };

            // Act
            var result = _caoServiceFactory.GetCaoService(employee);

            // Assert
            Assert.IsInstanceOf<MinorSixteenSeventeenCaoService>(result);
        }

        [Test]
        public void GetCaoService_EighteenAndAbove_ShouldReturnGeneralCaoService()
        {
            // Arrange
            var employee = new Employee
            {
                DateOfBirth = DateTime.Now.AddYears(-25), //Age above 18
            };
            // Act
            var result = _caoServiceFactory.GetCaoService(employee);

            // Assert
            Assert.IsInstanceOf<GeneralCaoService>(result);
        }

        [Test]
        public void GeneralCaoService_ValidateShift_ShouldPass()
        {
            // Arrange
            var shift = new Shift
            {
                Start = new DateTime(2023, 12, 18, 8, 0, 0),
                End = new DateTime(2023, 12, 18, 17, 0, 0),
            };

            var employee = new Employee();
            employee.Shifts = new List<Shift>();

            // Act
            var result = _generalCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GeneralCaoService_ValidateShift_ShouldFail_MaxWeeklyHoursExceeded()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
            };

            var employee = new Employee();
            employee.Shifts = new List<Shift>
            {
                new Shift { Start = DateTime.Now, End = DateTime.Now.AddHours(60) }
            };

            // Act
            var result = _generalCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GeneralCaoService_ValidateShift_ShouldFail_MaxShiftDuration()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(13),
            };

            var employee = new Employee();

            employee.Shifts = new List<Shift>
            {
                new Shift { Start = DateTime.Now.AddDays(-3), End = DateTime.Now.AddDays(-3).AddHours(4) }
            };
            // Act
            var result = _generalCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void MinorSixteenSeventeenCaoService_ValidateShift_ShouldPass()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(8),
                DepartmentName = Departments.Checkout, // Assuming a department is specified
            };

            var employee = new Employee();

            // Act
            var result = _minorSixteenSeventeenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void MinorSixteenSeventeenCaoService_ValidateShift_ShouldFail_MaxShiftDuration()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(10),
                DepartmentName = Departments.Dkw, // Assuming a department is specified
            };

            var employee = new Employee();
            employee.Shifts = new List<Shift>
            {
                new Shift { Start = DateTime.Now.AddDays(-10), End = DateTime.Now.AddHours(40) }
            };

            // Act
            var result = _minorSixteenSeventeenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void MinorSixteenSeventeenCaoService_ValidateShift_ShouldFail_On_GeneralShiftDuration_MaxShiftDuration()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(14),
                DepartmentName = Departments.Dkw, // Assuming a department is specified
            };

            var employee = new Employee();
            employee.Shifts = new List<Shift>
            {
                new Shift { Start = DateTime.Now.AddDays(-10), End = DateTime.Now.AddHours(40) }
            };

            // Act
            var result = _minorSixteenSeventeenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void MinorSixteenSeventeenCaoService_ValidateShift_ShouldFail_AverageHoursExceeded()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                DepartmentName = Departments.Dkw, // Assuming a department is specified
            };

            var employee = new Employee();
            employee.Shifts = new List<Shift>
            {
                new Shift { Start = DateTime.Now.AddDays(-10), End = DateTime.Now.AddHours(8) },
                new Shift { Start = DateTime.Now.AddDays(-11), End = DateTime.Now.AddHours(8) },
                new Shift { Start = DateTime.Now.AddDays(-12), End = DateTime.Now.AddHours(8) },
                new Shift { Start = DateTime.Now.AddDays(-13), End = DateTime.Now.AddHours(8) },
                new Shift { Start = DateTime.Now.AddDays(-14), End = DateTime.Now.AddHours(8) },
                new Shift { Start = DateTime.Now.AddDays(-15), End = DateTime.Now.AddHours(8) },
                new Shift { Start = DateTime.Now.AddDays(-16), End = DateTime.Now.AddHours(8) },
            };

            // Act
            var result = _minorSixteenSeventeenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UnderSixteenCaoService_ValidateShift_ShouldPass()
        {
            // Arrange
            var shift = new Shift
            {
                Start = new DateTime(2023, 12, 18, 8, 0, 0),
                End = new DateTime(2023, 12, 18, 13, 0, 0),
            };

            var employee = new Employee();
            // Act
            var result = _underSixteenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void UnderSixteenCaoService_ValidateShift_ShouldFail_On_GeneralCaoRules_MaxShiftDuration()
        {
            // Arrange
            var shift = new Shift
            {
                Start = new DateTime(2023, 12, 18, 8, 0, 0),
                End = new DateTime(2023, 12, 18, 21, 0, 0),
            };

            var employee = new Employee();

            employee.SchoolHours = new List<SchoolHours>
            {
                new SchoolHours { DayOfWeek = WeekDays.Monday, Hours = 4 }
            };

            // Act
            var result = _underSixteenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UnderSixteenCaoService_ValidateShift_ShouldFail_MaxEightHoursIncludingSchool()
        {
            // Arrange
            var shift = new Shift
            {
                Start = new DateTime(2023, 12, 18, 8, 0, 0),
                End = new DateTime(2023, 12, 18, 13, 0, 0),
            };

            var employee = new Employee();

            employee.SchoolHours = new List<SchoolHours>
            {
                new SchoolHours { DayOfWeek = WeekDays.Monday, Hours = 4 }
            };

            // Act
            var result = _underSixteenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UnderSixteenCaoService_ValidateShift_ShouldFail_ShiftEndAfterSevenPm()
        {
            // Arrange
            var shift = new Shift
            {
                Start = new DateTime(2023, 1, 2, 17, 0, 0), // Monday, 2nd January 2023, 8:00 AM
                End = new DateTime(2023, 1, 2, 20, 0, 0), // Monday, 2nd January 2023, 1:00 PM
            };

            var employee = new Employee();

            // Act
            var result = _underSixteenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UnderSixteenCaoService_ValidateShift_ShouldFail_MaxDailyHoursExceeded()
        {
            // Arrange
            var shift = new Shift
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(10),
            };

            var employee = new Employee();
            employee.Shifts = new List<Shift>
            {
                new Shift { Start = DateTime.Now.AddDays(-3), End = DateTime.Now.AddDays(-3).AddHours(4) }
            };
            // Act
            var result = _underSixteenCaoService.ValidateShift(shift, employee);

            // Assert
            Assert.IsFalse(result);
        }
    }
}