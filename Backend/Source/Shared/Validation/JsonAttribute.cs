namespace Shared.Validation
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// Attribute to annotate json string
    /// </summary>
    public class JsonAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validate the given value
        /// </summary>
        /// <param name="value"> Value to validate </param>
        /// <returns> Is the value valid </returns>
        public override bool IsValid(object value)
        {
            if (!(value is string))
            {
                return false;
            }

            try
            {
                JsonConvert.DeserializeObject(value as string);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}