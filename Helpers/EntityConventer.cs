using AutoMapper;
using PWSZ_Plan_WebApi.Data;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers
{
    public class EntityConventer<T> : ITypeConverter<int, T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repo;

        public EntityConventer(IGenericRepository<T> repository)
        {
            _repo = repository;
        }

        public T Convert(int id, T destination, ResolutionContext context)
        {
            T obj = _repo.Get(id).Result;
            return obj;
        }
    }
}
