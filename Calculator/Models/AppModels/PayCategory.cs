using System.Collections.Generic;

namespace Calculator.Models.AppModels
{
    public class PayCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PaymentStructure> PayStructures { get; set; }
    }
}