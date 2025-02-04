package log.laboratoire.entity.movieEntities;

public class Screenwriter {
    private int id;
    private String name;

    public Screenwriter(int id, String name) {
        this.id = id;
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }
}
