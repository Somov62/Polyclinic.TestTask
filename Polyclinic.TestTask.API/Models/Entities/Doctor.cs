namespace Polyclinic.TestTask.API.Models.Entities
{
    /// <summary>
    /// Модель врача.
    /// </summary>
    public class Doctor : Entity
    {
        /// <summary>
        /// ФИО.
        /// </summary>
        public string FIO { get; set; } = string.Empty;

        /// <summary>
        /// Рабочий кабинет.
        /// </summary>
        public Cabinet Cabinet { get; set; } = null!;

        /// <summary>
        /// Специализация.
        /// </summary>
        public Specialization Specialization { get; set; } = null!;

        /// <summary>
        /// Участок для участкового врача.
        /// </summary>
        public MedicalDistrict? MedicalDistrict { get; set; } = null;
    }
}
