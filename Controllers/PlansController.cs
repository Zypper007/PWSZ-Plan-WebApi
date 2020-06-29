using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;

namespace PWSZ_Plan_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : BaseApiController<Plan>
    {
        public PlansController(IServiceProvider provider) : base(provider)
        {

        }
        // GET api/Plans
        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _repo.GetAll();
            var plansMapped = plans.Count > 0 ? _mapper.Map<PlanDefaultInformationsDTO>(plans) : null;
            return Ok(plansMapped);
        }


        // GET api/Plans/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan(int id)
        {
            var plan = await _repo.Get(id);
            var planMapped = plan != null ? _mapper.Map<PlanDefaultInformationsDTO>(plan) : null;
            return Ok(planMapped);
        }


        // GET api/Plans/{id}/Informations
        [HttpGet("{id}/Informations")]
        public async Task<IActionResult> GetInformations(int id)
        {
            return Ok(id);
        }

        // POST api/Plans
        [HttpPost]
        public async Task<IActionResult> CreatePlan(PlanDTO planDTO)
        {
            if(ValidRequireArgs(planDTO))
            {
                Plan createdPlan = await CreatingPlan(planDTO);
                if(createdPlan != null)
                {
                    var mappedPlan = _mapper.Map<PlanDefaultInformationsDTO>(createdPlan);
                    return Ok(mappedPlan);
                }
            }
            return ResponeError();
        }

        private async Task<Plan> CreatingPlan(PlanDTO planDTO)
        {
            if (planDTO.EndDate > ((DateTime)planDTO.StartDate).AddDays(1))
            {
                errorsList.Add("Data zakończenia nie może być większa od daty rozpoczęcia + 1 dzień");
                return null;
            }
            if (planDTO.EndSessionDate > ((DateTime)planDTO.StartSessionDate).AddDays(1))
            {
                errorsList.Add("Data zakończenia sesji nie może być większa od daty rozpoczęcia sesji + 1 dzień");
                return null;
            }

            var year = await _repo.Get<Year>((int)planDTO.YearID);
            if (year == null)
            {
                errorsList.Add("Podany rok nie istnieje");
                return null;
            }

            if (IsInstitute() && GetInstituteID() != year.Specialization.Major.InstituteID)
            {
                errorsList.Add("Nie możesz dodawać planów nie do innych wydziałów");
                return null;
            }

            var plan = new Plan();
            plan.Year = year;
            plan.YearID = year.ID;
            plan.StartDate = (DateTime)planDTO.StartDate;
            plan.EndDate = (DateTime)planDTO.EndDate;
            plan.StartSessionDate = planDTO.StartSessionDate;
            plan.EndSessionDate = planDTO.EndSessionDate;
            foreach (int ClassID in planDTO.ClassesID)
            {

            }
            return null;
        }

        private bool ValidRequireArgs(PlanDTO p)
        {
            var f = true;
            if(p.ClassesID == null || p.ClassesID.Count == 0) { errorsList.Add("Brak zajęć"); f = false; }
            if(p.EndDate == null) { errorsList.Add("Brak daty zakończenia planu"); f = false; }
            if(p.StartDate == null) { errorsList.Add("Brak daty rozpoczęcia planu"); f = false; }
            if(p.YearID == null) { errorsList.Add("Plan nie jest przypisany do roku"); f = false; }
            return f;
        }

        // PATCH api/Plans/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, PlanDTO planDTO)
        {
            return Ok(new { id = id, plan = planDTO });
        }

        // DELETE api/Plans/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            return Ok(id);
        }

    }
}