using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class RequestBase
    {
        public string lang { get; set; }
        public string createUserName { get; set; }
        public string createUser { get; set; }
        public long createTime { get; set; }
    }
}
