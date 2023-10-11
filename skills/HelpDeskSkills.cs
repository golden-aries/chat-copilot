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
    /// GeQuoteAsync
    /// </summary>
    /// <param name="quoteKind"></param>
    /// <param name="department"></param>
    /// <returns></returns>
    [SKFunction, Description("Get Qpath System Deployment(Installation) Quote")]
    public async Task<SKContext> GeQuoteAsync(
        [Description("New Hospital or additional department installation")] string quoteKind,
        [Description("Department Name (for department installation)")] string department)
    {
        await Task.CompletedTask;
        var n = Guid.NewGuid();
        var result = new SKContext(
            new ContextVariables
            ($"Quote created #: {n}. Please download it here: https://telexy.com/quotes/{n}"));
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
