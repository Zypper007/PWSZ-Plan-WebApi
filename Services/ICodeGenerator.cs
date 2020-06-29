using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Services
{
    public interface ICodeGenerator
    {
        public byte[] HashingCode(string code);
        public bool VeryfingCode(string code, byte[] hashCode);
    }
}
