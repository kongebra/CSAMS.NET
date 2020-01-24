using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSAMS.Contracts.Interfaces;
using Moq;
using Xunit;

namespace CSAMS.Contracts.Tests {
    public class TestClass {
        public Guid Id { get; set; }
        public string A { get; set; }
        public int B { get; set; }
        public bool C { get; set; }

        public TestClass(Guid id) {
            this.Id = id;
            this.A = "foo";
            this.B = 6;
            this.C = false;
        }
    }

    public class IAsyncRepositoryTest {
        private readonly Mock<IAsyncRepository<TestClass>> _repository;
        private readonly DateTime _now;

        public IAsyncRepositoryTest() {
            _repository = new Mock<IAsyncRepository<TestClass>>();
            _now = DateTime.Now;
        }

        [Fact]
        public async void GetByIdTest() {
            var id = Guid.NewGuid();
            var testClass = new TestClass(id);

            _repository.Setup(r => r.GetById(id)).ReturnsAsync(testClass);

            var result = await _repository.Object.GetById(id);

            Assert.IsType<TestClass>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("foo", result.A);
            Assert.Equal(6, result.B);
            Assert.False(result.C);
        }

        [Fact]
        public async void FirstOrDefaultTest() {
            var id = Guid.NewGuid();
            var testClass = new TestClass(id);

            _repository.Setup(r => r.FirstOrDefault(o => o.C == false)).ReturnsAsync(testClass);

            var result = await _repository.Object.FirstOrDefault(o => o.C == false);

            Assert.IsType<TestClass>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("foo", result.A);
            Assert.Equal(6, result.B);
            Assert.False(result.C);
        }

        [Fact]
        public async void AddTest() {
            var testClass = new TestClass(Guid.Empty);

            _repository.Setup(r => r.Add(testClass)).Returns(Task.CompletedTask).Verifiable();

            await _repository.Object.Add(testClass);
            _repository.Verify();
        }

        [Fact]
        public async void UpdateTest() {
            var testClass = new TestClass(Guid.Empty);

            _repository.Setup(r => r.Update(testClass)).Returns(Task.CompletedTask).Verifiable();

            await _repository.Object.Update(testClass);
            _repository.Verify();
        }

        [Fact]
        public async void RemoveTest() {
            var testClass = new TestClass(Guid.Empty);

            _repository.Setup(r => r.Remove(testClass)).Returns(Task.CompletedTask).Verifiable();

            await _repository.Object.Remove(testClass);
            _repository.Verify();
        }

        [Fact]
        public async void GetAllTest() {
            var id0 = Guid.NewGuid();
            var id1 = Guid.NewGuid();

            var testClass0 = new TestClass(id0);
            var testClass1 = new TestClass(id1);

            _repository.Setup(r => r.GetAll()).ReturnsAsync(new List<TestClass> { testClass0, testClass1 });

            var result = await _repository.Object.GetAll();

            Assert.IsType<List<TestClass>>(result);
            Assert.Contains(testClass0, result);
            Assert.Contains(testClass1, result);
        }

        [Fact]
        public async void GetWhereTest() {
            var id = Guid.NewGuid();
            var testClass = new TestClass(id);

            _repository.Setup(r => r.GetWhere(o => o.Id == id)).ReturnsAsync(new List<TestClass> { testClass });

            var result = await _repository.Object.GetWhere(o => o.Id == id);

            Assert.IsType<List<TestClass>>(result);
            Assert.Contains(testClass, result);
        }

        [Fact]
        public async void CountAllTest() {
            var expectedCount = 2;

            _repository.Setup(r => r.CountAll()).ReturnsAsync(expectedCount);

            var result = await _repository.Object.CountAll();

            Assert.IsType<int>(result);
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async void CountWhereTest() {
            var expectedCount = 3;

            _repository.Setup(r => r.CountWhere(o => o.C == false)).ReturnsAsync(expectedCount);

            var result = await _repository.Object.CountWhere(o => o.C == false);

            Assert.IsType<int>(result);
            Assert.Equal(expectedCount, result);
        }
    }
}
