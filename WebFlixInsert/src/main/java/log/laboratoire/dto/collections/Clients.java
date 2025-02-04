package log.laboratoire.dto.collections;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import log.laboratoire.dto.dto.ClientDTO;

import java.util.List;

@XmlRootElement(name = "clients")
public class Clients {

    private List<ClientDTO> clients;

    @XmlElement(name = "client")
    public List<ClientDTO> getClients() {
        return clients;
    }

    public void setClients(List<ClientDTO> clients) {
        this.clients = clients;
    }
}
