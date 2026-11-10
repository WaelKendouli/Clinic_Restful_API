using DTOsLayer;
using DTOsLayer.Interfaces;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsAuthenticationChecker
    {
        public static UserDTO CheckAuthentication(string username, string password)
        {
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_CheckAuthentication", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conx.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Handle nullable fields safely
                            var EmailOrdinal = reader.GetOrdinal("Email");
                            string email = reader.IsDBNull(EmailOrdinal) ? "" : reader.GetString(EmailOrdinal);

                            var PhoneOrdinal = reader.GetOrdinal("Phone");
                            string phone = reader.IsDBNull(PhoneOrdinal) ? null : reader.GetString(PhoneOrdinal);

                            var GenderOrdinal = reader.GetOrdinal("Gender");
                            bool gender = reader.IsDBNull(GenderOrdinal) ? false : reader.GetBoolean(GenderOrdinal);

                            var DateOfBirthOrdinal = reader.GetOrdinal("DateOfBirth");
                            DateTime dateOfBirth = reader.IsDBNull(DateOfBirthOrdinal) ? DateTime.MinValue : reader.GetDateTime(DateOfBirthOrdinal);

                            // Return the populated UserDTO
                            return new UserDTO(
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetString(reader.GetOrdinal("Username")),
                                reader.GetString(reader.GetOrdinal("HashedPassword")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                email,
                                phone,
                                gender,
                                dateOfBirth,
                                reader.GetString(reader.GetOrdinal("RoleName"))
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

            // If no rows were returned, return null
            return null;
        }
        public static async Task<PatientDTO> GetPatientDataByUserIDAsync(UserDTO user)
        {
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetPatientDataByUserID", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", user.UserID);

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Handle nullable Address safely
                            var AddressOrdinal = reader.GetOrdinal("Address");
                            string address = reader.IsDBNull(AddressOrdinal) ? string.Empty : reader.GetString(AddressOrdinal);

                            int patientID = reader.GetInt32(reader.GetOrdinal("PatientID"));

                            // Construct the PatientDTO using the existing UserDTO data + the new Patient data
                            return new PatientDTO(
                                patientID,
                                user.UserID,
                                user.Username,
                                user.HashedPassword,
                                user.FirstName,
                                user.LastName,
                                user.Email,
                                user.Phone,
                                user.Gender,
                                user.DateOfBirth,
                                address
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
            return null;
        }

        public static async Task<AdminDTO> GetAdminDataByUserIDAsync(UserDTO user)
        {
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetAdminDataByUserID", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", user.UserID);

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            int adminID = reader.GetInt32(reader.GetOrdinal("AdminID"));

                            // Construct the AdminDTO using the existing UserDTO data + the new AdminID
                            return new AdminDTO(
                                adminID,
                                user.UserID,
                                user.Username,
                                user.HashedPassword,
                                user.FirstName,
                                user.LastName,
                                user.Email,
                                user.Phone,
                                user.Gender,
                                user.DateOfBirth
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
            return null;
        }

        public async static Task<IF_User> GetDataBasedOnRoles(UserDTO DTO)
        {
            switch (DTO.Role)
            {
                case "Patient":
                    return await GetPatientDataByUserIDAsync(DTO);
                case "Doctor":
                    break;
                case "Admin":
                    return await GetAdminDataByUserIDAsync(DTO);
            }
            return null;
        }

        public static bool SecondaryAuthentication(string phone, string email)
        {
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_SecondaryAuthentication", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    conx.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // If any row is returned, the credentials are valid
                        return reader.HasRows;
                    }
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
