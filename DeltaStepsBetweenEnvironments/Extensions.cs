namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
    public static class Extensions
    {
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
    }
}
