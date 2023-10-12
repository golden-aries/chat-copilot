using System.ComponentModel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Skills;

/// <summary>
/// HelpDeskSkill
/// </summary>
public class HelpDeskSkill
{
    /// <summary>
    /// GeQpathAdditinalDepartmentQuoteAsync
    /// </summary>
    /// <param name="department"></param>
    /// <returns></returns>
    [SKFunction, Description("Gets quote for adding department to existing Qpath Deployment.")]
    public async Task<SKContext> GeQpathAdditionalDepartmentQuoteAsync(
        [Description("Department Name (for department installation)")] string department)
    {
        await Task.CompletedTask;
        var n = Guid.NewGuid();
        var result = new SKContext(
            new ContextVariables
            ($"Quote created #: {n}. Please navigate to https://telexy.com/quotes/{n} to download the quote pdf file."));
        return result;
    }

    /// <summary>
    /// GeQpathNewHospitalDeploymentQuoteAsync
    /// </summary>
    /// <param name="department"></param>
    /// <returns></returns>
    [SKFunction, Description("Get Quote for deploying new instance of Qpath in a new hospital.")]
    public async Task<SKContext> GeQpathNewHospitalDeploymentQuoteAsync()
    {
        await Task.CompletedTask;
        var n = Guid.NewGuid();
        var result = new SKContext(
            new ContextVariables
            ($"Quote created #: {n}. Please navigate to https://telexy.com/quotes/{n} to download the quote pdf file."));
        return result;
    }

    [SKFunction, Description("Creates and files ticket for the Telexy Support Team")]
    public async Task<SKContext> FileTicketAsync(
        [Description("Problem Description")] string description)
    {
        await Task.CompletedTask;
        var n = Guid.NewGuid();
        return new SKContext(
            new ContextVariables(
                $"Ticket created #: {n}. You can check the progress here https://telexy.com/tickets/{n}"));
    }
}
