namespace ClinicManagementSystemAPI.Interfaces
{
    public interface ISendEmail
    {
        Task SendAsync(string to, string subject, string htmlbody);
    }
}
