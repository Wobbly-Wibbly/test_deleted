using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Words
{
    public enum ResponseCodes
    {
        OK = 1,
        UNKNOWN_USER,
        REQUEST_ERROR
    }

    public class ServerResponse
    {
        public string Message { get; set; }
        public string Data { get; set; }
        public ResponseCodes Code { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Description { get; set; }
    }
}
