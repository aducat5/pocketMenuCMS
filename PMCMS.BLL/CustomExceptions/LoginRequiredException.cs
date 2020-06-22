using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMCMS.BLL.CustomExceptions
{

    [Serializable]
    public class LoginRequiredException : Exception
    {
        public LoginRequiredException() { }
        public LoginRequiredException(string message) : base(message) { }
        public LoginRequiredException(string message, Exception inner) : base(message, inner) { }
        protected LoginRequiredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
