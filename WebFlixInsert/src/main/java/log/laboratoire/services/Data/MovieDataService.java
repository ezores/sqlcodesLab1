package log.laboratoire.services.Data;

import log.laboratoire.entity.movieEntities.*;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class MovieDataService implements IDataService<Movie> {

    @Override
    public void insertData(Connection connection, List<Movie> movies) throws SQLException {
        String movieSql = "INSERT INTO Film (filmId, titre, anneeSortie, langueOriginale, dureeMinute, afficheUrl, realisateurId) " +
                "VALUES (?, ?, ?, ?, ?, ?, ?)";
        String countryFilmSql = "INSERT INTO PaysFilm (paysId, filmId) VALUES (?, ?)";
        String actorFilmSql = "INSERT INTO ActeurFilm (acteurId, filmId, personnage) VALUES (?, ?, ?)";
        String screenwriterFilmSql = "INSERT INTO ScenaristeFilm (scenaristeId, filmId) VALUES (?, ?)";
        String screenwriterSql = "INSERT INTO Scenariste (scenaristeId, nom) VALUES (?, ?)";
        String countrySql = "INSERT INTO Pays (paysId, nom) VALUES (?, ?)";
        String genreFilmSql = "INSERT INTO GenreFilm (genre, filmId) VALUES (?, ?)";
        String movieCopySql = "INSERT INTO CopieFilm (copieId, filmId, statut) VALUES (?, ?, ?)";
        String trailerSql = "INSERT INTO BandeAnnonce (bandeAnnonceId, filmId, URL) VALUES (?, ?, ?)";

        try (
                PreparedStatement movieStmt = connection.prepareStatement(movieSql);
                PreparedStatement countryFilmStmt = connection.prepareStatement(countryFilmSql);
                PreparedStatement actorStmt = connection.prepareStatement(actorFilmSql);
                PreparedStatement screenwriterFilmStmt = connection.prepareStatement(screenwriterFilmSql);
                PreparedStatement genreStmt = connection.prepareStatement(genreFilmSql);
                PreparedStatement copyStmt = connection.prepareStatement(movieCopySql);
                PreparedStatement trailerStmt = connection.prepareStatement(trailerSql);
                PreparedStatement countryStmt = connection.prepareStatement(countrySql);
                PreparedStatement screenwriterStmt = connection.prepareStatement(screenwriterSql)
        ) {
            connection.setAutoCommit(false);

            // Batch insert Movies (note: no trailer column here)
            for (Movie movie : movies) {
                movieStmt.setInt(1, movie.getId());
                movieStmt.setString(2, movie.getTitle());
                movieStmt.setInt(3, movie.getReleaseDate());
                movieStmt.setString(4, movie.getOriginalLanguage());
                movieStmt.setInt(5, movie.getLength());
                movieStmt.setString(6, movie.getPoster());

                int directorId = movie.getDirectorId();
                if (directorId == 0) {
                    movieStmt.setNull(7, java.sql.Types.INTEGER);
                }
                else {
                    movieStmt.setInt(7, directorId);
                }
                movieStmt.addBatch();
            }
            movieStmt.executeBatch();

            // Batch insert Countries
            insertCountries(countryStmt, movies);

            // Batch insert Countries for each Movie
            for (Movie movie : movies) {
                for (Country country : movie.getCountries()) {
                    countryFilmStmt.setInt(1, country.getId());
                    countryFilmStmt.setInt(2, movie.getId());
                    countryFilmStmt.addBatch();
                }
            }
            countryFilmStmt.executeBatch();

            // Batch insert Actors for each Movie
            for (Movie movie : movies) {
                for (Actor actor : movie.getActors()) {
                    actorStmt.setInt(1, actor.getId());
                    actorStmt.setInt(2, movie.getId());
                    actorStmt.setString(3, actor.getCharacter());
                    actorStmt.addBatch();
                }
            }
            actorStmt.executeBatch();

            // Batch insert Screenwriters
            insertScreenwriters(screenwriterStmt, movies);

            // Batch insert Screenwriters for each Movie
            for (Movie movie : movies) {
                for (Screenwriter screenwriter : movie.getScreenwriters()) {
                    screenwriterFilmStmt.setInt(1, screenwriter.getId());
                    screenwriterFilmStmt.setInt(2, movie.getId());
                    screenwriterFilmStmt.addBatch();
                }
            }
            screenwriterFilmStmt.executeBatch();

            // Batch insert Genres for each Movie
            for (Movie movie : movies) {
                for (String genre : movie.getGenres()) {
                    genreStmt.setString(1, genre);
                    genreStmt.setInt(2, movie.getId());
                    genreStmt.addBatch();
                }
            }
            genreStmt.executeBatch();

            // Batch insert Movie Copies for each Movie
            for (Movie movie : movies) {
                List<MovieCopy> copies = movie.getCopies();
                if (copies != null) {
                    for (MovieCopy copy : copies) {
                        copyStmt.setInt(1, copy.getId());
                        copyStmt.setInt(2, copy.getMovieId());
                        copyStmt.setString(3, copy.getCopyStatus().name());
                        copyStmt.addBatch();
                    }
                }
            }
            copyStmt.executeBatch();

            // Batch insert all Trailers for each Movie.
            for (Movie movie : movies) {
                List<MovieTrailer> trailers = movie.getTrailers();
                if (trailers != null) {
                    for (MovieTrailer trailer : trailers) {
                        trailerStmt.setInt(1, trailer.getId());
                        trailerStmt.setInt(2, trailer.getMovieId());
                        trailerStmt.setString(3, trailer.getUrl());
                        trailerStmt.addBatch();
                    }
                }
            }
            trailerStmt.executeBatch();

            connection.commit();
        } catch (SQLException e) {
            connection.rollback();
            throw e;
        } finally {
            connection.setAutoCommit(true);
        }
    }

    private void insertCountries(PreparedStatement countryStmt, List<Movie> movies) throws SQLException {
        ArrayList<String> addedCountries = new ArrayList<>();

        for (Movie movie : movies) {
            for (Country country : movie.getCountries()) {

                if (addedCountries.contains(country.getName())) {
                    continue;
                } else {
                    addedCountries.add(country.getName());
                }

                countryStmt.setInt(1, country.getId());
                countryStmt.setString(2, country.getName());
                countryStmt.addBatch();
            }
        }
        countryStmt.executeBatch();
    }

    private void insertScreenwriters(PreparedStatement screenwriterStmt, List<Movie> movies) throws SQLException {
        ArrayList<String> addedScreenwriter = new ArrayList<>();

        for (Movie movie : movies) {
            for (Screenwriter screenwriter : movie.getScreenwriters()) {

                if (addedScreenwriter.contains(screenwriter.getName())) {
                    continue;
                } else {
                    addedScreenwriter.add(screenwriter.getName());
                }

                screenwriterStmt.setInt(1, screenwriter.getId());
                screenwriterStmt.setString(2, screenwriter.getName());
                screenwriterStmt.addBatch();
            }
        }
        screenwriterStmt.executeBatch();
    }
}
