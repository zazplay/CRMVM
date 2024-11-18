namespace CRMVM_Back_Presentation.Models.Requests
{
    public class UpdateClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
