package log.laboratoire.entity.movieEntities;

import log.laboratoire.entity.enums.MovieStatus;

public class MovieCopy {
    private int id;
    private int movieId;
    private MovieStatus copyStatus = MovieStatus.DISPONIBLE;

    public MovieCopy(int id, int movieId) {
        this.id = id;
        this.movieId = movieId;
    }

    public int getId() {
        return id;
    }

    public int getMovieId() {
        return movieId;
    }

    public MovieStatus getCopyStatus() {
        return copyStatus;
    }
}
