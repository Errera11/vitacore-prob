namespace VitakorProb.Model;

public class Bid
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Cost { get; set; }
    public int MandarinId { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
    public Mandarin Mandarin { get; set; }
}