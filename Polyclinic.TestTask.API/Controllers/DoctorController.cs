﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
