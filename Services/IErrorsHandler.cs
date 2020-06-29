using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Services
{
    public interface IErrorsHandler
    {
        public List<string> errorsList { get; }
        public UnauthorizedObjectResult ResponeError();
        public UnauthorizedObjectResult ResponeDeleteError();
    }
}
