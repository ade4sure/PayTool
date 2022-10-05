using System.Collections.Generic;

namespace Calculator.Models.AppModels
{
    public partial class Grade
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<Step> Steps { get; set; }
        public int CStructureId { get; set; }
        public PaymentStructure CStructure { get; set; }
    }
}
