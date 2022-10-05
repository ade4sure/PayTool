using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Models.ViewModels
{
    public partial class PayView
    {

        public int CategoryId { get; set; }
        public string StaffNumber { get; set; }
        public int Step { get; set; }
        public DateTime Dato { get; set; }
        public int Grade { get; set; }

    }


}
