using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DataAccessLayer
{
    public class clsDoctorsDA
    {
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
    }
}
