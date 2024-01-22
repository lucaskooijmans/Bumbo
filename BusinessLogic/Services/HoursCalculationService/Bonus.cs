using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.HoursCalculationService
{
    public class Bonus
    {
        public int startTime { get; private set; }
        public int endTime { get; private set; }
        public int bonusPercentage { get; private set; }

        public Bonus(int _startTime, int _endTime, int _bonusPercentage)
        {
            startTime = _startTime;
            endTime = _endTime;
            bonusPercentage = _bonusPercentage;
        }
    }
}
