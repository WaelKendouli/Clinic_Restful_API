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
                            // Safely handle nullable strings
                            var StrengthOrdinal = reader.GetOrdinal("Strength");
                            string strength = reader.IsDBNull(StrengthOrdinal) ? "" : reader.GetString(StrengthOrdinal);

                            var InstructionsOrdinal = reader.GetOrdinal("Instructions");
                            string instructions = reader.IsDBNull(InstructionsOrdinal) ? "" : reader.GetString(InstructionsOrdinal);

                            MedicationDTO medication = new MedicationDTO(
                                reader.GetInt32(reader.GetOrdinal("MedicationID")),
                                reader.GetString(reader.GetOrdinal("MedicationName")),
                                reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                reader.GetInt32(reader.GetOrdinal("FormID")),
                                strength,
                                instructions
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

        public static async Task<Dictionary<string, int>> GetMedicationCategoriesAsync()
        {
            Dictionary<string, int> DicCategories = new Dictionary<string, int>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetAllMedicationCategories", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Safely read nullable fields
                            var CategoryNameOrdinal = reader.GetOrdinal("CategoryName");
                            string categoryName = reader.IsDBNull(CategoryNameOrdinal) ? "" : reader.GetString(CategoryNameOrdinal);

                            int categoryID = reader.GetInt32(reader.GetOrdinal("CategoryID"));

                            if (!string.IsNullOrEmpty(categoryName) && !DicCategories.ContainsKey(categoryName))
                            {
                                DicCategories.Add(categoryName, categoryID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return new Dictionary<string, int>();
                }
            }
            return DicCategories;
        }

        public static async Task<List<DosageFormDTO>> GetListOfDosageFormsAsync()
        {
            List<DosageFormDTO> dosageForms = new List<DosageFormDTO>();
            using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetListOfDosageForms", conx))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conx.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Safely handle nullable FormName
                            var FormNameOrdinal = reader.GetOrdinal("FormName");
                            string formName = reader.IsDBNull(FormNameOrdinal) ? "" : reader.GetString(FormNameOrdinal);

                            DosageFormDTO form = new DosageFormDTO(
                                reader.GetInt32(reader.GetOrdinal("FormID")),
                                formName
                            );

                            dosageForms.Add(form);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, etc.)
                    return new List<DosageFormDTO>();
                }
            }
            return dosageForms;
        }
    }
}
