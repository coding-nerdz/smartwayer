using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IIncentiveCalculator
{
    decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
}
