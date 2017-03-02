namespace Static.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Static.Interface;

    /// <summary>
    /// Static class contains the extension method used for validating models
    /// </summary>
    public static class ModelValidator
    {
        /// <summary>
        /// Extension method used to validate the model
        /// </summary>
        /// <param name="model"> The model to validate </param>
        /// <returns> If valid returns the model itself </returns>
        public static IModel Validate(this IModel model)
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                foreach (var vatt in prop.GetCustomAttributes().OfType<ValidationAttribute>())
                {
                    vatt.Validate(prop.GetValue(model), prop.Name);
                }
            }

            return model;
        }
    }
}