using System;
using System.Reflection;

namespace Domain.Data.Enum
{
    /// <summary>
    /// StringEnum класс предоставляет статические методы для работы со строковыми значениями в перечислениях.
    /// </summary>
    public static class StringEnum
    {
        /// <summary>
        /// Метод GetStringValue преобразует значение перечисления в это строковое значение, установленное с помощью атрибута StringValueAttribute.
        /// Пример:
        ///     string str = StringEnum.GetStringValue(Status.Auto);
        /// </summary>
        public static string GetStringValue(this System.Enum value)
        {
            string stringValue = string.Empty;
            Type enumType = value.GetType();
            FieldInfo fi = enumType.GetField(value.ToString());
            StringValueAttribute[] attrs =
                fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
            if (attrs != null && attrs.Length > 0)
            {
                stringValue = attrs[0].Value;
            }
            return stringValue;
        }

        /// <summary>
        /// Метод GetDescription возвращает описание, установленное с помощью атрибута DescriptionAttribute.
        /// Пример:
        ///     string str = StringEnum.GetDescription(Status.Auto);
        /// </summary>
        public static string GetDescription(this System.Enum value)
        {
            string desc = string.Empty;
            Type enumType = value.GetType();
            FieldInfo fi = enumType.GetField(value.ToString());
            ValueNameAttribute[] attrs =
                fi.GetCustomAttributes(typeof (ValueNameAttribute), false) as ValueNameAttribute[];
            if (null != attrs && attrs.Length > 0)
            {
                desc = attrs[0].Value;
            }
            return desc;
        }

        /// <summary>
        /// Метод Parse преобразует строковое значение для конкретного типа перечислений к значению этого перечисления.
        /// Пример:
        ///     Status status = (Status)StringEnum.Parse(typeof(Status), "Результат расчитан автоматически");
        /// </summary>
        public static object Parse(Type enumType, string stringValue)
        {
            object result = null;
            foreach (FieldInfo fi in enumType.GetFields())
            {
                StringValueAttribute[] attrs =
                    fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
                if (attrs != null && attrs.Length > 0)
                {
                    string enumStringValue = attrs[0].Value;
                    if (enumStringValue == stringValue)
                    {
                        result = System.Enum.Parse(enumType, fi.Name);
                        break;
                    }
                }
            }
            return result;
        }

        public static T Parse<T>(string stringValue, T defautValue)
        {
            Type enumType = typeof (T);
            if (!enumType.IsEnum)
                throw new ApplicationException("Указанный тип должен быть перечислением (enum)!");

            return (T) (Parse(enumType, stringValue) ?? defautValue);
        }

        /// <summary>
        /// Метод IsStringDefined checks whether the particular string value was specified for enum using StringValueAttribute.
        /// </summary>
        public static bool IsStringDefined(Type enumType, string stringValue)
        {
            return Parse(enumType, stringValue) != null;
        }
    }

    /// <summary>
    /// StringValueAttribute should be used to provide string values for enum values.
    /// Then static methods from StringEnum class could be used to convert between string and enum values.
    /// Пример:
    ///     public enum Status
    ///     {
    ///         [StringValue("Результат расчитан автоматически")]
    ///         Auto
    ///     }
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// ValueNameAttribute should be used to provide description for enum values.
    /// Then static methods from StringEnum class could be used to get description for enum value.
    /// Пример:
    ///     public enum Status
    ///     {
    ///         [ValueName("Расчет данного результата был произведен автоматическим способом")]
    ///         Auto
    ///     }
    /// </summary>
    public class ValueNameAttribute : Attribute
    {
        public string Value { get; }

        public ValueNameAttribute(string value)
        {
            Value = value;
        }
    }
}
