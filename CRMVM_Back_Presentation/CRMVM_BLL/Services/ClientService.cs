using AutoMapper;
using CRMVM_BLL.DTO;
using CRMVM_BLL.Interfaces;
using CRMVM_DAL.Interfaces;
using CRMVM_DAL.Models.Entities;


namespace CRMVM_BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClients()
        {
            var clients = await _unitOfWork.Clients.GetAll();
            return _mapper.Map<IEnumerable<ClientDTO>>(clients);
        }

        public async Task<ClientDTO> GetClientById(Guid id)
        {
            var client = await _unitOfWork.Clients.Get(id);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<ClientDTO> GetClientByLogin(string login)
        {
            var client = await _unitOfWork.Clients.Find(c => c.Login == login);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<ClientDTO> CreateClient(ClientDTO newClient)
        {
            var clientEntity = _mapper.Map<Client>(newClient);
            newClient.Id = Guid.NewGuid();
            await _unitOfWork.Clients.Create(clientEntity);
            await _unitOfWork.CommitChanges();
            return _mapper.Map<ClientDTO>(clientEntity);
        }

        public async Task<ClientDTO> UpdateClient(ClientDTO updatedClient)
        {
            var clientEntity = await _unitOfWork.Clients.Get(updatedClient.Id);
            if (clientEntity == null)
            {
                throw new Exception("Client not found");
            }

            clientEntity.Name = updatedClient.Name;
            _unitOfWork.Clients.Update(clientEntity);
            await _unitOfWork.CommitChanges();
            return _mapper.Map<ClientDTO>(clientEntity);
        }

        public async Task<ClientDTO> DeleteClient(Guid id)
        {
            var clients = await _unitOfWork.Clients.GetAll();
            var client = clients.FirstOrDefault(item => item.Id == id);
            if ( client == null)
            {
                throw new Exception("Client with this id is not found");
            }
            await _unitOfWork.Clients.Delete(id);
            await _unitOfWork.CommitChanges();
            return (_mapper.Map<ClientDTO>( client));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}