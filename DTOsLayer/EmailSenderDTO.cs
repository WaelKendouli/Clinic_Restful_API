using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class EmailSenderDTO
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string Body { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public EmailSenderDTO()
        { 
        }

        public EmailSenderDTO(string from , string to , string subject , string body)
        {
            this.from = from;
            this.to = to;
            this.subject = subject;
            this.Body = body;
        }
    }
}
