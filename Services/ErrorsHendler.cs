using Microsoft.AspNetCore.Mvc;
using PWSZ_Plan_WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Services
{
    public class ErrorsHendler : ControllerBase, IErrorsHandler
    {
        private List<string> _errorsList = new List<string>();
        public List<string> errorsList { get => _errorsList; }

        public UnauthorizedObjectResult ResponeError()
        {
            var error = new ResponseType.Error(errorsList);
            errorsList.Clear();
            return Unauthorized(error);
        }

        public UnauthorizedObjectResult ResponeDeleteError()
        {
            var error = new ResponseType.Error(errorsList);
            errorsList.Clear();
            return Unauthorized(new ResponseType.Delete(errors: error));
        }
    }
}
