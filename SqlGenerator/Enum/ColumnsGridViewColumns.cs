using SqlGenerator.Attribute;

namespace SqlGenerator.Enum
{
    public enum ColumnsGridViewColumns
    {
        [DataGridColumn("TableName", 0, Description = "Table")]
        TableName,
        [DataGridColumn("ColumnName", 1, Description = "Column")]
        ColumnName,
        [DataGridColumn("Selected", 2, Description = "Selected")]
        Selected,
        [DataGridColumn("Alias", 3, Description = "Alias")]
        Alias,
        [DataGridColumn("Condition", 4, Description = "Condition")]
        Condition,
        [DataGridColumn("Group", 5, Description = "Group")]
        Group,
        [DataGridColumn("Aggregation", 6, Description = "Aggregation")]
        Aggregation,
        [DataGridColumn("Order", 7, Description = "Order")]
        Order,
    }
}
