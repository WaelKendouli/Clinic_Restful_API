using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTOsLayer;
namespace DataAccessLayer
{
    public class clsDoctorsDA
    {

        public static async Task<List<DoctorDTO>> ShowListOfDoctorsAsListAsync()
        {
            List<DoctorDTO> doctors = new List<DoctorDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_ShowListOfDoctors", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // --- 1. Safely Read Schedule Data ---
                            var StartTimeOrdinal = reader.GetOrdinal("StartTime");
                            TimeSpan startTime = reader.IsDBNull(StartTimeOrdinal) ? TimeSpan.Zero : reader.GetTimeSpan(StartTimeOrdinal);

                            var EndTimeOrdinal = reader.GetOrdinal("EndTime");
                            TimeSpan endTime = reader.IsDBNull(EndTimeOrdinal) ? TimeSpan.Zero : reader.GetTimeSpan(EndTimeOrdinal);

                            var SlotDurationOrdinal = reader.GetOrdinal("SlotDuration");
                            int slotDuration = reader.IsDBNull(SlotDurationOrdinal) ? 0 : reader.GetInt32(SlotDurationOrdinal);

                            var IsActiveOrdinal = reader.GetOrdinal("IsActive");
                            bool isActive = reader.IsDBNull(IsActiveOrdinal) ? false : reader.GetBoolean(IsActiveOrdinal);

                            // Create the Schedule DTO
                            DoctorScheduelDTO schedule = new DoctorScheduelDTO(startTime, endTime, slotDuration, isActive);


                            // --- 2. Safely Read User & Doctor Data ---
                            string email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email"));
                            string phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone"));
                            string photoURL = reader.IsDBNull(reader.GetOrdinal("PhotoURL")) ? "" : reader.GetString(reader.GetOrdinal("PhotoURL"));
                            string bio = reader.IsDBNull(reader.GetOrdinal("Bio")) ? "" : reader.GetString(reader.GetOrdinal("Bio"));
                            string specialization = reader.IsDBNull(reader.GetOrdinal("Specialization")) ? "" : reader.GetString(reader.GetOrdinal("Specialization"));

                            string username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "" : reader.GetString(reader.GetOrdinal("Username"));
                            string hashedPassword = reader.IsDBNull(reader.GetOrdinal("HashedPassword")) ? "" : reader.GetString(reader.GetOrdinal("HashedPassword"));
                            string firstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? "" : reader.GetString(reader.GetOrdinal("FirstName"));
                            string lastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? "" : reader.GetString(reader.GetOrdinal("LastName"));
                            bool gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? false : reader.GetBoolean(reader.GetOrdinal("Gender"));
                            DateTime dateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));


                            // --- 3. Create DoctorDTO ---
                            DoctorDTO doctor = new DoctorDTO(
                                reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                photoURL,
                                reader.GetInt32(reader.GetOrdinal("SpecialazationID")),
                                specialization,
                                bio,
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                username,
                                hashedPassword,
                                firstName,
                                lastName,
                                email,
                                phone,
                                gender,
                                dateOfBirth,
                                "Doctor", // Hardcoded role
                                schedule  // The newly created DoctorScheduelDTO object
                            );

                            doctors.Add(doctor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (ex.Message) here if needed
                    return new List<DoctorDTO>();
                }
            }
            return doctors;
        }
        public static async Task<bool> UpdateDoctorAsync(int doctorID, string firstName, string lastName, DateTime dateOfBirth, string phone, string email, string address, string gender, string photoURL, int specializationID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_UpdateDoctor", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                
                command.Parameters.AddWithValue("@DoctorID", doctorID);

                
                command.Parameters.AddWithValue("@FirstName", firstName);

               
                command.Parameters.AddWithValue("@LastName", lastName);

                
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                
                command.Parameters.AddWithValue("@Phone", phone);

                
                if (string.IsNullOrEmpty(email))
                {
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email", email);
                }

                
                if (string.IsNullOrEmpty(address))
                {
                    command.Parameters.AddWithValue("@Address", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address", address);
                }

               
                if (string.IsNullOrEmpty(gender))
                {
                    command.Parameters.AddWithValue("@Gender", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Gender", gender);
                }

                
                if (string.IsNullOrEmpty(photoURL))
                {
                    command.Parameters.AddWithValue("@PhotoURL", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@PhotoURL", photoURL);
                }

               
                command.Parameters.AddWithValue("@SpecializationID", specializationID);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }
        public static async Task<int> AddNewDoctorAsync(string firstName, string lastName, DateTime dateOfBirth, string phone, string email, string address, string gender, string photoURL, int specializationID)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewDoctor", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

               
                command.Parameters.AddWithValue("@FirstName", firstName);

                
                command.Parameters.AddWithValue("@LastName", lastName);

             
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                command.Parameters.AddWithValue("@Phone", phone);

                
                if (string.IsNullOrEmpty(email))
                {
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email", email);
                }

                
                if (string.IsNullOrEmpty(address))
                {
                    command.Parameters.AddWithValue("@Address", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address", address);
                }

                
                if (string.IsNullOrEmpty(gender))
                {
                    command.Parameters.AddWithValue("@Gender", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Gender", gender);
                }

              
                if (string.IsNullOrEmpty(photoURL))
                {
                    command.Parameters.AddWithValue("@PhotoURL", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@PhotoURL", photoURL);
                }

                
                command.Parameters.AddWithValue("@SpecializationID", specializationID);

                
                var outputIdParam = new SqlParameter("@NewDoctorID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                try
                {
                    
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                   
                    return (int)outputIdParam.Value;
                }
                catch (Exception ex)
                {
                    
                    return -1; 
                }
            }
        }

        public static async Task<Dictionary<string, int>> GetSpecialazationsAsync()
        {
            Dictionary<string, int> DicSpecializations = new Dictionary<string, int>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetSpecialazations", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string specializationName = reader.GetString(reader.GetOrdinal("Specialization"));
                            int specializationID = reader.GetInt32(reader.GetOrdinal("SpecialazationID"));

                            if (!DicSpecializations.ContainsKey(specializationName))
                            {
                                DicSpecializations.Add(specializationName, specializationID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return null;
                }
            }
            return DicSpecializations;
        }

        public static async Task<List<us_AppointmentItemDTO>> GetListOfAppointmentsByDoctorIDAsync(int doctorID)
        {
            List<us_AppointmentItemDTO> appointments = new List<us_AppointmentItemDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetListOfAppointmentsByDoctorID", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Safely handle nullable fields
                            string status = reader.IsDBNull(reader.GetOrdinal("StatusName"))
                                ? "" : reader.GetString(reader.GetOrdinal("StatusName"));

                            string service = reader.IsDBNull(reader.GetOrdinal("ServiceName"))
                                ? "" : reader.GetString(reader.GetOrdinal("ServiceName"));

                            string patientName = reader.IsDBNull(reader.GetOrdinal("Patient"))
                                ? "" : reader.GetString(reader.GetOrdinal("Patient"));

                            TimeSpan startTime = reader.IsDBNull(reader.GetOrdinal("StartTime"))
                                ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("StartTime"));

                            TimeSpan endTime = reader.IsDBNull(reader.GetOrdinal("EndTime"))
                                ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

                            // Construct the DTO using the new constructor
                            us_AppointmentItemDTO appointment = new us_AppointmentItemDTO(
                                reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                reader.GetDateTime(reader.GetOrdinal("Date")),
                                "", // DoctorName is not returned by this SP (we already know the doctor)
                                status,
                                service,
                                reader.GetInt32(reader.GetOrdinal("PatientID")),
                                startTime,
                                endTime,
                                patientName
                            );

                            appointments.Add(appointment);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return new List<us_AppointmentItemDTO>();
                }
            }
            return appointments;
        }
        public static async Task<Dictionary<string, int>> GetDoctorsListForOptionsAsync()
        {
            Dictionary<string, int> DicDoctors = new Dictionary<string, int>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetDoctorsListForOptions", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string doctorName = reader.GetString(reader.GetOrdinal("name"));
                            int doctorID = reader.GetInt32(reader.GetOrdinal("id"));

                            if (!DicDoctors.ContainsKey(doctorName))
                            {
                                DicDoctors.Add(doctorName, doctorID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return new Dictionary<string, int>();
                }
            }
            return DicDoctors;
        }

    }
}
