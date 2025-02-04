package log.laboratoire.services.XmlParser.Strategies;

public interface IParsingStrategy<T> {
    /**
     * parse XML file into a DTO collection of type T
     * @param filePath Path to the XML file
     * @return The DTO collection
     * @throws Exception
     */
    T parse(String filePath) throws Exception;
}
