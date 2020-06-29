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
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : BaseApiController<Class>
    {
        public ClassesController(IServiceProvider provider) : base(provider)
        {
        }

        // GET api/Classes/
        [HttpGet]
        public async Task<IActionResult> GetClasses()
        {
            var classesList = await _repo.GetAll();
            var classesListMapped = classesList.Count > 0 ? _mapper.Map<List<ClassDefaultInformationsDTO>>(classesList) : null;
            return Ok(classesListMapped);
        }

        // GET api/Classes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var @class = await _repo.Get(id);
            var classMapped = @class != null ? _mapper.Map<ClassDefaultInformationsDTO>(@class) : null;
            return Ok(classMapped);
        }

        // GET api/Classes/{id}/Informations
        [HttpGet("{id}/Informations")]
        public async Task<IActionResult> GetInformations(int id)
        {
            return Ok(id);
        }

        // POST api/Classes
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPost]
        public async Task<IActionResult> CreateClass(ClassDTO classDTO)
        {
            if(RequiredArgs(classDTO))
            {
                Class createdClass = await CreatingClass(classDTO);
                if (createdClass != null)
                {
                    var classMapped = _mapper.Map<ClassDefaultInformationsDTO>(createdClass);
                    return Ok(classMapped);
                }
            }
            return ResponeError();
        }

        // POST api/Classes/{id}/Informations 
        [HttpPost("{id}/Informations")]
        public async Task<IActionResult> CreateInformation(int id, InformationToCreateDTO informationDTO)
        {
            return Ok(new { id = id, inf = informationDTO });
        }

        // PATCH api/Classes/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClass(int id, ClassDTO classDTO)
        {
            return Ok(new { id = id, @class= classDTO });
        }

        // DELETE api/Classes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            return Ok(id);
        }

        #region Private Methods
        private async Task<Class> CreatingClass(ClassDTO classDTO)
        {
            if (!await _repo.IsExist<Room>(r => r.ID == classDTO.RoomID))
            {
                errorsList.Add("Podana sala nie istnieje");
                return null;
            }
            if (!await _repo.IsExist<Subject>(s => s.ID == classDTO.SubjectID))
            {
                errorsList.Add("Podana nazwa przedmiotu nie istnieje");
                return null;
            }

            var lecturer = await _repo.Get<User>((int)classDTO.LecurerID);
            if (lecturer == null)
            {
                errorsList.Add("Wykładowca nie istnieje");
                return null;
            }
            if (lecturer.Permission != Permission.LECTURER)
            {
                errorsList.Add("Użytkownik nie może być wykładowcą");
                return null;
            }

            var @class = _mapper.Map<Class>(classDTO);
            var createdClass = await Create(@class);
            if (createdClass == null) errorsList.Add("Nie udało się stworzyć zajęć");
            return createdClass;
        }

        private bool RequiredArgs(ClassDTO c)
        {
            var flag = true;
            if (c.LecurerID == null) { errorsList.Add("Brak prowadzącego zajęcia"); flag = false; }
            if (c.SubjectID == null) { errorsList.Add("Brak nazwy zajęć"); flag = false; }
            if (c.RoomID == null) { errorsList.Add("Brak klasy dla zajęć"); flag = false; }
            if (c.TotalHours == null) { errorsList.Add("Brak całkowitej ilości godzin"); flag = false; }
            if (c.TotalHours < 1) { errorsList.Add("Liczba godzin nie może być 0 lub ujemna"); flag = false; }
            if (c.Start == null) { errorsList.Add("Brak określenia daty rozpoczęcia zajęć"); flag = false; }
            if (c.Start < TimeSpan.Zero) { errorsList.Add("Rozpoczęcie zajęć nie może być ujemne"); flag = false; }
            if (c.Duration == null) { errorsList.Add("Brak długości zajęć"); flag = false; }
            if (c.Duration < TimeSpan.Zero) { errorsList.Add("Długość zajęć nie może być ujemna"); flag = false; }
            if (c.Recurrence == null) { errorsList.Add("Nie określono powtarzalności"); flag = false; }
            if (c.Recurrence <= (TimeSpan.Zero + new TimeSpan(TimeSpan.TicksPerHour) * 0.75)) { errorsList.Add("Powtarzalność zajęć nie może być mniejsza niż 45 minut"); flag = false; }
            return flag;
        }
        #endregion
    }
}