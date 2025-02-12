package log.laboratoire.services.XmlParser.Strategies;

import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.Unmarshaller;
import log.laboratoire.dto.collections.Movies;

import java.io.File;

public class MovieParsingStrategy implements IParsingStrategy<Movies> {

    @Override
    public Movies parse(String filePath) throws Exception {
        JAXBContext context = JAXBContext.newInstance(Movies.class);
        Unmarshaller unmarshaller = context.createUnmarshaller();
        return (Movies) unmarshaller.unmarshal(new File(filePath));
    }
}
