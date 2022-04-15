﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Queries.Response
{
    public class ContactQueryResponse
    {
        public string Email { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string DirectoryId { get; set; }
    }
}
