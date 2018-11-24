using System;

namespace Funda1.Exceptions
{
    public class CustomReportException : Exception
    {
        public CustomReportException()
        {
        }

        public CustomReportException(string message)
            : base(message)
        {
        }

        public CustomReportException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
