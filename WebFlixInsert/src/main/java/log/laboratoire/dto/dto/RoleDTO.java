package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

@XmlRootElement(name = "role")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class RoleDTO {

    private ActorDTO actor = new ActorDTO();
    private String character;

    @XmlElement(name = "acteur")
    public ActorDTO getActor() {
        return actor;
    }

    public void setActor(ActorDTO actor) {
        this.actor = actor;
    }

    @XmlElement(name = "personnage")
    public String getCharacter() {
        return character;
    }

    public void setCharacter(String character) {
        this.character = character;
    }
}
