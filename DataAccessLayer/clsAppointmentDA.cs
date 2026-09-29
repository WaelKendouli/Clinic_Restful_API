using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsAppointmentDA
    {
        public static async Task<int> AddNewAppointmentAsync(DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add Date parameter
                command.Parameters.AddWithValue("@Date", date);

                // Add Time parameter
                command.Parameters.AddWithValue("@Time", time);

                // Add DoctorID parameter
                command.Parameters.AddWithValue("@DoctorID", doctorID);

                // Add PatientID parameter
                command.Parameters.AddWithValue("@PatientID", patientID);

                // Add AppointmentStatusID parameter
                command.Parameters.AddWithValue("@AppointmentStatusID", appointmentStatusID);

                // Output parameter for the new Appointment ID
                var outputIdParam = new SqlParameter("@NewAppointmentID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                try
                {
                    // Execute the stored procedure asynchronously
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Return the newly generated Appointment ID
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
