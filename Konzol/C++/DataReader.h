#ifndef DATAREADER_H
#define DATAREADER_H

#include <string>
#include <vector>
#include <map>

class DataReader {
public:
    static std::map<std::string, std::vector<int>> BeolvasFajl(const std::string & fileName);
};

#endif