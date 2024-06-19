using FinanceReport.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FinanceReport.Models.Repository
{
    public class ExpenseRepository
    {
        #region Private Properties

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        #endregion

        public List<Expense> GetAllExpenses()
        {
            var expenses = new List<Expense>();

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand(
                        "SELECT e.ExpenseId, e.ExpenseName, e.ExpenseAmount, ec.ExpenseCategoryName FROM expense AS e INNER JOIN expensecategory AS ec ON e.ExpenseCategoryId = ec.ExpenseCategoryId", conn);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(new Expense
                            {
                                ExpenseId = reader.GetInt32("ExpenseId"),
                                ExpenseName = reader.GetString("ExpenseName"),
                                ExpenseAmount = reader.GetDecimal("ExpenseAmount"),
                                ExpenseCategoryName = reader.GetString("ExpenseCategoryName")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving expenses.", ex);
            }

            return expenses;
        }

        public void AddExpense(Expense expense)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("INSERT INTO expense (ExpenseName, ExpenseAmount, ExpenseDate, ExpenseCategoryId) VALUES (@ExpenseName, @ExpenseAmount, @ExpenseDate, @ExpenseCategoryId)", conn);
                    cmd.Parameters.AddWithValue("@ExpenseName", expense.ExpenseName);
                    cmd.Parameters.AddWithValue("@ExpenseAmount", expense.ExpenseAmount);
                    cmd.Parameters.AddWithValue("@ExpenseDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ExpenseCategoryId", expense.ExpenseCategoryId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while adding an expense.", ex);
            }
        }

        public Expense GetExpenseById(int id)
        {
            Expense expense = null;

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand(
                        "SELECT e.ExpenseId, e.ExpenseName, e.ExpenseAmount, e.ExpenseCategoryId, ec.ExpenseCategoryName " +
                        "FROM expense e " +
                        "JOIN expensecategory ec ON e.ExpenseCategoryId = ec.ExpenseCategoryId " +
                        "WHERE e.ExpenseId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            expense = new Expense
                            {
                                ExpenseId = reader.GetInt32("ExpenseId"),
                                ExpenseName = reader.GetString("ExpenseName"),
                                ExpenseAmount = reader.GetDecimal("ExpenseAmount"),
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
                throw new Exception("An error occurred while retrieving the expense by ID.", ex);
            }

            return expense;
        }

        public void UpdateExpense(Expense expense)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("UPDATE expense SET ExpenseName = @ExpenseName, ExpenseAmount = @ExpenseAmount, ExpenseCategoryId = @ExpenseCategoryId WHERE ExpenseId = @ExpenseId", conn);
                    cmd.Parameters.AddWithValue("@ExpenseName", expense.ExpenseName);
                    cmd.Parameters.AddWithValue("@ExpenseAmount", expense.ExpenseAmount);
                    cmd.Parameters.AddWithValue("@ExpenseCategoryId", expense.ExpenseCategoryId);
                    cmd.Parameters.AddWithValue("@ExpenseId", expense.ExpenseId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while updating the expense.", ex);
            }
        }

        public void DeleteExpense(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("DELETE FROM expense WHERE ExpenseId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while deleting the expense.", ex);
            }
        }
    }
}
