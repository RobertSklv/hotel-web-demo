using HotelWebDemo.Models.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelWebDemo.Data.Seeding;

public class JsonSeeder<TEntity>
    where TEntity : BaseEntity
{
    public const string DIR = @"Data\Seeding\";

    private readonly string dir;

    private readonly string name;

    private JsonSeeder(string name, string dir = "DefaultData")
    {
        this.name = name + ".json";
        this.dir = dir;
    }

    private void PrivateSeed(ModelBuilder modelBuilder)
    {
        try
        {
            List<TEntity> data = LoadData();

            for (int i = 0; i < data.Count; i++)
            {
                data[i].Id = i + 1;
            }

            modelBuilder.Entity<TEntity>().HasData(data);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while seeding '{name}'. Original message: {e.Message}");
        }
    }

    public List<TEntity> LoadData()
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), DIR, this.dir, name);

        string rawJson = File.ReadAllText(dir);
        List<TEntity> data = JsonConvert.DeserializeObject<List<TEntity>>(rawJson) ?? throw new Exception("Failed to deserialize data.");

        return data;
    }

    public static void SeedDefaultData(ModelBuilder modelBuilder, string seederName)
    {
        new JsonSeeder<TEntity>(seederName, "DefaultData").PrivateSeed(modelBuilder);
    }

    public static void SeedSampleData(ModelBuilder modelBuilder, string seederName)
    {
        new JsonSeeder<TEntity>(seederName, "SampleData").PrivateSeed(modelBuilder);
    }
}
