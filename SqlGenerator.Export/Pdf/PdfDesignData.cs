using SqlGenerator.Export.Pdf.Enum;

namespace SqlGenerator.Export.Pdf
{
    public class PdfDesignData
    {
        public string Name { get; set; }

        public bool Hidden { get; set; }

        public float Width { get; set; }

        public PdfAlignment Alignment { get; set; }

        public bool Grouped { get; set; }
    }
}
