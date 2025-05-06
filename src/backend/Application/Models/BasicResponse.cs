using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BasicResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public BasicResponse(StatusCode code, string message, T data)
        {
            StatusCode = code;
            Message = message;
            Data = data;
        }
    }
}
