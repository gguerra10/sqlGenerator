using SqlGenerator.Export.Pdf.Enum;

namespace SqlGenerator.Export.Pdf
{
    public class PdfDesignData
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column legend if needed
        /// </summary>
        public string Legend { get; set; }

        /// <summary>
        /// Column show on table
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// Add total on last row
        /// </summary>
        public bool Total { get; set; }

        /// <summary>
        /// Column width
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Column text alignment
        /// </summary>
        public PdfAlignment Alignment { get; set; }

        public PdfDesignData()
        {
            Name = string.Empty;
            Legend = string.Empty;
        }
    }
}
