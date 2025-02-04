package log.laboratoire.services.Data;

import log.laboratoire.entity.Person;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.List;

public class PersonDataService implements IDataService<Person> {

    @Override
    public void insertData(Connection connection, List<Person> data) throws SQLException {

        String personneSql = "INSERT INTO Personne (personneId, nom, dateNaissance, lieuNaissance, photo, biographie) " +
                "VALUES (?, ?, ?, ?, ?, ?)";

        connection.setAutoCommit(false);

        try (
                PreparedStatement personStmt = connection.prepareStatement(personneSql);
        ) {
            for (Person person : data) {
                personStmt.setInt(1, person.getId());
                personStmt.setString(2, person.getName());

                if (person.getBirthdate() == null) {
                    personStmt.setNull(3, java.sql.Types.DATE);
                } else {
                    personStmt.setDate(3, person.getBirthdate());
                }

                personStmt.setString(4, person.getBirthplace());

                if (person.getPhoto() == null) {
                    personStmt.setNull(5, java.sql.Types.VARCHAR);
                } else {
                    personStmt.setString(5, person.getPhoto());
                }

                if (person.getBiography() == null) {
                    personStmt.setNull(6, java.sql.Types.VARCHAR);
                } else {
                    personStmt.setString(6, person.getBiography());
                }
                personStmt.addBatch();
            }
            personStmt.executeBatch();

            connection.commit();
        } catch (SQLException e) {
            connection.rollback();
            throw e;
        } finally {
            connection.setAutoCommit(true);
        }
    }
}
