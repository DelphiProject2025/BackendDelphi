﻿using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.Delphi.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using delphibackend.User.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace delphibackend.Delphi.Infrastructure.Persistence.EFC.Repositories;

public class RoomRepository(AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
    private readonly AppDbContext _context;

    // Find Room by Id
    public async Task<Room?> FindByIdAsync(Guid roomId)
    {
        return await _context.Rooms
            .Include(r => r.Host) // Incluir la relación con Host
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }


    // Find Room by Name
    public async Task<Room?> FindByNameAsync(string roomName)
    {
        return await _context.Rooms
            .FirstOrDefaultAsync(r => r.RoomName == roomName);
    }

    // Get Room with Users
    public async Task<Room?> GetRoomWithHostsAsync(Guid roomId)
    {
        return await _context.Rooms
            .Include(r => r.Host)
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }

    // Get Participants by Room Id
    
    // Save changes to the database
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}