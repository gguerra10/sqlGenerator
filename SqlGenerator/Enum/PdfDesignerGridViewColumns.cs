using SqlGenerator.Attribute;

namespace SqlGenerator.Enum
{
    public enum PdfDesignerGridViewColumns
    {
        [DataGridColumn("Data", 0, Description = "Data")]
        Data,
        [DataGridColumn("Show", 1, Description = "Show")]
        Show,
        [DataGridColumn("Width", 2, Description = "Width")]
        Width,
        [DataGridColumn("Alignment", 3, Description = "Alignment")]
        Alignment,
    }
}
