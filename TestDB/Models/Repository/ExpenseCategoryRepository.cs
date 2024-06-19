using FinanceReport.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FinanceReport.Models.Repository
{
    public class ExpenseCategoryRepository
    {
        #region Private Properties

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        #endregion


        #region Public Methods


        public List<ExpenseCategory> GetAllExpenseCategories()
        {
            var expenseCategories = new List<ExpenseCategory>();

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("SELECT ExpenseCategoryId, ExpenseCategoryName FROM expensecategory", conn);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenseCategories.Add(new ExpenseCategory
                            {
                                ExpenseCategoryId = reader.GetInt32("ExpenseCategoryId"),
                                ExpenseCategoryName = reader.GetString("ExpenseCategoryName")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving expense categories.", ex);
            }

            return expenseCategories;
        }

        public void AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("INSERT INTO expensecategory (ExpenseCategoryName) VALUES (@ExpenseCategoryName)", conn);
                    cmd.Parameters.AddWithValue("@ExpenseCategoryName", expenseCategory.ExpenseCategoryName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while adding an expense category.", ex);
            }
        }

        public ExpenseCategory GetExpenseCategoryById(int id)
        {
            ExpenseCategory expenseCategory = null;

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("SELECT ExpenseCategoryId, ExpenseCategoryName FROM expensecategory WHERE ExpenseCategoryId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            expenseCategory = new ExpenseCategory
                            {
                                ExpenseCategoryId = reader.GetInt32("ExpenseCategoryId"),
                                ExpenseCategoryName = reader.GetString("ExpenseCategoryName")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving the expense category by ID.", ex);
            }

            return expenseCategory;
        }

        public void UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("UPDATE expensecategory SET ExpenseCategoryName = @ExpenseCategoryName WHERE ExpenseCategoryId = @ExpenseCategoryId", conn);
                    cmd.Parameters.AddWithValue("@ExpenseCategoryName", expenseCategory.ExpenseCategoryName);
                    cmd.Parameters.AddWithValue("@ExpenseCategoryId", expenseCategory.ExpenseCategoryId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while updating the expense category.", ex);
            }
        }

        public void DeleteExpenseCategory(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("DELETE FROM expensecategory WHERE ExpenseCategoryId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while deleting the expense category.", ex);
            }
        }

        #endregion
    }
}
