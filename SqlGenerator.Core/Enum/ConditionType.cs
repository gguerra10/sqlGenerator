

namespace SqlGenerator.Core.Enum
{
    public enum ConditionType
    {
        None,
        Equals,             //'='
        Different,          //'<>'
        GreaterThan,        //'>'
        GreaterEqualThan,   //'>='
        LessThan,           //'<'
        LessEqualThan,      //'<='
        Between,            //'BEETWEEN'
        NotBetween,         //'NOT BEETWEEN'
        ClauseIn,           //'IN (x,y,z)'
    }
}
