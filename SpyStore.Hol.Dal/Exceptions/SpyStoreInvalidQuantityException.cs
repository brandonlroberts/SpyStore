﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpyStore.Hol.Dal.Exceptions
{
    public class SpyStoreInvalidQuantityException : SpyStoreException
    {
        public SpyStoreInvalidQuantityException() { }
        public SpyStoreInvalidQuantityException(string message) : base(message) { }
        public SpyStoreInvalidQuantityException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
