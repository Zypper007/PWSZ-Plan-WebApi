using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PWSZ_Plan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorsController : BaseApiController<Major>
    {
        //new private readonly IMajorRepository _repo;

        public MajorsController( IServiceProvider provider): base(provider)
        {
           // _repo = majorRepository;
        }
        #region Endpoints
        // GET api/Majors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var majorsList = await _repo.GetAll();
            var majorListMapped = majorsList.Count>0? _mapper.Map<List<MajorDefaultInformationsDTO>>(majorsList) : null;
            return Ok(majorListMapped);
        }

        // GET api/Majors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMajor(int id)
        {
            var major = await _repo.Get(id);
            var majorMapped = major != null ? _mapper.Map<MajorSendDTO>(major) : null ;
            return Ok(majorMapped);
        }

        // GET api/Majors/{id}/Specializations
        [HttpGet("{id}/Specializations")]
        public async Task<IActionResult> GetSpecializations(int id)
        {
            var specList = await _repo.GetAll<Specialization>(x => x.Major.ID == id);
            var specListMapped = specList.Count > 0 ? _mapper.Map<List<SpecializationDefaultInformationsDTO>>(specList) : null;
            return Ok(specListMapped);
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

        // POST api/Majors/{id}/Specializations 
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPost("{id}/Specializations")]
        public async Task<IActionResult> CreateSpecjalizations(int id, SpecializationDTO specDTO)
        {
            specDTO.MajorID = id;
            if (ValidSpecArgs(specDTO))
            {
                var spec = await CreatingSpecialization(specDTO);
                if (spec != null)
                {
                    var specMapped = _mapper.Map<SpecializationSendDTO>(spec);
                    return Ok(specMapped);
                }
            }
            return ResponeError();
        }

       

        // PATCH api/Majors/{id} 
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMajor(int id, MajorDTO majorDTO)
        {
            var UpdatedMajor = await UpdatingMajor(id, majorDTO);
            if (UpdatedMajor != null)
            {
                var majorMapped = _mapper.Map<MajorSendDTO>(UpdatedMajor);
                return Ok(majorMapped);
            }
            return ResponeError();
        }
        
        // DELETE api/Majors/{id}
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMajor(int id)
        {
            Major deletedMajor = await deletingMajor(id);
            if(deletedMajor != null)
            {
                var majorMapped = _mapper.Map<MajorSendDTO>(deletedMajor);
                return ResponeDelete(majorMapped);
            }

            return ResponeDeleteError();
        }
        #endregion

        #region Private Methods
        private async Task<Major> deletingMajor(int id)
        {
            var major = await _repo.Get(id);
            if (major == null)
            {
                errorsList.Add("Kierunek nie istnieje");
                return null;
            }
            
            if (IsInstitute() && major.Institute.ID != GetInstituteID())
            {
                errorsList.Add("Nie możesz usuwać kierunku z innego wydziału");
                return null;
            }

            var deletedMajor = await _repo.Delete(major);
            if (deletedMajor == null)
            {
                errorsList.Add("Nie udało sie usunąć kierunku");
                return null;
            }

            return deletedMajor;
        }

        private async Task<Specialization> CreatingSpecialization(SpecializationDTO specDTO)
        {
            var major = await _repo.Get((int)specDTO.MajorID);
            if (major == null)
            {
                errorsList.Add("Nie odnaleziono kierunku");
                return null;
            }
            if (IsInstitute() && major.InstituteID != GetInstituteID())
            {
                errorsList.Add("Nie możesz dodawać kierunków do innych wydziałów");
                return null;
            }

            var spec = _mapper.Map<Specialization>(specDTO);
            spec.Major = major;
            var createdSpec = await Create(spec);
            if (createdSpec != null)
                return createdSpec;

            errorsList.Add("Nie udało się stworzyć specjalizacji");
            return null;

        }
        private bool ValidSpecArgs(SpecializationDTO s)
        {
            var f = true;
            if(s.Name == null) { errorsList.Add("Brak nazwy specjalizacji"); f = false; }
            if(s.Name.Length < 3) { errorsList.Add("Nazwa specjalizacji jest za któtka"); f = false; }
            if(s.Name.Length > 50) { errorsList.Add("Nazwa specjalizacji jest za długa"); f = false; }
            if(s.MajorID == null) { errorsList.Add("Brak kierunku"); f = false; }
            return f;
        }

        private async Task<Major> UpdatingMajor(int id, MajorDTO majorDTO)
        {
            var major = await _repo.Get(id);
            if (major == null)
            {
                errorsList.Add("Kierunek nie istnieje");
                return null;
            }

            if (IsInstitute() && GetInstituteID() != major.Institute.ID)
            {
                errorsList.Add("Nie możesz mdyfikować kierunków z innego wydziału");
                return null;
            }

            Tools.CopyValues(major, majorDTO);

            if (IsSuperUser() && majorDTO.InstituteID != null)
            {
                var inst = await _repo.Get<Institute>((int)majorDTO.InstituteID);
                if (inst == null)
                {
                    errorsList.Add("Wydział nie istnieje");
                    return null;
                }
                major.Institute = inst;
            }

            var updatedMajor = await Update(major);
            if (updatedMajor == null)
            {
                errorsList.Add("Nie udało się zmodyfikować kierunku");
                return null;
            }
            return updatedMajor;
        }
        #endregion
    }
}
