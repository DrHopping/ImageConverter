using System.IO;
using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public interface IReader
    {
        Image Read(TextReader stream);
    }
}