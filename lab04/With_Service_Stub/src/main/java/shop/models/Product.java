package shop.models;

public class Product {
    private final String name;
    private final double price;
    private final double weight;

    public Product(String name, double price, double weight) {
        this.name = name;
        this.price = price;
        this.weight = weight;
    }

    public String getName() { return name; }
    public double getPrice() { return price; }
    public double getWeight() { return weight; }

    @Override
    public String toString() {
        return String.format("%s (%.0f руб, %.2f кг)", name, price, weight);
    }
}