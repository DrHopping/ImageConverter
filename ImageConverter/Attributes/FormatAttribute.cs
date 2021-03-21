namespace ImageConverter.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class FormatAttribute : System.Attribute
    {
        public string Format { get;}

        public FormatAttribute(string format)
        {
            Format = format;
        }
    }
}