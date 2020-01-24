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
            CreateMap<Assignment, AssignmentDetail>();
            CreateMap<Assignment, AssignmentInfo>();

            // Model <-> Commands
            CreateMap<User, CreateUserCommand>();

            // Requests <-> Commands
            CreateMap<CreateUserRequest, CreateUserCommand>();
            CreateMap<UpdateUserDetailRequest, UpdateUserCommand>();
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
            CreateMap<UpdateCourseRequest, UpdateCourseCommand>();
            CreateMap<CreateAssignmentRequest, CreateAssignmentCommand>();
            CreateMap<UpdateAssignmentRequest, UpdateAssignmentCommand>();
        }
    }
}
