using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public class PpmReader : IReader
    {
        public Image Read(TextReader stream)
            => GetImage(GetFilteredImageString(stream));

        private Image GetImage(string stream)
        {
            var ppmValues = stream.Split(new[] {' ', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries)
                                  .Skip(1)
                                  .Select(int.Parse)
                                  .ToList();

            var width = ppmValues[0];
            var height = ppmValues[1];
            var maxColor = ppmValues[2];
            var baseColor = 255;

            var image = new Image {Width = width, Height = height};
            for (int i = 0; i < height; i++)
            {
                var row = new List<Pixel>();
                for (int j = 0; j < width * 3; j += 3)
                {
                    var index = i * (width * 3) + j + 3;
                    var pixel = new Pixel {Red = NormalizeColor(ppmValues[index], maxColor, baseColor), 
                                           Green = NormalizeColor(ppmValues[index + 1], maxColor, baseColor), 
                                           Blue = NormalizeColor(ppmValues[index + 2], maxColor, baseColor)};
                    row.Add(pixel);
                }
                image.Pixels.Add(row);
            }

            return image;
        }

        private string GetFilteredImageString(TextReader stream)
            => String.Join('\n', stream.ReadToEnd().Split('\n').Where(str => !str.StartsWith("#")));

        private int NormalizeColor(int color, int maxColor, int baseColor)
            => (int)Math.Floor((decimal)color / maxColor * baseColor);
    }
}