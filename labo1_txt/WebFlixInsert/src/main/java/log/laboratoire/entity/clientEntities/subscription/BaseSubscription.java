package log.laboratoire.entity.clientEntities.subscription;

public class BaseSubscription {
    private char code;
    private String plan;
    private float monthlyCost;
    private int maxNoLocations;
    private int maxLocationLength;

    public BaseSubscription(char code, String plan, float monthlyCost, int maxNoLocations, int maxLocationLenght) {
        this.code = code;
        this.plan = plan;
        this.monthlyCost = monthlyCost;
        this.maxNoLocations = maxNoLocations;
        this.maxLocationLength = maxLocationLenght;
    }

    public char getCode() {
        return code;
    }

    public String getPlan() {
        return plan;
    }

    public float getMonthlyCost() {
        return monthlyCost;
    }

    public int getMaxNoLocations() {
        return maxNoLocations;
    }

    public int getMaxLocationLength() {
        return maxLocationLength;
    }
}
