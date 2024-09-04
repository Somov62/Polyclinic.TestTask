namespace Polyclinic.TestTask.API.Models.Entities
{
    /// <summary>
    /// Специализация врача. 
    /// </summary>
    public class Specialization : Entity
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
