using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

var rebateStore = new RebateDataStore();
var productStore = new ProductDataStore();

var rebateService = new RebateService(rebateStore, productStore);

var request = new CalculateRebateRequest
{
    RebateIdentifier = "Rebate1",
    ProductIdentifier = "Product1",
    Quantity = 10
};

var result = rebateService.Calculate(request);

Console.WriteLine(result.Success
    ? $"Rebate calculated successfully. Amount: {result.RebateAmount}"
    : "Rebate calculation failed.");
