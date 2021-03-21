using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using ImageConverter.Exceptions;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public interface IReadStrategy
    {
        Image Read(string format, TextReader stream);
    }

    public class ReadStrategy : IReadStrategy
    {
        private readonly IEnumerable<IReader> _readers;

        public ReadStrategy(IEnumerable<IReader> readers)
        {
            _readers = readers;
        }

        public Image Read(string format, TextReader stream)
        {
            var readersFormatsDictionary = GetReaderFormatsDictionary(_readers);
            if (!readersFormatsDictionary.Keys.Contains(format)) 
                throw new NotSupportedReadFormatException(readersFormatsDictionary.Keys, format);
            return readersFormatsDictionary[format].Read(stream);
        }

        private Dictionary<string, IReader> GetReaderFormatsDictionary(IEnumerable<IReader> readers)
        {
            var formatDictionary = new Dictionary<string, IReader>();
            readers.ToList().ForEach(r => formatDictionary.Add($".{r.GetType().Name.Replace("Reader", "").ToLower()}", r));
            return formatDictionary;
        }
    }
}