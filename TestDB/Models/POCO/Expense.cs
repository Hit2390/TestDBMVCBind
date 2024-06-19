using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceReport.Models.POCO
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "Expense name is required")]
        public string ExpenseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Expense amount is required")]
        public Decimal ExpenseAmount { get; set; }

        public int ExpenseCategoryId { get; set; }

        public string ExpenseCategoryName { get; set; } = string.Empty; // New property
    }
}
