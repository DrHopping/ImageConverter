using System.Collections.Generic;

namespace ImageConverter.Models
{
    public class Image
    {
        public Pixel[,] Pixels { get; set; }
        public int Width { get; }
        public int Height { get; }

        public Image(int height, int width)
        {
            Height = height;
            Width = width;
            this.Pixels = new Pixel[height,width];
        }
    }
}