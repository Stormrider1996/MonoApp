using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using VehicleWiki.DAL;
using VehicleWiki.Repository;
using VehicleWiki.Repository.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace VehicleWiki.Repository.Tests;

public class RepositoryTests
{
    private readonly Mock<VehicleDbContext> _mockContext;
    private readonly Mock<DbSet<TestEntity>> _mockSet;

    public RepositoryTests()
    {
        _mockContext = new Mock<VehicleDbContext>();
        _mockSet = new Mock<DbSet<TestEntity>>();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectEntity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new TestEntity { Id = id, Name = "Test" };
        _mockSet.Setup(m => m.FindAsync(id)).ReturnsAsync(entity);
        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
        var repository = new GenericRepository<TestEntity>(_mockContext.Object);

        // Act
        var result = await repository.GetByIdAsync(id);

        // Assert
        Assert.Equal(entity, result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
            {
                new TestEntity { Id = Guid.NewGuid(), Name = "Test1" },
                new TestEntity { Id = Guid.NewGuid(), Name = "Test2" }
            };
        var mockDbSet = CreateMockDbSet(entities);
        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(mockDbSet.Object);
        var repository = new GenericRepository<TestEntity>(_mockContext.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(entities, result);
    }


    [Fact]
    public async Task AddRangeAsync_ShouldAddMultipleEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid(), Name = "Test1" },
            new TestEntity { Id = Guid.NewGuid(), Name = "Test2" }
        };
        _mockSet.Setup(m => m.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>(), default)).Returns(Task.CompletedTask);
        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
        var repository = new GenericRepository<TestEntity>(_mockContext.Object);

        // Act
        await repository.AddRangeAsync(entities);

        // Assert
        _mockSet.Verify(m => m.AddRangeAsync(entities, default), Times.Once);
    }

    [Fact]
    public void Remove_ShouldRemoveEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        _mockSet.Setup(m => m.Remove(It.IsAny<TestEntity>()));
        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
        var repository = new GenericRepository<TestEntity>(_mockContext.Object);

        // Act
        repository.Remove(entity);

        // Assert
        _mockSet.Verify(m => m.Remove(entity), Times.Once);
    }

    [Fact]
    public void RemoveRange_ShouldRemoveMultipleEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new TestEntity { Id = Guid.NewGuid(), Name = "Test1" },
            new TestEntity { Id = Guid.NewGuid(), Name = "Test2" }
        };
        _mockSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TestEntity>>()));
        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
        var repository = new GenericRepository<TestEntity>(_mockContext.Object);

        // Act
        repository.RemoveRange(entities);

        // Assert
        _mockSet.Verify(m => m.RemoveRange(entities), Times.Once);
    }

    [Fact]
    public void Update_ShouldUpdateEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        _mockSet.Setup(m => m.Update(It.IsAny<TestEntity>()));
        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
        var repository = new GenericRepository<TestEntity>(_mockContext.Object);

        // Act
        repository.Update(entity);

        // Assert
        _mockSet.Verify(m => m.Update(entity), Times.Once);
    }

    private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();
        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        dbSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(sourceList.GetEnumerator()));

        return dbSet;
    }
}

public class TestEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public T Current => _inner.Current;

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        return new ValueTask();
    }
}


