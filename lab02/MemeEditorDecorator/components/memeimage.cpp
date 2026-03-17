#include "memeimage.h"

MemeImage::MemeImage(const std::string& path) : imagePath(path) {}

std::string MemeImage::render() {
    return "[BASE] Rendering image: " + imagePath;
}

std::string MemeImage::save() {
    return "[SAVE] Meme saved: " + imagePath;
}

std::string MemeImage::toJSON() const {
    return "{\"type\":\"image\",\"path\":\"" + imagePath + "\"}";
}

std::string MemeImage::getImagePath() const {
    return imagePath;
}

void MemeImage::setImagePath(const std::string& path) {
    imagePath = path;
}