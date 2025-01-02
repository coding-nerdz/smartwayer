using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    [Fact]
    public void CalculateRebate_ShouldReturnSuccess_ForValidRequest()
    {
        // Arrange
        var rebate = new Rebate { Amount = 100, IncentiveType = IncentiveType.FixedCashAmount };
        var product = new Product { Identifier = "Product1", Price = 500 };
        var request = new CalculateRebateRequest { RebateIdentifier = "Rebate1", ProductIdentifier = "Product1" };

        var mockRebateStore = new Mock<IRebateDataStore>();
        mockRebateStore.Setup(store => store.GetRebate("Rebate1")).Returns(rebate);

        var mockProductStore = new Mock<IProductDataStore>();
        mockProductStore.Setup(store => store.GetProduct("Product1")).Returns(product);

        var service = new RebateService(mockRebateStore.Object, mockProductStore.Object);

        // Act
        var result = service.CalculateRebate(request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(100, result.RebateAmount);
    }

    [Fact]
    public void CalculateRebate_ShouldReturnFailure_ForInvalidRequest()
    {
        // Arrange
        var request = new CalculateRebateRequest { RebateIdentifier = "Invalid", ProductIdentifier = "Invalid" };

        var mockRebateStore = new Mock<IRebateDataStore>();
        var mockProductStore = new Mock<IProductDataStore>();

        var service = new RebateService(mockRebateStore.Object, mockProductStore.Object);

        // Act
        var result = service.CalculateRebate(request);

        // Assert
        Assert.False(result.Success);
    }
}
