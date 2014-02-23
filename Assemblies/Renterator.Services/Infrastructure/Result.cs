using System.Collections.Generic;
using System.Linq;

namespace Renterator.Services.Infrastructure
{
    public class Result
    {
        public Result(params LogMessage[] logMessages)
        {
            Messages = logMessages != null && logMessages.Any() ? new List<LogMessage>(logMessages) : null;
        }

        public ICollection<LogMessage> Messages { get; private set; }
    }

    public class Result<T> : Result
    {
        public Result(T result, params LogMessage[] logMessages)
            : base(logMessages)
        {
            Value = result;
        }

        public T Value { get; private set; }
    }
}
