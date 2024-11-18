using CRMVM_BLL.DTO;


namespace CRMVM_BLL.Interfaces
{
    public interface IDealService : IDisposable
    {
        Task<IEnumerable<DealDTO>> GetAllDeals();
        Task<DealDTO> GetDealById(Guid id);
        Task<DealDTO> GetDealByName(string name);
        Task<DealDTO> CreateDeal(DealDTO newDeal);
        Task<DealDTO> UpdateDeal(DealDTO updatedDeal);
        Task<DealDTO> UpdateDealStatus(string id, string newStatus);
        Task DeleteDeal(Guid id);
    }
}
