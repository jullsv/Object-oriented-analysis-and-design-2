#ifndef TEXT_DECORATOR_H
#define TEXT_DECORATOR_H

#include "memedecorator.h"
#include <string>

class TextDecorator : public MemeDecorator {
private:
    std::string text;
    std::string position;
    int fontSize;
    std::string color;
    
public:
    TextDecorator(MemeComponent* comp, const std::string& txt,
                  const std::string& pos = "top", int size = 40,
                  const std::string& col = "white");
    
    std::string render() override;
    std::string toJSON() const override;
};

#endif