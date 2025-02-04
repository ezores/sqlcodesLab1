package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

@XmlRootElement(name = "acteur")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class ActorDTO {
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
