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

        /// <summary>
        /// Название специализации участкового врача.
        /// Необходимо для валидации специализаций врачей.
        /// </summary>
        public const string THERAPIST_SPECIALIZATION_NAME = "Терапевт";

        /// <summary>
        /// Позволяет ли специальность работать на участке
        /// (быть участковым врачом). 
        /// </summary>
        public bool CanWorkOnMedicalDistrict()
        {
            return Name == THERAPIST_SPECIALIZATION_NAME;
        }
    }
}
