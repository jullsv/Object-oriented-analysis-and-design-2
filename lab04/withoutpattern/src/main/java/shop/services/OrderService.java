package shop.services;

import shop.models.DeliveryResult;
import shop.models.Product;
import java.util.List;

public class OrderService {
    // ПРЯМАЯ зависимость от конкретного класса (не от интерфейса!)
    private final DeliveryService deliveryService;

    public OrderService() {
        // Создаём объект ВНУТРИ класса (жёсткая привязка)
        this.deliveryService = new DeliveryService();
    }

    public DeliveryResult calculateDelivery(String cityFrom, String cityTo, List<Product> products) {
        double totalWeight = 0;
        for (Product p : products) {
            totalWeight += p.getWeight();
        }
        return deliveryService.calculate(cityFrom, cityTo, totalWeight);
    }

    public String getServiceName() {
        return deliveryService.getServiceName();
    }
}