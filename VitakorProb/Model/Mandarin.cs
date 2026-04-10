using Microsoft.EntityFrameworkCore;
using VitakorProb.Interfaces;

namespace VitakorProb.Model;

public enum MandarinStatusEnum
{
    Pending,
    Sold,
    Failed
}

public class Mandarin : ILot<Mandarin>
{
    private static readonly List<string> UrlPool =
    [
        "https://ixbt.online/live/ai/fusionbrain/00/33/44/16/98a2e4c2a0.jpg",
        "https://madarmart.com/wp-content/uploads/2025/04/mandarin-freshleaf-dubai-uae-img01.jpg",
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRDWq_3tfbQw2rnDV2Nq0sv4QU3I9WkTLtRlw&s",
    ];

    private static readonly List<string> DescriptionPool =
    [
        "Яркая оранжевая сфера, заряженная витаминным электричеством. Взрыв сока, который переносит в эпицентр праздника.",
        "Маленькое оранжевое солнце, скрывающее внутри медовое цунами. Текстура — чистый шелк, вкус — абсолютный экстаз. Самый сочный хит этой зимы.",
        "Идеальный глянец и безупречная форма. Каждая долька — это концентрат свежести, бьющий точно в цель. Запредельный уровень сладости в каждом сегменте."
    ];

    public static Mandarin CreateLot()
    {
        Random rnd = new Random();
        
        int descriptionPoolLen = DescriptionPool.Count;
        var description = DescriptionPool[rnd.Next(0, descriptionPoolLen)];
        
        int urlPoolLen = UrlPool.Count;
        var url = UrlPool[rnd.Next(0, urlPoolLen)];
        
        var expiresAt = DateTime.UtcNow.AddMinutes(1);

        return new Mandarin() { Description = description, Url = url, ExpiresAt = expiresAt};
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public string Url { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ExpiresAt { get; set; } = DateTime.Now;
    public MandarinStatusEnum Status { get; set; } = MandarinStatusEnum.Pending;

    public List<Bid> Bids { get; } = [];
}