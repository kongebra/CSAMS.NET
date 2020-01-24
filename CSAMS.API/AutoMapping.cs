using AutoMapper;
using CSAMS.Commands;
using CSAMS.Contracts.Requests;
using CSAMS.Contracts.Responses;
using CSAMS.Domain.Models;

namespace CSAMS.API {
    public class AutoMapping : Profile {
        public AutoMapping() {
            // Model <-> Responses
            CreateMap<User, UserDetail>();
            CreateMap<User, UserInfo>();
            CreateMap<Course, CourseDetail>();
            CreateMap<Course, CourseInfo>();

            // Model <-> Commands
            CreateMap<User, CreateUserCommand>();

            // Requests <-> Commands
            CreateMap<RegisterUserRequest, CreateUserCommand>();
            CreateMap<UpdateUserDetailRequest, UpdateUserCommand>();
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
            CreateMap<UpdateCourseRequest, UpdateCourseCommand>();
        }
    }
}
