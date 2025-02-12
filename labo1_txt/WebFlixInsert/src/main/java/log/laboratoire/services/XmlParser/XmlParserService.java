package log.laboratoire.services.XmlParser;

import log.laboratoire.dto.collections.Clients;
import log.laboratoire.dto.collections.Movies;
import log.laboratoire.dto.collections.People;
import log.laboratoire.services.XmlParser.Strategies.ClientParsingStrategy;
import log.laboratoire.services.XmlParser.Strategies.IParsingStrategy;
import log.laboratoire.services.XmlParser.Strategies.MovieParsingStrategy;
import log.laboratoire.services.XmlParser.Strategies.PeopleParsingStrategy;

import java.util.Map;

public class XmlParserService {

    private final Map<Class<?>, IParsingStrategy<?>> strategies = Map.of(
            Movies.class, new MovieParsingStrategy(),
            Clients.class, new ClientParsingStrategy(),
            People.class, new PeopleParsingStrategy()
    );

    public <T> T parseXml(String filePath, Class<T> type) {

        // Get the appropriate strategy to parse the XML file
        IParsingStrategy<?> strategy = strategies.get(type);
        if (strategy == null) {
            throw new IllegalArgumentException("No parsing strategy found for: " + type.getSimpleName());
        }

        // Parse the file and cast into the DTO collection type
        try {
            return type.cast(strategy.parse(filePath));
        } catch (Exception e) {
            System.err.println("Error parsing " + type.getSimpleName() + ": " + e.getMessage());
            return null;
        }
    }
}
