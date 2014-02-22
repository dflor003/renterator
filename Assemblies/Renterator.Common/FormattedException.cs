using System;
using System.Runtime.Serialization;

namespace Renterator.Common
{
    [Serializable]
    public class FormattedException : ApplicationException
    {
        public FormattedException()
        {
        }

        public FormattedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public FormattedException(Exception inner, string format, params object[] args)
            : base(string.Format(format, args), inner)
        {
        }

        protected FormattedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
