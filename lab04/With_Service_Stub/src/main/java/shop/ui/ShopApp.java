package shop.ui;

import java.util.ArrayList;
import java.util.List;

import javafx.application.Application;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.scene.control.Separator;
import javafx.scene.control.TextField;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import shop.db.DatabaseManager;
import shop.models.DeliveryResult;
import shop.models.Order;
import shop.models.Product;
import shop.services.OrderService;
import shop.services.StubDeliveryService;

public class ShopApp extends Application {

    private OrderService orderService;
    private DatabaseManager databaseManager;
    private final List<Product> cart = new ArrayList<>();
    
    private ListView<Product> cartView;
    private Label resultLabel;
    private TextField cityFromField;
    private TextField cityToField;

    @Override
    public void start(Stage stage) {
        orderService = new OrderService(new StubDeliveryService());
        databaseManager = new DatabaseManager();

        stage.setTitle("Магазин техники - Расчет Доставки");
        
        VBox root = new VBox(15);
        root.setPadding(new Insets(20));
        root.setAlignment(Pos.TOP_CENTER);
        
        VBox contentPanel = new VBox(15);
        contentPanel.setPadding(new Insets(25));
        contentPanel.setAlignment(Pos.TOP_CENTER);
        contentPanel.setStyle(
            "-fx-background-color: rgba(255, 255, 255, 0.9);" +
            "-fx-background-radius: 15;" +
            "-fx-effect: dropshadow(gaussian, rgba(0,0,0,0.3), 20, 0, 0, 5);"
        );
        contentPanel.setMaxWidth(900);

        Label title = new Label("Оформление заказа");
        title.setStyle("-fx-font-size: 26px; -fx-font-weight: bold; -fx-text-fill: #2c3e50;");

        Label productsLabel = new Label("Выберите товары:");
        productsLabel.setStyle("-fx-font-size: 16px; -fx-font-weight: bold;");
        
        List<Product> productsFromDB = databaseManager.getAllProducts();
        
        GridPane productsGrid = new GridPane();
        productsGrid.setHgap(15);
        productsGrid.setVgap(15);
        productsGrid.setAlignment(Pos.CENTER);
        
        int col = 0;
        int row = 0;
        for (Product product : productsFromDB) {
            Button btn = new Button(product.getName() + "\n" + (int)product.getPrice() + " руб");
            btn.setPrefSize(200, 70);
            btn.setStyle(
                "-fx-background-color: #3498db;" +
                "-fx-text-fill: white;" +
                "-fx-font-size: 12px;" +
                "-fx-padding: 10 15;" +
                "-fx-background-radius: 8;" +
                "-fx-cursor: hand"
            );
            
            Product finalProduct = product;
            btn.setOnAction(e -> {
                cart.add(finalProduct);
                updateCartView();
                updateResultLabel("Товар добавлен в корзину.");
            });
            
            productsGrid.add(btn, col, row);
            
            col++;
            if (col > 1) {
                col = 0;
                row++;
            }
        }

        Label cartLabel = new Label("Ваша корзина:");
        cartLabel.setStyle("-fx-font-size: 16px; -fx-font-weight: bold;");
        cartView = new ListView<>();
        cartView.setPrefHeight(250);
        cartView.setPrefWidth(600);
        cartView.setStyle("-fx-background-radius: 8; -fx-border-color: #3498db; -fx-border-width: 2;");

        GridPane addressGrid = new GridPane();
        addressGrid.setHgap(15);
        addressGrid.setVgap(15);
        
        Label lblFrom = new Label("Откуда (Склад):");
        lblFrom.setStyle("-fx-font-size: 14px;");
        cityFromField = new TextField("Москва");
        cityFromField.setPrefWidth(250);
        cityFromField.setPromptText("Город отправки");
        
        Label lblTo = new Label("Куда (Доставка):");
        lblTo.setStyle("-fx-font-size: 14px;");
        cityToField = new TextField("Санкт-Петербург");
        cityToField.setPrefWidth(250);
        cityToField.setPromptText("Город получения");
        
        addressGrid.add(lblFrom, 0, 0);
        addressGrid.add(cityFromField, 1, 0);
        addressGrid.add(lblTo, 0, 1);
        addressGrid.add(cityToField, 1, 1);

        HBox actionButtons = new HBox(15);
        actionButtons.setAlignment(Pos.CENTER);
        
        Button calcBtn = new Button("Рассчитать доставку");
        calcBtn.setStyle(
            "-fx-background-color: #3498db;" +
            "-fx-text-fill: white;" +
            "-fx-font-size: 15px;" +
            "-fx-font-weight: bold;" +
            "-fx-padding: 12 25;" +
            "-fx-background-radius: 8;" +
            "-fx-cursor: hand"
        );
        calcBtn.setOnAction(e -> calculateDelivery());

        Button placeOrderBtn = new Button("Оформить заказ");
        placeOrderBtn.setStyle(
            "-fx-background-color: #27ae60;" +
            "-fx-text-fill: white;" +
            "-fx-font-size: 15px;" +
            "-fx-font-weight: bold;" +
            "-fx-padding: 12 25;" +
            "-fx-background-radius: 8;" +
            "-fx-cursor: hand"
        );
        placeOrderBtn.setOnAction(e -> placeOrder());

        actionButtons.getChildren().addAll(calcBtn, placeOrderBtn);

        resultLabel = new Label("Добавьте товары и нажмите 'Рассчитать доставку'.");
        resultLabel.setStyle("-fx-text-fill: #7f8c8d; -fx-font-size: 15px;");
        resultLabel.setWrapText(true);
        resultLabel.setAlignment(Pos.CENTER);
        resultLabel.setPadding(new Insets(15, 20, 15, 20));
        resultLabel.setMaxWidth(Double.MAX_VALUE);
        resultLabel.setMinHeight(80);

        Label serviceInfo = new Label("Режим: " + orderService.getServiceName());
        serviceInfo.setStyle("-fx-text-fill: #95a5a6; -fx-font-size: 12px;");

        contentPanel.getChildren().addAll(
            title,
            new Separator(),
            productsLabel,
            productsGrid,
            new Separator(),
            cartLabel,
            cartView,
            new Separator(),
            addressGrid,
            actionButtons,
            resultLabel,
            new Separator(),
            serviceInfo
        );

        root.getChildren().add(contentPanel);

        Scene scene = new Scene(root, 950, 900);
        stage.setScene(scene);
        stage.show();
    }

    private void updateCartView() {
        cartView.getItems().clear();
        for (Product p : cart) {
            cartView.getItems().add(p);
        }
        if (cart.isEmpty()) {
            resultLabel.setText("Корзина пуста. Добавьте товары.");
        }
    }

    private void updateResultLabel(String text) {
        resultLabel.setText(text);
        resultLabel.setStyle("-fx-text-fill: #2c3e50; -fx-font-size: 15px;");
    }

    private void calculateDelivery() {
        if (cart.isEmpty()) {
            resultLabel.setText("Ошибка: Корзина пуста!");
            resultLabel.setStyle("-fx-text-fill: #e74c3c; -fx-font-weight: bold;");
            return;
        }

        String from = cityFromField.getText();
        String to = cityToField.getText();

        if (from.isEmpty() || to.isEmpty()) {
            resultLabel.setText("Укажите оба города!");
            resultLabel.setStyle("-fx-text-fill: #e74c3c; -fx-font-weight: bold;");
            return;
        }

        DeliveryResult result = orderService.calculateDelivery(from, to, cart);

        if (result.isSuccess()) {
            double goodsSum = cart.stream().mapToDouble(Product::getPrice).sum();
            double totalCost = goodsSum + result.getCost();
            
            String msg = String.format(
                " Расчёт доставки выполнен!\n\n" +
                " Товары: %.0f руб\n" +
                " Доставка (%s → %s): %.0f руб (%d дн.)\n" +
                "━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                " ИТОГО: %.0f руб",
                goodsSum, from, to, result.getCost(), result.getDays(), totalCost
            );
            
            resultLabel.setText(msg);
            resultLabel.setStyle("-fx-text-fill: #27ae60; -fx-font-size: 15px; -fx-font-weight: bold;");
        } else {
            resultLabel.setText("Ошибка: " + result.getErrorMessage());
            resultLabel.setStyle("-fx-text-fill: #e74c3c; -fx-font-weight: bold;");
        }
    }

    private void placeOrder() {
        if (cart.isEmpty()) {
            showAlert("Ошибка", "Корзина пуста!");
            return;
        }

        String cityFrom = cityFromField.getText();
        String cityTo = cityToField.getText();

        if (cityFrom.isEmpty() || cityTo.isEmpty()) {
            showAlert("Ошибка", "Укажите города доставки!");
            return;
        }

        DeliveryResult result = orderService.calculateDelivery(cityFrom, cityTo, cart);
        if (!result.isSuccess()) {
            showAlert("Ошибка доставки", result.getErrorMessage());
            return;
        }

        Order currentOrder = new Order(0, "Клиент", "+70000000000", cityFrom, cityTo);
        
        for (Product p : cart) {
            currentOrder.addProduct(p);
        }
        
        double goodsSum = cart.stream().mapToDouble(Product::getPrice).sum();
        currentOrder.setTotalAmount(goodsSum);
        currentOrder.setDeliveryCost(result.getCost());
        currentOrder.setDeliveryDays(result.getDays());
        
        int orderId = databaseManager.saveOrder(currentOrder);
        
        double totalCost = goodsSum + result.getCost();
        
        showAlert("Успех", String.format("Заказ #%d оформлен!\n\n" +
            "Товары: %.0f руб\n" +
            "Доставка: %.0f руб\n" +
            "━━━━━━━━━━━━━━\n" +
            "ИТОГО: %.0f руб",
            orderId, goodsSum, result.getCost(), totalCost));
        
        cart.clear();
        cartView.getItems().clear();
        resultLabel.setText("Заказ оформлен. Добавьте новые товары.");
        resultLabel.setStyle("-fx-text-fill: #7f8c8d;");
    }

    private void showAlert(String title, String message) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(message);
        alert.showAndWait();
    }

    @Override
    public void stop() {
        if (databaseManager != null) {
            databaseManager.close();
        }
    }

    public static void main(String[] args) {
        launch(args);
    }
}
