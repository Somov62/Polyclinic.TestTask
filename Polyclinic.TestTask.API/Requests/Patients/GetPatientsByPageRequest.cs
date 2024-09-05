namespace Polyclinic.TestTask.API.Requests.Patients
{

    /// <summary>
    /// Запрос страницы пациентов.
    /// </summary>
    /// <param name="OrderBy"> Название поля для сортировки. </param>
    /// <param name="OrderDirection"> Направление сортировки asc/desc. </param>
    /// <param name="Size"> Кол-во элементов на странице. </param>
    /// <param name="Page"> Номер страницы. </param>
    public record GetPatientsByPageRequest(
         string? OrderBy,
         string OrderDirection = "asc",
         int Size = 10,
         int Page = 1
        );
}
