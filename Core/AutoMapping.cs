using AutoMapper;
using Core.Commands;
using Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core {
    public class AutoMapping : Profile {
        public AutoMapping() {
            CreateMap<CreateCourseRequest, CreateCourseCommand>();
            CreateMap<CreateTeacherRequest, CreateTeacherCommand>();
            CreateMap<CreatePersonRequest, CreatePersonCommand>();
        }
    }
}
