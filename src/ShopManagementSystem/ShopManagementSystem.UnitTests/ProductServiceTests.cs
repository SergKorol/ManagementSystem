using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ShopManagementSystem.Data;
using ShopManagementSystem.Data.Models;
using ShopManagementSystem.Services.ProductServices;

namespace ShopManagementSystem.UnitTests;

public sealed class ProductServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<ProductService>> _loggerMock;
    
    private readonly ProductService _productService;
    
    public ProductServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<ProductService>>();

        _productService = new ProductService( _unitOfWorkMock.Object, _loggerMock.Object );
    }
    
    [Fact]
    public async Task GetProducts_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new() { ProductId = Guid.NewGuid(), Name = "iPhone" },
            new() { ProductId = Guid.NewGuid(), Name = "MacBook" },
        };
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.ProductRepository.GetAll()).ReturnsAsync(products);

        // Act
        var result = await _productService.GetProducts();

        // Assert
        Assert.Equal(products, result);
    }
    
    [Fact]
    public async Task CreateProduct_ValidInput_ReturnsTrue()
    {
        // Arrange
        var productName = "iPhone";

        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.ProductRepository.Add(It.IsAny<Product>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Save()).Verifiable();

        // Act
        var result = await _productService.CreateProduct(productName);

        // Assert
        _unitOfWorkMock.Verify(unitOfWork => unitOfWork.ProductRepository.Add(It.Is<Product>(p => p.Name == productName)), Times.Once);
        _unitOfWorkMock.Verify(unitOfWork => unitOfWork.Save(), Times.Never);
        Assert.True(result);
    }
    
    [Fact]
    public async Task EditProduct_ProductExists_ReturnsTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productName = "New Product";

        var product = new Product
        {
            ProductId = productId,
            Name = "Old Product"
        };

        _unitOfWorkMock
            .Setup(x => x.ProductRepository.GetById(productId))
            .ReturnsAsync(product);

        _unitOfWorkMock
            .Setup(x => x.Save())
            .Verifiable();

        var productService = new ProductService(_unitOfWorkMock.Object, _loggerMock.Object);

        // Act
        var result = await productService.EditProduct(productId, productName);

        // Assert
        result.Should().BeTrue();
        product.Name.Should().Be(productName);
        _unitOfWorkMock.Verify(x => x.Save(), Times.Once);
    }
    
    [Fact]
    public async Task DeleteProduct_ProductExists_ReturnsTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var product = new Product
        {
            ProductId = productId,
            Name = "Laptop"
        };

        _unitOfWorkMock
            .Setup(x => x.ProductRepository.GetById(productId))
            .ReturnsAsync(product);

        _unitOfWorkMock
            .Setup(x => x.Save())
            .Verifiable();

        var productService = new ProductService(_unitOfWorkMock.Object, _loggerMock.Object);

        // Act
        var result = await productService.DeleteProduct(productId);

        // Assert
        Assert.True(result);
        _unitOfWorkMock.Verify(x => x.ProductRepository.Delete(product), Times.Once);
        _unitOfWorkMock.Verify(x => x.Save(), Times.Once);
    }
    
    [Fact]
    public async Task GetProductById_ProductExists_ReturnsProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var product = new Product
        {
            ProductId = productId,
            Name = "iPhone"
        };

        _unitOfWorkMock
            .Setup(x => x.ProductRepository.GetById(productId))
            .ReturnsAsync(product);

        var productService = new ProductService(_unitOfWorkMock.Object, _loggerMock.Object);

        // Act
        var result = await productService.GetProductById(productId.ToString());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal("iPhone", result.Name);
    }

    [Fact]
    public async Task GetProductById_ProductNotFound_ReturnsNull()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _unitOfWorkMock
            .Setup(x => x.ProductRepository.GetById(productId))!
            .ReturnsAsync((Product)null!);

        var productService = new ProductService(_unitOfWorkMock.Object, _loggerMock.Object);

        // Act
        var result = await productService.GetProductById(productId.ToString());

        // Assert
        Assert.Null(result);
    }
}