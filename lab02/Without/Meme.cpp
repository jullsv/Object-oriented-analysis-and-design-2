#include "Meme.h"
#include <sstream>

Meme::Meme(const std::string& path) : imagePath(path) {}

void Meme::addText(const std::string& text, const std::string& pos, 
                   int size, const std::string& color) {
    texts.push_back({text, pos, size, color});
}

void Meme::addFilter(const std::string& filter) {
    filters.push_back({filter});
}

void Meme::addBorder(int width, const std::string& color) {
    borders.push_back({width, color});
}

void Meme::addSticker(const std::string& path, const std::string& pos) {
    stickers.push_back({path, pos});
}

std::string Meme::render() {
    std::ostringstream oss;
    oss << "[BASE] Rendering image: " << imagePath << "\n";
    
    for (const auto& filter : filters) {
        oss << "[FILTER] Applying filter: " << filter.filterType << "\n";
    }
    
    for (const auto& text : texts) {
        oss << "[TEXT] Adding text: \"" << text.text << "\" at " 
            << text.position << "\n";
    }
    
    for (const auto& border : borders) {
        oss << "[BORDER] Adding border: " << border.width 
            << "px " << border.color << "\n";
    }
    
    for (const auto& sticker : stickers) {
        oss << "[STICKER] Adding sticker: " << sticker.stickerPath 
            << " at " << sticker.position << "\n";
    }
    
    return oss.str();
}

std::string Meme::save() {
    return "[SAVE] Meme saved: " + imagePath;
}

std::string Meme::getDescription() const {
    std::ostringstream oss;
    oss << "Base Image: " << imagePath;
    
    for (const auto& text : texts) {
        oss << " | Text: " << text.text;
    }
    
    for (const auto& filter : filters) {
        oss << " | Filter: " << filter.filterType;
    }
    
    for (const auto& border : borders) {
        oss << " | Border: " << border.width << "px";
    }
    
    for (const auto& sticker : stickers) {
        oss << " | Sticker: " << sticker.stickerPath;
    }
    
    return oss.str();
}

std::string Meme::toJSON() const {
    std::ostringstream oss;
    oss << "{\"imagePath\":\"" << imagePath << "\",";
    
    oss << "\"texts\":[";
    for (size_t i = 0; i < texts.size(); i++) {
        if (i > 0) oss << ",";
        oss << "{\"text\":\"" << texts[i].text 
            << "\",\"position\":\"" << texts[i].position
            << "\",\"fontSize\":" << texts[i].fontSize
            << ",\"color\":\"" << texts[i].color << "\"}";
    }
    oss << "],";
    
    // Фильтры
    oss << "\"filters\":[";
    for (size_t i = 0; i < filters.size(); i++) {
        if (i > 0) oss << ",";
        oss << "{\"type\":\"" << filters[i].filterType << "\"}";
    }
    oss << "],";
    
    // Рамки
    oss << "\"borders\":[";
    for (size_t i = 0; i < borders.size(); i++) {
        if (i > 0) oss << ",";
        oss << "{\"width\":" << borders[i].width 
            << ",\"color\":\"" << borders[i].color << "\"}";
    }
    oss << "],";
    
    // Стикер
    oss << "\"stickers\":[";
    for (size_t i = 0; i < stickers.size(); i++) {
        if (i > 0) oss << ",";
        oss << "{\"path\":\"" << stickers[i].stickerPath 
            << "\",\"position\":\"" << stickers[i].position << "\"}";
    }
    oss << "]}";
    
    return oss.str();
}

void Meme::clear() {
    texts.clear();
    filters.clear();
    borders.clear();
    stickers.clear();
}

std::string Meme::getImagePath() const {
    return imagePath;
}

void Meme::setImagePath(const std::string& path) {
    imagePath = path;
}