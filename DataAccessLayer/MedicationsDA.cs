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
        public static async Task<Dictionary<string, int>> GetListOfMedicationsAsync()
        {
            Dictionary<string, int> DicMedications = new Dictionary<string, int>();
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
                            string medicationName = reader.GetString(reader.GetOrdinal("MedicationName"));
                            int medicationID = reader.GetInt32(reader.GetOrdinal("MedicationID"));

                            if (!DicMedications.ContainsKey(medicationName))
                            {
                                DicMedications.Add(medicationName, medicationID);
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
            return DicMedications;
        }
    }
}
