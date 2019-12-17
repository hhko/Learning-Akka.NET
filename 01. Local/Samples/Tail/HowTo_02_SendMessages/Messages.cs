namespace HowTo_02_SendMessages
{
    class Messages
    {
        #region Neutral/system messages
        /// <summary>
        /// Marker class to continue processing.
        /// </summary>
        public class ContinueProcessing { }
        #endregion

        #region Success messages
        /// <summary>
        /// Base class for signalling that user input was valid.
        /// </summary>
        public class InputSuccess
        {
            public InputSuccess(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; private set; }
        }
        #endregion

        #region Error messages
        /// <summary>
        /// Base class for signalling that user input was invalid.
        /// </summary>
        public class InputFailure
        {
            public InputFailure(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; private set; }
        }

        /// <summary>
        /// User provided blank input.
        /// </summary>
        public class NullInputFailure : InputFailure
        {
            public NullInputFailure(string reason) : base(reason) { }
        }

        /// <summary>
        /// User provided invalid input (currently, input w/ odd # chars)
        /// </summary>
        public class ValidationInputFailure : InputFailure
        {
            public ValidationInputFailure(string reason) : base(reason) { }
        }
        #endregion
    }
}
