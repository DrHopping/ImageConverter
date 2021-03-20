using System;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public class BpmReader : IReader
    {
        public Image Read(string stream)
        {
            Console.WriteLine("Bpm reader");
            return new Image();
        }
    }
}