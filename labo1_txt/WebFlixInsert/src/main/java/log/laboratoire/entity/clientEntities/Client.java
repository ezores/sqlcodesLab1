package log.laboratoire.entity.clientEntities;

import java.sql.Date;

public class Client {
    private int id;
    private String password;
    private String email;
    private String firstName;
    private String lastName;
    private Date birthdate;
    private String phoneNumber;
    private Address address;
    private CreditCard creditCard;
    private char subscriptionCode;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public Date getBirthdate() {
        return birthdate;
    }

    public void setBirthdate(Date birthdate) {
        this.birthdate = birthdate;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public Address getAddress() {
        return address;
    }

    public void setAddress(Address addressId) {
        this.address = addressId;
    }

    public CreditCard getCreditCard() {
        return creditCard;
    }

    public void setCreditCard(CreditCard creditId) {
        this.creditCard = creditId;
    }

    public char getSubscriptionCode() {
        return subscriptionCode;
    }

    public void setSubscriptionCode(char subscriptionCode) {
        this.subscriptionCode = subscriptionCode;
    }
}
