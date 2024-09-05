using Microsoft.AspNetCore.Mvc;
using Polyclinic.TestTask.API.Requests.Patients;
using Polyclinic.TestTask.API.Services;

namespace Polyclinic.TestTask.API.Controllers;

/// <summary>
/// Контроллер для работы с сущностями пациента.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PatientController(PatientsService patientsService) : ControllerBase
{
    /// <summary>
    /// Endpoint для создания пациента.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePatientRequest request,
        CancellationToken ct)
    {
        try
        {
            var id = await patientsService.Create(request, ct);
            return Ok(id);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint для получения пациента по id на редактирование.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id,
        CancellationToken ct)
    {
        try
        {
            var result = await patientsService.GetById(id, ct);
            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint для получения врачей постранично с сортировкой.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetByPage(
        [FromQuery] GetPatientsByPageRequest request,
        CancellationToken ct)
    {
        var result = await patientsService.GetByPage(request, ct);
        return Ok(result);
    }


    /// <summary>
    /// Endpoint для редактирования пациента.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdatePatientRequest request,
        CancellationToken ct)
    {
        try
        {
            var result = await patientsService.Update(id, request, ct);
            return result.HasValue ? Ok(id) : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint для удаления пациента.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var result = await patientsService.Delete(id, ct);
        return result.HasValue ? Ok(id) : NotFound();
    }
}
