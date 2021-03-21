using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ImageConverter.Models;

namespace ImageConverter.Readers
{

    public class BmpReader : IReader
    {
        public Image Read(FileStream stream)
        {
            using var reader = new BinaryReader(stream);
            var info = reader.ReadBytes(54);
            var width = info[18] | info[19] << 8 | info[20] << 16 | info[21] << 24;
            var height = info[22] | info[23] << 8 | info[24] << 16 | info[25] << 24;
            var fileSize = info[2] | info[3] << 8 | info[4] << 16 | info[5] << 24;
            return GetImage(reader, width, height);
        }

        private static Image GetImage(BinaryReader reader, int width, int height)
        {
            var image = new Image (height, width);
            var skipAmount = ((3 * width) % 4 == 0) ? 0 : ((4 - (3 * width) % 4));
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image.Pixels[i,j] = new Pixel
                    {
                        Blue = reader.ReadByte(),
                        Green = reader.ReadByte(),
                        Red = reader.ReadByte()
                    };
                }
                reader.ReadBytes(skipAmount);
            }
            return image;
        }
    }
}