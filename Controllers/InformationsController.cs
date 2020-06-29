using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.DTOs;

namespace PWSZ_Plan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationsController : ControllerBase
    {
        // GET api/Informations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInformation(int id)
        {
            return Ok(id);
        }

        // PATCH api/Informations/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateInformation(int id, InformationToUpdateDTO informationDTO)
        {
            return Ok(new { id = id, inf = informationDTO });
        }

        // DELETE api/Informations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            return Ok(id);
        }
    }
}