using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceReport.Models.POCO
{
    public class Income
    {
        public int IncomeId { get; set; }

        [Required(ErrorMessage = "Income source is required")]
        public string IncomeSource { get; set; } = string.Empty;


        [Required(ErrorMessage = "Income amount is required")]
        public Decimal IncomeAmount { get; set;}
    }
}