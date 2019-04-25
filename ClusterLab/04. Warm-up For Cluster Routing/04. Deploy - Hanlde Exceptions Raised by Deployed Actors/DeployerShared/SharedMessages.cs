using System;

namespace DeployerShared
{
    public class SharedMessages
    {
        public class Hello
        {
            public Hello(string message)
            {
                Message = message;
            }

            public string Message { get; }
        }
    }
}
