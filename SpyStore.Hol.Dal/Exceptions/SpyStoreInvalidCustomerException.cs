using System;
using System.Collections.Generic;
using System.Text;

namespace SpyStore.Hol.Dal.Exceptions
{
    public class SpyStoreInvalidCustomerException : SpyStoreException
    {
        public SpyStoreInvalidCustomerException() { }
        public SpyStoreInvalidCustomerException(string message) : base(message) { }
        public SpyStoreInvalidCustomerException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
