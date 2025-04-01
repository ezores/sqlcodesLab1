﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Models.Entities;
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
    
    public Task<bool> CheckIfCanRentMore(int clientId) => _clientRepository.CanRentMoreFilmsAsync(clientId);
    
    public Task<IEnumerable<CopieFilm>> GetAvailableCopiesAsync(int filmId) => _copieFilmRepository.GetAvailableCopiesAsync(filmId);
    
    public async Task RentMovieAsync(IEnumerable<CopieFilm> availableCopies, int clientId)
    {
        var selectedCopyId = availableCopies.First().CopieId;
        await _copieFilmRepository.UpdateStatusAsync(selectedCopyId, StatutCopie.PRETE);

        var client = await _clientRepository.GetByIdAsync(clientId);
        
        var newRental = new Emprunt
        {
            ClientId = client.ClientId,
            CopieId = selectedCopyId,
            DateDebutEmprunt = DateTime.UtcNow
        };
        
        await _rentalRepository.AddAsync(newRental);
        Console.WriteLine($"Movie rented successfully: {selectedCopyId}" + " by " +  client.Courriel);
    }
    
    public async Task ReturnMovie(int filmId, int clientId)
    {
        // Find the rental for this client and film
        var rental = await _rentalRepository.GetActiveRentalAsync(clientId, filmId);

        if (rental == null)
        {
            Console.WriteLine("No active rental found for this movie and client.");
            return;
        }

        // Update the copy status to available
        await _copieFilmRepository.UpdateStatusAsync(rental.CopieId, StatutCopie.DISPONIBLE);

        // Remove the rental record
        await _rentalRepository.DeleteAsync(rental);

        Console.WriteLine($"Movie returned successfully: {rental.CopieId} by {rental.ClientId}");
    }
}