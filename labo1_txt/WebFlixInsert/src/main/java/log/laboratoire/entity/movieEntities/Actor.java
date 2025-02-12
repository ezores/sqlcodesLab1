package log.laboratoire.entity.movieEntities;

public class Actor {
    private int id;
    private String character;

    public Actor(int id, String character) {
        this.id = id;
        this.character = character;
    }

    public int getId() {
        return id;
    }

    public String getCharacter() {
        return character;
    }
}
