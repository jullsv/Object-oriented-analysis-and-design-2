package shop.services;

import shop.models.DeliveryResult;

public final class StubDeliveryService implements IDeliveryService {

    @Override
    public DeliveryResult calculate(String cityFrom, String cityTo, double weight) {
        double baseCost;
        int days;

        if (cityFrom.equalsIgnoreCase(cityTo)) {
            baseCost = 300; 
            days = 1;
        } else {
            // Междугородняя доставка
            baseCost = 800;
            days = 3;
        }

        // Добавляем стоимость за вес (50 руб за кг)
        double totalCost = baseCost + (weight * 50);

        return new DeliveryResult(totalCost, days);
    }

    @Override
    public String getName() {
        return "Stub Delivery (Test Logic)";
    }
}