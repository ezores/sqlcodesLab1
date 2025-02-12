package log.laboratoire.entity.movieEntities;

public class MovieTrailer {
    private int id;
    private int movieId;
    private String url;

    public MovieTrailer(int id, int movieId, String url) {
        this.id = id;
        this.movieId = movieId;
        this.url = url;
    }

    public int getId() {
        return id;
    }

    public int getMovieId() {
        return movieId;
    }

    public String getUrl() {
        return url;
    }
}
