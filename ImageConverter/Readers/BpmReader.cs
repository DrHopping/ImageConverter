using System;
using System.IO;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public class BpmReader : IReader
    {
        public Image Read(TextReader stream)
        {
            Console.WriteLine("Bpm reader");
            return new Image();
        }
    }
}