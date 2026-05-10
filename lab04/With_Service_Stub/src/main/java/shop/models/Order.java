package shop.models;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Order {
    private int id;
    private String customerName;
    private String customerPhone;
    private String cityFrom;
    private String cityTo;
    private double totalAmount;
    private double deliveryCost;
    private int deliveryDays;
    private LocalDateTime orderDate;
    private List<Product> products;

    public Order(int id, String customerName, String customerPhone, String cityFrom, String cityTo) {
        this.id = id;
        this.customerName = customerName;
        this.customerPhone = customerPhone;
        this.cityFrom = cityFrom;
        this.cityTo = cityTo;
        this.products = new ArrayList<>();
        this.orderDate = LocalDateTime.now();
    }

    public int getId() {
        return id;
    }

    public String getCustomerName() {
        return customerName;
    }

    public String getCustomerPhone() {
        return customerPhone;
    }

    public String getCityFrom() {
        return cityFrom;
    }

    public String getCityTo() {
        return cityTo;
    }

    public double getTotalAmount() {
        return totalAmount;
    }

    public void setTotalAmount(double totalAmount) {
        this.totalAmount = totalAmount;
    }

    public double getDeliveryCost() {
        return deliveryCost;
    }

    public void setDeliveryCost(double deliveryCost) {
        this.deliveryCost = deliveryCost;
    }

    public int getDeliveryDays() {
        return deliveryDays;
    }

    public void setDeliveryDays(int deliveryDays) {
        this.deliveryDays = deliveryDays;
    }

    public LocalDateTime getOrderDate() {
        return orderDate;
    }

    public void setOrderDate(LocalDateTime orderDate) {
        this.orderDate = orderDate;
    }

    public List<Product> getProducts() {
        return products;
    }

    public void addProduct(Product product) {
        products.add(product);
    }

    public double getTotalWeight() {
        return products.stream().mapToDouble(Product::getWeight).sum();
    }

    @Override
    public String toString() {
        return String.format("Заказ #%d | %s %s | %s → %s | Сумма: %.0f руб (доставка: %.0f руб)",
            id, customerName, customerPhone, cityFrom, cityTo, 
            totalAmount, deliveryCost);
    }
}