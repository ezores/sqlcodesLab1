package log.laboratoire.mappers;

import log.laboratoire.dto.dto.ClientDTO;
import log.laboratoire.dto.dto.CreditInfoDTO;
import log.laboratoire.entity.clientEntities.Address;
import log.laboratoire.entity.clientEntities.Client;
import log.laboratoire.entity.clientEntities.CreditCard;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class ClientMapper {
    private static int adressIdCounter = 1;
    private static int creditCardIdCounter = 1;

    public static List<Client> map(List<ClientDTO> clientsDto) {
        List<Client> clients = new ArrayList<>();

        for (ClientDTO clientDto : clientsDto) {
            Client client = new Client();

            client.setId(clientDto.getId());
            client.setPassword(clientDto.getPassword());
            client.setEmail(clientDto.getEmail());
            client.setFirstName(clientDto.getFirstName());
            client.setLastName(clientDto.getLastName());
            client.setPhoneNumber(clientDto.getPhone());
            client.setSubscriptionCode(clientDto.getPlan());
            client.setBirthdate(toDate(clientDto.getBirthday()));
            client.setAddress(new Address(getNextAdressId(), clientDto.getAddress(), clientDto.getCity(), clientDto.getProvince(), clientDto.getPostalCode()));

            // Create a SQL date out of the expiry month and year of the credit card
            CreditInfoDTO creditInfoDto = clientDto.getCreditInfo();
            LocalDate localDate = LocalDate.of(creditInfoDto.getExpiryYear(), creditInfoDto.getExpiryMonth(), 1);
            client.setCreditCard(new CreditCard(getNextCreditCardId(), creditInfoDto.getCardType(), creditInfoDto.getCardNumber(), Date.valueOf(localDate)));

            clients.add(client);
        }

        return clients;
    }

    private static int getNextAdressId() {
        return adressIdCounter++;
    }

    private static int getNextCreditCardId() {
        return creditCardIdCounter++;
    }

    /***
     * Parse a string into a SQL date
     * @param date Date string to parse
     * @return SQL date
     */
    private static Date toDate(String date) {
        try {
            java.util.Date utilDate = new SimpleDateFormat("yyyy-MM-dd").parse(date);
            return new Date(utilDate.getTime());
        }
        catch (Exception e) {
            //ignored
        }

        return null;
    }
}
