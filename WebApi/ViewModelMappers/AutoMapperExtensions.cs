using AutoMapper;
using System.Reflection;

namespace WebApi.ViewModelMappers
{
    /// <summary>
    /// AutoMapper Extensions Class
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Extension method to ignore all non-existing class members during automapping
        /// </summary>
        /// <typeparam name="TSource">Source class to map from</typeparam>
        /// <typeparam name="TDestination">Destination class to map to</typeparam>
        /// <param name="expression">Input IMappingExpression</param>
        /// <returns>Result IMappingExpression after ignoring all non-existing class members</returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
