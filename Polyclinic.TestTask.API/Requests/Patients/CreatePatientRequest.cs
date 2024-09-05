namespace Polyclinic.TestTask.API.Requests.Patients;

/// <summary>
/// Запрос на добавление пациента.
/// </summary>
/// <param name="FirstName"> Имя. </param>
/// <param name="LastName"> Фамилия. </param>
/// <param name="Patronymic"> Отчество. </param>
/// <param name="Address"> Адрес. </param>
/// <param name="Gender"> Пол. </param>
/// <param name="BirthDate"> Дата рождения. </param>
/// <param name="MedicalDistrict"> Участок, к которому прикреплен. </param>
public record CreatePatientRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string Address,
    string Gender,
    DateTime BirthDate,
    int MedicalDistrict);