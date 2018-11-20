using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    [Serializable()]
    public class InvalidIDException : System.Exception
    {
        public InvalidIDException() : base() { }
        public InvalidIDException(string message) : base(message) { }
        public InvalidIDException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidIDException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
