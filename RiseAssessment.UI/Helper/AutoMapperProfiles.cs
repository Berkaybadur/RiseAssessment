using AutoMapper;
using RiseAssessment.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssessment.UI.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Directory

            CreateMap<CreateContactCommandRequest, CreateContactCommandRequestViewModel>().ReverseMap();
            CreateMap<DeleteContactCommandRequest, DeleteContactCommandRequestViewModel>().ReverseMap();
            CreateMap<UpdateContactCommandRequest, UpdateContactCommandRequestViewModel>().ReverseMap();
            CreateMap<EmptyResponse, EmptyResponseViewModel>().ReverseMap();
            CreateMap<GetContactQueryRequest, GetContactQueryRequestViewModel>().ReverseMap();
            CreateMap<ListContactQueryRequest, ListContactQueryRequestViewModel>().ReverseMap();
            CreateMap<ListContactQueryResponse, ListContactQueryResponseViewModel>().ReverseMap();

            #endregion

            #region Directory
            CreateMap<CreateDirectoryCommandRequest, CreateDirectoryCommandRequestViewModel>().ReverseMap();
            CreateMap<DeleteDirectoryCommandRequest, DeleteDirectoryCommandRequestViewModel>().ReverseMap();
            CreateMap<UpdateDirectoryCommandRequest, UpdateDirectoryCommandRequestViewModel>().ReverseMap();
            CreateMap<EmptyResponse, EmptyResponseViewModel>().ReverseMap();
            CreateMap<GetDirectoryQueryRequest, GetDirectoryQueryRequestViewModel>().ReverseMap();
            CreateMap<ListDirectoryQueryRequest, ListDirectoryQueryRequestViewModel>().ReverseMap();
            CreateMap<ListDirectoryQueryResponse, ListDirectoryQueryResponseViewModel>().ReverseMap();
                        CreateMap<ListDirectoryQueryResponse, ListDirectoryQueryResponseViewModel>().ReverseMap();

            #endregion

        }
    }
}
