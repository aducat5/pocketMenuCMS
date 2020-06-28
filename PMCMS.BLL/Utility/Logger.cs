using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using EventLog = PMCMS.DAL.EventLog;

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
        public static async Task LogAsync(string message)
        {
            StackTrace stackTrace = new StackTrace();
            string source = stackTrace.GetFrame(1).GetMethod().Name;
            EventLog log = new EventLog()
            {
                Message = message,
                CreateDate = DateTime.Now,
                Source = source
            };
            db.EventLogs.Add(log);
            await db.SaveChangesAsync();
        }

        public static async Task LogAsync(Exception exception)
        {
            StackTrace stackTrace = new StackTrace();
            string source = stackTrace.GetFrame(1).GetMethod().Name;
            EventLog log = new EventLog()
            {
                Message = exception.Message,
                CreateDate = DateTime.Now,
                Source = source + "-" + exception.Source,
                Detail = exception.StackTrace
            };
            db.EventLogs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
