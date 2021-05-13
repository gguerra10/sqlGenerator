using SqlGenerator.UI.Attribute;

namespace SqlGenerator.UI.Enum
{
    public enum TablesGridViewColumns
    {
        [DataGridColumn("TableName", 0, Description = "Table")]
        TableName,
        [DataGridColumn("Join", 1, Description = "Join")]
        Join,
    }
}
