using CRMVM_DAL.Interfaces;
using CRMVM_DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Ovile_DAL_Layer.Contexts;

namespace CRMVM_DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly CRMVM_db_context _context;

        public ClientRepository(CRMVM_db_context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Get all clients with their deals
        /// </summary>
        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients
                .ToListAsync();
        }

        /// <summary>
        /// Get a client by id with their deals
        /// </summary>
        public async Task<Client?> Get(Guid id)
        {
            var result = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                throw new Exception($"User with id {id} is not found");
            }
            return result;
                
        }

        /// <summary>
        /// Find client with their deals
        /// </summary>
        public async Task<Client?> Find(Func<Client, bool> predicate)
        {
            // Заменяем Task.Run на асинхронный метод
            return await _context.Clients
                .FirstOrDefaultAsync(c => predicate(c));
        }

        /// <summary>
        /// Create client and save changes
        /// </summary>
        public async Task Create(Client item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _context.Clients.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update client and save changes
        /// </summary>
        public async Task Update(Client item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete client and save changes
        /// </summary>
        public async Task Delete(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}