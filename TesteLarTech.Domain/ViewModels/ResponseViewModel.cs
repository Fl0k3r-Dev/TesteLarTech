using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLarTech.Domain.ViewModels
{
    public class ResponseViewModel<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ResponseViewModel(T data)
        {
            Success = true;
            Data = data;
            Errors = null;
        }

        public ResponseViewModel(IEnumerable<string> errors)
        {
            Success = false;
            Data = default;
            Errors = errors;
        }
    }

}
