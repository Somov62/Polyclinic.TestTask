namespace Polyclinic.TestTask.API.Helpers
{
    /// <summary>
    /// Методы расширения для строк.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Сравнение двух строк без учета регистра.
        /// </summary>
        public static bool IgnoreCaseEquals(this string a, string? b)
            => a.Equals(b, StringComparison.CurrentCultureIgnoreCase);
    }
}
