using CRMVM_DAL.Interfaces;
using CRMVM_DAL.Models.Entities;
using CRMVM_DAL.Repositories;
using Ovile_DAL_Layer.Contexts;

namespace CRMVM_DAL.Repositories
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private bool disposed = false;
        private readonly CRMVM_db_context _context;

        private ClientRepository _clientRepository;
        private DealRepository _dealRepository;

        public UnitOfWorkEF(CRMVM_db_context context)
        {
            _context = context;
        }

        public IRepository<Client> Clients
        {
            get
            {
                _clientRepository ??= new ClientRepository(_context);
                return _clientRepository;
            }
        }

        public IRepository<Deal> Deals
        {
            get
            {
                _dealRepository ??= new DealRepository(_context);
                return _dealRepository;
            }
        }

        public async Task CommitChanges()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}