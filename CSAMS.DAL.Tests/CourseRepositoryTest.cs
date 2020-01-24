using CSAMS.DAL.Repositories;
using CSAMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CSAMS.DAL.Tests {
    public class CourseRepositoryTest {
        public CourseRepositoryTest() {

        }

        private DbContextOptions<CSAMSDbContext> getOptions(string name) {
            return new DbContextOptionsBuilder<CSAMSDbContext>()
                .UseInMemoryDatabase($"CourseDatabase{name}")
                .Options;
        }

        private async Task setup(DbContextOptions<CSAMSDbContext> options) {
            var now = DateTime.Now;

            using (var context = new CSAMSDbContext(options)) {
                await context.AddAsync(new Course {
                    CreatedAt = now,
                    UpdatedAt = now,
                    Name = "Basic Programming",
                    Code = "CS101",
                    Description = "Some description",
                });

                await context.AddAsync(new Course {
                    CreatedAt = now,
                    UpdatedAt = now,
                    Name = "Object-Orientated Programming",
                    Code = "CS201",
                    Description = "Some description",
                });

                await context.AddAsync(new Course {
                    CreatedAt = now,
                    UpdatedAt = now,
                    Name = "Advanced Algorithms",
                    Code = "CS301",
                    Description = "Some description",
                });

                await context.SaveChangesAsync();
            }
        }

        private Course getCourse() {
            var now = DateTime.Now;
            var course = new Course {
                CreatedAt = now,
                UpdatedAt = now,
                Name = "Basic Economics",
                Code = "EC101",
                Description = "Some description",
            };

            return course;
        }

        [Fact]
        public async void AddTest() {
            var options = getOptions(nameof(AddTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                await repo.Add(course);
                // as List<Course> is added for getting List's methods available
                var courses = await repo.GetAll() as List<Course>;

                // Add assertions
                Assert.IsType<List<Course>>(courses);
                Assert.Contains(course, courses);
                Assert.Equal(4, courses.Count);
            }
        }

        [Fact]
        public async void CountAllTest() {
            var options = getOptions(nameof(CountAllTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                var firstCount = await repo.CountAll();
                await repo.Add(course);
                var secondCount = await repo.CountAll();

                // Add assertions
                Assert.NotEqual(firstCount, secondCount);
                Assert.Equal(3, firstCount);
                Assert.Equal(4, secondCount);
            }
        }

        [Fact]
        public async void CountWhereTest() {
            var options = getOptions(nameof(CountWhereTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                await repo.Add(course);
                var csCourseCount = await repo.CountWhere(c => c.Code.ToLower().Contains("cs"));
                var ecCourseCount = await repo.CountWhere(c => c.Code.ToLower().Contains("ec"));

                // Add assertions
                Assert.NotEqual(csCourseCount, ecCourseCount);
                Assert.Equal(3, csCourseCount);
                Assert.Equal(1, ecCourseCount);
            }
        }

        [Fact]
        public async void FirstOrDefaultTest() {
            var options = getOptions(nameof(FirstOrDefaultTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                await repo.Add(course);
                var basicProgramming = await repo.FirstOrDefault(o => o.Name.ToLower().Contains("basic programming"));
                var basicEconomics = await repo.FirstOrDefault(o => o.Name.ToLower().Contains("basic economics"));

                // Add assertions
                Assert.NotEqual(basicProgramming, basicEconomics);
                Assert.Equal("CS101", basicProgramming.Code);
                Assert.Equal("EC101", basicEconomics.Code);
            }
        }

        [Fact]
        public async void GetByCodeTest() {
            var options = getOptions(nameof(GetByCodeTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                await repo.Add(course);
                var basicProgramming = await repo.GetByCode("CS101");
                var basicEconomics = await repo.GetByCode("EC101");

                // Different kind of input
                var anotherBasicProgramming = await repo.GetByCode("cs101");
                var yetAnotherBasicProgramming = await repo.GetByCode("cS101");

                // Add assertions
                Assert.NotEqual(basicProgramming, basicEconomics);
                Assert.Equal("CS101", basicProgramming.Code);
                Assert.Equal("EC101", basicEconomics.Code);

                Assert.Equal(basicProgramming.Id, anotherBasicProgramming.Id);
                Assert.Equal(basicProgramming, anotherBasicProgramming);
                Assert.Equal(basicProgramming, yetAnotherBasicProgramming);
                Assert.Equal(anotherBasicProgramming, yetAnotherBasicProgramming);
            }
        }

        [Fact]
        public async void GetByIdTest() {
            var options = getOptions(nameof(GetByIdTest));
            await setup(options);

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                var basicProgramming = await repo.GetByCode("CS101");
                var anotherBasicProgramming = await repo.GetById(basicProgramming.Id);

                // Add assertions
                Assert.Equal(basicProgramming, anotherBasicProgramming);
            }
        }

        [Fact]
        public async void GetWhereTest() {
            var options = getOptions(nameof(GetWhereTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                await repo.Add(course);

                var csCourses = await repo.GetWhere(o => o.Code.ToLower().Contains("cs"));
                var ecCourses = await repo.GetWhere(o => o.Code.ToLower().Contains("ec"));

                var basicProgramming = await repo.GetByCode("CS101");

                // Add assertions
                Assert.NotNull(csCourses);
                Assert.Contains(basicProgramming, csCourses);

                Assert.NotNull(ecCourses);
                Assert.Contains(course, ecCourses);
            }
        }

        [Fact]
        public async void RemoveTest() {
            var options = getOptions(nameof(RemoveTest));
            await setup(options);

            var course = getCourse();

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                var basicProgramming = await repo.GetByCode("cs101");

                Assert.NotNull(basicProgramming);

                await repo.Remove(basicProgramming);

                var retry = await repo.GetByCode("cs101");

                Assert.Null(retry);
            }
        }

        [Fact]
        public async void UpdateTest() {
            var options = getOptions(nameof(UpdateTest));
            await setup(options);

            var course = getCourse();
            var startUpdatedAt = course.UpdatedAt;
            var startName = course.Name;

            using (var context = new CSAMSDbContext(options)) {
                // Initialize repository
                var repo = new CourseRepository(context);
                // Add actions
                await repo.Add(course);
                var ec101 = await repo.GetByCode("ec101");
                ec101.Name = "Intro to Economics";

                await repo.Update(ec101);
                var retry = await repo.GetByCode("ec101");

                // Add assertions
                Assert.NotNull(retry);
                Assert.NotEqual(retry.Name, startName);
                Assert.Equal(retry.Code, course.Code);
                Assert.Equal(retry.Description, course.Description);
            }
        }
    }
}
