using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Common
{
    public class EmptyResponse
    {
        public static readonly EmptyResponse Default = new();
    }
}
