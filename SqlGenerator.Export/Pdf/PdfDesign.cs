using SqlGenerator.Export.Pdf.Enum;
using System.Collections.Generic;
using System.Drawing;

namespace SqlGenerator.Export.Pdf
{
    public class PdfDesign
    {
        /// <summary>
        /// Main title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Show pagination
        /// </summary>
        public bool ShowPages { get; set; }

        /// <summary>
        /// Show timestamp
        /// </summary>
        public bool ShowTimestamp { get; set; }

        /// <summary>
        /// General font size
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Header background color
        /// </summary>
        public Color HeaderBackgroundColor { get; set; }

        /// <summary>
        /// Header foreground color
        /// </summary>
        public Color HeaderForegroundColor { get; set; }

        /// <summary>
        /// Page orientation
        /// </summary>
        public PdfOrientation Orientation { get; set; }

        /// <summary>
        /// Table horizontal alignment
        /// </summary>
        public PdfAlignment Alignment { get; set; }

        /// <summary>
        /// Column design collection
        /// </summary>
        public List<PdfDesignData> DataCollection { get; set; }

        public PdfDesign()
        {
            DataCollection = new List<PdfDesignData>();
        }
    }
}
