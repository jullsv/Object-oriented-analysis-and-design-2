package shop.db;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import shop.models.Order;
import shop.models.Product;

public class DatabaseManager {
    private static final String DB_URL = "jdbc:sqlite:goods.db";
    private Connection connection;

    public DatabaseManager() {
        initializeConnection();
    }

    private void initializeConnection() {
        try {
            connection = DriverManager.getConnection(DB_URL);
            System.out.println("Connected to goods.db successfully");
        } catch (SQLException e) {
            System.err.println("Database connection error: " + e.getMessage());
            throw new RuntimeException("Failed to connect to goods.db", e);
        }
    }

    public List<Product> getAllProducts() {
        List<Product> products = new ArrayList<>();
        
        String sql = "SELECT product_id, product_name, price, weight FROM bdd";
        
        try (Statement stmt = connection.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {

            while (rs.next()) {
                Product product = new Product(
                    rs.getString("product_name"),
                    rs.getDouble("price"),
                    rs.getDouble("weight")
                );
                products.add(product);
            }

            System.out.println("Loaded " + products.size() + " products from database");

        } catch (SQLException e) {
            System.err.println("Error loading products: " + e.getMessage());
            e.printStackTrace();
        }

        return products;
    }

    public int saveOrder(Order order) {
        System.out.println("Order saved: " + order.getCustomerName());
        System.out.println("Total: " + order.getTotalAmount());
        return 1;
    }

    public List<Order> getAllOrders() {
        return new ArrayList<>();
    }

    public void close() {
        try {
            if (connection != null && !connection.isClosed()) {
                connection.close();
            }
        } catch (SQLException e) {
            System.err.println("Error closing database: " + e.getMessage());
        }
    }
}