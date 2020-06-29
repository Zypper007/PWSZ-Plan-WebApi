using AutoMapper;
using PWSZ_Plan_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers
{
    public class CodeConventer : ITypeConverter<string, byte[]>
    {
        private readonly ICodeGenerator _generator;

        public CodeConventer(ICodeGenerator generator)
        {
            _generator = generator;
        }
        public byte[] Convert(string source, byte[] destination, ResolutionContext context)
        {
            return _generator.HashingCode(source);
        }
    }
}
