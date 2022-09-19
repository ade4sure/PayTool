using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;

namespace Calculator.Models.ViewModels
{


    public class PromotionArr
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string staffNumber { get; set; } = string.Empty;
        public string staffName { get; set; } = string.Empty;
        public string staffStatus { get; set; } = string.Empty;
        public string staffUnit { get; set; } = string.Empty;
        [NotMapped]
        public Extras Extras { get; set; }
        [NotMapped]
        public List<MonthRange> Ranges { get; set; } = new List<MonthRange>();
        public decimal TotalMargin
        {
            get
            {
                if (Ranges.Count == 0) return 0;

                return Ranges.Sum(s => s.RangeAmount);
            }

        }
        public string ExtraJson { get; set; } = JsonSerializer.Serialize(new Extras());
        public string RangeJson { get; set; } = JsonSerializer.Serialize(new List<MonthRange>());
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
        public int NumberOfMonths
        {
            get
            {

                return (((EndMonth.Year - StartMonth.Year) * 12) + EndMonth.Month - StartMonth.Month) + 1;
            }

        }
        public decimal RangeAmount
        {
            get
            {

                return PayMargin * NumberOfMonths;
            }

        }
        public decimal PayMargin
        {
            get
            {
                return Expected.Amount - Paid.Amount;
            }

        }
    }


    public class GradStep
    {
        public int Grade { get; set; }
        public int Step { get; set; }
        public decimal Amount { get; set; }
    }
    public class Extras
    {
        public bool IsProffesional { get; set; } = false;
        public bool IsCallDutyNurse { get; set; } = false;
        public bool IsCallDutyOthers { get; set; } = false;
        public bool IsCallDutyASUU { get; set; } = false;
        public bool IsShiftDutyNurse { get; set; } = false;
        public bool IsShiftDutyOthers { get; set; } = false;
    }

   
    
}
