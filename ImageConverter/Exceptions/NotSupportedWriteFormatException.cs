using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageConverter.Exceptions
{
    public class NotSupportedWriteFormatException : ArgumentException
    {
        public override string Message { get; }
        public NotSupportedWriteFormatException(IEnumerable<string> supportedFormats, string format)
        {
            Message = $"Error: you are trying to write {format} file, but only {supportedFormats.Aggregate((msg, next) => msg + $", {next}")} files are supported";
        }
    }
}