using AutoMapper;
using Demo.DAL.Models;
using Demo.pl.ViewModels;

namespace Demo.pl.MappingProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmoployeeViewModel, Employee>().ReverseMap();
        }
    }
}
