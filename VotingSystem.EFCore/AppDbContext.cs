using Microsoft.EntityFrameworkCore;

namespace VotingSystem.EFCore;

public class AppDbContext :DbContext
{
    // provide abstractions for a table

    public DbSet<Fruit> Fruits { get; set; }
    


}

public class Fruit{
    public string Name { get; set; }
    public int Weight { get; set; }
}
