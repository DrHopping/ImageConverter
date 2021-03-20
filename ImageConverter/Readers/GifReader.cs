using System;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public class GifReader : IReader
    {
        public Image Read(string stream)
        {
            Console.WriteLine("Gif reader");
            return new Image();
        }
    }
}