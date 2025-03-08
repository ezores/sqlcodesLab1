using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Repositories.Interfaces;

namespace Webflix.Services;

public class CopieFilmService
{
    private readonly ICopieFilmRepository _copieFilmRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IRentalRepository _rentalRepository;
    
    public CopieFilmService(ICopieFilmRepository copieFilmRepository, IClientRepository clientRepository, IRentalRepository rentalRepository)
    {
        _copieFilmRepository = copieFilmRepository;
        _clientRepository = clientRepository;
        _rentalRepository = rentalRepository;
    }
    
    //If dispo ET depasse pas nb max d'emprunt de son abonnement
    //Add a copie to Emprunt avec la date
    //Change statut de la copie
    //PARAMS: Film, Copies, Client
    //RETURN:
    public async Task RentMovie(int filmId, int clientId)
    {

        if (await _clientRepository.CanRentMoreFilmsAsync(clientId) == false)
        {
            Console.WriteLine("Maximum rental for your subscription reached");
            return;
        }
        
        IEnumerable<CopieFilm> availableCopies = await _copieFilmRepository.GetAvailableCopiesAsync(filmId);

        if (!availableCopies.Any())
        {
            Console.WriteLine("No available copies to rent.");
            Console.WriteLine("There is" + availableCopies.Count());
            return;
        }
        
        int selectedCopyId = availableCopies.First().CopieId;
        await _copieFilmRepository.UpdateStatusAsync(selectedCopyId, StatutCopie.PRETE);

        Client client = await _clientRepository.GetByIdAsync(clientId);
        
        Emprunt newRental = new Emprunt
        {
            NomUsager = client.Courriel,
            CopieId = selectedCopyId,
            DateDebutEmprunt = DateTime.UtcNow
        };
        await _rentalRepository.AddAsync(newRental);
        Console.WriteLine($"Movie rented successfully: {selectedCopyId}" + " by " +  client.Courriel);
    }
    
    //Au retour, emleve de emprunt
    //Rechange pour statut dispo
    public async void ReturnMovie(int filmId, int clientId)
    {
        // Find the rental for this client and film
        Emprunt rental = await _rentalRepository.GetActiveRentalAsync(clientId, filmId);

        if (rental == null)
        {
            Console.WriteLine("No active rental found for this movie and client.");
            return;
        }

        // Update the copy status to available
        await _copieFilmRepository.UpdateStatusAsync(rental.CopieId, StatutCopie.DISPONIBLE);

        // Remove the rental record
        await _rentalRepository.DeleteAsync(rental);

        Console.WriteLine($"Movie returned successfully: {rental.CopieId} by {rental.NomUsager}");
    }
}