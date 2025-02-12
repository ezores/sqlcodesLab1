package log.laboratoire.entity.clientEntities;

public class Address {
    private int id;
    private String street;
    private String city;
    private String province;
    private String postalCode;

    public Address(int id, String street, String city, String province, String postalCode) {
        this.id = id;
        this.street = street;
        this.city = city;
        this.province = province;
        this.postalCode = postalCode;
    }

    public int getId() {
        return id;
    }

    public String getStreet() {
        return street;
    }

    public String getCity() {
        return city;
    }

    public String getProvince() {
        return province;
    }

    public String getPostalCode() {
        return postalCode;
    }
}

