using SqlGenerator.Attribute;

namespace SqlGenerator.Enum
{
    public enum PdfDesignerGridViewColumns
    {
        [DataGridColumn("Data", 0, Description = "Data")]
        Data,
        [DataGridColumn("Hide", 1, Description = "Hide")]
        Hide,
        [DataGridColumn("Width", 2, Description = "Width")]
        Width,
        //[DataGridColumn("Group", 3, Description = "Group")]
        //Group,
    }
}
