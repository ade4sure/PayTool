using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Models.AppModels
{
    public class PaymentStructure
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public PayCategory Category { get; set; }
        public List<Grade> Grades { get; set; }
        public EffectiveDate EffectiveDate { get; set; } 

    }
}
