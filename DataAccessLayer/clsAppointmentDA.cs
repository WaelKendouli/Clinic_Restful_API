using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsLayer;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsAppointmentDA
    {
        public static async Task<int> AddNewAppointmentAsync(AppointmentDTO appointment)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Map properties from AppointmentDTO
                command.Parameters.AddWithValue("@Date", appointment.Date);
                command.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                command.Parameters.AddWithValue("@PatientID", appointment.PatientID);
                command.Parameters.AddWithValue("@AppointmentStatusID", appointment.AppointmentStatusID);

                // Map new properties from your updated constructor
                command.Parameters.AddWithValue("@IssueDate", appointment.IssueDate);
                command.Parameters.AddWithValue("@StartTime", appointment.StartTime);
                command.Parameters.AddWithValue("@EndTime", appointment.EndTime);

                // Map additional parameter required by the SP
                command.Parameters.AddWithValue("@ServiceID", appointment.ServiceID);

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
        public static async Task<AppointmentDTO> FindAppointmentByIDAsync(int appointmentID)
        {
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("FindAppointmentByID", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Safely handle nullable fields
                            var IssueDateOrdinal = reader.GetOrdinal("IssueDate");
                            DateTime issueDate = reader.IsDBNull(IssueDateOrdinal) ? DateTime.MinValue : reader.GetDateTime(IssueDateOrdinal);

                            var StartTimeOrdinal = reader.GetOrdinal("StartTime");
                            TimeSpan startTime = reader.IsDBNull(StartTimeOrdinal) ? TimeSpan.Zero : reader.GetTimeSpan(StartTimeOrdinal);

                            var EndTimeOrdinal = reader.GetOrdinal("EndTime");
                            TimeSpan endTime = reader.IsDBNull(EndTimeOrdinal) ? TimeSpan.Zero : reader.GetTimeSpan(EndTimeOrdinal);

                            var ServiceIDOrdinal = reader.GetOrdinal("ServiceID");
                            int serviceID = reader.IsDBNull(ServiceIDOrdinal) ? 0 : reader.GetInt32(ServiceIDOrdinal);

                            // Construct the DTO
                            return new AppointmentDTO(
                                reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                reader.GetDateTime(reader.GetOrdinal("Date")),
                                issueDate,
                                startTime,
                                endTime,
                                reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                reader.GetInt32(reader.GetOrdinal("PatientID")),
                                reader.GetInt32(reader.GetOrdinal("AppointmentStatusID")),
                                serviceID
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return null;
                }
            }

            // Return null if no appointment was found with the given ID
            return null;
        }

        public static async Task<List<us_AppointmentItemDTO>> GetListOfCancelledAppointmentsAsync(int patientID)
        {
            List<us_AppointmentItemDTO> cancelledAppointments = new List<us_AppointmentItemDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetListOFCancelledAppointments", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientID", patientID);

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Handle potential NULLs safely
                            string doctorName = reader.IsDBNull(reader.GetOrdinal("FullName"))
                                ? "" : reader.GetString(reader.GetOrdinal("FullName"));

                            string status = reader.IsDBNull(reader.GetOrdinal("StatusName"))
                                ? "" : reader.GetString(reader.GetOrdinal("StatusName"));

                            string service = reader.IsDBNull(reader.GetOrdinal("ServiceName"))
                                ? "" : reader.GetString(reader.GetOrdinal("ServiceName"));

                            TimeSpan startTime = reader.IsDBNull(reader.GetOrdinal("StartTime"))
                                ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("StartTime"));

                            TimeSpan endTime = reader.IsDBNull(reader.GetOrdinal("EndTime"))
                                ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

                            // Construct the DTO
                            us_AppointmentItemDTO appointment = new us_AppointmentItemDTO(
                                reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                reader.GetDateTime(reader.GetOrdinal("Date")),
                                doctorName,
                                status,
                                service,
                                reader.GetInt32(reader.GetOrdinal("PatientID")),
                                startTime,
                                endTime
                            );

                            cancelledAppointments.Add(appointment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return null;
                }
            }
            return cancelledAppointments;
        }


        public static async Task<bool> ChangeAppointmentStatusToCanceledAsync(int appointmentID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_ChangeAppointmentStatusToCanceled", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add AppointmentID parameter
                command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                try
                {
                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was updated (appointment existed)
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return false;
                }
            }
        }

        public static async Task<List<us_AppointmentItemDTO>> GetListAppointementByPatientIDAsync(int patientID)
        {
            List<us_AppointmentItemDTO> appointments = new List<us_AppointmentItemDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetListAppointementByPatientID", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientID", patientID);

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Handle potential NULLs safely
                            string doctorName = reader.IsDBNull(reader.GetOrdinal("FullName"))
                                ? "" : reader.GetString(reader.GetOrdinal("FullName"));

                            string status = reader.IsDBNull(reader.GetOrdinal("StatusName"))
                                ? "" : reader.GetString(reader.GetOrdinal("StatusName"));

                            string service = reader.IsDBNull(reader.GetOrdinal("ServiceName"))
                                ? "" : reader.GetString(reader.GetOrdinal("ServiceName"));

                            TimeSpan startTime = reader.IsDBNull(reader.GetOrdinal("StartTime"))
                                ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("StartTime"));

                            TimeSpan endTime = reader.IsDBNull(reader.GetOrdinal("EndTime"))
                                ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

                            // Construct the DTO using the mapped values
                            us_AppointmentItemDTO appointment = new us_AppointmentItemDTO(
                                reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                reader.GetDateTime(reader.GetOrdinal("Date")),
                                doctorName,
                                status,
                                service,
                                reader.GetInt32(reader.GetOrdinal("PatientID")),
                                startTime,
                                endTime
                            );

                            appointments.Add(appointment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return null;
                }
            }
            return appointments;
        }
        public static async Task<bool> UpdateAppointmentAsync(int appointmentID, DateTime date, TimeSpan time, int doctorID, int patientID, int appointmentStatusID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_UpdateAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add Appointment ID parameter
                command.Parameters.AddWithValue("@AppointmentID", appointmentID);

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

                try
                {
                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if at least one row was updated (appointment existed)
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return false;
                }
            }
        }

        public static async Task<bool> ChangeAppointmentStatusAsync(int newAppointmentStatusID, int appointmentID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_ChangeAppointmentSatus", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@NewAppointmentStatusID", newAppointmentStatusID);
                command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                try
                {
                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Return true if the appointment was found and updated
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return false;
                }
            }
        }

    }
}