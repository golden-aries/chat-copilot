using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace Tlx
{
    public class HelpDeskSkill
    {
        //// <summary>
        /// GeQpathAdditinalDepartmentQuoteAsync
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [KernelFunction, Description("Gets quote for adding department to existing Qpath Deployment.")]
        public async Task<string> GeQpathAdditionalDepartmentQuoteAsync([Description("Number of departments to add to Qpath")] int depNum)
        {
            await Task.CompletedTask;
            var n = Guid.NewGuid();
            return $"Quote for adding {depNum} departments to existing QPath Deployment created #: {n}. Please navigate to https://telexy.com/quotes/{n} to download the quote pdf file.";
        }

        /// <summary>
        /// GeQpathNewHospitalDeploymentQuoteAsync
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [KernelFunction, Description("Get Quote for deploying new instance of Qpath in a new hospital.")]
        public async Task<string> GetQpathNewHospitalDeploymentQuoteAsync()
        {
            await Task.CompletedTask;
            var n = Guid.NewGuid();
            return $"Quote for deploying a new instance of QPath created #: {n}. Please navigate to https://telexy.com/quotes/{n} to download the quote pdf file.";
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
            await Task.CompletedTask;
            var n = Guid.NewGuid();
            return $"Ticket created #: {n}. You can check the progress here https://telexy.com/tickets/{n}";
        }
    }
}
