package log.laboratoire.entity.movieEntities;

import java.util.List;

public class Movie {
    private int id;
    private String title;
    private int releaseDate;
    private List<Country> countries;
    private String originalLanguage;
    private int length;
    private String synopsis;
    private List<String> genres;
    private int directorId;
    private List<Actor> actors;
    private List<Screenwriter> screenwriters;
    private List<MovieTrailer> trailers;
    private String poster;
    private List<MovieCopy> copies;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public int getReleaseDate() {
        return releaseDate;
    }

    public void setReleaseDate(int releaseDate) {
        this.releaseDate = releaseDate;
    }

    public String getOriginalLanguage() {
        return originalLanguage;
    }

    public void setOriginalLanguage(String originalLanguage) {
        this.originalLanguage = originalLanguage;
    }

    public int getLength() {
        return length;
    }

    public void setLength(int length) {
        this.length = length;
    }

    public List<Country> getCountries() {
        return countries;
    }

    public void setCountries(List<Country> countries) {
        this.countries = countries;
    }

    public int getDirectorId() {
        return directorId;
    }

    public void setDirectorId(int directorId) {
        this.directorId = directorId;
    }

    public List<Actor> getActors() {
        return actors;
    }

    public void setActors(List<Actor> actors) {
        this.actors = actors;
    }

    public List<Screenwriter> getScreenwriters() {
        return screenwriters;
    }

    public void setScreenwriters(List<Screenwriter> screenwriters) {
        this.screenwriters = screenwriters;
    }

    public List<MovieTrailer> getTrailers() {
        return trailers;
    }

    public void setTrailers(List<MovieTrailer> trailers) {
        this.trailers = trailers;
    }

    public String getPoster() {
        return poster;
    }

    public void setPoster(String poster) {
        this.poster = poster;
    }

    public List<MovieCopy> getCopies() {
        return copies;
    }

    public void setCopies(List<MovieCopy> copies) {
        this.copies = copies;
    }

    public String getSynopsis() {
        return synopsis;
    }

    public void setSynopsis(String synopsis) {
        this.synopsis = synopsis;
    }

    public List<String> getGenres() {
        return genres;
    }

    public void setGenres(List<String> genres) {
        this.genres = genres;
    }

}
