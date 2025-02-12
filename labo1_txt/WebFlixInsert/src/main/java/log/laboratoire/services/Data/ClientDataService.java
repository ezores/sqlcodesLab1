package log.laboratoire.services.Data;

import log.laboratoire.entity.clientEntities.Address;
import log.laboratoire.entity.clientEntities.Client;
import log.laboratoire.entity.clientEntities.CreditCard;
import log.laboratoire.entity.clientEntities.subscription.AdvancedSubscription;
import log.laboratoire.entity.clientEntities.subscription.BaseSubscription;
import log.laboratoire.entity.clientEntities.subscription.IntermediateSubscription;
import log.laboratoire.entity.clientEntities.subscription.StarterSubscription;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ClientDataService implements IDataService<Client> {


    @Override
    public void insertData(Connection connection, List<Client> data) throws SQLException {
        String subscriptionSql = "INSERT INTO Abonnement (code, forfait, coutMensuel, empruntMax, dureeMax) VALUES (?, ?, ?, ?, ?)";
        String clientSql = "INSERT INTO Client (clientId, motDePasse, courriel, prenom, nom, dateNaissance, numeroTelephone, codeAbonnement) " +
                "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
        String addressSql = "INSERT INTO Adresse (adresseId, rue, ville, province, codePostal) VALUES (?, ?, ?, ?, ?)";
        String creditCardSql = "INSERT INTO CarteCredit (carteCreditId, numero, typeCarte, dateExpiration, CVV) VALUES (?, ?, ?, ?, ?)";
        String updateClientSql = "UPDATE Client SET adresseId = ?, carteCreditId = ? WHERE clientId = ?";

        try (
                PreparedStatement subscriptionStmt = connection.prepareStatement(subscriptionSql);
                PreparedStatement clientStmt = connection.prepareStatement(clientSql);
                PreparedStatement addressStmt = connection.prepareStatement(addressSql);
                PreparedStatement creditCardStmt = connection.prepareStatement(creditCardSql);
                PreparedStatement updateClientStmt = connection.prepareStatement(updateClientSql)
        ) {
            connection.setAutoCommit(false);

            insertSubscriptions(subscriptionStmt);

            Map<Integer, Integer> clientToAddressId = new HashMap<>();
            Map<Integer, Integer> clientToCreditCardId = new HashMap<>();

            // Insert Clients (without foreign keys)
            for (Client client : data) {
                clientStmt.setInt(1, client.getId());
                clientStmt.setString(2, client.getPassword());
                clientStmt.setString(3, client.getEmail());
                clientStmt.setString(4, client.getFirstName());
                clientStmt.setString(5, client.getLastName());
                clientStmt.setDate(6, client.getBirthdate());
                clientStmt.setString(7, client.getPhoneNumber());
                clientStmt.setString(8, String.valueOf(client.getSubscriptionCode()));
                clientStmt.addBatch();
            }
            clientStmt.executeBatch();

            // Insert Addresses for each Client (if available)
            for (Client client : data) {
                Address address = client.getAddress();
                if (address != null) {
                    addressStmt.setInt(1, address.getId());
                    addressStmt.setString(2, address.getStreet());
                    addressStmt.setString(3, address.getCity());
                    addressStmt.setString(4, address.getProvince());
                    addressStmt.setString(5, address.getPostalCode());
                    addressStmt.addBatch();

                    clientToAddressId.put(client.getId(), address.getId());
                }
            }
            addressStmt.executeBatch();

            // Insert Credit Cards for each Client (if available)
            for (Client client : data) {
                CreditCard creditCard = client.getCreditCard();
                if (creditCard != null) {
                    creditCardStmt.setInt(1, creditCard.getId());
                    creditCardStmt.setString(2, creditCard.getCardNumber());
                    creditCardStmt.setString(3, creditCard.getCardType());
                    creditCardStmt.setDate(4, creditCard.getExpiryDate());
                    creditCardStmt.setString(5, creditCard.getCvv());
                    creditCardStmt.addBatch();

                    clientToCreditCardId.put(client.getId(), creditCard.getId());
                }
            }
            creditCardStmt.executeBatch();

            // Update each Client with its Address and Credit Card IDs (if any)
            for (Client client : data) {
                Integer addressId = clientToAddressId.get(client.getId());
                if (addressId != null) {
                    updateClientStmt.setInt(1, addressId);
                } else {
                    updateClientStmt.setNull(1, Types.INTEGER);
                }
                Integer creditCardId = clientToCreditCardId.get(client.getId());
                if (creditCardId != null) {
                    updateClientStmt.setInt(2, creditCardId);
                } else {
                    updateClientStmt.setNull(2, Types.INTEGER);
                }
                updateClientStmt.setInt(3, client.getId());
                updateClientStmt.addBatch();
            }
            updateClientStmt.executeBatch();

            connection.commit();
        } catch (SQLException e) {
            connection.rollback();
            throw e;
        } finally {
            connection.setAutoCommit(true);
        }
    }

    private void insertSubscriptions(PreparedStatement subStatement) throws SQLException {
        ArrayList<BaseSubscription> subscriptions = new ArrayList<>(List.of(new StarterSubscription(), new IntermediateSubscription(), new AdvancedSubscription()));

        for (BaseSubscription subscription : subscriptions) {
            var test = Character.toString(subscription.getCode());
            subStatement.setString(1, Character.toString(subscription.getCode()));
            subStatement.setString(2, subscription.getPlan());
            subStatement.setFloat(3, subscription.getMonthlyCost());
            subStatement.setInt(4, subscription.getMaxNoLocations());
            subStatement.setInt(5, subscription.getMaxLocationLength());

            subStatement.addBatch();
        }

        subStatement.executeBatch();
    }

}
