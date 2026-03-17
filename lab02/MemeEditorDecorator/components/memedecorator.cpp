#include "memedecorator.h"

MemeDecorator::MemeDecorator(MemeComponent* comp) : component(comp) {}

MemeDecorator::~MemeDecorator() {
    delete component;
}

std::string MemeDecorator::render() {
    return component->render();
}

std::string MemeDecorator::save() {
    return component->save();
}

std::string MemeDecorator::toJSON() const {
    return component->toJSON();
}