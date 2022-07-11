using AutoMapper;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Models.DTOs;

namespace TodoApp.Services {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<TaskWork, TaskWorkDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserGroup, UserGroupDTO>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, UserDetailsViewModel>().ReverseMap();
            CreateMap<User, EditInfoViewModel>().ReverseMap();
        }
    }
}