using SqlGenerator.UI.Attribute;

namespace SqlGenerator.UI.Enum
{
    public enum JoinConditionsGridViewColumns
    {
        [DataGridColumn("ColumnName", 0, Description = "Column")]
        ColumnName,
        [DataGridColumn("Condition", 1, Description = "Condition")]
        Condition,
        [DataGridColumn("Combination", 2, Description = "Combination")]
        Combination,
    }
}
