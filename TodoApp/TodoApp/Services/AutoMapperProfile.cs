using AutoMapper;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Models.DTOs;

namespace TodoApp.Services {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
        }
    }
}