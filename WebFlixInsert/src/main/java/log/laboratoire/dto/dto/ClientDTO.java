package log.laboratoire.dto.dto;

import jakarta.xml.bind.annotation.*;

@XmlRootElement(name = "client")
@XmlAccessorType(XmlAccessType.PROPERTY)
public class ClientDTO {
    private int id;
    private String lastName;
    private String firstName;
    private String email;
    private String phone;
    private String birthday;
    private String address;
    private String city;
    private String province;
    private String postalCode;
    private CreditInfoDTO creditInfo = new CreditInfoDTO();
    private String password;
    private char plan;

    @XmlAttribute(name = "id")
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @XmlElement(name = "nom-famille")
    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    @XmlElement(name = "prenom")
    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    @XmlElement(name = "courriel")
    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    @XmlElement(name = "tel")
    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    @XmlElement(name = "anniversaire")
    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    @XmlElement(name = "adresse")
    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    @XmlElement(name = "ville")
    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    @XmlElement(name = "province")
    public String getProvince() {
        return province;
    }

    public void setProvince(String province) {
        this.province = province;
    }

    @XmlElement(name = "code-postal")
    public String getPostalCode() {
        return postalCode;
    }

    public void setPostalCode(String postalCode) {
        this.postalCode = postalCode;
    }

    @XmlElement(name = "info-credit")
    public CreditInfoDTO getCreditInfo() {
        return creditInfo;
    }

    public void setCreditInfo(CreditInfoDTO creditInfo) {
        this.creditInfo = creditInfo;
    }

    @XmlElement(name = "mot-de-passe")
    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    @XmlElement(name = "forfait")
    public char getPlan() {
        return plan;
    }

    public void setPlan(char plan) {
        this.plan = plan;
    }
}
