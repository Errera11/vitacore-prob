using VitakorProb.Model;

namespace VitakorProb.Interfaces;

public interface IBidService
{
    public Task<Bid> PlaceBid(Bid bid);
    public Task PlaceReceipt(Mandarin mandarin);
}