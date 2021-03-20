using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using CommandDotNet;
using ImageConverter.Readers;
using ImageConverter.Writers;

namespace ImageConverter
{    
    public interface IApp
    {
        [DefaultMethod]
        void Execute(
            [Option(LongName = "source", ShortName = "s", Description = "Source file path."), Required] string sourcePath,
            [Option(LongName = "goal-format", ShortName = "g", Description = "Output image format."), Required] string outputFormat,
            [Option(LongName = "output", ShortName = "o", Description = "Output path.")] string outputPath);
    }
    public class App : IApp
    {
        private readonly IReadStrategy _readStrategy;
        private readonly IWriteStrategy _writeStrategy;


        public App(IReadStrategy readStrategy, IWriteStrategy writeStrategy)
        {
            _readStrategy = readStrategy;
            _writeStrategy = writeStrategy;
        }

        public void Execute(string sourcePath, string outputFormat, string outputPath)
        {
            var sourceFormat = Path.GetExtension(sourcePath);
            using var sr = new StreamReader(sourcePath);
            var image = _readStrategy.Read(sourceFormat, sr.ReadToEnd());

            outputPath += outputFormat;

            _writeStrategy.Write(image, outputPath);
        }
    }


}