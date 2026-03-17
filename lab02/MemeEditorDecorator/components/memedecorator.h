#ifndef MEME_DECORATOR_H
#define MEME_DECORATOR_H

#include "memecomponent.h"

class MemeDecorator : public MemeComponent {
protected:
    MemeComponent* component;
    
public:
    MemeDecorator(MemeComponent* comp);
    virtual ~MemeDecorator();
    
    std::string render() override;
    std::string save() override;
    std::string toJSON() const override;
};

#endif