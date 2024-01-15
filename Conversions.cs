using System;

namespace ChargesWFM.UI
{
    public static class Conversions
    {
        public static object ConvertTo(this string value, Type type)
        {
            if (type == typeof(string))
            {
                return value;
            }
            else if (Nullable.GetUnderlyingType(type) is Type underlyingType)
            {
                return value.ConvertTo(underlyingType);
            }
            else if (type.IsPrimitive)
            {
                return value.ConvertToPrimitive(type);
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return (decimal)double.Parse(value);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return DateTime.Parse(value);
            }

            return Convert.ChangeType(value, type);
        }

        public static object ConvertToPrimitive(this string value, Type type)
        {
            if (!type.IsPrimitive)
            {
                throw new Exception($"Not a primitive type: {type.Name}");
            }

            return Convert.ChangeType(value, type);
        }
    }
}