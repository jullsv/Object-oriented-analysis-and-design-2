#ifndef MEME_H
#define MEME_H

#include <string>
#include <vector>

// Структуры для хранения данных
struct TextElement {
    std::string text;
    std::string position;
    int fontSize;
    std::string color;
};

struct FilterElement {
    std::string filterType;
};

struct BorderElement {
    int width;
    std::string color;
};

struct StickerElement {
    std::string stickerPath;
    std::string position;
};

// Монолитный класс без паттерна Decorator
class Meme {
private:
    std::string imagePath;
    
    // Все элементы хранятся напрямую в классах
    std::vector<TextElement> texts;
    std::vector<FilterElement> filters;
    std::vector<BorderElement> borders;
    std::vector<StickerElement> stickers;
    
public:
    Meme(const std::string& path = "cat.jpg");
    
    // Методы для добавления элементов
    void addText(const std::string& text, const std::string& pos, 
                 int size, const std::string& color);
    void addFilter(const std::string& filter);
    void addBorder(int width, const std::string& color);
    void addSticker(const std::string& path, const std::string& pos);
    
    // Основные методы
    std::string render();
    std::string save();
    std::string getDescription() const;
    std::string toJSON() const;
    
    // Очистка
    void clear();
    
    // Геттеры
    std::string getImagePath() const;
    void setImagePath(const std::string& path);
};

#endif