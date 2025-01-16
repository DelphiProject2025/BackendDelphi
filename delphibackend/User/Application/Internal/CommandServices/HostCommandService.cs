using delphibackend.User.Domain.Model.Entities;
using delphibackend.User.Domain.Repositories;
using delphibackend.User.Domain.Services;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.User.Application.Internal.CommandServices;

public class HostCommandService : IHostCommandService
{
    private readonly IHostRepository _hostRepository;

    public HostCommandService(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task<Host> CreateHostAsync(Guid authUserId)
    {
        var host = new Host
        {
            AuthUserId = authUserId,
            IsActive = true, 
            CreatedAt = DateTime.UtcNow
        };

        await _hostRepository.AddAsync(host);
        return host;
    }

    public async Task<bool> DeleteHostAsync(Guid hostId)
    {
        await _hostRepository.DeleteAsync(hostId);
        return true;
    }

    public async Task<bool> UpdateHostAsync(Host host)
    {
        var existingHost = await _hostRepository.FindByIdAsync(host.Id);
        if (existingHost == null) return false;

        // Actualizar campos relevantes
        existingHost.AuthUserId = host.AuthUserId;
        existingHost.IsActive = host.IsActive;
        existingHost.CreatedAt = host.CreatedAt;

        await _hostRepository.UpdateAsync(existingHost);
        return true;
    }

    public async Task<bool> ActivateHostAsync(Guid hostId)
    {
        var host = await _hostRepository.FindByIdAsync(hostId);
        if (host == null) return false;

        host.IsActive = true;
        await _hostRepository.UpdateAsync(host);
        return true;
    }

    public async Task<bool> DeactivateHostAsync(Guid hostId)
    {
        var host = await _hostRepository.FindByIdAsync(hostId);
        if (host == null) return false;

        host.IsActive = false;
        await _hostRepository.UpdateAsync(host);
        return true;
    }
}