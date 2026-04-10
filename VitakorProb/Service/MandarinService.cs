using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using VitakorProb.Context;
using VitakorProb.Interfaces;
using VitakorProb.Model;

namespace VitakorProb.Service;

public class MandarinService : IMandarinService
{
  private AppDbContext _dbContext;
  private IBidService _bidService;
  
  public MandarinService(AppDbContext dbContext,  IBidService bidService)
  {
    _dbContext = dbContext;
    _bidService = bidService;
  }
  
  public async Task<Mandarin> CreateMandarin()
  {
    var mandarin = Mandarin.CreateLot(); 
    
    _dbContext.Add(mandarin);
    await _dbContext.SaveChangesAsync();
    
    return mandarin;
  }

  public async Task HandleMandarinLot()
  {
    var lastExposedMandarin = await _dbContext.Mandarins
      .Where(m => m.Status == MandarinStatusEnum.Pending)
      .OrderByDescending(m => m.ExpiresAt)
      .FirstOrDefaultAsync();

    if (lastExposedMandarin != null)
    {
      lastExposedMandarin.Status = MandarinStatusEnum.Sold;
      await _dbContext.SaveChangesAsync();
      
      await _bidService.PlaceReceipt(lastExposedMandarin);
    }
    
    await CreateMandarin();
  }

  public async Task<Mandarin> GetMandarin()
  {
    var mandarin = await _dbContext.Mandarins
      .OrderByDescending(m => m.ExpiresAt)
      .Include(m => m.Bids)
      .ThenInclude(b => b.User)
      .FirstOrDefaultAsync();
    
     if (mandarin == null)
     {
         return await CreateMandarin();
     }
     
     return mandarin;
  }

  public async Task CleanupMandarins()
  {
    var currentTime = DateTime.Now;
    await _dbContext.Mandarins.Where(m => m.ExpiresAt < currentTime).ExecuteDeleteAsync();
  }
}