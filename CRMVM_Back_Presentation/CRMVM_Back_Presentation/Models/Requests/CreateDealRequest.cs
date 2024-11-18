using System.ComponentModel.DataAnnotations;

namespace CRMVM_Back_Presentation.Models.Requests
{
    public class CreateDealRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public Guid? ClientId { get; set; }

        public decimal? Amount { get; set; }
    }
}
