using System;
using System.Collections.Generic;
using System.Text;

namespace LogStore
{
    public class RunningTotal
    {
        public DateTime Day { get; set; }
        public decimal Count { get; set; }

        public RunningTotal Update(DateTimeOffset time, decimal value)
        {
            var normalizedTime = time.ToUniversalTime();
            var newDay = new DateTime(normalizedTime.Year, normalizedTime.Month, normalizedTime.Day);
            
            var result = newDay > Day && Day != DateTime.MinValue ? 
                new RunningTotal
                {
                    Day=Day,
                    Count=Count
                } 
                : null;
            if(newDay > Day) Day = newDay;
            if (result != null) Count = value;
            else Count += value;
            return result;
        }
    }
}
