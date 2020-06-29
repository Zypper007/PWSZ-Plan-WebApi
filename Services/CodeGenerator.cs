using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Services
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly IConfiguration _configuration;

        public CodeGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] HashingCode(string code)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(
                                    Encoding.UTF8.GetBytes(
                                        _configuration.GetSection("HashSaltCode").Value)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(code));
            }
        }

        public bool VeryfingCode(string code, byte[] hashCode)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(
                                    Encoding.UTF8.GetBytes(
                                        _configuration.GetSection("HashSaltCode").Value)))
            {
                var computtedCode = hmac.ComputeHash(Encoding.UTF8.GetBytes(code));

                for (int i = 0; i < computtedCode.Length; i++)
                {
                    if (computtedCode[i] != hashCode[i])
                        return false;
                }

                return true;
            }
        }
    }
}
