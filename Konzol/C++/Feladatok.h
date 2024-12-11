#ifndef FELADATOK_H
#define FELADATOK_H

#include <string>
#include <vector>
#include <map>

class Feladatok {
public:
    static void LegnepszerubbNyelvek(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak);
    static int BekerEv();
    static void LegnagyobbSikertelenVizsgakAranya(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak, int ev);
    static void NemVoltVizsgazo(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak, int ev);
    static void Osszesites(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak, const std::string & outputFile);
};

#endif