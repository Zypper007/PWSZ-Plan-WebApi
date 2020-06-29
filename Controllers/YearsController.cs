using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Controllers;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;

namespace PWSZ_Plan_WebApi.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearsController : BaseApiController<Year>
    {
        //new private readonly IYearRepository _repo;

        public YearsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
           // _repo = yearRepository;
        }

        // GET Years
        [HttpGet]
        public async Task<IActionResult> GetYears()
        {
            var years = await _repo.GetAll();
            var yearsMapped = years.Count > 0 ? _mapper.Map<List<YearDefaultInformationsDTO>>(years) : null ;
            return Ok(yearsMapped);
        }

        // GET Years/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetYear(int id)
        {
            var year = await _repo.Get(id);
            var yearMapped = year != null ? _mapper.Map<YearSendDTO>(year) : null ;
            return Ok(yearMapped);
        }

        // GET Years/{id}/Plans
        [HttpGet("{id}/Plans")]
        public async Task<IActionResult> GetPlans(int id)
        {
            return Ok(new { message = Messages.NotImplementYet });
        }

        // GET Years/{id}/Classes
        [HttpGet("{id}/Classes")]
        public async Task<IActionResult> GetClasses(int id)
        {
            return Ok(new { message = Messages.NotImplementYet });
        }

        // PATCH Years/{id}
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, YearDTO yearDTO)
        {
            var year = await Updating(id, yearDTO);
            if(year != null)
            {
                var yearMapped = _mapper.Map<YearSendDTO>(year);
                return Ok(yearMapped);
            }
            return ResponeError();
        }

       

        // DELETE Years/{id}
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedYear = await Deleting(id);
            if (deletedYear != null)
            {
                var yearMapped = _mapper.Map<YearSendDTO>(deletedYear);
                return ResponeDelete(yearMapped);
            }

            return ResponeDeleteError();
        }



        private async Task<Year> Deleting(int id)
        {
            var year = await _repo.Get(id);
            if(year == null)
            {
                errorsList.Add("Rok nie istnieje");
                return null;
            }

            if (IsInstitute() && year.Specialization.Major.Institute.ID != GetInstituteID())
            {
                errorsList.Add("Nie mozesz usuwać roków z innych wydziałow");
                return null;
            }

            var deletedYear = await _repo.Delete(year);
            if(deletedYear != null)
                return deletedYear;

            errorsList.Add("Nie udało się usunąć roku");
            return null;
        }

        private async Task<Year> Updating(int id, YearDTO yearDTO)
        {
            var year = await _repo.Get(id);
            if (year == null)
            {
                errorsList.Add("Rok nie istnieje");
                return null;
            }

            if (IsInstitute() && GetInstituteID() != year.Specialization.Major.Institute.ID)
            {
                errorsList.Add("Nie możesz modyfikować roków z innych wydziałów");
                return null;
            }

            Tools.CopyValues(year, yearDTO);

            if (IsSuperUser() && yearDTO.SpecializationID != null)
            {
                var spec = await _repo.Get<Specialization>((int)yearDTO.SpecializationID);
                if (spec != null)
                    year.Specialization = spec;
            }

            var updatedYear = await _repo.Update(year);
            if (updatedYear != null)
                return updatedYear;

            errorsList.Add("Nie udało się zmodyfikować roku");
            return null;
        }
    }

}