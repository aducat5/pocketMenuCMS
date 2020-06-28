using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMCMS.BLL.Utility
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Result { get; set; }
        public Exception Exception { get; set; }
    }
}
