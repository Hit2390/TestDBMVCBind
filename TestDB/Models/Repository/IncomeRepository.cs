using FinanceReport.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FinanceReport.Models.Repository
{
    public class IncomeRepository
    {
        #region Private Properties

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        #endregion

        public List<Income> GetAllIncomes()
        {
            var incomes = new List<Income>();

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("SELECT IncomeId, IncomeSource, IncomeAmount FROM income", conn);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incomes.Add(new Income
                            {
                                IncomeId = reader.GetInt32("IncomeId"),
                                IncomeSource = reader.GetString("IncomeSource"),
                                IncomeAmount = reader.GetDecimal("IncomeAmount")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving incomes.", ex);
            }

            return incomes;
        }

        public void AddIncome(Income income)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("INSERT INTO income (IncomeSource, IncomeAmount, IncomeDate) VALUES (@IncomeSource, @IncomeAmount, @IncomeDate)", conn);
                    cmd.Parameters.AddWithValue("@IncomeSource", income.IncomeSource);
                    cmd.Parameters.AddWithValue("@IncomeAmount", income.IncomeAmount);
                    cmd.Parameters.AddWithValue("@IncomeDate", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while adding income.", ex);
            }
        }

        public Income GetIncomeById(int id)
        {
            Income income = null;

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("SELECT IncomeId, IncomeSource, IncomeAmount FROM income WHERE IncomeId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            income = new Income
                            {
                                IncomeId = reader.GetInt32("IncomeId"),
                                IncomeSource = reader.GetString("IncomeSource"),
                                IncomeAmount = reader.GetDecimal("IncomeAmount")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving the income by ID.", ex);
            }

            return income;
        }

        public void UpdateIncome(Income income)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("UPDATE income SET IncomeSource = @IncomeSource, IncomeAmount = @IncomeAmount WHERE IncomeId = @IncomeId", conn);
                    cmd.Parameters.AddWithValue("@IncomeSource", income.IncomeSource);
                    cmd.Parameters.AddWithValue("@IncomeAmount", income.IncomeAmount);
                    cmd.Parameters.AddWithValue("@IncomeId", income.IncomeId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while updating the income.", ex);
            }
        }

        public void DeleteIncome(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    var cmd = new MySqlCommand("DELETE FROM income WHERE IncomeId = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while deleting the income.", ex);
            }
        }
    }
}
