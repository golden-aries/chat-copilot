// Copyright (c) Microsoft. All rights reserved.

using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace TxExperiment.Skills;

/// <summary>
/// HelpDeskSkill
/// </summary>
public class HelpDeskSkill
{
    /// <summary>
    /// GeQpathAdditionalDepartmentQuoteAsync
    /// </summary>
    /// <param name="departmentNames"></param>
    /// <returns></returns>
    [KernelFunction, Description("Gets quote for adding a number of departments to existing Qpath Deployment.")]
    public async Task<string> GeQpathAdditionalDepartmentQuoteAsync(
        [Description("Please provide names of departments to add, separated by comma")] string departmentNames)
    {
        await Task.CompletedTask.ConfigureAwait(false);
        var n = Guid.NewGuid();
        var result = $"Quote for adding {departmentNames} to existing QPath Deployment created #: {n}. Please navigate to https://telexy.com/quotes/{n} to download the quote pdf file.";
        return result;
    }

    /// <summary>
    /// GetQpathNewHospitalDeploymentQuoteAsync
    /// </summary>
    /// <returns></returns>
    [KernelFunction, Description("Get Quote for deploying new instance of Qpath in a new hospital.")]
    public async Task<string> GetQpathNewHospitalDeploymentQuoteAsync()
    {
        await Task.CompletedTask.ConfigureAwait(false);
        var n = Guid.NewGuid();
        var result = $"Quote for deploying a new instance of QPath created #: {n}. Please navigate to https://telexy.com/quotes/{n} to download the quote pdf file.";
        return result;
    }

    /// <summary>
    /// FileTicketAsync
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    [KernelFunction, Description("Creates and files ticket for the Telexy Support Team")]
    public async Task<string> FileTicketAsync(
        [Description("Problem Description")] string description)
    {
        await Task.CompletedTask.ConfigureAwait(false);
        var n = Guid.NewGuid();
        return $"Ticket created #: {n}. You can check the progress here https://telexy.com/tickets/{n}";
    }
}
