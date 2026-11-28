using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsLayer;
using Microsoft.Data.SqlClient;
namespace DataAccessLayer
{
    public class clsPrescriptionDA
    {
        public static async Task<int> AddNewPrescriptionAsync(PrescriptionDTO dto)
        {

            try
            {
                var meds = new DataTable();
                meds.Columns.Add("MedicationID", typeof(int));
                meds.Columns.Add("Dosage", typeof(string));
                meds.Columns.Add("Frequncy", typeof(string));
                meds.Columns.Add("StartDate", typeof(DateTime));
                meds.Columns.Add("EndDate", typeof(DateTime));

                foreach (var m in dto.liMedications)
                {
                    meds.Rows.Add(
                        m.MedicationID,
                        m.Dosage,
                        m.Frequncy,
                        m.StartDate,
                        m.EndDate
                    );
                }
                using (SqlConnection conx = new SqlConnection(clsConnection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddNewPrescription", conx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MedicalRecordID", SqlDbType.Int).Value = dto.MedicalRecordID;
                        cmd.Parameters.Add("@IssueDate", SqlDbType.Date).Value = dto.IssueDate;

                        var medParam = cmd.Parameters.AddWithValue("@Medications", meds);
                        medParam.SqlDbType = SqlDbType.Structured;
                        medParam.TypeName = "dbo.DescribedMedicationType";

                      await  conx.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }

                        return -1;
                    }

                }
            }
            catch (Exception e)
            {
                return -1;
            }
            

        }

       

    }
}
