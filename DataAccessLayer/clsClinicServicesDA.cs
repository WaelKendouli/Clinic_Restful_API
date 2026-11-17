using DTOsLayer;
using System;
using System.Collections.Generic;
using Microsoft.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class clsClinicServicesDA
    {
        public static async Task<List<ServiceDTO>> GetClinicServicesAsync()
        {
            List<ServiceDTO> services = new List<ServiceDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetClinicServices", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Handle nullable fields safely
                            var DescriptionOrdinal = reader.GetOrdinal("Description");
                            string description = reader.IsDBNull(DescriptionOrdinal) ? "" : reader.GetString(DescriptionOrdinal);

                            ServiceDTO service = new ServiceDTO(
                                reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                reader.GetString(reader.GetOrdinal("ServiceName")),
                                description,
                                reader.GetDecimal(reader.GetOrdinal("Price"))
                            );

                            services.Add(service);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return new List<ServiceDTO>();
                }
            }
            return services;
        }
    }
}
