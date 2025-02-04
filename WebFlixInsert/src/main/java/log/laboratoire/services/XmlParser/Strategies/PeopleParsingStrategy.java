package log.laboratoire.services.XmlParser.Strategies;

import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.Unmarshaller;
import log.laboratoire.dto.collections.People;

import java.io.File;

public class PeopleParsingStrategy implements IParsingStrategy<People> {

    @Override
    public People parse(String filePath) throws Exception {
        JAXBContext context = JAXBContext.newInstance(People.class);
        Unmarshaller unmarshaller = context.createUnmarshaller();
        return (People) unmarshaller.unmarshal(new File(filePath));
    }
}
