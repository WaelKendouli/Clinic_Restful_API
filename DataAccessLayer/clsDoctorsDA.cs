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

        public static async Task<List<DoctorsListDTO>> ShowListOfDoctorsAsListAsync()
        {
            List<DoctorsListDTO> doctors = new List<DoctorsListDTO>();
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
                            // Handle nullable fields
                            var GenderOrdinal = reader.GetOrdinal("Gender");
                            string Gender = reader.IsDBNull(GenderOrdinal) ? "" : reader.GetString(GenderOrdinal);

                            var EmailOrdinal = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(EmailOrdinal) ? "" : reader.GetString(EmailOrdinal);

                            var AddressOrdinal = reader.GetOrdinal("Address");
                            string Address = reader.IsDBNull(AddressOrdinal) ? "" : reader.GetString(AddressOrdinal);

                            var PhotoURLOrdinal = reader.GetOrdinal("PhotoURL");
                            string PhotoURL = reader.IsDBNull(PhotoURLOrdinal) ? "" : reader.GetString(PhotoURLOrdinal);

                            var SpecializationOrdinal = reader.GetOrdinal("Specialization");
                            string Specialization = reader.IsDBNull(SpecializationOrdinal) ? "" : reader.GetString(SpecializationOrdinal);

                            DoctorsListDTO doctor = new DoctorsListDTO(
                                reader.GetInt32(reader.GetOrdinal("DoctorID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                Gender,
                                reader.GetString(reader.GetOrdinal("Phone")),
                                Email,
                                Address,
                                PhotoURL,
                                reader.GetInt32(reader.GetOrdinal("SpecialazationID")),
                                Specialization,
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth"))
                            );
                            doctors.Add(doctor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    return null;
                }
            }
            return doctors;
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
    }
}
