using ImageConverter.Models;

namespace ImageConverter.Writers
{
    public interface IWriter
    {
        void Write(Image image, string file);
    }
}