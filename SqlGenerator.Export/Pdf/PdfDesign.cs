using System.Collections.Generic;
using System.Drawing;

namespace SqlGenerator.Export.Pdf
{
    public class PdfDesign
    {
        public string Title { get; set; }

        public bool Landscape { get; set; }

        public bool Timestamp { get; set; }

        public int FontSize { get; set; }


        public List<PdfDesignData> DataCollection { get; set; }

        public PdfDesign()
        {
            DataCollection = new List<PdfDesignData>();
        }
    }
}
