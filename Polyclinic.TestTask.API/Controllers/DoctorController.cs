using Microsoft.AspNetCore.Mvc;
using Polyclinic.TestTask.API.Requests.Doctors;
using Polyclinic.TestTask.API.Services.Doctors;

namespace Polyclinic.TestTask.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с сущностями врача.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DoctorController(DoctorsService doctorsService) : ControllerBase
    {
        /// <summary>
        /// Endpoint для создания врача.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] AddDoctorRequest request,
            CancellationToken ct)
        {
            try
            {
                var id = await doctorsService.Create(request, ct);
                return Ok(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint для получения врача по id на редактирование.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken ct)
        {
            try
            {
                var result = await doctorsService.GetById(id, ct);
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
            [FromQuery] GetDoctorsByPageRequest request,
            CancellationToken ct)
        {
            var result = await doctorsService.GetByPage(request, ct);
            return Ok(result);
        }


        /// <summary>
        /// Endpoint для редактирования врача.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateDoctorRequest request,
            CancellationToken ct)
        {
            try
            {
                var result = await doctorsService.Update(id, request, ct);
                return result.HasValue ? Ok(id) : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint для удаления врача.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken ct)
        {
            var result = await doctorsService.Delete(id, ct);
            return result.HasValue ? Ok(id) : NotFound();
        }
    }
}
