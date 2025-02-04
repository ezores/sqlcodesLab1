package log.laboratoire.dto.collections;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import log.laboratoire.dto.dto.PersonDTO;

import java.util.List;

@XmlRootElement(name = "personnes")
public class People {

    private List<PersonDTO> people;

    @XmlElement(name = "personne")
    public List<PersonDTO> getPeople() {
        return people;
    }

    public void setPeople(List<PersonDTO> people) {
        this.people = people;
    }
}
