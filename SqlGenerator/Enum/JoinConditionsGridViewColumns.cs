﻿using SqlGenerator.Attribute;

namespace SqlGenerator.Enum
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
