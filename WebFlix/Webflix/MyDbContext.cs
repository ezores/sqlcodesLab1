using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<CarteCreditBackup> CartesCreditBackup { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle("User Id=EQUIPE201;Password=yy3IR1VP;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=bdlog660.ens.ad.etsmtl.ca)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCLPDB.ens.ad.etsmtl.ca)))");
    }
}