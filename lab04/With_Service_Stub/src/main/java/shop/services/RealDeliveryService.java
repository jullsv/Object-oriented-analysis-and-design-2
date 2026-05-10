package shop.services;

import shop.models.DeliveryResult;

public class RealDeliveryService implements IDeliveryService {

    @Override
    public DeliveryResult calculate(String cityFrom, String cityTo, double weight) {
        double cost = 1000 + (weight * 70);
        return new DeliveryResult(cost, 4);
    }

    @Override
    public String getName() {
        return "Real Delivery API";
    }
}