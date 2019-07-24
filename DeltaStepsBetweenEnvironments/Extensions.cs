using Microsoft.Xrm.Sdk;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
    public static class Extensions
    {
        public static T GetAliasedValue<T>(this Entity entity, string varName)
        {
            return (T)(entity.GetAttributeValue<AliasedValue>(varName) == null 
                ? default(T) 
                : entity.GetAttributeValue<AliasedValue>(varName).Value);
        }

        public static string Capitalize(this string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            var firstLetter = value[0].ToString().ToUpper();
            if (value.Length == 1)
            {
                return firstLetter;
            } 
            return firstLetter + value.Substring(1);
        }

        public static string FormatForCsv(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
