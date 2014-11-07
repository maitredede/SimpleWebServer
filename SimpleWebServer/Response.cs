using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebServer
{
    public sealed class Response
    {
        public string Encoding { get; set; }
        public byte[] Content { get; set; }
    }
}
