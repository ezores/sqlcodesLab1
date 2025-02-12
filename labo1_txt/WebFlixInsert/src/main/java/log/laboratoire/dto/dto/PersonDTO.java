package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

@XmlRootElement(name = "personne")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class PersonDTO {

    private int id;
    private String name;
    private BirthDTO birth = new BirthDTO();
    private String photo;
    private String bio;

    @XmlAttribute
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @XmlElement(name = "nom")
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @XmlElement(name = "naissance")
    public BirthDTO getBirth() {
        return birth;
    }

    public void setBirth(BirthDTO birth) {
        this.birth = birth;
    }

    @XmlElement(name = "photo")
    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    @XmlElement(name = "bio")
    public String getBio() {
        return bio;
    }

    public void setBio(String bio) {
        this.bio = bio;
    }
}
