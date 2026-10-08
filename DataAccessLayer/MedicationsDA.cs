using DTOsLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DataAccessLayer
{
    public class MedicationsDA
    {
        public static async Task<List<MedicationDTO>> GetListOfMedicationsAsync()
        {
            List<MedicationDTO> medications = new List<MedicationDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetListOfMedications", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            MedicationDTO medication = new MedicationDTO(
                                reader.GetInt32(reader.GetOrdinal("MedicationID")),
                                reader.GetString(reader.GetOrdinal("MedicationName"))
                            );
                            medications.Add(medication);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return null;
                }
            }
            return medications;
        }
    }
}
