using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageConverter.Attributes;
using ImageConverter.Exceptions;
using ImageConverter.Models;
using ImageConverter.Readers;

namespace ImageConverter.Writers
{
    public interface IWriteStrategy
    {
        void Write(Image image, string file);
    }

    public class WriteStrategy : IWriteStrategy
    {
        private readonly IEnumerable<IWriter> _writers;

        public WriteStrategy(IEnumerable<IWriter> writers)
        {
            _writers = writers;
        }

        public void Write(Image image, string file)
        {
            var writerFormatsDictionary = GetWriterFormatsDictionary(_writers);
            var format = Path.GetExtension(file);
            if (!writerFormatsDictionary.Keys.Contains(format))
                throw new NotSupportedWriteFormatException(writerFormatsDictionary.Keys, format);
            writerFormatsDictionary[format].Write(image, file);
        }

        private Dictionary<string, IWriter> GetWriterFormatsDictionary(IEnumerable<IWriter> writers)
        {
            var formatDictionary = new Dictionary<string, IWriter>();
            writers.ToList().ForEach(r => formatDictionary.Add($".{((FormatAttribute)Attribute.GetCustomAttribute(r.GetType(), typeof(FormatAttribute)))?.Format}", r));
            return formatDictionary;
        }
    }
}