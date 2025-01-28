using Microsoft.EntityFrameworkCore;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Model.Repositories;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }

    // Método personalizado para obtener un chat por RoomId
    public async Task<Chat?> GetByRoomIdAsync(Guid roomId)
    {
        return await _context.Chats.FirstOrDefaultAsync(c => c.Id == roomId);
    }

    // Implementación del método AddAsync
    public async Task AddAsync(Chat entity)
    {
        await _context.Chats.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // Implementación del método AddSync
    public Task AddSync(Chat entity)
    {
        _context.Chats.Add(entity);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    // Implementación del método FindByIdAsync
    public async Task<Chat?> FindByIdAsync(Guid id)
    {
        return await _context.Chats.FindAsync(id);
    }

    // Implementación del método UpdateAsync
    public async Task UpdateAsync(Chat entity)
    {
        _context.Chats.Update(entity);
        await _context.SaveChangesAsync();
    }

    // Implementación del método Update
    public void Update(Chat entity)
    {
        _context.Chats.Update(entity);
        _context.SaveChanges();
    }

    // Implementación del método DeleteAsync
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Chats.FindAsync(id);
        if (entity != null)
        {
            _context.Chats.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    // Implementación del método Remove
    public void Remove(Chat entity)
    {
        _context.Chats.Remove(entity);
        _context.SaveChanges();
    }

    // Implementación del método ListAsync
    public async Task<IEnumerable<Chat>> ListAsync()
    {
        return await _context.Chats.ToListAsync();
    }
}
