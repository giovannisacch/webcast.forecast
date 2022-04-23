using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCast.Forecast.Domain.Common.CustomExceptions
{
    public static class ErrorConstants
    {
        public const string INVALID_TEMPERATURE = " Temperature cannot be more than 60C and cannot be less than 60C degrees ";
        public const string PAST_FOREACAST_DATE = "Forecast cannot be in the past";
    }
}
