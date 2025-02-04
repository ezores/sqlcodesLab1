package log.laboratoire.mappers;

import log.laboratoire.dto.dto.PersonDTO;
import log.laboratoire.entity.Person;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

public class PersonMapper {

    public static List<Person> map(List<PersonDTO> personDTOList) {
        List<Person> personList = new ArrayList<>();

        for (PersonDTO personDTO : personDTOList) {
            Person person = new Person();

            person.setId(personDTO.getId());
            person.setName(personDTO.getName());
            person.setPhoto(personDTO.getPhoto());
            person.setBiography(personDTO.getBio());

            var birth = personDTO.getBirth();
            person.setBirthplace(birth.getBirthPlace());
            person.setBirthdate(toDate(birth.getBirthday()));

            personList.add(person);
        }

        return personList;
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
            // ignored
        }

        return null;
    }
}
