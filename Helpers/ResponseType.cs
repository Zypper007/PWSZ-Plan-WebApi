using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers
{
    public static class ResponseType
    {
        public class Error
        {
            public Error(IEnumerable<string> messages)
            {
                Type = "Error";
                Messages = new List<string>(messages);
            }
            public string Type { get; }
            public List<string> Messages { get; set; }
        }

        public class Delete
        {
            public Delete(object? item = null, Error? errors = null)
            {
                Type = "DeleteResponse";
                if (errors != null)
                    Error = errors;
                if (item != null || Item != null)
                {
                    Item = item;
                    Status = true;
                }
                else Status = false;
            }
            public string Type { get; }
            public Error? Error { get; set; }
            public object? Item { get; set; }
            public bool Status { get; }
        }
    }
}
