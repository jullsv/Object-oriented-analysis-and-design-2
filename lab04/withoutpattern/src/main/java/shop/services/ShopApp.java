package shop.services;

import java.util.ArrayList;
import java.util.List;

import javafx.application.Application;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.scene.control.Separator;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import shop.models.DeliveryResult;
import shop.models.Product;

public class ShopApp extends Application {

    private OrderService orderService;
    private final List<Product> cart = new ArrayList<>();
    private ListView<Product> cartView;
    private Label resultLabel;
    private TextField cityFromField;
    private TextField cityToField;

    @Override
    public void start(Stage stage) {
        orderService = new OrderService();

        stage.setTitle("Магазин Электроники - Расчет Доставки");
        
        VBox root = new VBox(15);
        root.setPadding(new Insets(20));
        root.setAlignment(Pos.TOP_CENTER);
        
        root.setStyle(
            "-fx-background-image: url('/sky.jpg');" +
            "-fx-background-size: cover;" +
            "-fx-background-position: center;" +
            "-fx-background-repeat: no-repeat"
        );
        
        VBox contentPanel = new VBox(15);
        contentPanel.setPadding(new Insets(25));
        contentPanel.setAlignment(Pos.TOP_CENTER);
        contentPanel.setStyle(
            "-fx-background-color: rgba(255, 255, 255, 0.9);" +
            "-fx-background-radius: 15;" +
            "-fx-effect: dropshadow(gaussian, rgba(0,0,0,0.3), 20, 0, 0, 5);"
        );
        contentPanel.setMaxWidth(550);

        Label title = new Label("🛒 Оформление заказа");
        title.setStyle("-fx-font-size: 22px; -fx-font-weight: bold; -fx-text-fill: #2c3e50;");

        Label productsLabel = new Label("📱 Выберите товары:");
        productsLabel.setStyle("-fx-font-size: 14px; -fx-font-weight: bold;");
        
        HBox buttonsBox = new HBox(10);
        buttonsBox.getChildren().addAll(
            createProductButton("Смартфон", 50000, 0.2),
            createProductButton("Ноутбук", 90000, 1.5),
            createProductButton("Наушники", 15000, 0.3)
        );
        buttonsBox.setAlignment(Pos.CENTER);

        Label cartLabel = new Label("📦 Ваша корзина:");
        cartView = new ListView<>();
        cartView.setPrefHeight(120);

        GridPane addressGrid = new GridPane();
        addressGrid.setHgap(10);
        addressGrid.setVgap(10);
        
        addressGrid.add(new Label(" Откуда (Склад):"), 0, 0);
        cityFromField = new TextField("Москва");
        addressGrid.add(cityFromField, 1, 0);
        
        addressGrid.add(new Label(" Куда (Доставка):"), 0, 1);
        cityToField = new TextField("Санкт-Петербург");
        addressGrid.add(cityToField, 1, 1);

        Button calcBtn = new Button("Рассчитать итог");
        calcBtn.setStyle(
            "-fx-background-color: #27ae60;" +
            "-fx-text-fill: white;" +
            "-fx-font-size: 14px;" +
            "-fx-font-weight: bold;" +
            "-fx-padding: 10 20;" +
            "-fx-background-radius: 8;"
        );
        calcBtn.setOnAction(e -> calculateDelivery());

        resultLabel = new Label("Добавьте товары и выберите города.");
        resultLabel.setStyle("-fx-text-fill: #7f8c8d;");

        Label serviceInfo = new Label("🔧 Режим: " + orderService.getServiceName());
        serviceInfo.setStyle("-fx-text-fill: #95a5a6; -fx-font-size: 11px;");

        contentPanel.getChildren().addAll(
            title, new Separator(),
            productsLabel, buttonsBox, new Separator(),
            cartLabel, cartView, new Separator(),
            addressGrid, calcBtn, resultLabel, new Separator(),
            serviceInfo
        );

        root.getChildren().add(contentPanel);

        Scene scene = new Scene(root, 600, 700);
        stage.setScene(scene);
        stage.show();
    }

    private Button createProductButton(String name, double price, double weight) {
        Button btn = new Button(name);
        btn.setStyle(
            "-fx-background-color: #3498db;" +
            "-fx-text-fill: white;" +
            "-fx-padding: 8 15;" +
            "-fx-background-radius: 8;"
        );
        btn.setOnAction(e -> {
            Product p = new Product(name, price, weight);
            cart.add(p);
            cartView.getItems().add(p);
        });
        return btn;
    }

    private void calculateDelivery() {
        if (cart.isEmpty()) {
            resultLabel.setText(" Корзина пуста!");
            resultLabel.setStyle("-fx-text-fill: #e74c3c; -fx-font-weight: bold;");
            return;
        }

        String from = cityFromField.getText();
        String to = cityToField.getText();

        DeliveryResult result = orderService.calculateDelivery(from, to, cart);

        if (result.isSuccess()) {
            double goodsSum = cart.stream().mapToDouble(Product::getPrice).sum();
            double total = goodsSum + result.getCost();

            String msg = String.format(
                "Товары: %.0f руб\n Доставка (%s → %s): %.0f руб (%d дн.)\n ИТОГО: %.0f руб",
                goodsSum, from, to, result.getCost(), result.getDays(), total
            );
            
            resultLabel.setText(msg);
            resultLabel.setStyle("-fx-text-fill: #27ae60; -fx-font-weight: bold;");
        }
    }

    public static void main(String[] args) {
        launch(args);
    }
}