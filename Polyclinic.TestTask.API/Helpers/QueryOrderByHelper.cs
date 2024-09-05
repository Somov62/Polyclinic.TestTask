using System.Linq.Expressions;

namespace Polyclinic.TestTask.API.Helpers
{
    /// <summary>
    /// Хелпер для сортировки в запросах.
    /// </summary>
    public static class QueryOrderByHelper
    {
        /// <summary>
        /// Метод сортировки, в котором направление задается с помощью true-false.
        /// </summary>
        public static IOrderedQueryable<TModel> OrderBy<TModel, TValue>(
            this IQueryable<TModel> query, 
            Expression<Func<TModel, TValue>> prop, 
            bool desc)
        {
            return desc ? 
                query.OrderByDescending(prop) : 
                query.OrderBy(prop);
        }
    }
}
