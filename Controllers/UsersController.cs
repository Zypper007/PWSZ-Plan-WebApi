using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Services;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PWSZ_Plan_WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController<User>
    {
        new private readonly IUserRepository _repo;

        public UsersController(IUserRepository userRepository, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _repo = userRepository;
        }


        #region Endpoints
        // GET: api/Users
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userList = await GetUsers();
            var userListMapped = userList.Count>0? _mapper.Map<List<UserSendDTO>>(userList) : null;
            return Ok(userListMapped);
        }

        // GET api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await GetUserByID(id);
            var userMapped = user!=null? _mapper.Map<UserSendDTO>(user):null;
            return Ok(userMapped);
        }

        // GET api/Users/{id}/Plan
        [AllowAnonymous]
        [HttpGet("{id}/Plan")]
        public async Task<IActionResult> GetCurrentPlan(int id)
        {
            return Ok(id);
        }

        // GET api/Users/{id}/Plans
        [HttpGet("{id}/Plans")]
        public async Task<IActionResult> GetPlans(int id)
        {
            //var plans = await _repo.GetAll<Plan>(x => x.Classes.All(c => c.Class.LecturerID == id));
            return Ok(id);
        }

        // POST api/Users
        [Authorize(Roles = Roles.SUPERUSER + "," + Roles.INSTITUTE)]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            if (RequiredArgs(userDTO))
            {
                var user = await CreatingUser(userDTO);
                if (user != null)
                {
                    var userMapped = _mapper.Map<UserSendDTO>(user);
                    return Ok(userMapped);
                }
            }
            return ResponeError();
        }

        // PATCH api/Users/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            var user = await UpdatingUser(id, userDTO);
            if (user != null)
            {
                var userMapped = _mapper.Map<UserSendDTO>(user);
                return Ok(userMapped);
            }

            return ResponeError();
        }

        // DELETE api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await RemoveUser(id);
            if (user != null)
            {
                var userMapped = _mapper.Map<UserSendDTO>(user);
                return ResponeDelete(userMapped);
            }

            return ResponeDeleteError();
        }

        #endregion
        #region Privite Methods
        private async Task<List<User>> GetUsers()
        {
            var usersList = await _repo.GetAll();
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (IsInstitute())
                usersList = usersList.Where(x => x.Permission < Permission.INSTITUTE).ToList();

            return usersList;
        }

        private async Task<User> GetUserByID(int id)
        {
            if (IsSuperUser())
            {
                var user = await _repo.Get(id);
                return user;
            }
            if (IsInstitute())
            {
                var user = await _repo.Get(id);
                if (PermissionAreLower(user.Permission))
                    return user;
            }
            if (IsSameId(id))
            {
                var user = await _repo.Get(id);
                return user;
            }

            return null;
        }

        

        private async Task<User> CreatingUser(UserDTO userDTO)
        {
            if (IsInstitute() && !PermissionAreLower(userDTO.Permission))
            {
                errorsList.Add("Nie możliwe jest stworzenie użytkownika z większymi uprawnieniami");
                return null;
            }

            if (await _repo.IsExist(x=> x.Name == userDTO.Name))
            {
                errorsList.Add("Nazwa użytkownika jest już zajęta");
                return null;
            }

            var user = _mapper.Map<User>(userDTO);
            if(userDTO.InstituteID != null && user.Institute == null)
            {
                errorsList.Add("Nie odnaleziono wydziału");
                return null;
            }
            var userReturned = await Create(user);

            return userReturned;
        }

        private async Task<User> UpdatingUser(int id, UserDTO userDTO)
        {
            if(!IsSuperUser())
            {
                if(userDTO.InstituteID != null || IsInstitute() && GetInstituteID() != userDTO.InstituteID)
                {
                    errorsList.Add("Nie możesz zmieniać wydziału");
                    return null;
                }
                if (userDTO.Permission != null && IsSameId(id) && !PermissionAreSome(userDTO.Permission))
                {
                    errorsList.Add("Nie możesz zmieniać sobie uprawnień");
                    return null;
                }
                if (userDTO.Permission != null && !PermissionAreLower(userDTO.Permission))
                {
                    errorsList.Add("Nie możesz podnosić uprawnień");
                    return null;
                }
                if(!IsInstitute() && !IsSameId(id))
                {
                    errorsList.Add("Nie możesz modyfikować innych użytkowników");
                    return null;
                }
                if (userDTO.Name.Length < 3) 
                { 
                    errorsList.Add("Nazwa użytkownika jest za krótka");
                    return null ; 
                }
                if (userDTO.Name.Length > 30) 
                { 
                    errorsList.Add("Nazwa użytkownika jest za długa"); 
                    return null; 
                }
                if (Tools.ValidIsNumber(userDTO.Name)) 
                { 
                    errorsList.Add("Nazwa użytkownika zawiera niedozwolone znaki - liczby"); 
                    return null; 
                }
                if (Tools.ValidIsSpecial(userDTO.Name)) 
                { 
                    errorsList.Add("Nazwa użytkownika zawiera niedozwolone znaki - znaki specjalne");
                    return null;  
                }
            }
            Institute inst = null;
            if (userDTO.InstituteID != null)
            {
                inst = await _repo.Get<Institute>((int)userDTO.InstituteID);
                if(inst == null)
                {
                    errorsList.Add("Wydział nie istnieje");
                    return null;
                }
            }


            var user = await _repo.Get(id);
            if(user == null)
            {
                errorsList.Add("Użytkownik nie istnieje");
                return null;
            }

            Tools.CopyValues(user, userDTO);
            user.Institute = inst;
            var updatedUser = await Update(user);
            if (updatedUser != null)
                return updatedUser;

            errorsList.Add("Nie udało zmodyfikować użytkownika");
            return null;
        }
       

        private async Task<User> RemoveUser(int id)
        {
            if (IsSameId(id))
                return await Deleting(await _repo.Get(id));

            if (IsSuperUser() || IsInstitute())
            {
                var user = await _repo.Get(id);
                if (user == null)
                {
                    errorsList.Add("Użytkownik nie istnieje");
                    return null;
                }

                if (IsSuperUser())
                    return await Deleting(user);

                if(IsInstitute() && PermissionAreLower(user.Permission))
                    return await Deleting(user);
            }
            errorsList.Add(Messages.NotAllowAction);
            return null;
        }
        #endregion

        #region Untils

        private bool RequiredArgs(UserDTO u)
        {
            var f = true;
            // walidacja kodu
            if (u.Code == null) { errorsList.Add("Brak kodu dla użytkownika"); f = false; }
            if (u.Code.Length < 6) { errorsList.Add("Kod jest za krótki"); f = false; }
            if (u.Code.Length > 20) { errorsList.Add("Kod jest za długi"); f = false; }
            if (!Tools.ValidIsLower(u.Code)) { errorsList.Add("Brak małych liter w kodzie"); f = false; }
            if (!Tools.ValidIsUpper(u.Code)) { errorsList.Add("Brak dużych liter w kodzie"); f = false; }
            if (!Tools.ValidIsSpecial(u.Code)) { errorsList.Add("Brak znaków specjalnuch w kodzie"); f = false; }
            if (!Tools.ValidIsNumber(u.Code)) { errorsList.Add("Brak liczby w kodzie"); f = false; }
            // koniec walidacji kodu
            // walidacja czy użutkownik jest wykładowcą
            if (u.IsLecturer == null) { errorsList.Add("Brak opcji czy użytkownik jest wykładowcą"); f = false; }
            // walidacja nazwy
            if (u.Name == null) { errorsList.Add("Brak nazwy użytkownika"); f = false; }
            if (u.Name.Length < 3) { errorsList.Add("Nazwa użytkownika jest za krótka"); f = false; }
            if (u.Name.Length > 30) { errorsList.Add("Nazwa użytkownika jest za długa"); f = false; }
            if (Tools.ValidIsNumber(u.Name)) { errorsList.Add("Nazwa użytkownika zawiera niedozwolone znaki - liczby"); f = false; }
            if (Tools.ValidIsSpecial(u.Name)) { errorsList.Add("Nazwa użytkownika zawiera niedozwolone znaki - znaki specjalne"); f = false; }
            // koniec walidacji nazwy
            // walidacja uprawnien
            if (u.Permission == null) { errorsList.Add("Brak nadanych uprawnien"); f = false; }
            return f;
        }
            private async Task<User> Deleting(User user)
        {
            var deletedUser = await _repo.Delete(user);
            return deletedUser;
        }
        #endregion
    }
}
