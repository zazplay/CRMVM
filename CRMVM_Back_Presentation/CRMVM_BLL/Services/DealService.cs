using AutoMapper;
using CRMVM_BLL.DTO;
using CRMVM_BLL.Interfaces;
using CRMVM_DAL.Interfaces;
using CRMVM_DAL.Models.Entities;

namespace CRMVM_BLL.Services
{
    public class DealService : IDealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DealDTO>> GetAllDeals()
        {
            var deals = await _unitOfWork.Deals.GetAll();
            return _mapper.Map<IEnumerable<DealDTO>>(deals);
        }

        public async Task<DealDTO> GetDealById(Guid id)
        {
            var deal = await _unitOfWork.Deals.Get(id);
            return _mapper.Map<DealDTO>(deal);
        }

        public async Task<DealDTO> GetDealByName(string name)
        {
            var deal = await _unitOfWork.Deals.Find(c => c.Name == name);
            return _mapper.Map<DealDTO>(deal);
        }

        public async Task<DealDTO> CreateDeal(DealDTO newDeal)
        {
            var dealEntity = _mapper.Map<Deal>(newDeal);
            newDeal.Id = Guid.NewGuid();
            await _unitOfWork.Deals.Create(dealEntity);
            return _mapper.Map<DealDTO>(dealEntity);
        }

        public async Task<DealDTO> UpdateDeal(DealDTO updatedDeal)
        {
            var dealEntity = await _unitOfWork.Deals.Get(updatedDeal.Id);
            if (dealEntity == null)
            {
                throw new Exception("Deal not found");
            }

            dealEntity.Name = updatedDeal.Name;
            _unitOfWork.Deals.Update(dealEntity);
            return _mapper.Map<DealDTO>(dealEntity);
        }

        public async Task<DealDTO> UpdateDealStatus(string id, string newStatus)
        {
            
                Guid.TryParse(id, out var dealId);

                var dealEntity = await _unitOfWork.Deals.Get(dealId);
                
                dealEntity.Status = newStatus;
                _unitOfWork.Deals.Update(dealEntity);

                 return (_mapper.Map<DealDTO>(dealEntity));   

        }

        public async Task DeleteDeal(Guid id)
        {
            await _unitOfWork.Deals.Delete(id);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
