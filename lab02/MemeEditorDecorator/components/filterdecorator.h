#ifndef FILTER_DECORATOR_H
#define FILTER_DECORATOR_H

#include "memedecorator.h"
#include <string>

class FilterDecorator : public MemeDecorator {
private:
    std::string filterType;
    
public:
    FilterDecorator(MemeComponent* comp, const std::string& filter);
    
    std::string render() override;
    std::string toJSON() const override;
};

#endif