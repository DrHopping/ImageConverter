using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ImageConverter.Attributes;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    [Format("ppm")]
    public class PpmReader : IReader
    {
        public Image Read(FileStream stream)
        {
            using var reader = new StreamReader(stream);
            return GetImage(GetFilteredImageString(reader));
        }

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

            var image = new Image(height, width);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var index = i * (width * 3) + (j * 3) + 3;
                    image.Pixels[i,j] = new Pixel {Red = NormalizeColor(ppmValues[index], maxColor, baseColor), 
                                                   Green = NormalizeColor(ppmValues[index + 1], maxColor, baseColor), 
                                                   Blue = NormalizeColor(ppmValues[index + 2], maxColor, baseColor)};
                }
            }
            return image;
        }

        private string GetFilteredImageString(TextReader stream)
            => String.Join('\n', stream.ReadToEnd().Split('\n').Where(str => !str.StartsWith("#")));

        private int NormalizeColor(int color, int maxColor, int baseColor)
            => (int)Math.Floor((decimal)color / maxColor * baseColor);
    }
}