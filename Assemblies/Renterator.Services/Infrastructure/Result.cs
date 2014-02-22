using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Renterator.Services.Infrastructure
{
    public class Result
    {
        public Result(bool isSuccess, IEnumerable<ValidationResult> validationErrors)
            : this(isSuccess, validationErrors == null ? new ValidationResult[0] : validationErrors.ToArray())
        {
        }

        public Result(bool isSuccess, ValidationResult[] validationErrors)
            : this(isSuccess, ToMessages(validationErrors))
        {
        }

        public Result(bool isSuccess, params LogMessage[] messages)
        {
            IsSuccess = isSuccess;
            Messages = messages != null && messages.Any() ? new List<LogMessage>(messages) : null;
        }

        public bool IsSuccess { get; private set; }

        public ICollection<LogMessage> Messages { get; private set; }

        protected static LogMessage[] ToMessages(IEnumerable<ValidationResult> validationResults)
        {
            return
                (from result in validationResults
                 select new LogMessage(MessageType.Error, result.ErrorMessage)).ToArray();
        }
    }

    public class Result<T> : Result
    {
        public Result(T result, bool isSuccess, IEnumerable<ValidationResult> validationErrors)
            : this(result, isSuccess, validationErrors == null ? new ValidationResult[0] : validationErrors.ToArray())
        {
        }

        public Result(T result, bool isSuccess, ValidationResult[] validationErrors)
            : this(result, isSuccess, ToMessages(validationErrors))
        {
        }

        public Result(T result, bool isSuccess, params LogMessage[] validationErrors)
            : base(isSuccess, validationErrors)
        {
            Value = result;
        }

        public T Value { get; private set; }
    }
}
