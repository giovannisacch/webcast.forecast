using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCast.Forecast.Domain.Common.CustomExceptions
{
    public class DomainLogicException : Exception
    {

        public DomainLogicException(string message)
            : base(message)
        {
        }

        public DomainLogicException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
