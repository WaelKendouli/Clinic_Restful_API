using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DataAccessLayer
{
    public class clsPrescriptionDA
    {
        public static async Task<int> AddNewPrescriptionAsync(int medicalRecordID, DateTime startDate, DateTime endDate)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewPrescription", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add MedicalRecordID parameter
                command.Parameters.AddWithValue("@MedicalRecordID", medicalRecordID);

                // Add StartDate parameter
                command.Parameters.AddWithValue("@StartDate", startDate);

                // Handle EndDate (nullable)
                if (endDate == DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@EndDate", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@EndDate", endDate);
                }

                // Output parameter for the new Prescription ID
                var outputIdParam = new SqlParameter("@NewPrescriptionID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                try
                {
                    // Execute the stored procedure asynchronously
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Return the newly generated Prescription ID
                    return (int)outputIdParam.Value;
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return -1; // Return -1 to indicate failure
                }
            }
        }
    }
}
