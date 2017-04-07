namespace Common.Data.Convert
{
    public static class Convert
    {
        public static bool ConvertToBool(this object value, bool defaultValue = false)
        {
            bool tempResult;
            bool result = bool.TryParse((string) value, out tempResult) ? tempResult : defaultValue;
            return result;
        }

        public static bool ConvertToBool(this string value, bool defaultValue = false)
        {
            bool tempResult;
            bool result = bool.TryParse(value, out tempResult) ? tempResult : defaultValue;
            return result;
        }

        public static int ConvertToInt(this object value, int defaultValue = 0)
        {
            int tempResult;
            int result = int.TryParse((string) value, out tempResult) ? tempResult : defaultValue;
            return result;
        }

        public static int ConvertToInt(this string value, int defaultValue = 0)
        {
            int tempResult;
            int result = int.TryParse(value, out tempResult) ? tempResult : defaultValue;
            return result;
        }
        public static long ConvertToLong(this object value, long defaultValue = 0)
        {
            long tempResult;
            long result = long.TryParse((string) value, out tempResult) ? tempResult : defaultValue;
            return result;
        }
        public static long ConvertToLong(this string value, long defaultValue = 0)
        {
            long tempResult;
            long result = long.TryParse(value, out tempResult) ? tempResult : defaultValue;
            return result;
        }
        public static decimal ConvertToDecimal(this object value, decimal defaultValue = decimal.Zero)
        {
            decimal tempResult;
            decimal result = decimal.TryParse((string)value, out tempResult) ? tempResult : defaultValue;
            return result;
        }

        public static decimal ConvertToDecimal(this string value, decimal defaultValue = decimal.Zero)
        {
            decimal tempResult;
            decimal result = decimal.TryParse(value, out tempResult) ? tempResult : defaultValue;
            return result;
        }

        public static string ConvertToString(this object value, string defaultValue = "")
        {
            string result = value is string ? value.ToString() : defaultValue;
            return result;
        }

    }
}
