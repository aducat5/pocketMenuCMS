using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMCMS.BLL.Utility
{
    public static class Logger
    {
        private static readonly PocketMenuDBEntities db = DBTool.GetInstance();
        public static async Task LogAsync(EventLog log) 
        {
            db.EventLogs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
