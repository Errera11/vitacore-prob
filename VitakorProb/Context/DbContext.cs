using Microsoft.EntityFrameworkCore;
using VitakorProb.Model;

namespace VitakorProb.Context;

public class AppDbContext : DbContext
{
    public DbSet<Mandarin> Mandarins { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "vitakor.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}