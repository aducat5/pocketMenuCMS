//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMCMS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventLog
    {
        public int LogId { get; set; }
        public string LogType { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string TraceId { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
