using System;

namespace ClusterClientSharedMessages
{
    public class SendMessages
    {
        public sealed class CustomWelcome
        {
            public string Text { get; private set; }

            public CustomWelcome(string text)
            {
                Text = text;
            }
        }
    }
}
