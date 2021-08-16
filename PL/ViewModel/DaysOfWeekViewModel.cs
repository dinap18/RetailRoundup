using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
   public class DaysOfWeekViewModel
    {
        public IEnumerable<DayOfWeek> Data { get; set; }
        public DaysOfWeekViewModel()
        {
            Data= new List<DayOfWeek> { DayOfWeek.Sunday ,DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,
            DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday};
        }
    }
}
