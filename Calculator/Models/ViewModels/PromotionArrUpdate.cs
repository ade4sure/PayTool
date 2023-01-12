using Calculator.Models.AppModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Models.ViewModels
{
    
    public partial class PromotionArr
    {
        public int? UnionId { get; set; }
        public Union Union { get; set; }
    }


    public partial class PromotionArrReportDto
    {        
        public string Category { get; set; }
        public string StaffNumber { get; set; } = string.Empty;
        public string StaffName { get; set; } = string.Empty;
        public string StaffStatus { get; set; } = string.Empty;
        public string StaffUnit { get; set; } = string.Empty;
        public string LastPaidGradeStep { get; set; } = string.Empty;
        public string LastExpectedGradeStep { get; set; } = string.Empty;
        public DateTime LastEndMonth { get; set; } = DateTime.MinValue;
        public decimal Variance { get; set; } = 0;
        
       
    }

}
