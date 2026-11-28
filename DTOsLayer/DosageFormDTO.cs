using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer
{
    public class DosageFormDTO
    {
        public int FormID { get; set; }
        public string FormName { get; set; }

        public DosageFormDTO()
        {
            FormName = string.Empty;
        }

        public DosageFormDTO(int formID, string formName)
        {
            FormID = formID;
            FormName = formName;
        }
    }
}
