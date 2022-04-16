using AutoMapper;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Response;
using RiseAssesment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateContactCommandRequest, Contact>();
            CreateMap<Contact, ListContactQueryResponse>();
            CreateMap<Contact, ContactQueryResponse>();

            CreateMap<CreateDirectoryCommandRequest, Directory>();
            CreateMap<Directory, ListDirectoryQueryResponse>();
            CreateMap<Directory, DirectoryQueryResponse>();
        }
    }
}
