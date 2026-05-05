package shop.services;

import java.util.List;

import shop.models.DeliveryResult;
import shop.models.Product;

public class OrderService {
    private final IDeliveryService deliveryService;

    public OrderService(IDeliveryService deliveryService) {
        this.deliveryService = deliveryService;
    }

    public DeliveryResult calculateDelivery(String cityFrom, String cityTo, List<Product> products) {
        double totalWeight = 0;
        for (Product p : products) {
            totalWeight += p.getWeight();
        }
        return deliveryService.calculate(cityFrom, cityTo, totalWeight);
    }

    public String getServiceName() {
        return deliveryService.getName();
    }
}