#ifndef BORDER_DECORATOR_H
#define BORDER_DECORATOR_H

#include "memedecorator.h"
#include <string>

class BorderDecorator : public MemeDecorator {
private:
    int width;
    std::string color;
    
public:
    BorderDecorator(MemeComponent* comp, int w = 50, const std::string& col = "white");
    
    std::string render() override;
    std::string toJSON() const override;
};

#endif