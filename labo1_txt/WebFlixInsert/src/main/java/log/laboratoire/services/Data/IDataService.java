package log.laboratoire.services.Data;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;

public interface IDataService<T>{
    public void insertData(Connection connection, List<T> data) throws SQLException;
}
