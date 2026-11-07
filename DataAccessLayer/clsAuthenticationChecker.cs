using DTOsLayer;
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
                            bool? gender = reader.IsDBNull(GenderOrdinal) ? (bool?)null : reader.GetBoolean(GenderOrdinal);

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
