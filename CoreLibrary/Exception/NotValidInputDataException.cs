namespace CoreLibrary.Exception
{
    public class NotValidInputDataException : System.Exception

    {
        public NotValidInputDataException()
        {
        }

        public NotValidInputDataException(string message) : base(message)
        {
        }

        public NotValidInputDataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}