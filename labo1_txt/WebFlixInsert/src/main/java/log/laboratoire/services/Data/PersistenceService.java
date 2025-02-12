package log.laboratoire.services.Data;

import log.laboratoire.config.DatabaseConfig;
import log.laboratoire.entity.clientEntities.Client;
import log.laboratoire.entity.movieEntities.Movie;
import log.laboratoire.entity.Person;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;

public class PersistenceService {

    private final ClientDataService clientDataService;
    private final MovieDataService movieDataService;
    private final PersonDataService personDataService;

    public PersistenceService() {
        this.clientDataService = new ClientDataService();
        this.movieDataService = new MovieDataService();
        this.personDataService = new PersonDataService();
    }

    public void insertAllData(List<Client> clients, List<Person> persons, List<Movie> movies) {
        try (Connection conn = DatabaseConfig.getConnection()) {
            System.out.println("Inserting Clients.");
            clientDataService.insertData(conn, clients);

            System.out.println("Inserting People.");
            personDataService.insertData(conn, persons);

            System.out.println("Inserting Movies.");
            movieDataService.insertData(conn, movies);
        } catch (SQLException e) {
            throw new RuntimeException("Database connection error", e);
        }
    }
}
