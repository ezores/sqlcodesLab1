package log.laboratoire.mappers;

import log.laboratoire.dto.dto.MovieDTO;
import log.laboratoire.dto.dto.RoleDTO;
import log.laboratoire.entity.movieEntities.*;

import java.util.ArrayList;
import java.util.List;

public class MovieMapper {
    private static int countryIdCounter = 1;
    private static int screenwriterIdCounter = 1;
    private static int movieTrailerIdCounter = 1;
    private static int movieCopyIdCounter = 1;

    public static List<Movie> map(List<MovieDTO> movieDTOList) {
        List<Movie> movieList = new ArrayList<>();

        for (MovieDTO movieDTO : movieDTOList) {
            Movie movie = new Movie();

            movie.setId(movieDTO.getId());
            movie.setTitle(movieDTO.getTitle());
            movie.setReleaseDate(movieDTO.getYear());
            movie.setOriginalLanguage(movieDTO.getLanguage());
            movie.setLength(movieDTO.getLength());
            movie.setGenres(movieDTO.getGenres());
            movie.setDirectorId(movieDTO.getDirector().getId());
            movie.setPoster(movieDTO.getPoster());

            setTrailers(movie, movieDTO);
            setCountries(movie, movieDTO);
            setScreenwriters(movie, movieDTO);
            setActors(movie, movieDTO);
            setCopies(movie);

            movieList.add(movie);
        }

        return movieList;
    }

    private static int getNextCountryId() {
        return countryIdCounter++;
    }

    private static int getNextScreenwriterId() {
        return screenwriterIdCounter++;
    }

    private static int getNextMovieTrailerId() {
        return movieTrailerIdCounter++;
    }

    private static int getNextMovieCopyId() {
        return movieCopyIdCounter++;
    }

    private static void setTrailers(Movie movie, MovieDTO movieDTO) {
        List<MovieTrailer> movieTrailerList = new ArrayList<>();

        for(String trailer : movieDTO.getAnnounces()) {
            movieTrailerList.add(new MovieTrailer(getNextMovieTrailerId(), movie.getId(), trailer));
        }

        movie.setTrailers(movieTrailerList);
    }

    private static void setCountries(Movie movie, MovieDTO movieDTO) {
        List<Country> countries = new ArrayList<>();

        for (String country : movieDTO.getCountries()) {
            countries.add(new Country(getNextCountryId(), country));
        }

        movie.setCountries(countries);
    }

    private static void setScreenwriters(Movie movie, MovieDTO movieDTO) {
        List<Screenwriter> screenwriters = new ArrayList<>();

        for (String screenwriter : movieDTO.getScreenwriters()) {
            screenwriters.add(new Screenwriter(getNextScreenwriterId(), screenwriter));
        }

        movie.setScreenwriters(screenwriters);
    }

    private static void setActors(Movie movie, MovieDTO movieDTO) {
        List<Actor> actors = new ArrayList<>();

        for (RoleDTO role: movieDTO.getRoles()) {
            Actor actor = new Actor(role.getActor().getId(), role.getCharacter());
            actors.add(actor);
        }

        movie.setActors(actors);
    }

    private static void setCopies(Movie movie) {
        List<MovieCopy> copies = new ArrayList<>();

        for(int i = 0; i < 2; i++) {
            copies.add(new MovieCopy(getNextMovieCopyId(), movie.getId()));
        }

        movie.setCopies(copies);
    }
}
