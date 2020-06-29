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
    public class RoomsController : BaseApiController<Room>
    {


        public RoomsController(IServiceProvider provider) : base(provider) { }

        #region Endpoints
        // GET api/Rooms
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var roomsList = await _repo.GetAll();
            var rlM = roomsList.Count > 0 ? _mapper.Map<List<RoomDefaultInformationsDTO> >(roomsList) : null;
            return Ok(rlM);
        }

        // GET  api/Rooms/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _repo.Get(id);
            var rlM = room!=null ? _mapper.Map<RoomDefaultInformationsDTO>(room) : null;
            return Ok(rlM);
        }

        // GET api/Rooms/Free/{year},{month},{day},{hours},{minuts}
        [AllowAnonymous]
        [HttpGet("Free/{year},{month},{day},{hours},{minuts}")]
        public async Task<IActionResult> GetFreeRooms(int? year, int? month, int? day, int? hours, int? minutes)
        {
            return Ok(new { 
                year = year,
                month = month,
                day = day,
                hours = hours,
                minutes = minutes
            });
        }

        // GET  api/Rooms/{id}/Plan
        [HttpGet("{id}/Plan")]
        public async Task<IActionResult> GetPlan(int id)
        {
            return Ok(id);
        }

        // GET  api/Rooms/{id}/Plans
        [HttpGet("{id}/Plans")]
        public async Task<IActionResult> GetPlans(int id)
        {
            return Ok(id);
        }

        // POST  api/Rooms
        [Authorize(Roles = Roles.SUPERUSER+","+Roles.INSTITUTE)]
        [HttpPost]
        public async Task<IActionResult> CreateRoom(RoomDTO roomDTO)
        {
            if(RequiredArgs(roomDTO))
            {
                var createdRoom = await creatingRoom(roomDTO);
                if (createdRoom != null)
                {
                    var roomMapped = _mapper.Map<RoomDefaultInformationsDTO>(createdRoom);
                    return Ok(roomMapped);
                }

            }
            return ResponeError();
        }

        // PATCH  api/Rooms/{id}
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, RoomDTO roomDTO)
        {
            var room = await UpdatingRoom(id, roomDTO);
            if (room != null)
            {
                var roomMapped = _mapper.Map<RoomDefaultInformationsDTO>(room);
                return Ok(roomMapped);
            }
            return ResponeError();
        }

        // DELETE  api/Rooms/{id}
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await DeletingRoom(id);
            if (room != null)
                return ResponeDelete(room);
            return ResponeDeleteError();
        }
        #endregion
        #region Private Methods
        private async Task<Room> DeletingRoom(int id)
        {
            var room = await _repo.Get(id);
            if (room == null)
            {
                errorsList.Add("Sala nie istnieje");
                return null;
            }

            var deletedRoom = await _repo.Delete(room);
            if (deletedRoom == null) errorsList.Add("Nie udało się usunąć sali");
            return deletedRoom;
        }

        private async Task<Room> UpdatingRoom(int id, RoomDTO roomDTO)
        {
            var room = await _repo.Get(id);
            if (room == null)
            {
                errorsList.Add("Sala nie istnieje");
                return null;
            }

            Tools.CopyValues(room, roomDTO);
            var updatedRoom = await _repo.Update(room);
            if (updatedRoom == null) errorsList.Add("Nie udało się zaktualizować sali");
            return updatedRoom;
        }

        private async Task<Room> creatingRoom(RoomDTO roomDTO)
        {
            var room = _mapper.Map<Room>(roomDTO);
            var createdRoom = await _repo.Create(room);
            if (createdRoom == null)
                errorsList.Add("Nie udało się stworzyć sali");

            return createdRoom;
        }

        private bool RequiredArgs(RoomDTO r)
        {
            var f = true;
            if (r.Name == null) { errorsList.Add("Brak nazwy sali"); f = false; }
            if (r.Name.Length < 1) { errorsList.Add("Nazwa sali jest pusta"); f = false; }
            if (r.Name.Length > 50) { errorsList.Add("Nazwa sali jest za długa"); f = false; }
            return f;
        }
        #endregion
    }
}