using System;
using System.Collections.Generic;

namespace Calculator.Models.ViewModels
{
    public class SalaryArrearsResponse
    {
        public string StaffNumber { get; set; }
        public decimal DifferenceSum { get; set; }
        public DateTime Month { get; set; }
        public GetStructureresponse AnalysisResponse { get; set; }
        public int Grade { get; set; }
        public int Step { get; set; }
    }

   

}
