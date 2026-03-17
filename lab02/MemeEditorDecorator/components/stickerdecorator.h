#ifndef STICKER_DECORATOR_H
#define STICKER_DECORATOR_H

#include "memedecorator.h"
#include <string>

class StickerDecorator : public MemeDecorator {
private:
    std::string stickerPath;
    std::string position;
    
public:
    StickerDecorator(MemeComponent* comp, const std::string& path,
                     const std::string& pos = "center");
    
    std::string render() override;
    std::string toJSON() const override;
};

#endif