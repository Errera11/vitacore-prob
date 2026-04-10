namespace VitakorProb.Interfaces;

public interface ILot<T>
{
    public static T CreateLot()
    {
        throw new NotImplementedException();
    }
    
    DateTime CreatedAt { get; set; }
    DateTime ExpiresAt { get; set; }
}