using Server.Api.Models;
using System.Text.Json;

namespace Server.Api.Repositories;

public interface IKioskStateRepository
{
    Task<KioskState?> GetByKioskIdAsync(Guid kioskId);
    Task UpsertDraftAsync(Guid kioskId, JsonDocument state);
    Task UpsertPublishedAsync(Guid kioskId, JsonDocument state);
    Task ResetAsync(Guid kioskId);
}