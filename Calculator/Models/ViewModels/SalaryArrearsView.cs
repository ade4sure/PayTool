using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Calculator.Models.ViewModels
{
    public partial class SalaryArrearsView
    {
        public int CategoryId { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public DateTime Month { get; set; }
          

    }


}
