using System.Runtime.Serialization;

namespace CoreLibrary.Exception
{
    public class CnbServiceRequestException : System.Exception
    {
        public CnbServiceRequestException()
        {
        }

        protected CnbServiceRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CnbServiceRequestException(string message) : base(message)
        {
        }

        public CnbServiceRequestException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}