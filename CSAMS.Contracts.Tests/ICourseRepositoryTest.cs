using System;
using System.Collections.Generic;
using System.Text;
using CSAMS.Contracts.Interfaces;
using CSAMS.Domain.Models;
using Moq;
using Xunit;

namespace CSAMS.Contracts.Tests {
    public class ICourseRepositoryTest {
        private readonly Mock<ICourseRepository> _repository;

        public ICourseRepositoryTest() {
            _repository = new Mock<ICourseRepository>();
        }

        [Fact]
        public async void GetByCodeTest() {
            var courseCode = "CS100";

            var course = new Course {
                Name = "Basic Computer Science",
                Code = "CS100",
                Descripion = "Sample description"
            };

            _repository.Setup(r => r.GetByCode(courseCode)).ReturnsAsync(course);

            var result = await _repository.Object.GetByCode(courseCode);

            Assert.IsType<Course>(result);
            Assert.Equal(result, course);
            Assert.Equal(courseCode, result.Code);
        }
    }
}
