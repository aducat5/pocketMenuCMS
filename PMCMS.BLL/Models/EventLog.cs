using PMCMS.BLL.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMCMS.BLL.Models
{
    public class EventLog
    {
        public int LogType { get; set; }
        public DateTime CreateDate { get; set; }
        public string LogMessage { get; set; }
        public string LogDetail { get; set; }
        public string Machine { get; set; }
    }
}