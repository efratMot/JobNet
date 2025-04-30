using AutoMapper;
using JobNet.Core.Entities;
using JobNet.Models;

namespace JobNet
{
    public class MappingPutModel:Profile
    {
        public MappingPutModel()
        {
            CreateMap<Employer, EmployerPutModel>().ReverseMap();
            CreateMap<Job, JobPutModel>().ReverseMap();

        }
    }
}
