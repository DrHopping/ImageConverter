using System;
using System.IO;
using System.Text;
using ImageConverter.Attributes;
using ImageConverter.Models;

namespace ImageConverter.Writers
{
    [Format("bpm")]
    public class BmpWriter : IWriter
    {
        public void Write(Image image, string file)
        {
            throw new NotImplementedException("Bmp writer not implemented yet");
            using var stream = new MemoryStream();

            stream.Write(Encoding.UTF8.GetBytes("BM"));
            stream.Write(BitConverter.GetBytes(138 + 4 * image.Width * image.Height));
            stream.Write(new byte[4]);
            stream.Write(BitConverter.GetBytes(138));
            stream.Write(BitConverter.GetBytes(124));
            stream.Write(BitConverter.GetBytes(image.Width));
            stream.Write(BitConverter.GetBytes(image.Height));
            stream.Write(BitConverter.GetBytes(1));
            stream.Write(BitConverter.GetBytes(32));
            stream.Write(BitConverter.GetBytes(3));
            stream.Write(BitConverter.GetBytes(image.Height));
            stream.Write(BitConverter.GetBytes(image.Height));
            stream.Write(BitConverter.GetBytes(image.Height));

        }

    }
}