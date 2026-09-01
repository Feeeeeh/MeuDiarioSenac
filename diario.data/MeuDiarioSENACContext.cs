using Microsoft.EntityFrameworkCore;

public class MeuDiarioSENACContext : DbContext
{
    public DbSet<Registro> Registros { get; set; }
    private readonly string ConnectionString = 
    "server=localhost;port=3305;database=MeuDiarioSENAC;uid=root;pwd=1234";

    protected override void OnConfiguring
    (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(ConnectionString,
            ServerVersion.AutoDetect(ConnectionString));
    }
}