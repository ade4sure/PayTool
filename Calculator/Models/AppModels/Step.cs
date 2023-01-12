using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator.Models.AppModels
{
    public partial class Step
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CallDutyNurses { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CallDutyOthers { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CallDutyASUU { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ShiftDutyNurses { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ShiftDutyOthers { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProffesionalAllowance { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Contiss { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ContissRent { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Conuss { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Hazard { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Conuass { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ConuassRent { get; set; } = decimal.Zero;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Conpuaa { get; set; } = decimal.Zero;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProffNaat
        {
            get
            {
                decimal perCent = 2;
                return decimal.Round(((perCent / 100) * Contiss), 2);
                //return (perCent / 100) * Contiss;
            }
        }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProffSsanu
        {
            get
            {
                decimal perCent = 0;

                if (Grade.Number == 6 || Grade.Number == 7)
                {
                    perCent = 3;
                }
                else if (Grade.Number == 8)
                {
                    perCent = 2.5M;
                }
                else if (Grade.Number >= 9 && Grade.Number <= 12)
                {
                    perCent = 2;
                }
                else if (Grade.Number >= 13 && Grade.Number <= 15)
                {
                    perCent = 1.5M;
                }

                return perCent > 0 ? decimal.Round(((perCent / 100) * Contiss), 2) : 0;
            }
        }

        public decimal ShiftDutyOthersComputed
        {
            get
            {
                return Math.Round((decimal)(3.5 / 100) * Contiss, 2);
            }
        }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal NonTeachingAmount
        {
            get
            {
                return Contiss + ContissRent + Conuss + Hazard;
            }
        }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TeachingAmount
        {
            get
            {
                return Conuass + ConuassRent + Conpuaa + Hazard;
            }
        }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

    }

}
