using MailKit;
using Microsoft.EntityFrameworkCore;
using VitakorProb.Context;
using VitakorProb.Interfaces;
using VitakorProb.Model;

namespace VitakorProb.Service;

public class BidService : IBidService
{
    private AppDbContext _dbContext;
    private IMailer _mailerService;
  
    public BidService(AppDbContext dbContext, IMailer mailerService)
    {
        _dbContext = dbContext;
        _mailerService = mailerService;
    }
    
    public async Task<Bid> PlaceBid(Bid bid)
    {
        var prevBid = await _dbContext.Bids
            .Include(b => b.User)
            .OrderByDescending(b => b.CreatedAt)
            .FirstOrDefaultAsync(b => b.MandarinId == bid.MandarinId);

        if (prevBid != null && bid.Cost > prevBid.Cost)
        {
            var prevBidderEmail = prevBid.User.Email;

            await _mailerService.SendEmailAsync(prevBidderEmail, "Ваша ставка перебита", $@"Кто-то поставил большую цену,
                Новая цена мандаринки: {bid.Cost}
                -- Service");
        }
        
        await _dbContext.Bids.AddAsync(bid);
        await _dbContext.SaveChangesAsync();

        return bid;
    }

    public async Task PlaceReceipt(Mandarin mandarin)
    {
        var lastBid = await _dbContext.Bids
            .Where(b => b.MandarinId == mandarin.Id)
            .Include(b => b.User)
            .OrderByDescending(m => m.CreatedAt)
            .FirstOrDefaultAsync();

        if (lastBid != null)
        {
            await _mailerService.SendEmailAsync(lastBid.User.Email, "Вы выиграли", $@"Поздравляем вы победили,
                ---
                Необходимо оплатить: {lastBid.Cost}
                -- Service");
        }
    }
}