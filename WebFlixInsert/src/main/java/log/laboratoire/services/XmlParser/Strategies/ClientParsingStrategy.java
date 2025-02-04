package log.laboratoire.services.XmlParser.Strategies;

import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.Unmarshaller;
import log.laboratoire.dto.collections.Clients;

import java.io.File;

public class ClientParsingStrategy implements IParsingStrategy<Clients>{

    @Override
    public Clients parse(String filePath) throws Exception {
        JAXBContext context = JAXBContext.newInstance(Clients.class);
        Unmarshaller unmarshaller = context.createUnmarshaller();
        return (Clients) unmarshaller.unmarshal(new File(filePath));
    }
}
