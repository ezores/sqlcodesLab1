package log.laboratoire.dto.collections;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import log.laboratoire.dto.dto.MovieDTO;

import java.util.List;

@XmlRootElement(name = "films")
public class Movies {

    private List<MovieDTO> movies;

    @XmlElement(name = "film")
    public List<MovieDTO> getMovies() {
        return movies;
    }

    public void setMovies(List<MovieDTO> movies) {
        this.movies = movies;
    }
}
