using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common
{
    public class ServiceResult
    {

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        
    }
}