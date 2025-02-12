package log.laboratoire.entity.clientEntities;

import java.sql.Date;
import java.util.Random;

public class CreditCard {
    private int id;
    private String cardType;
    private String cardNumber;
    private Date expiryDate;
    private String cvv;

    public CreditCard(int id, String cardType, String cardNumber, Date expiryDate) {
        this.id = id;
        this.cardType = cardType;
        this.cardNumber = cardNumber;
        this.expiryDate = expiryDate;
        this.cvv = generateCvv();
    }

    public String getCardType() {
        return cardType;
    }

    public String getCardNumber() {
        return cardNumber;
    }

    public Date getExpiryDate() {
        return expiryDate;
    }

    public String getCvv() {
        return cvv;
    }

    public int getId() {
        return id;
    }

    private String generateCvv() {
        Random random = new Random();
        int number = 100 + random.nextInt(900);
        return String.valueOf(number);
    }
}
