#include "stickerdecorator.h"

StickerDecorator::StickerDecorator(MemeComponent* comp, const std::string& path,
                                   const std::string& pos)
    : MemeDecorator(comp), stickerPath(path), position(pos) {}

std::string StickerDecorator::render() {
    return MemeDecorator::render() + " | [STICKER] " + stickerPath + " at " + position;
}

std::string StickerDecorator::toJSON() const {
    return "{\"type\":\"sticker\",\"path\":\"" + stickerPath +
           "\",\"position\":\"" + position + "\"," +
           "\"base\":" + MemeDecorator::toJSON() + "}";
}