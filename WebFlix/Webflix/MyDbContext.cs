using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Webflix.Models.Entities;

namespace Webflix;

public class MyDbContext : DbContext
{
    public DbSet<Employe> Employes { get; set; }
    public DbSet<Client> Clients { get; set; }

    public DbSet<CarteCredit> CartesCredit { get; set; }
    public DbSet<CopieFilm> CopiesFilm { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Adresse> Adresses { get; set; }
    public DbSet<BandeAnnonce> BandesAnnonces { get; set; }
    public DbSet<Emprunt> Emprunts { get; set; }
    public DbSet<Personne> Personnes { get; set; }
    public DbSet<Pays> Pays { get; set; }
    public DbSet<Scenariste> Scenaristes { get; set; }
    public DbSet<Abonnement> Abonnements { get; set; }
    public DbSet<ActeurFilm> ActeursFilms { get; set; }
    public DbSet<GenreFilm> GenresFilms { get; set; }
    public DbSet<PaysFilm> PaysFilms { get; set; }
    public DbSet<ScenaristeFilm> ScenaristesFilms { get; set; }
    public DbSet<MovieRating> MovieRatings { get; set; }
    public DbSet<MovieCorrelation> MovieCorrelations { get; set; }
    public DbSet<ClientMovie> ClientMovies { get; set; }
    
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration de la conversion pour l'enum StatutCopie
        modelBuilder.Entity<CopieFilm>()
            .Property(e => e.Statut)
            .HasConversion<string>(); // Converts Enum to VARCHAR
            
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
        
        modelBuilder.Entity<BandeAnnonce>()
            .HasKey(b => new { b.Id, b.FilmId });
            
        modelBuilder.Entity<Emprunt>()
            .HasKey(e => new { e.CopieId, e.ClientId });
        
        modelBuilder.Entity<ClientMovie>()
            .HasKey(cm => new { cm.ClientId, cm.MovieId });
        
        modelBuilder.Entity<MovieRating>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VUE_MOYENNE");
        });
        
        modelBuilder.Entity<MovieCorrelation>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VUE_CORRELATIONS");
        });
            
        // Configuration des relations
        modelBuilder.Entity<Film>()
            .HasMany(f => f.BandesAnnonces)
            .WithOne(ba => ba.Film)
            .HasForeignKey(ba => ba.FilmId);


        modelBuilder.Entity<Film>()
            .HasOne(e => e.Realisateur)
            .WithMany(e => e.FilmsRealises)
            .HasForeignKey(e => e.RealisateurId)
            .IsRequired();
        
        
        // Configure Client -> Abonnement relationship
        // Remove the explicit HasPrincipalKey for Abonnement
        modelBuilder.Entity<Client>()
            .Property(c => c.CodeAbonnement)
            .HasConversion(
                v => v.ToString(),   // Convert from string to CHAR(1)
                v => v.Length > 0 ? v[0].ToString() : null  // Ensure conversion back to string
            );

        modelBuilder.Entity<Client>()
            .HasOne(c => c.Abonnement)
            .WithMany(a => a.Clients)
            .HasForeignKey(c => c.CodeAbonnement)
            .HasPrincipalKey(a => a.Code);

        
        modelBuilder.Entity<Client>()
            .Property(c => c.CarteCreditId)
            .HasConversion<string>();  // Force CarteCreditId to be treated as a string

        // Configure Client -> CarteCredit relationship
        modelBuilder.Entity<Client>()
            .HasOne(c => c.CarteCredit)
            .WithMany(cc => cc.Clients)
            .HasForeignKey(c => c.CarteCreditId);
            
        // Autres configurations de relations...
        
        base.OnModelCreating(modelBuilder);
    }
}