using System;
using System.Collections.Generic;
using System.Text;
using VirtualMind.Test.Model;

namespace VirtualMind.Test.Repositories.Common
{
    public class CustomException : Exception
    {
        public CustomException()
            : base() { }

        public CustomException(string message)
            : base(message) { }

        public CustomException(ResultJson message)
            : base(message.Message) { }
    }
}
