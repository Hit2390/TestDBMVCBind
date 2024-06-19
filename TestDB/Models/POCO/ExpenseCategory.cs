using System.ComponentModel.DataAnnotations;

namespace FinanceReport.Models.POCO
{
    public class ExpenseCategory
    {
        public int ExpenseCategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string ExpenseCategoryName { get; set; } = string.Empty;
    }
}