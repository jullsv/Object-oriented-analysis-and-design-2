package shop.services;

import shop.models.DeliveryResult;

public final class RealDeliveryService implements IDeliveryService {

    @Override
    public DeliveryResult calculate(String cityFrom, String cityTo, double weight) {
        // здесь был бы запрос к API СДЭК с координатами городов
        double cost = 1000 + (weight * 70);
        return new DeliveryResult(cost, 4);
    }

    @Override
    public String getName() {
        return "Real Delivery API";
    }
}