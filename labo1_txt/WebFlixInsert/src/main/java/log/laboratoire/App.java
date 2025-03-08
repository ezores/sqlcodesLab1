package log.laboratoire;

import log.laboratoire.dto.collections.Clients;
import log.laboratoire.dto.collections.Movies;
import log.laboratoire.dto.collections.People;
import log.laboratoire.entity.Person;
import log.laboratoire.entity.clientEntities.Client;
import log.laboratoire.entity.movieEntities.Movie;
import log.laboratoire.mappers.ClientMapper;
import log.laboratoire.mappers.MovieMapper;
import log.laboratoire.mappers.PersonMapper;
import log.laboratoire.services.Data.PersistenceService;
import log.laboratoire.services.XmlParser.XmlParserService;

import java.io.File;
import java.util.List;

public class App {

    public static void main(String[] args) {
        // Validate arguments
//        if (args.length < 3) {
//            System.out.println("Usage: java -jar your-app.jar <movies_xml_path> <clients_xml_path> <people_xml_path>");
//            System.exit(1);
//        }

        String moviesXmlPath = "C:/Users/max97/OneDrive/Bureau/All/ETS/LOG/Session5/LOG660/Donnees/films_latin1.xml";
        String clientsXmlPath = "C:/Users/max97/OneDrive/Bureau/All/ETS/LOG/Session5/LOG660/Donnees/clients_latin1.xml";
        String peopleXmlPath = "C:/Users/max97/OneDrive/Bureau/All/ETS/LOG/Session5/LOG660/Donnees/personnes_latin1.xml";

        // Validate if files exist
        validateFile(moviesXmlPath, "Movies XML");
        validateFile(clientsXmlPath, "Clients XML");
        validateFile(peopleXmlPath, "People XML");

        // Initialize XML Parserj
        XmlParserService xmlParserService = new XmlParserService();

        // Parse XML files
        System.out.println("Parsing Movies.");
        Movies movies = xmlParserService.parseXml(moviesXmlPath, Movies.class);

        System.out.println("Parsing Clients.");
        Clients clients = xmlParserService.parseXml(clientsXmlPath, Clients.class);

        System.out.println("Parsing People.");
        People people = xmlParserService.parseXml(peopleXmlPath, People.class);

        // Convert parsed data into Lists
        List<Movie> movieList = MovieMapper.map(movies.getMovies());
        List<Client> clientList = ClientMapper.map(clients.getClients());
        List<Person> personList = PersonMapper.map(people.getPeople());

        // Insert data into database
        PersistenceService dbService = new PersistenceService();
        dbService.insertAllData(clientList, personList, movieList);

        System.out.println("Data successfully inserted from provided XML files!");
    }

    // Helper method to validate if file exists
    private static void validateFile(String filePath, String fileType) {
        File file = new File(filePath);
        if (!file.exists() || !file.isFile()) {
            System.err.println("Error: " + fileType + " file not found - " + filePath);
            System.exit(1);
        }
    }
}