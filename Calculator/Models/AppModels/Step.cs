using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Models.AppModels
{
    public class Step
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }

    }
  
}
