using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DTOsLayer;
using DTOsLayer.Interfaces;
using System.Security.Cryptography;
using System.Security.Policy;
namespace ClinicLogicLayer
{
    public class LoggingAuthenticator
    {
        enum enAuthenticationType
        {
            PrimaryMethod = 1,
            SecondaryMethod = 2
        }
        enAuthenticationType _Type { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IF_User UserObj { get; set; }

       

        public LoggingAuthenticator(string username, string password)
        {
            _Type = enAuthenticationType.PrimaryMethod;
            Username = username;
            Password = password;
            

        }
        public LoggingAuthenticator(string username, string password, string Phone = "", string Email = "")
        {
            _Type = enAuthenticationType.PrimaryMethod;
            Username = username;
            Password = password;
            this.Phone = Phone;
            this.Email = Email;

        }

        public void SwitchToSecondaryAuthenticationMethod()
        {
            _Type = enAuthenticationType.SecondaryMethod;
        }

        private async Task<bool> _PrimaryAuthentication()
        {
            string HashedValue = clsMyHash.GetSha256Hash(this.Password);
            var UserDTO =  clsAuthenticationChecker.CheckAuthentication(this.Username,HashedValue);
            if (UserDTO == null)
            {
                return false;
            }
            this.UserObj = await clsAuthenticationChecker.GetDataBasedOnRoles(UserDTO);
            return true;
        }

        private bool SecondaryAuthentication()
        {
            if (string.IsNullOrEmpty(this.Email)|| string.IsNullOrEmpty(this.Phone))
            {
                throw new Exception("invalid inputs");
            }
            return clsAuthenticationChecker.SecondaryAuthentication(this.Phone, this.Email);
        }

        public async Task<bool> Authenticate()
        {
           
                 return  await _PrimaryAuthentication();
            
        }

    }
}
