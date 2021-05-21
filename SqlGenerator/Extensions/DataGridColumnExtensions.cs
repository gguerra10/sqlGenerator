using SqlGenerator.Attribute;
using System.Reflection;

namespace SqlGenerator.Extensions
{
    public static class DataGridColumnExtensions
    {
        private static DataGridColumnAttribute[] GetAttributes(this System.Enum columnEnum)
        {
            FieldInfo fi = columnEnum.GetType().GetField(columnEnum.ToString());
            return (DataGridColumnAttribute[])fi.GetCustomAttributes(typeof(DataGridColumnAttribute), false);
        }

        public static string GetName(this System.Enum columnEnum)
        {
            var attributes = columnEnum.GetAttributes();

            if (attributes.Length > 0)
                return attributes[0].Name;
            else
                return string.Empty;
        }

        public static int GetPosition(this System.Enum columnEnum)
        {
            var attributes = columnEnum.GetAttributes();

            if (attributes.Length > 0)
                return attributes[0].Position;
            else
                return -1;
        }

        public static string GetDescription(this System.Enum columnEnum)
        {
            var attributes = columnEnum.GetAttributes();

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return string.Empty;
        }
    }
}
