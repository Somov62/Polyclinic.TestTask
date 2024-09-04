namespace Polyclinic.TestTask.API.Models.Entities
{
    /// <summary>
    /// Модель пациента.
    /// </summary>
    public class Patient : Entity
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество.
        /// </summary>
        public string? Patronymic { get; set; } = null;

        /// <summary>
        /// Место жительства.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Пол.
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Участок, за которым закреплен данный пациент.
        /// </summary>
        public MedicalDistrict MedicalDistrict { get; set; } = null!;
    }
}
