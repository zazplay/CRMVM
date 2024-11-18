namespace CRMVM_Back_Presentation.Models.Responses
{
    public class DealResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
