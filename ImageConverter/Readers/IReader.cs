using ImageConverter.Models;

namespace ImageConverter.Readers
{
    public interface IReader
    {
        Image Read(string stream);
    }
}