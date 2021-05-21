using SqlGenerator.Export.Pdf.Enum;
using System.Collections.Generic;
using System.Drawing;

namespace SqlGenerator.Export.Pdf
{
    public class PdfDesign
    {
        public string Title { get; set; }

        public bool ShowPages { get; set; }

        public bool ShowTimestamp { get; set; }

        public int FontSize { get; set; }

        public Color HeaderBackgroundColor { get; set; }

        public Color HeaderForegroundColor { get; set; }

        public PdfOrientation Orientation { get; set; }

        public PdfAlignment Alignment { get; set; }

        public List<PdfDesignData> DataCollection { get; set; }

        public PdfDesign()
        {
            DataCollection = new List<PdfDesignData>();
        }
    }
}
