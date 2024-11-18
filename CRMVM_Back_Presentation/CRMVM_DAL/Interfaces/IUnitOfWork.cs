
using CRMVM_DAL.Models.Entities;

namespace CRMVM_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Client> Clients { get; }
        IRepository<Deal> Deals { get; }

        Task CommitChanges();
    }
}