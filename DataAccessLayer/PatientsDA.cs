using DTOsLayer;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
                            // Handle nullable Email field
                            var EmailOrdinal = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(EmailOrdinal) ? "" : reader.GetString(EmailOrdinal);

                            PatientDTO patient = new PatientDTO(
                                reader.GetInt32(reader.GetOrdinal("PatientID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Email,
                                reader.GetString(reader.GetOrdinal("Phone"))
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

    }
}
