using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMCMS.BLL.CustomExceptions
{

    [Serializable]
    public class LogoutRequiredException : Exception
    {
        public LogoutRequiredException() { }
        public LogoutRequiredException(string message) : base(message) { }
        public LogoutRequiredException(string message, Exception inner) : base(message, inner) { }
        protected LogoutRequiredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
