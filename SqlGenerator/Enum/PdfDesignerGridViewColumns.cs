using SqlGenerator.Attribute;

namespace SqlGenerator.Enum
{
    public enum PdfDesignerGridViewColumns
    {
        [DataGridColumn("Data", 0, Description = "Data")]
        Data,
        [DataGridColumn("Legend", 1, Description = "Legend")]
        Legend,
        [DataGridColumn("Show", 2, Description = "Show")]
        Show,
        [DataGridColumn("Total", 3, Description = "Total")]
        Total,
        [DataGridColumn("Width", 4, Description = "Width")]
        Width,
        [DataGridColumn("Alignment", 5, Description = "Alignment")]
        Alignment,
    }
}
