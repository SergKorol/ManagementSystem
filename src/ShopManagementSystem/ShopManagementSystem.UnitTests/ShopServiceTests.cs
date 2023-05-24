using Microsoft.Extensions.Logging;
using Moq;
using ShopManagementSystem.Data;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Services.ShopServices;

namespace ShopManagementSystem.UnitTests;

public sealed class ShopServiceTests
{
    private readonly ShopService _shopService;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<ShopService>> _loggerMock;

    public ShopServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<ShopService>>();
        _shopService = new ShopService(_unitOfWorkMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateShop_ValidData_ReturnsTrue()
    {
        // Arrange
        var title = "Apple Corp";
        var phone = "123456789";
        var shop = new Shop { Title = title, Phone = phone };

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.Add(It.IsAny<Shop>()));

        // Act
        var result = await _shopService.CreateShop(title, phone);

        // Assert
        Assert.True(result);
        _unitOfWorkMock.Verify(uow => uow.ShopRepository.Add(It.IsAny<Shop>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
    }

    [Fact]
    public async Task CreateShop_ExceptionThrown_ReturnsFalse()
    {
        // Arrange
        var title = "Facebook Inc";
        var phone = "123456789";

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.Add(It.IsAny<Shop>()))
            .ThrowsAsync(new Exception("An error occurred"));

        // Act
        var result = await _shopService.CreateShop(title, phone);

        // Assert
        Assert.False(result);
        _unitOfWorkMock.Verify(uow => uow.ShopRepository.Add(It.IsAny<Shop>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Never);
    }
    
    [Fact]
    public async Task EditShop_ValidData_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var title = "New Shop";
        var phone = "987654321";
        var shop = new Shop { ShopId = id, Title = "Old Shop", Phone = "123456789" };

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.GetById(id)).ReturnsAsync(shop);

        // Act
        var result = await _shopService.EditShop(id, title, phone);

        // Assert
        Assert.True(result);
        Assert.Equal(title, shop.Title);
        Assert.Equal(phone, shop.Phone);
        _unitOfWorkMock.Verify(uow => uow.ShopRepository.Update(shop), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
    }

    [Fact]
    public async Task EditShop_ShopNotFound_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var title = "New Shop";
        var phone = "987654321";

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.GetById(id))!.ReturnsAsync((Shop)null!);

        // Act
        var result = await _shopService.EditShop(id, title, phone);

        // Assert
        Assert.False(result);
        _unitOfWorkMock.Verify(uow => uow.ShopRepository.Update(It.IsAny<Shop>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Never);
    }

    [Fact]
    public async Task EditShop_ExceptionThrown_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var title = "New Shop";
        var phone = "987654321";
        var shop = new Shop { ShopId = id, Title = "Old Shop", Phone = "123456789" };

        // Mock the ShopRepository's GetById to return the shop
        _unitOfWorkMock.Setup(uow => uow.ShopRepository.GetById(id)).ReturnsAsync(shop);

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.Update(shop))
            .Throws(new Exception("An error occurred"));

        // Act
        var result = await _shopService.EditShop(id, title, phone);

        // Assert
        Assert.False(result);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Never);
    }
    
    [Fact]
    public async Task DeleteShop_ExistingShop_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var shop = new Shop { ShopId = id };

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.GetById(id)).ReturnsAsync(shop);

        // Act
        var result = await _shopService.DeleteShop(id);

        // Assert
        Assert.True(result);
        _unitOfWorkMock.Verify(uow => uow.ShopRepository.Delete(shop), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
    }

    [Fact]
    public async Task DeleteShop_NonExistingShop_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();

        _unitOfWorkMock.Setup(uow => uow.ShopRepository.GetById(id))!.ReturnsAsync((Shop)null!);

        // Act
        var result = await _shopService.DeleteShop(id);

        // Assert
        Assert.False(result);
        _unitOfWorkMock.Verify(uow => uow.ShopRepository.Delete(It.IsAny<Shop>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.Save(), Times.Never);
    }
}