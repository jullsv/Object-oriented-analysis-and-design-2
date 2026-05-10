package shop.services;

import shop.models.DeliveryResult;
import shop.models.Product;
import java.util.List;

public class OrderService {
    private final IDeliveryService deliveryService;

    public OrderService(IDeliveryService deliveryService) {
        this.deliveryService = deliveryService;
    }

    public DeliveryResult calculateDelivery(String cityFrom, String cityTo, List<Product> products) {
        double totalWeight = products.stream()
            .mapToDouble(Product::getWeight)
            .sum();
        return deliveryService.calculate(cityFrom, cityTo, totalWeight);
    }

    public String getServiceName() {
        return deliveryService.getName();
    }
}