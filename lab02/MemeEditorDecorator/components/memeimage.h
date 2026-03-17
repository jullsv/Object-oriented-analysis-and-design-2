#ifndef MEME_IMAGE_H
#define MEME_IMAGE_H

#include "memecomponent.h"
#include <string>

class MemeImage : public MemeComponent {
private:
    std::string imagePath;
    
public:
    MemeImage(const std::string& path = "cat.jpg");
    
    std::string render() override;
    std::string save() override;
    std::string toJSON() const override;
    
    std::string getImagePath() const;
    void setImagePath(const std::string& path);
};

#endif