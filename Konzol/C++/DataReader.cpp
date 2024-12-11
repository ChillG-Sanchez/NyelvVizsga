#include "DataReader.h"
#include <fstream>
#include <sstream>
#include <iostream>

std::map<std::string, std::vector<int>> DataReader::BeolvasFajl(const std::string & fileName) {
    std::map<std::string, std::vector<int>> result;
    std::ifstream file(fileName);
    if (!file.is_open()) {
        std::cerr << "Hiba: Nem sikerült megnyitni a fájlt: " << fileName << std::endl;
        return result;
    }

    std::string line;
    bool isHeader = true;

    while (std::getline(file, line)) {
        if (isHeader) {
            isHeader = false;
            continue;
        }

        std::istringstream ss(line);
        std::string nyelv;
        std::getline(ss, nyelv, ';');

        std::vector<int> vizsgak;
        std::string value;
        while (std::getline(ss, value, ';')) {
            vizsgak.push_back(std::stoi(value));
        }

        result[nyelv] = vizsgak;
    }

    return result;
}