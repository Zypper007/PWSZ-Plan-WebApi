using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.Helpers;
using PWSZ_Plan_WebApi.Models;
using PWSZ_Plan_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PWSZ_Plan_WebApi.Controllers
{
    public abstract class BaseApiController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IErrorsHandler _errors;
        protected readonly IMapper _mapper;
        protected readonly IGenericRepository<TEntity> _repo;
        private readonly IServiceProvider _serviceProvider;

        public BaseApiController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _errors = _serviceProvider.GetService<IErrorsHandler>();
            _mapper = _serviceProvider.GetService<IMapper>();
            _repo = _serviceProvider.GetService<IGenericRepository<TEntity>>();
        }


        protected async Task<TOtherEntity> Create<TOtherEntity>(TOtherEntity entity) where TOtherEntity : BaseEntity
        {
            var createdEntity = await _repo.Create<TOtherEntity>(AddLastUpdateBy(entity));
            return createdEntity;
        }

        protected async Task<TEntity> Create(TEntity entity)
        {
            var createdEntity = await _repo.Create(AddLastUpdateBy(entity));
            return createdEntity;
        }

        protected async Task<TEntity> Update(TEntity entity)
        {
            var updatedEntity = await _repo.Update(AddLastUpdateBy(entity));
            return updatedEntity;
        }

        private TSomeEntity AddLastUpdateBy<TSomeEntity>(TSomeEntity entity) where TSomeEntity : BaseEntity
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id;
            if (int.TryParse(identity.FindFirst(ClaimTypes.NameIdentifier).Value, out id))
                entity.LastUpdateBy = id;
            return entity;
        }

        #region errors hendler and resulting
        protected List<string> errorsList => _errors.errorsList;
        protected UnauthorizedObjectResult ResponeError() => _errors.ResponeError();
        protected UnauthorizedObjectResult ResponeDeleteError() => _errors.ResponeDeleteError();

    
        public OkObjectResult ResponeDelete(object item)
        {
            return base.Ok(new ResponseType.Delete(item: item));
        }
        #endregion

        #region tests
        protected int GetInstituteID()
        {
            if (!IsInstitute())
                return -1;
            return int.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("Institute").Value);
        }

        protected bool IsSuperUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.FindFirst(ClaimTypes.Role).Value == Roles.SUPERUSER)
                return true;

            return false;
        }

        protected bool IsInstitute()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.FindFirst(ClaimTypes.Role).Value == Roles.INSTITUTE)
                return true;

            return false;
        }

        protected bool IsSameId(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.FindFirst(ClaimTypes.NameIdentifier).Value == id.ToString())
                return true;

            return false;
        }

        protected bool PermissionAreLower(Permission? perm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (perm != null && Enum.Parse<Permission>(identity.FindFirst(ClaimTypes.Role).Value) < perm)
                return true;

            return false;
        }

        protected bool PermissionAreSome(Permission? perm)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (perm != null && Enum.Parse<Permission>(identity.FindFirst(ClaimTypes.Role).Value) == perm)
                return true;

            return false;
        }
        #endregion
    }
}
