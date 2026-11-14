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
                            // Safe string reads (returns "" if DB value is NULL)
                            string Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email"));
                            string Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone"));
                            string PhotoURL = reader.IsDBNull(reader.GetOrdinal("PhotoURL")) ? "" : reader.GetString(reader.GetOrdinal("PhotoURL"));
                            string Bio = reader.IsDBNull(reader.GetOrdinal("Bio")) ? "" : reader.GetString(reader.GetOrdinal("Bio"));
                            string Specialization = reader.IsDBNull(reader.GetOrdinal("Specialization")) ? "" : reader.GetString(reader.GetOrdinal("Specialization"));
                            string Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "" : reader.GetString(reader.GetOrdinal("Username"));
                            string HashedPassword = reader.IsDBNull(reader.GetOrdinal("HashedPassword")) ? "" : reader.GetString(reader.GetOrdinal("HashedPassword"));
                            string FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? "" : reader.GetString(reader.GetOrdinal("FirstName"));
                            string LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? "" : reader.GetString(reader.GetOrdinal("LastName"));

                            // Safe boolean read
                            bool gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? false : reader.GetBoolean(reader.GetOrdinal("Gender"));

                            // Safe DateTime read
                            DateTime dateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                            // Safe Integer reads (use 0 if NULL)
                            int specializationID = reader.IsDBNull(reader.GetOrdinal("SpecialazationID")) ? 0 : reader.GetInt32(reader.GetOrdinal("SpecialazationID"));
                            int doctorID = reader.IsDBNull(reader.GetOrdinal("DoctorID")) ? 0 : reader.GetInt32(reader.GetOrdinal("DoctorID"));
                            int userID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserID"));

                            DoctorDTO doctor = new DoctorDTO(
                                doctorID,
                                PhotoURL,
                                specializationID,
                                Bio,
                                Specialization,
                                userID,
                                Username,
                                HashedPassword,
                                FirstName,
                                LastName,
                                Email,
                                Phone,
                                gender,
                                dateOfBirth,
                                "Doctor"
                            );

                            doctors.Add(doctor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception here
                    return null;
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

        public static async Task<List<AppointmentItemDTO>> GetListOfAppointmentsByDoctorIDAsync(int doctorID)
        {
            List<AppointmentItemDTO> appointments = new List<AppointmentItemDTO>();
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
                            AppointmentItemDTO appointment = new AppointmentItemDTO(
                                reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("DoctorName")),
                                reader.GetString(reader.GetOrdinal("AppointmentStatus")),
                                reader.GetString(reader.GetOrdinal("Field")),
                                reader.GetDateTime(reader.GetOrdinal("Date")),
                                reader.GetTimeSpan(reader.GetOrdinal("Time")) ,
                                reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                reader.GetInt32(reader.GetOrdinal("PatientID"))
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
