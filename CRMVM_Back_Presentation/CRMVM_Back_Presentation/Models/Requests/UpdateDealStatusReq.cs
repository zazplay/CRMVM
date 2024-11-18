using System.ComponentModel.DataAnnotations;

namespace CRMVM_Back_Presentation.Models.Requests
{
    public class UpdateDealStatusReq
    {
        
        public string Id { get; set; }

        public string Status { get; set; }

    }
}
