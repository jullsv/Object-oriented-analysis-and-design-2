package shop.services;

import shop.models.DeliveryResult;

public interface IDeliveryService {
    DeliveryResult calculate(String cityFrom, String cityTo, double weight);
    String getName();
}