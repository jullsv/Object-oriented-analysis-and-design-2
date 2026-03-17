#include "textdecorator.h"

TextDecorator::TextDecorator(MemeComponent* comp, const std::string& txt,
                             const std::string& pos, int size, const std::string& col)
    : MemeDecorator(comp), text(txt), position(pos), fontSize(size), color(col) {}

std::string TextDecorator::render() {
    return MemeDecorator::render() + " | [TEXT] " + text + " at " + position;
}

std::string TextDecorator::toJSON() const {
    return "{\"type\":\"text\",\"content\":\"" + text + 
           "\",\"position\":\"" + position + 
           "\",\"fontSize\":" + std::to_string(fontSize) +
           ",\"color\":\"" + color + "\"," +
           "\"base\":" + MemeDecorator::toJSON() + "}";
}