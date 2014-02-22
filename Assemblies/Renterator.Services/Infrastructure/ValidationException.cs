using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Renterator.Services.Infrastructure
{
    public class ValidationException : Exception
    {
        private const string ValidationExceptionMessage = "One or more errors have occurred";

        public ValidationException(string message)
            : this(MessageType.Error, message)
        {
        }

        public ValidationException(MessageType messageType, string message)
            : this(new LogMessage(messageType, message))
        {
        }

        public ValidationException(IEnumerable<ValidationResult> results)
            : this(results.Select(msg => new LogMessage(MessageType.Error, msg.ErrorMessage)).ToArray())
        {
        }

        public ValidationException(params LogMessage[] messages)
            : base(ValidationExceptionMessage)
        {
            Messages = messages;
        }

        public LogMessage[] Messages { get; set; }
    }
}
