using PMCMS.DAL;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using EventLog = PMCMS.DAL.EventLog;

namespace PMCMS.BLL.Utility
{
    public static class Logger
    {
        //private static readonly PocketMenuDBEntities db = DBTool.GetNewInstance();
        public static async Task LogAsync(EventLog log) 
        {
            using (var db = DBTool.GetNewInstance())
            {
                db.EventLogs.Add(log);
                await db.SaveChangesAsync(); 
            }
        }
        public static async Task LogAsync(string message)
        {
            using (var db = DBTool.GetNewInstance())
            {
                StackTrace stackTrace = new StackTrace();
                string source = stackTrace.GetFrame(1).GetMethod().Name;
                EventLog log = new EventLog()
                {
                    LogType = "Information",
                    Message = message,
                    CreateDate = DateTime.Now,
                    Source = source
                };
                db.EventLogs.Add(log);
                await db.SaveChangesAsync(); 
            }
        }
        public static async Task LogAsync(string message, string logtype)
        {
            using (var db = DBTool.GetNewInstance())
            {
                StackTrace stackTrace = new StackTrace();
                string source = stackTrace.GetFrame(1).GetMethod().Name;
                EventLog log = new EventLog()
                {
                    LogType = logtype,
                    Message = message,
                    CreateDate = DateTime.Now,
                    Source = source
                };
                db.EventLogs.Add(log);
                await db.SaveChangesAsync(); 
            }
        }
        public static async Task LogAsync(Exception exception)
        {
            using (var db = DBTool.GetNewInstance())
            {
                StackTrace stackTrace = new StackTrace();
                string source = stackTrace.GetFrame(1).GetMethod().Name;
                EventLog log = new EventLog()
                {
                    Message = exception.Message,
                    CreateDate = DateTime.Now,
                    Source = source + "-" + exception.Source,
                    Detail = exception.StackTrace,
                    LogType = "Exception"
                };
                db.EventLogs.Add(log);
                await db.SaveChangesAsync(); 
            }
        }
        public static void Log(string message)
        {
            using (var db = DBTool.GetNewInstance())
            {
                StackTrace stackTrace = new StackTrace();
                string source = stackTrace.GetFrame(1).GetMethod().Name;
                EventLog log = new EventLog()
                {
                    LogType = "Information",
                    Message = message,
                    CreateDate = DateTime.Now,
                    Source = source
                };
                db.EventLogs.Add(log);
                db.SaveChangesAsync(); 
            }
        }
    }
}
