using CRMVM_DAL.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace CRMVM_BLL.DTO
{
    public class CreateDealDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public Guid? ClientId { get; set; }
        public decimal? Amount { get; set; }
    }
}
