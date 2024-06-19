using FinanceReport.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FinanceReport.Models.Repository
{
    public class ReportRepository
    {
        #region Private Properties

        // Connection string to connect to the MySQL database
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        #endregion

        // Method to retrieve the net report from the database
        public List<Report> GetAllReports()
        {
            // List to hold the retrieved report
            var reports = new List<Report>();
            try
            {

            // Using statement ensures that the connection is properly closed and disposed
            using (var conn = new MySqlConnection(_connectionString))
            {
                // SQL command to calculate the net amount
                var cmd = new MySqlCommand(
                    "SELECT (SELECT SUM(incomeAmount) FROM income) - (SELECT SUM(expenseAmount) FROM expense) AS NetAmount", conn);

                // Open the database connection
                conn.Open();

                // Execute the command and use a data reader to read the results
                using (var reader = cmd.ExecuteReader())
                {
                    // Read the result and add it to the reports list
                    if (reader.Read())
                    {
                        reports.Add(new Report
                        {
                            NetAmount = reader.GetDecimal("NetAmount")
                        });
                    }
                }
            }
            }
            catch(Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving incomes.", ex);
            }

            // Return the list of reports
            return reports;
        }


       
    }
}
