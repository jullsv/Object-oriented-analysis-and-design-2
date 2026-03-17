#ifndef MEME_COMPONENT_H
#define MEME_COMPONENT_H

#include <string>
#include <vector>

class MemeComponent {
public:
    virtual ~MemeComponent() = default;
    virtual std::string render() = 0;
    virtual std::string save() = 0;
    virtual std::string toJSON() const = 0;
};

#endif