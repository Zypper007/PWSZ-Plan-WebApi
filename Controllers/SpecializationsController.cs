using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;

namespace PWSZ_Plan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : BaseApiController<Specialization>
    {
        //new private readonly ISpecializationsRepository _repo;

        public SpecializationsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //_repo = specializationsRepository;
        }

        #region Endpoints
        // GET api/Specializations
        [Authorize(Roles = Roles.SUPERUSER)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specList = await _repo.GetAll();
            var specListMapped = specList.Count > 0 ? _mapper.Map<List<SpecializationSendDTO>>(specList) : null ;
            return Ok(specListMapped);
        }

        // GET api/Specializations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var spec = await _repo.Get(id);
            var specMapped = spec != null ? _mapper.Map<SpecializationSendDTO>(spec) : null ;
            return Ok(specMapped);
        }

        // GET api/Specializations/{id}/Years
        [HttpGet("{id}/Years")]
        public async Task<IActionResult> GetYears(int id)
        {
            var yearsList = await _repo.GetAll<Year>(x => x.Specialization.ID == id);
            var yearsListMapped = yearsList.Count > 0 ? _mapper.Map<List<YearDefaultInformationsDTO>>(yearsList) : null ;
            return Ok(yearsListMapped);
        }

        // GET api/Majors/{id}/Plan
        [HttpGet("{id}/Plan")]
        public async Task<IActionResult> GetPlan(int id)
        {
            return Ok(id);
        }

        // GET api/Majors/{id}/Plans
        [HttpGet("{id}/Plans")]
        public async Task<IActionResult> GetPlans(int id)
        {
            return Ok(id);
        }

        // GET api/Majors/{id}/Classes
        [HttpGet("{id}/Classes")]
        public async Task<IActionResult> GetClasses(int id)
        {
            return Ok(id);
        }

        // POST api/Specializations/{id}/Year 
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpPost("{id}/Years")]
        public async Task<IActionResult> CreateYear(int id, YearDTO yearDTO)
        {
            yearDTO.SpecializationID = id;
            if (ValidYearArgs(yearDTO))
            {
                var year = await CreatingYear(yearDTO);
                if (year != null)
                {
                    var yearMapped = _mapper.Map<YearSendDTO>(year);
                    return Ok(yearMapped);
                }
            }
            return ResponeError();
        }


        // PATCH api/Specializations/{id} 
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSpec(int id, SpecializationDTO specDTO)
        {
            var updatedSpec = await UpdatingSpec(id, specDTO);
            if (updatedSpec != null)
            {
                var specMapped = _mapper.Map<SpecializationSendDTO>(updatedSpec);
                return Ok(specMapped);
            }

            return ResponeError();
        }

        // DELETE api/Majors/{id}
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedSpec = await deletingSpec(id);
            if (deletedSpec != null)
            {
                var specMapped = _mapper.Map<SpecializationSendDTO>(deletedSpec);
                return ResponeDelete(specMapped);
            }
            return ResponeDeleteError();
        }
        #endregion
        #region Private Methods
        private async Task<Specialization> deletingSpec(int id)
        {
            var spec = await _repo.Get(id);
            if (spec == null)
            {
                errorsList.Add("Specializacja nie istnieje");
                return null;
            }

            if (IsInstitute() && spec.Major.Institute.ID != GetInstituteID())
            {
                errorsList.Add("Nie możesz usuwać specializacji kierunków z innego wydziału");
                return null;
            }

            var deletedSpec = await _repo.Delete(spec);
            if (deletedSpec != null)
                return deletedSpec;
            
            errorsList.Add("Nie udało sie usunąć specializacji");
            return null;
        }

        private async Task<Year> CreatingYear(YearDTO yearDTO)
        {
            var spec = await _repo.Get((int)yearDTO.SpecializationID);
            if (spec == null)
            {
                errorsList.Add("Nie odnaleziono specializacji");
                return null;
            }
            if (IsInstitute() && spec.Major.InstituteID != GetInstituteID())
            {
                errorsList.Add("Nie możesz dodawać specializacji kierunków do innych wydziałów");
                return null;
            }

            var year = _mapper.Map<Year>(yearDTO);
            year.Specialization = spec;
            var createdYear = await _repo.Create(year);
            if (createdYear == null)
                errorsList.Add("Nie udało się stworzyć roku");
            
            return createdYear;
        }

        private bool ValidYearArgs(YearDTO y)
        {
            var f = true;
            if (y.Name == null) { errorsList.Add("Brak nazwy roku"); f = false; }
            if (y.Name.Length < 3) { errorsList.Add("Nazwa roku jest za któtka"); f = false; }
            if (y.Name.Length > 50) { errorsList.Add("Nazwa roku jest za długa"); f = false; }
            if (y.SpecializationID == null) { errorsList.Add("Brak specializacji"); f = false; }
            return f;
        }

        private async Task<Specialization> UpdatingSpec(int id, SpecializationDTO specDTO)
        {
            var spec = await _repo.Get(id);
            if (spec == null)
            {
                errorsList.Add("Specializacja nie istnieje");
                return null;
            }

            if (IsInstitute() && GetInstituteID() != spec.Major.Institute.ID)
            {
                errorsList.Add("Nie możesz mdyfikować kierunków z innego wydziału");
                return null;
            }

            Tools.CopyValues(spec, specDTO);

            if (IsSuperUser() && specDTO.MajorID != null)
            {
                var major = await _repo.Get<Major>((int)specDTO.MajorID);
                if (major == null)
                {
                    errorsList.Add("Wydział nie istnieje");
                    return null;
                }
                spec.Major = major;
            }

            var UpdatedSpec = await _repo.Update(spec);
            if (UpdatedSpec != null)
                return UpdatedSpec;
            
            errorsList.Add("Nie udało się zmodyfikować kierunku");
            return null;
        }
        #endregion
    }
}
