using System;
using Microsoft.EntityFrameworkCore;
using Webflix;
using Webflix.Models;

public class MyDbContext : DbContext
{
    public DbSet<CarteCredit> CartesCredit { get; set; }
    public DbSet<CopieFilm> CopiesFilm { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Adresse> Adresses { get; set; }
    public DbSet<BandeAnnonce> BandesAnnonces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle("User Id=EQUIPE201;Password=yy3IR1VP;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=bdlog660.ens.ad.etsmtl.ca)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCLPDB.ens.ad.etsmtl.ca)))");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration de la conversion pour l'enum StatutCopie
        modelBuilder.Entity<CopieFilm>()
            .Property(e => e.Statut)
            .HasConversion(
                v => v.ToString(),
                v => (StatutCopie)Enum.Parse(typeof(StatutCopie), v)
            );
            
        // Vous pouvez ajouter d'autres configurations spécifiques ici
        
        // Par exemple, configuration des clés composites pour les tables de jointure
        modelBuilder.Entity<ActeurFilm>()
            .HasKey(af => new { af.ActeurId, af.FilmId });
            
        modelBuilder.Entity<ScenaristeFilm>()
            .HasKey(sf => new { sf.ScenaristeId, sf.FilmId });
            
        modelBuilder.Entity<PaysFilm>()
            .HasKey(pf => new { pf.PaysId, pf.FilmId });
            
        modelBuilder.Entity<GenreFilm>()
            .HasKey(gf => new { gf.Genre, gf.FilmId });
            
        modelBuilder.Entity<Emprunt>()
            .HasKey(e => new { e.CopieId, e.NomUsager });
            
        // Configuration des relations
        modelBuilder.Entity<Film>()
            .HasMany(f => f.BandesAnnonces)
            .WithOne(ba => ba.Film)
            .HasForeignKey(ba => ba.FilmId);
            
        // Autres configurations de relations...
        
        base.OnModelCreating(modelBuilder);
    }
}