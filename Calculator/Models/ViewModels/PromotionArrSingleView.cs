using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Models.ViewModels
{
    

    public class PromotionArrManyView
    {
        public int CategoryId { get; set; } 
        public string staffNumber { get; set; } = string.Empty;
       
        public List<MonthRange> ranges { get; set; } = new List<MonthRange>();
        public decimal TotalMargin
        {
            get
            {
                if (ranges.Count == 0) return 0;
                
                return ranges.Sum(s => s.Margin);
            }

        }


    }

    public class MonthRange
    {
        public DateTime StartMonth
        {
            get
            {
                DateTime.TryParse(StartMonthStr, out DateTime result);
                return result;
            }

        }
        public string StartMonthStr { get; set; }
        public GradStep Paid { get; set; }
        public GradStep Expected { get; set; }
        public string EndMonthStr { get; set; }
        public DateTime EndMonth
        {
            get
            {
                DateTime.TryParse(EndMonthStr, out DateTime result);
                return result;
            }

        }
        public decimal Margin
        {
            get
            {
                var diff = Expected.Amount - Paid.Amount;

                //var dif = viewModel.ranges[0].StartMonth.Subtract(viewModel.ranges[0].EndMonth);
                var monts = (((EndMonth.Year - StartMonth.Year) * 12) + EndMonth.Month - StartMonth.Month) + 1; //add one extra

                return diff * monts;
            }

        }
    }

   
    public class GradStep
    {        
        public int Grade { get; set; }
        public int Step { get; set; }
        public decimal Amount { get; set; }
    }
}
