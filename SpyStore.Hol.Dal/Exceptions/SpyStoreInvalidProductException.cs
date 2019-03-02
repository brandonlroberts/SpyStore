using System;
using System.Collections.Generic;
using System.Text;

namespace SpyStore.Hol.Dal.Exceptions
{
    public class SpyStoreInvalidProductException : SpyStoreException
    {
        public SpyStoreInvalidProductException() { }
        public SpyStoreInvalidProductException(string message) : base(message) { }
        public SpyStoreInvalidProductException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
