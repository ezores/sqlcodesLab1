package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

@XmlRootElement(name = "realisateur")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class DirectorDTO {

    private int id;
    private String name;

    @XmlAttribute
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @XmlValue
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
