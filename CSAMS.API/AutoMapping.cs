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
            CreateMap<User, RegisterUserCommand>();

            // Requests <-> Commands
            CreateMap<RegisterUserRequest, RegisterUserCommand>();
            CreateMap<UpdateUserDetailRequest, UpdateUserDetailCommand>();
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
            CreateMap<UpdateCourseRequest, UpdateCourseCommand>();
        }
    }
}
