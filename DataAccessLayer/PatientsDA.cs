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
using System.Globalization;


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
                                ); // I removed the rest of ordinalites for testing purposes 
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

        public static async Task<int> AddNewPatientAsync(string firstName, string lastName, DateTime dateOfBirth, string phone, string email, string address, bool gender, string password)
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
                if (gender==null)
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

        public static async Task<bool> UpdatePatientAsync(PatientDTO patient)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_UpdatePatient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // 1. Add UserID parameter
                command.Parameters.AddWithValue("@UserID", patient.UserID);

                // 2. Add Base User Data
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);

                // Handle Phone (nullable in DB)
                if (string.IsNullOrEmpty(patient.Phone))
                    command.Parameters.AddWithValue("@Phone", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Phone", patient.Phone);

                // Handle Email (nullable in DB)
                if (string.IsNullOrEmpty(patient.Email))
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Email", patient.Email);

                // 3. Add Patient Specific Data
                // Handle Address (nullable in DB)
                if (string.IsNullOrEmpty(patient.Address))
                    command.Parameters.AddWithValue("@Address", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Address", patient.Address);

                // 4. Add Gender (Mapped to BIT in SQL)
                command.Parameters.AddWithValue("@Gender", patient.Gender);

                // 5. Add Return Value Parameter to capture @@ROWCOUNT
                var returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValueParam);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    // Read the RETURN @@ROWCOUNT value from the stored procedure
                    int rowsAffected = (int)returnValueParam.Value;

                    // Return true if the user was found and updated (rowsAffected > 0)
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                   
                    return false;
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }


    }
}
