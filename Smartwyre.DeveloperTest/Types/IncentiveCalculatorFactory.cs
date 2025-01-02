using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public static class IncentiveCalculatorFactory
{
    public static IIncentiveCalculator GetCalculator(IncentiveType incentiveType)
    {
        return incentiveType switch
        {
            IncentiveType.FixedCashAmount => new FixedCashAmountCalculator(),
            IncentiveType.FixedRate => new FixedRateCalculator(),
            IncentiveType.AmountPerUnit => new AmountPerUnitCalculator(),
            _ => throw new NotImplementedException($"Calculator for {incentiveType} is not implemented")
        };
    }
}
