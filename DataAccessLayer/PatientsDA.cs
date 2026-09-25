using DTOsLayer;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography;


namespace DataAccessLayer
{
    public class PatientsDA
    {

        public static async Task<List<PatientDTO>> GetAllPatientsAsListAsync()
        {
            List<PatientDTO> patients = new List<PatientDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetAllPatients", conx))
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
                            var EmailOrdinal = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(EmailOrdinal) ? "" : reader.GetString(EmailOrdinal);

                            var AddressOrdinal = reader.GetOrdinal("Address");
                            string Address = reader.IsDBNull(AddressOrdinal) ? "" : reader.GetString(AddressOrdinal);

                            var GenderOrdinal = reader.GetOrdinal("Gender");
                            string Gender = reader.IsDBNull(GenderOrdinal) ? "" : reader.GetString(GenderOrdinal);

                            PatientDTO patient = new PatientDTO(
                                reader.GetInt32(reader.GetOrdinal("PatientID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Email,
                                reader.GetString(reader.GetOrdinal("Phone")),
                                Address,
                                Gender
                            );
                            patients.Add(patient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return new List<PatientDTO>();
                }
            }
            return patients;
        }

        public static async Task<int> AddNewPatientAsync(string firstName, string lastName, DateTime dateOfBirth, string phone, string email, string address, string gender, string password)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewPatient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add FirstName parameter
                command.Parameters.AddWithValue("@FirstName", firstName);

                // Add LastName parameter
                command.Parameters.AddWithValue("@LastName", lastName);

                // Add DateOfBirth parameter
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                // Add Phone parameter
                command.Parameters.AddWithValue("@Phone", phone);

                // Handle Email (nullable)
                if (string.IsNullOrEmpty(email))
                {
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email", email);
                }

                // Handle Address (nullable)
                if (string.IsNullOrEmpty(address))
                {
                    command.Parameters.AddWithValue("@Address", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address", address);
                }

                // Handle Gender (nullable)
                if (string.IsNullOrEmpty(gender))
                {
                    command.Parameters.AddWithValue("@Gender", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Gender", gender);
                }

                // Add Password parameter
                command.Parameters.AddWithValue("@Password", password);

                // Output parameter for the new Patient ID
                var outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                try
                {
                    // Execute the stored procedure asynchronously
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    
                    return (int)outputIdParam.Value;
                }
                catch (Exception ex)
                {
                    
                    return -1; // Return -1 to indicate failure
                }
            }
        }

        public static async Task<bool> DeletePatientAsync(int patientID)
        {
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_DeletePatient", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientID", patientID);

                try
                {
                    await conx.OpenAsync();

                    // The stored procedure returns the rows affected via SELECT
                    object result = await cmd.ExecuteScalarAsync();

                    int rowsAffected = Convert.ToInt32(result);

                    // Return true if at least one row was deleted
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
