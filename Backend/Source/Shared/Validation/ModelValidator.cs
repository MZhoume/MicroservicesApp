namespace Shared.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using Shared.Interface;

    /// <summary>
    /// Static class contains the extension method used for validating models
    /// </summary>
    public static class ModelValidator
    {
        /// <summary>
        /// Extension method used to validate the model
        /// </summary>
        /// <param name="model"> The model to validate </param>
        public static void Validate(this IModel model)
        {
            var type = model.GetType();
            foreach (var prop in type.GetProperties())
            {
                foreach (var att in prop.GetCustomAttributes(false).OfType<ValidationAttribute>())
                {
                    att.Validate(prop.GetValue(model), prop.Name);
                }
            }

            foreach (var att in type.GetTypeInfo().GetCustomAttributes(false).OfType<ValidationAttribute>())
            {
                att.Validate(model, type.Name);
            }
        }
    }
}