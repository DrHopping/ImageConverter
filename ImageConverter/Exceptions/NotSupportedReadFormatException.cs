using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageConverter.Exceptions
{
    public class NotSupportedReadFormatException : ArgumentException
    {
        public override string Message { get; }
        public NotSupportedReadFormatException(IEnumerable<string> supportedFormats, string format)
        {
            Message = $"Error: you are trying to open {format} file, but only {supportedFormats.Aggregate((msg, next) => msg + $", {next}")} files are supported";
        }
    }
}