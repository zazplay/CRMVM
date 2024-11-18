using CRMVM_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMVM_BLL.Interfaces
{
    public interface IClientService : IDisposable
    {
        Task<IEnumerable<ClientDTO>> GetAllClients();
        Task<ClientDTO> GetClientById(Guid id);
        Task<ClientDTO> GetClientByLogin(string name);
        Task<ClientDTO> CreateClient(ClientDTO newCategory);
        Task<ClientDTO> UpdateClient(ClientDTO updatedCategory);
        Task<ClientDTO> DeleteClient(Guid id);
    }
}
