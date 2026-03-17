#include "borderdecorator.h"

BorderDecorator::BorderDecorator(MemeComponent* comp, int w, const std::string& col)
    : MemeDecorator(comp), width(w), color(col) {}

std::string BorderDecorator::render() {
    return MemeDecorator::render() + " | [BORDER] " + std::to_string(width) + "px " + color;
}

std::string BorderDecorator::toJSON() const {
    return "{\"type\":\"border\",\"width\":" + std::to_string(width) +
           ",\"color\":\"" + color + "\"," +
           "\"base\":" + MemeDecorator::toJSON() + "}";
}