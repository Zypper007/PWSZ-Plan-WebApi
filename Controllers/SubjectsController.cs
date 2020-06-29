using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;

namespace PWSZ_Plan_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : BaseApiController<Subject>
    {


        public SubjectsController(IServiceProvider provider) : base(provider) { }

        #region Endpoints
        // GET api/Subjects
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjectsList = await _repo.GetAll();
            var subjectsMapped = subjectsList.Count > 0 ? _mapper.Map<List<SubjectDefaultInformationsDTO>>(subjectsList) : null;
            return Ok(subjectsMapped);
        }

        // GET api/Subjects/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var sub = await _repo.Get(id);
            var subMapped = sub != null ? _mapper.Map<SubjectDefaultInformationsDTO>(sub) : null;
            return Ok(subMapped);
        }

        // POST api/Subjects
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectDTO subjectDTO)
        {
            if(RequiredArgs(subjectDTO))
            {
                var createdSub = await CreatingSubject(subjectDTO);
                var subMapped = _mapper.Map<SubjectDefaultInformationsDTO>(createdSub);
                return Ok(subMapped);
            }
            return ResponeError();
        }

        //PATCH api/Subjects/{id}
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectDTO subjectDTO)
        {
            var updatedSub = await UpdatingSub(id, subjectDTO);
            if (updatedSub != null)
            {
                var subMapped = _mapper.Map<SubjectDefaultInformationsDTO>(updatedSub);
                return Ok(subMapped);
            }
            return ResponeError();
        }

        //DELETE Subjects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var deletedSub = await DeletingSub(id);
            if (deletedSub != null)
            {
                var subMapped = _mapper.Map<SubjectDefaultInformationsDTO>(deletedSub);
                return ResponeDelete(subMapped);
            }
            return ResponeDeleteError();
        }
        #endregion



        #region Private Methods
        private async Task<Subject> CreatingSubject(SubjectDTO subjectDTO)
        {
            var sub = _mapper.Map<Subject>(subjectDTO);
            var createdSub = await _repo.Create(sub);
            if (createdSub == null) errorsList.Add("Nie udało się stworzyc nazwy zajęć");
            return createdSub;
        }

        private bool RequiredArgs(SubjectDTO s)
        {
            var f = true;
            if (s.Name == null) { errorsList.Add("Brak nazwy zajęć"); f = false; }
            if (s.Name.Length < 3) { errorsList.Add("Nazwa Zajęć jest za krótka"); f = false; }
            if (s.Name.Length > 50) { errorsList.Add("Nazwa Zajęć jest za długa"); f = false; }
            return f;
        }

        private async Task<Subject> UpdatingSub(int id, SubjectDTO subjectDTO)
        {
            var sub = await _repo.Get(id);
            if (sub == null)
            {
                errorsList.Add("Nazwa zajęc nie istnieje");
                return null;
            }
            Tools.CopyValues(sub, subjectDTO);
            var updatedSub = await _repo.Update(sub);
            if (updatedSub == null) errorsList.Add("Nie udało się zaktualizować nazwy zajęć");
            return updatedSub;
        }

        private async Task<Subject> DeletingSub(int id)
        {
            var sub = await _repo.Get(id);
            if (sub == null)
            {
                errorsList.Add("Nazwa zajęć nie istnieje");
            }
            var dsub = await _repo.Delete(sub);
            if (dsub == null) errorsList.Add("Nie udało sie usunąć nazwy zajęć");
            return dsub;
        }
        #endregion

    }
}