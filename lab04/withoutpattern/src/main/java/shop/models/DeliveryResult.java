package shop.models;

public class DeliveryResult {
    private final double cost;
    private final int days;
    private final boolean success;
    private final String error;

    public DeliveryResult(double cost, int days) {
        this.cost = cost;
        this.days = days;
        this.success = true;
        this.error = null;
    }

    public DeliveryResult(String error) {
        this.cost = 0;
        this.days = 0;
        this.success = false;
        this.error = error;
    }

    public double getCost() { return cost; }
    public int getDays() { return days; }
    public boolean isSuccess() { return success; }
    public String getError() { return error; }
}