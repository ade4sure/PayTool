namespace Calculator.Models.ViewModels
{
    public class GetStructureresponse
    {

        public string ApprovedStructure { get; set; }
        public decimal ApprovedStructureAmount { get; set; }
        public string OpStructure { get; set; }
        public decimal OpStructureAmount { get; set; }
        public decimal PayDiffrence
        {
            get { return ApprovedStructureAmount - OpStructureAmount; }

        }


    }
}
