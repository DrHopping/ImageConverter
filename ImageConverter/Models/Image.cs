using System.Collections.Generic;

namespace ImageConverter.Models
{
    public class Image
    {
        public List<List<Pixel>> Pixels { get; set; } = new List<List<Pixel>>();
        public int Width { get; set; }
        public int Height { get; set; }
    }
}