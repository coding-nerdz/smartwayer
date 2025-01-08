using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly RebateDataStore _rebateDataStore;
    private readonly ProductDataStore _productDataStore;

    public RebateService(RebateDataStore rebateDataStore, ProductDataStore productDataStore)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null || product == null)
        {
            return new CalculateRebateResult { Success = false };
        }

        if (rebate.Incentive == default(IncentiveType) || rebate.Amount <= 0)
        {
            return new CalculateRebateResult { Success = false };
        }

        var calculator = IncentiveCalculatorFactory.GetCalculator(rebate.Incentive);
        var rebateAmount = calculator.Calculate(rebate, product, request);

        if (rebateAmount <= 0)
        {
            return new CalculateRebateResult { Success = false };
        }

        _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

        return new CalculateRebateResult { Success = true, RebateAmount = rebateAmount };
    }
}
