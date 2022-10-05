using System;

namespace Calculator.Models.AppModels
{
    public partial class EffectiveDate
    {
        public int Id { get; set; }
        public DateTime ApprovedStartDate { get; set; }
        public DateTime OperationStartDate { get; set; }
        public DateTime OperationEndDate { get; set; }
        public DateTime ApprovedEndDate { get; set; }
        public int CStructureId { get; set; }
        public PaymentStructure CStructure { get; set; }

    }
}
