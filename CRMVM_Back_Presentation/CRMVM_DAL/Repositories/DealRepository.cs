using CRMVM_DAL.Interfaces;
using CRMVM_DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Ovile_DAL_Layer.Contexts;

/// <summary>
/// Repository for managing Deal entities
/// </summary>
public class DealRepository : IRepository<Deal>
{
    private readonly CRMVM_db_context _context;

    /// <summary>
    /// Initializes a new instance of DealRepository
    /// </summary>
    /// <param name="context">Database context</param>
    public DealRepository(CRMVM_db_context context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all deals from the database
    /// </summary>
    /// <returns>Collection of all deals</returns>
    public async Task<IEnumerable<Deal>> GetAll()
    {
        return await _context.Deals
            .ToListAsync();
    }

    /// <summary>
    /// Gets a deal by its identifier
    /// </summary>
    /// <param name="id">The deal identifier</param>
    /// <returns>Deal if found, null otherwise</returns>
    public async Task<Deal?> Get(Guid id)
    {
        return await _context.Deals
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    /// <summary>
    /// Finds a deal based on a predicate
    /// </summary>
    /// <param name="predicate">The condition to filter deals</param>
    /// <returns>First deal matching the condition, null if not found</returns>
    public async Task<Deal?> Find(Func<Deal, bool> predicate)
    {
        return await _context.Deals
            .FirstOrDefaultAsync(d => predicate(d));
    }

    /// <summary>
    /// Creates a new deal in the database
    /// </summary>
    /// <param name="item">Deal to create</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task Create(Deal item)
    {
        await _context.Deals.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing deal in the database
    /// </summary>
    /// <param name="item">Deal with updated values</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task Update(Deal item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a deal from the database
    /// </summary>
    /// <param name="id">Identifier of the deal to delete</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task Delete(Guid id)
    {
        var deal = await _context.Deals.FindAsync(id);
        if (deal != null)
        {
            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();
        }
    }
}