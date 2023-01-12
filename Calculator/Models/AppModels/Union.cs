using System.ComponentModel.DataAnnotations;

namespace Calculator.Models.AppModels
{
    public partial class Union
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}