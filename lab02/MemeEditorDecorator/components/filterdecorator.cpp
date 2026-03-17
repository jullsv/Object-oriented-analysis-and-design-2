#include "filterdecorator.h"

FilterDecorator::FilterDecorator(MemeComponent* comp, const std::string& filter)
    : MemeDecorator(comp), filterType(filter) {}

std::string FilterDecorator::render() {
    return MemeDecorator::render() + " | [FILTER] " + filterType;
}

std::string FilterDecorator::toJSON() const {
    return "{\"type\":\"filter\",\"filterType\":\"" + filterType + "\"," +
           "\"base\":" + MemeDecorator::toJSON() + "}";
}