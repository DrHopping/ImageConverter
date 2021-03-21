﻿using System;
using System.IO;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public class GifReader : IReader
    {
        public Image Read(TextReader stream)
        {
            Console.WriteLine("Gif reader");
            return new Image();
        }
    }
}