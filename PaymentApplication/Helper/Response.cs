using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApplication.Helper
{
    public class Response<T>
    {
       
            public ResponseCodes Code { get; set; }
            public int Result
            {
                get
                {
                    return (int)Code;
                }
            }
        public T Payload { get; set; }
            public string Description { get; set; }
    }

    public enum ResponseCodes
    {
        EXCEPTION = -4,
        NOT_FOUND = -3,
        INVALID_REQUEST = -2,
        [Description("Server error occured, please try again.")]
        ERROR = -1,
        [Description("FAIL")]
        FAIL = 2,
        [Description("SUCCESS")]
        OK = 1,
    }
}
