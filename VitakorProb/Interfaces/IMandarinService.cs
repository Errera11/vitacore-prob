using VitakorProb.Model;

namespace VitakorProb.Interfaces;

public interface IMandarinService
{
  public Task<Mandarin> CreateMandarin();
  public Task HandleMandarinLot();
  public Task<Mandarin> GetMandarin();
  public Task CleanupMandarins();
}