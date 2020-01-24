using System;
using System.Collections.Generic;
using System.Text;
using CSAMS.Contracts.Interfaces;
using CSAMS.Core.Enums;
using CSAMS.Domain.Models;
using Moq;
using Xunit;

namespace CSAMS.Contracts.Tests {
    public class IUserRepositoryTest {
        private readonly Mock<IUserRepository> _repository;

        public IUserRepositoryTest() {
            _repository = new Mock<IUserRepository>();
        }

        [Fact]
        public async void GetByEmailTest() {
            var john = new User {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Sex = Sex.Male,
            };

            var jane = new User {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@doe.com",
                Sex = Sex.Female,
            };

            var josh = new User {
                FirstName = "Josh",
                LastName = "Doe",
                Email = "josh@doe.com",
                Sex = Sex.Male,
            };

            _repository.Setup(r => r.GetByEmail(john.Email)).ReturnsAsync(john);
            _repository.Setup(r => r.GetByEmail(jane.Email)).ReturnsAsync(jane);
            _repository.Setup(r => r.GetByEmail(josh.Email)).ReturnsAsync(josh);

            var result = await _repository.Object.GetByEmail(john.Email);
            Assert.IsType<User>(result);
            Assert.Equal(john, result);

            result = await _repository.Object.GetByEmail(jane.Email);
            Assert.IsType<User>(result);
            Assert.Equal(jane, result);

            result = await _repository.Object.GetByEmail(josh.Email);
            Assert.IsType<User>(result);
            Assert.Equal(josh, result);
        }

        [Fact]
        public async void GetByUsernameTest() {
            var john = new User {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Username = "johndoe",
                Sex = Sex.Male,
            };

            var jane = new User {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@doe.com",
                Username = "janedoe",
                Sex = Sex.Female,
            };

            var josh = new User {
                FirstName = "Josh",
                LastName = "Doe",
                Email = "josh@doe.com",
                Username = "joshdoe",
                Sex = Sex.Male,
            };

            _repository.Setup(r => r.GetByUsername(john.Username)).ReturnsAsync(john);
            _repository.Setup(r => r.GetByUsername(jane.Username)).ReturnsAsync(jane);
            _repository.Setup(r => r.GetByUsername(josh.Username)).ReturnsAsync(josh);

            var result = await _repository.Object.GetByUsername(john.Username);
            Assert.IsType<User>(result);
            Assert.Equal(john, result);

            result = await _repository.Object.GetByUsername(jane.Username);
            Assert.IsType<User>(result);
            Assert.Equal(jane, result);

            result = await _repository.Object.GetByUsername(josh.Username);
            Assert.IsType<User>(result);
            Assert.Equal(josh, result);
        }

        [Fact]
        public async void SearchTest() {
            var john = new User {
                FirstName = "John",
                LastName = "Doe",
                Sex = Sex.Male,
            };

            var jane = new User {
                FirstName = "Jane",
                LastName = "Doe",
                Sex = Sex.Female,
            };

            var josh = new User {
                FirstName = "Josh",
                LastName = "Doe",
                Sex = Sex.Male,
            };

            _repository.Setup(r => r.Search(new { LastName = "doe" })).ReturnsAsync(new List<User> { john, jane, josh });
            _repository.Setup(r => r.Search(new { FirstName = "john" })).ReturnsAsync(new List<User> { john });
            _repository.Setup(r => r.Search(new { LastName = "doe", Sex = Sex.Male })).ReturnsAsync(new List<User> { john, josh });

            var result = await _repository.Object.Search(new { FirstName = "john" });

            Assert.IsType<List<User>>(result);
            Assert.Contains(john, result);
            Assert.DoesNotContain(jane, result);
            Assert.DoesNotContain(josh, result);

            result = await _repository.Object.Search(new { LastName = "doe" });

            Assert.IsType<List<User>>(result);
            Assert.Contains(john, result);
            Assert.Contains(jane, result);
            Assert.Contains(josh, result);

            result = await _repository.Object.Search(new { LastName = "doe", Sex = Sex.Male });

            Assert.IsType<List<User>>(result);
            Assert.Contains(john, result);
            Assert.Contains(josh, result);
            Assert.DoesNotContain(jane, result);
        }
    }
}
