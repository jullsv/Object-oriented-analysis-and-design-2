#define _WIN32_WINNT 0x0A00
#include "httplib.h"
#include "Meme.h"
#include <iostream>
#include <sstream>

Meme* currentMeme = nullptr;

std::string getJsonValue(const std::string& json, const std::string& key) {
    size_t pos = json.find("\"" + key + "\"");
    if (pos == std::string::npos) return "";
    
    pos = json.find(":", pos);
    if (pos == std::string::npos) return "";
    
    pos = json.
    find("\"", pos);
    if (pos == std::string::npos) return "";
    
    size_t end = json.find("\"", pos + 1);
    if (end == std::string::npos) return "";
    
    return json.substr(pos + 1, end - pos - 1);
}

int getIntValue(const std::string& json, const std::string& key, int def = 0) {
    size_t pos = json.find("\"" + key + "\"");
    if (pos == std::string::npos) return def;
    
    pos = json.find(":", pos);
    if (pos == std::string::npos) return def;
    
    pos++;
    while (pos < json.size() && (json[pos] == ' ' || json[pos] == '\t')) pos++;
    
    std::string num;
    while (pos < json.size() && (isdigit(json[pos]) || json[pos] == '-')) {
        num += json[pos++];
    }
    
    return num.empty() ? def : std::stoi(num);
}

int main() {
    httplib::Server svr;
    
    svr.set_mount_point("/", "./www");
    
    svr.Get("/api/meme", [](const httplib::Request&, httplib::Response& res) {
        if (currentMeme) {
            res.set_content(currentMeme->toJSON(), "application/json");
        } else {
            res.set_content("{\"error\":\"No meme loaded\"}", "application/json");
        }
    });
    
    svr.Post("/api/meme", [](const httplib::Request& req, httplib::Response& res) {
        const std::string& body = req.body;
        
        if (!currentMeme) {
            currentMeme = new Meme("cat.jpg");
        }
        
        std::string type = getJsonValue(body, "type");
        
        if (type == "text") {
            std::string text = getJsonValue(body, "text");
            std::string pos = getJsonValue(body, "position");
            int size = getIntValue(body, "fontSize", 40);
            std::string color = getJsonValue(body, "color");
            currentMeme->addText(text, pos, size, color);
        } else if (type == "filter") {
            std::string filter = getJsonValue(body, "filterType");
            currentMeme->addFilter(filter);
        } else if (type == "border") {
            int width = getIntValue(body, "width", 50);
            std::string color = getJsonValue(body, "color");
            currentMeme->addBorder(width, color);
        } else if (type == "sticker") {
            std::string path = getJsonValue(body, "stickerPath");
            std::string pos = getJsonValue(body, "position");
            currentMeme->addSticker(path, pos);
        }
        
        std::string response = "{\"description\":\"" + currentMeme->getDescription() + "\"}";
        res.set_content(response, "application/json");
    });
    
    svr.Post("/api/reset", [](const httplib::Request&, httplib::Response& res) {
        delete currentMeme;
        currentMeme = new Meme("cat.jpg");
        res.set_content("{\"message\":\"Reset complete\"}", "application/json");
    });
    
    svr.Post("/api/save", [](const httplib::Request&, httplib::Response& res) {
        if (currentMeme) {
            std::string result = currentMeme->save();
            res.set_content("{\"message\":\"" + result + "\"}", "application/json");
        } else {
            res.set_content("{\"error\":\"No meme to save\"}", "application/json");
        }
    });
    
    std::cout << " Сервер запущен: http://localhost:8080\n";
    std::cout << " Откройте в браузере: http://localhost:8080\n";
    
    svr.listen("localhost", 8080);
    
    delete currentMeme;
    return 0;
}