using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
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

        private bool _PrimaryAuthentication()
        {
            return clsAuthenticationChecker.CheckAuthentication(this.Username, this.Password);
        }

        private bool SecondaryAuthentication()
        {
            if (string.IsNullOrEmpty(this.Email)|| string.IsNullOrEmpty(this.Phone))
            {
                throw new Exception("invalid inputs");
            }
            return clsAuthenticationChecker.SecondaryAuthentication(this.Phone, this.Email);
        }

        public bool Authenticate()
        {
            switch(_Type)
            {
                case enAuthenticationType.PrimaryMethod:
                 return   _PrimaryAuthentication();
                   
                case enAuthenticationType.SecondaryMethod:
                    return SecondaryAuthentication();
            }
            return false;
        }

    }
}
