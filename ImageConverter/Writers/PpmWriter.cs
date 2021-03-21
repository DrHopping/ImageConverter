using System.IO;
using ImageConverter.Models;

namespace ImageConverter.Writers
{
    public class PpmWriter : IWriter
    {
        public void Write(Image image, string file)
        {
            using var writer = new StreamWriter(file);
            writer.WriteLine("P3");
            writer.WriteLine($"{image.Width} {image.Height}");
            writer.WriteLine("255");

            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image.Pixels[y,x];
                    writer.Write($"{pixel.Red} ");
                    writer.Write($"{pixel.Green} ");
                    writer.Write($"{pixel.Blue}\r");
                }
        }
    }
}