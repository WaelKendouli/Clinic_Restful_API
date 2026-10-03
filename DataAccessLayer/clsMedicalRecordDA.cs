using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsMedicalRecordDA
    {
        public static async Task<int> AddNewMedicalRecordAsync(string description, string diagnosis, string additionalNotes, int doctorID, int patientID, int appointmentID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewMedicalRecord", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add Description parameter
                command.Parameters.AddWithValue("@Description", description);

                // Add Diagnosis parameter
                command.Parameters.AddWithValue("@Diagnosis", diagnosis);

                // Handle AdditionalNotes (nullable)
                if (string.IsNullOrEmpty(additionalNotes))
                {
                    command.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@AdditionalNotes", additionalNotes);
                }

                // Add DoctorID parameter
                command.Parameters.AddWithValue("@DoctorID", doctorID);

                // Add PatientID parameter
                command.Parameters.AddWithValue("@PatientID", patientID);

                // Add AppointmentID parameter
                command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                // Output parameter for the new MedicalRecord ID
                var outputIdParam = new SqlParameter("@NewMedicalRecordID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                try
                {
                    // Execute the stored procedure asynchronously
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Return the newly generated MedicalRecord ID
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
