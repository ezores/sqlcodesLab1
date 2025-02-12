package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

@XmlRootElement(name = "film")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class MovieDTO {
    private int id;
    private String title;
    private int year;
    private List<String> countries = new ArrayList<String>();
    private String language;
    private int length;
    private String synopsis;
    private List<String> genres =  new ArrayList<>();
    private DirectorDTO director = new DirectorDTO();
    private List<String> screenwriters = new ArrayList<>();
    private List<RoleDTO> roles = new ArrayList<>();
    private String poster;
    private List<String> announces =  new ArrayList<>();

    @XmlAttribute(name = "id")
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @XmlElement(name = "titre")
    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    @XmlElement(name = "annee")
    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }

    @XmlElement(name = "pays")
    public List<String> getCountries() {
        return countries;
    }

    public void setCountries(List<String> countries) {
        this.countries = countries;
    }

    @XmlElement(name = "langue")
    public String getLanguage() {
        return language;
    }

    public void setLanguage(String language) {
        this.language = language;
    }

    @XmlElement(name = "duree")
    public int getLength() {
        return length;
    }

    public void setLength(int length) {
        this.length = length;
    }

    @XmlElement(name = "resume")
    public String getSynopsis() {
        return synopsis;
    }

    public void setSynopsis(String synopsis) {
        this.synopsis = synopsis;
    }

    @XmlElement(name = "genre")
    public List<String> getGenres() {
        return genres;
    }

    public void setGenres(List<String> genres) {
        this.genres = genres;
    }

    @XmlElement(name = "realisateur")
    public DirectorDTO getDirector() {
        return director;
    }

    public void setDirector(DirectorDTO director) {
        this.director = director;
    }

    @XmlElement(name = "scenariste")
    public List<String> getScreenwriters() {
        return screenwriters;
    }

    public void setScreenwriters(List<String> screenwriters) {
        this.screenwriters = screenwriters;
    }

    @XmlElement(name = "role")
    public List<RoleDTO> getRoles() {
        return roles;
    }

    public void setRoles(List<RoleDTO> roles) {
        this.roles = roles;
    }

    @XmlElement(name = "poster")
    public String getPoster() {
        return poster;
    }

    public void setPoster(String poster) {
        this.poster = poster;
    }

    @XmlElement(name = "annonce")
    public List<String> getAnnounces() {
        return announces;
    }

    public void setAnnounces(List<String> announces) {
        this.announces = announces;
    }

}
