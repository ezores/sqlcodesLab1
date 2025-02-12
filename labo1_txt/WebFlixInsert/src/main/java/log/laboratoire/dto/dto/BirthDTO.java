package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

@XmlRootElement(name = "naissance")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class BirthDTO {

    private String birthday = new String();
    private String birthPlace;

    @XmlElement(name = "anniversaire")
    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    @XmlElement(name = "lieu")
    public String getBirthPlace() {
        return birthPlace;
    }

    public void setBirthPlace(String birthPlace) {
        this.birthPlace = birthPlace;
    }
}
