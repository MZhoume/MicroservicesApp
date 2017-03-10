namespace Shared.EnumHelper
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension methods for enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Extension method for enum to get the string values
        /// </summary>
        /// <param name="entry"> The enum entry </param>
        /// <returns> The string value </returns>
        public static string GetStringValue(this Enum entry)
        {
            foreach (var prop in entry.GetType().GetFields())
            {
                foreach (var att in prop.GetCustomAttributes(false).OfType<StringValueAttribute>())
                {
                    return att.Value;
                }
            }

            return null;
        }
    }
}