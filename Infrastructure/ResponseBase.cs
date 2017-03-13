using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ResponseBase
    {
        public StatusCode code { get; set; }

        public string message { get; set; }

        public void SetReponse(StatusCode code,string message="")
        {
            this.code = code;
            this.message = message;
        }
    }

    public class ResponseBase<T> : ResponseBase where T: class,new()
    {
        public T data { get; set; }

        public long totalCount { get; set; }

        public void SetReponse(StatusCode code, T data, long totalCount=0,string message="")
        {
            this.code = code;
            this.message = message;
            this.data = data;
            this.totalCount = totalCount;
        }
    }
}
