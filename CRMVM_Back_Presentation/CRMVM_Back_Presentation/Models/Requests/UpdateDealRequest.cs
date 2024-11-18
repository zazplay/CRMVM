using System.ComponentModel.DataAnnotations;

namespace CRMVM_Back_Presentation.Models.Requests
{
    public class UpdateDealRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public decimal? Amount { get; set; }
    }
}
