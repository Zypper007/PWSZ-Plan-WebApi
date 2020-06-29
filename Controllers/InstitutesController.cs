using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PWSZ_Plan_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : BaseApiController<Institute>
    {
        public InstitutesController(IServiceProvider provider) : base(provider) { }
        
        // GET: api/Institutes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instutesList = await _repo.GetAll();
            var instutesListMapped = instutesList.Count>0? _mapper.Map<List<InstituteDefaultInformationsDTO>>(instutesList): null;
            return Ok(instutesListMapped);
        }

        // GET api/Institutes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitute(int id)
        {
            var inst = await _repo.Get(id);
            var mappedInst = inst!=null? _mapper.Map<InstituteSendDTO>(inst) : null;
            return Ok(mappedInst);
        }

        // GET api/Institutes/{id}/Plans
        [HttpGet("{id}/Plans")]
        public async Task<IActionResult> GetPlans(int id)
        {
            return Ok(id);
        }

        // GET api/Institutes/{id}/Classes
        [HttpGet("{id}/Classes")]
        public async Task<IActionResult> GetClasses(int id)
        {
            return Ok(id);
        }

        // GET api/Institutes/{id}/Majors
        [HttpGet("{id}/Majors")]
        public async Task<IActionResult> GetMajors(int id)
        {
            var majorsList = await _repo.GetAll<Major>(x => x.InstituteID == id);
            var majorsListMapped = majorsList.Count>0? _mapper.Map<List<MajorDefaultInformationsDTO>>(majorsList) : null;
            return Ok(majorsListMapped);
        }

        // POST api/Institutes
        [Authorize(Roles = Roles.SUPERUSER)]
        [HttpPost]
        public async Task<IActionResult> CreateInstitute(InstituteDTO instituteDTO)
        {
            if (ValidInstituteArgs(instituteDTO))
            {
                var institute = _mapper.Map<Institute>(instituteDTO);
                var createdInstitute = await Create(institute);
                var createdInstituteMapped = _mapper.Map<InstituteSendDTO>(createdInstitute);
                return Ok(createdInstituteMapped);
            }
            return ResponeError();
        }

        

        // POST api/Institutes/{id}/Majors
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPost("{id}/Majors")]
        public async Task<IActionResult> CreateMajor(int id, MajorDTO majorDTO)
        {
            majorDTO.InstituteID = id;
            var major = await CreatingMajor(majorDTO);
            if(major != null)
            {
                var majorMapped = _mapper.Map<MajorSendDTO>(major);
                return Ok(majorMapped);
            }

            return ResponeError();
        }

        // PATCH api/Institutes/{id}
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateInstitute(int id, InstituteDTO instituteDTO)
        {
            var updatedInstitute = await UpdatingInsitute(id, instituteDTO);
            if(updatedInstitute != null)
            {
                var updatedInstituteMapped = _mapper.Map<InstituteSendDTO>(updatedInstitute);
                return Ok(updatedInstituteMapped);
            }

            return ResponeError();
        }

        // DELETE api/Institutes
        [Authorize(Roles = Roles.SUPERUSER)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute(int id)
        {
            var inst = await _repo.Get(id);
            if (inst != null)
            {
                var instToReturn = await _repo.Delete(inst);
                if (instToReturn != null)
                {
                    var instMapped = _mapper.Map<InstituteSendDTO>(instToReturn);
                    return ResponeDelete(instMapped);
                }
                else errorsList.Add("Nie udało się usunąć wydziału");
            }
            else errorsList.Add("Nie odnaleziono wydziału");
            return ResponeDeleteError();
        }

        private async Task<Major> CreatingMajor(MajorDTO majorDTO)
        {
            if (IsInstitute() && !IsSameId((int)majorDTO.InstituteID))
            {
                errorsList.Add("Nie możesz dodawać kierunków w innym wydziale");
                return null;
            }

            if (ValidMajor(majorDTO))
            {
                var major = _mapper.Map<Major>(majorDTO);
                if (major.Institute == null)
                {
                    errorsList.Add("Brak wydziału. Kierunek nie może istnieć bez wydziału");
                    return null;
                }
                var createdMajor = await Create(major);
                return createdMajor;

            }

            return null;
        }

        private bool ValidMajor(MajorDTO m)
        {
            var f = true;
            if(m.InstituteID == null) { errorsList.Add("Brak wydziału. Kierunek nie może istnieć bez wydziału"); f = false; }
            if(m.Name == null) { errorsList.Add("Brak nazwy kierunku"); f = false; }
            if(m.Name.Length < 3) { errorsList.Add("Nazwa jest za krótka"); f = false; }
            if(m.Name.Length > 50) { errorsList.Add("Nazwa jest za długa"); f = false; }
            return f;
        }

        private async Task<Institute> UpdatingInsitute(int id, InstituteDTO instituteDTO)
        {
            if (IsInstitute() && !IsSameId(id))
            {
                errorsList.Add("Nie możesz modyfikować innego wydziału");
                return null;
            }

            var institute = await _repo.Get(id);
            if (institute == null)
            {
                errorsList.Add("Instutut nie istnieje");
                return null;
            }

            Tools.CopyValues(institute, instituteDTO);
            var updatedInsitute = await _repo.Update(institute);
            if(updatedInsitute != null)
                return updatedInsitute;

            errorsList.Add("Nie udało się zaktualizować wydziału");
            return null;
        }

        private bool ValidInstituteArgs(InstituteDTO i)
        {
            var f = true;
            if (i.Name == null) { errorsList.Add("Brak nazwy wydziału"); f = false; }
            if (i.Name.Length < 3) { errorsList.Add("Nazwa jest za krótka"); f = false; }
            if (i.Name.Length > 50) { errorsList.Add("Nazwa jest za długa"); f = false; }
            return f;
        }
    }
}
