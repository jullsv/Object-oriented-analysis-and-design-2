package shop.models;

public class DeliveryResult {
    private double cost;
    private int days;
    private boolean success;
    private String errorMessage;

    public DeliveryResult(double cost, int days) {
        this.cost = cost;
        this.days = days;
        this.success = true;
        this.errorMessage = null;
    }

    public DeliveryResult(String errorMessage) {
        this.cost = 0;
        this.days = 0;
        this.success = false;
        this.errorMessage = errorMessage;
    }

    public double getCost() {
        return cost;
    }

    public int getDays() {
        return days;
    }

    public boolean isSuccess() {
        return success;
    }

    public String getErrorMessage() {
        return errorMessage;
    }

    @Override
    public String toString() {
        if (success) {
            return String.format("Доставка: %.0f руб (%d дн.)", cost, days);
        } else {
            return "Ошибка: " + errorMessage;
        }
    }
}