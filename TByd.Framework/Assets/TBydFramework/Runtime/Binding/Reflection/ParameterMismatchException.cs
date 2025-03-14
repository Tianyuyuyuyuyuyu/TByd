﻿using System;

namespace TBydFramework.Runtime.Binding.Reflection
{
    public class ParameterMismatchException : Exception
    {
        public ParameterMismatchException()
        {
        }

        public ParameterMismatchException(string message) : base(message)
        {
        }

        public ParameterMismatchException(Exception exception) : base("", exception)
        {
        }

        public ParameterMismatchException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
