using DTOsLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsMedicalRecordDA
    {
        public static async Task<int> AddNewMedicalRecordAsync(MedicalRecordAggregateDTO aggregate)
        {
            using (var connection = new SqlConnection(clsConnection.ConnectionString))
            using (var command = new SqlCommand("SP_AddNewMedicalRecord", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // --- 1. Core Medical Record Data ---
                command.Parameters.AddWithValue("@Description", aggregate.MedicalRecord.Description);

                if (string.IsNullOrEmpty(aggregate.MedicalRecord.AdditionalNotes))
                    command.Parameters.AddWithValue("@AdditionalNotes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@AdditionalNotes", aggregate.MedicalRecord.AdditionalNotes);

                command.Parameters.AddWithValue("@DoctorID", aggregate.MedicalRecord.DoctorID);
                command.Parameters.AddWithValue("@PatientID", aggregate.MedicalRecord.PatientID);
                command.Parameters.AddWithValue("@AppointmentID", aggregate.MedicalRecord.AppointmentID);

                // --- 2. Vitals Data ---
                command.Parameters.AddWithValue("@Height", aggregate.Vitals.Height);
                command.Parameters.AddWithValue("@Weight", aggregate.Vitals.Weight);

                if (string.IsNullOrEmpty(aggregate.Vitals.BloodPressure))
                    command.Parameters.AddWithValue("@BloodPressure", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@BloodPressure", aggregate.Vitals.BloodPressure);

                command.Parameters.AddWithValue("@Temperature", aggregate.Vitals.Temperature);
                command.Parameters.AddWithValue("@Pulse", aggregate.Vitals.Pulse);

                // --- 3. Diagnosis Data ---
                command.Parameters.AddWithValue("@Conclution", aggregate.Diagnosis.Conclution);
                command.Parameters.AddWithValue("@ICD10Code", aggregate.Diagnosis.ICD10Code);

                try
                {
                    await connection.OpenAsync();

                    // The stored procedure uses SELECT SCOPE_IDENTITY(), so we use ExecuteScalarAsync
                    object result = await command.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    return -1; // Should not reach here if transaction succeeded
                }
                catch (SqlException ex)
                {
                    
                    return -1;
                }
                catch (Exception ex)
                {
                    
                    return -1;
                }
            }
        }
    }
}
