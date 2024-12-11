#include "Feladatok.h"
#include <iostream>
#include <fstream>
#include <algorithm>
#include <numeric>
#include <iomanip>

void Feladatok::LegnepszerubbNyelvek(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak) {
    if (sikeresVizsgak.empty() || sikertelenVizsgak.empty()) {
        std::cerr << "Hiba: Az adatszerkezetek üresek." << std::endl;
        return;
    }

    std::map<std::string, int> osszesVizsga;

    for (const auto & nyelv_sikeres : sikeresVizsgak) {
        const std::string & nyelv = nyelv_sikeres.first;
        const std::vector<int> & sikeres = nyelv_sikeres.second;
        int sikeresSum = std::accumulate(sikeres.begin(), sikeres.end(), 0);
        int sikertelenSum = sikertelenVizsgak.count(nyelv) ? std::accumulate(sikertelenVizsgak.at(nyelv).begin(), sikertelenVizsgak.at(nyelv).end(), 0) : 0;
        osszesVizsga[nyelv] = sikeresSum + sikertelenSum;
    }

    std::vector<std::pair<std::string, int>> sortedVizsga(osszesVizsga.begin(), osszesVizsga.end());
    std::sort(sortedVizsga.begin(), sortedVizsga.end(), [](const std::pair<std::string, int> & a, const std::pair<std::string, int> & b) {
        return b.second < a.second;
    });

    for (int i = 0; i < 3; ++i) {
        std::cout << sortedVizsga[i].first << std::endl;
    }
}

int Feladatok::BekerEv() {
    int ev;
    while (true) {
        std::cout << "Adjon meg egy évet (2009 és 2017 között): ";
        std::cin >> ev;

        if (ev >= 2009 && ev <= 2017) {
            break;
        } else {
            std::cout << "Helytelen év. Kérem, adjon meg egy évet 2009 és 2017 között." << std::endl;
        }
    }
    return ev;
}

void Feladatok::LegnagyobbSikertelenVizsgakAranya(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak, int ev) {
    if (sikeresVizsgak.empty() || sikertelenVizsgak.empty()) {
        std::cerr << "Hiba: Az adatszerkezetek üresek." << std::endl;
        return;
    }

    int index = ev - 2009;
    std::string legnagyobbNyelv;
    double legnagyobbArany = 0;

    for (const auto & nyelv_sikeres : sikeresVizsgak) {
        const std::string & nyelv = nyelv_sikeres.first;
        const std::vector<int> & sikeres = nyelv_sikeres.second;
        int sikeresCount = sikeres[index];
        int sikertelenCount = sikertelenVizsgak.count(nyelv) ? sikertelenVizsgak.at(nyelv)[index] : 0;
        int osszes = sikeresCount + sikertelenCount;
        double arany = osszes > 0 ? static_cast<double>(sikertelenCount) / osszes * 100 : 0;

        if (arany > legnagyobbArany) {
            legnagyobbArany = arany;
            legnagyobbNyelv = nyelv;
        }
    }

    std::cout << ev << "-ben a legnagyobb sikertelen vizsgák aránya " << legnagyobbNyelv << " nyelvből volt: " << std::fixed << std::setprecision(2) << legnagyobbArany << "%" << std::endl;
}

void Feladatok::NemVoltVizsgazo(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak, int ev) {
    if (sikeresVizsgak.empty() || sikertelenVizsgak.empty()) {
        std::cerr << "Hiba: Az adatszerkezetek üresek." << std::endl;
        return;
    }

    int index = ev - 2009;
    bool voltVizsgazo = false;

    for (const auto & nyelv_sikeres : sikeresVizsgak) {
        const std::string & nyelv = nyelv_sikeres.first;
        const std::vector<int> & sikeres = nyelv_sikeres.second;
        int sikeresCount = sikeres[index];
        int sikertelenCount = sikertelenVizsgak.count(nyelv) ? sikertelenVizsgak.at(nyelv)[index] : 0;

        if (sikeresCount == 0 && sikertelenCount == 0) {
            std::cout << nyelv << std::endl;
            voltVizsgazo = true;
        }
    }

    if (!voltVizsgazo) {
        std::cout << "Minden nyelvből volt vizsgázó." << std::endl;
    }
}

void Feladatok::Osszesites(const std::map<std::string, std::vector<int>> & sikeresVizsgak, const std::map<std::string, std::vector<int>> & sikertelenVizsgak, const std::string & outputFile) {
    if (sikeresVizsgak.empty() || sikertelenVizsgak.empty()) {
        std::cerr << "Hiba: Az adatszerkezetek üresek." << std::endl;
        return;
    }

    std::ofstream writer(outputFile);

    for (const auto & nyelv_sikeres : sikeresVizsgak) {
        const std::string & nyelv = nyelv_sikeres.first;
        const std::vector<int> & sikeres = nyelv_sikeres.second;
        int sikeresSum = std::accumulate(sikeres.begin(), sikeres.end(), 0);
        int sikertelenSum = sikertelenVizsgak.count(nyelv) ? std::accumulate(sikertelenVizsgak.at(nyelv).begin(), sikertelenVizsgak.at(nyelv).end(), 0) : 0;
        int osszes = sikeresSum + sikertelenSum;
        double arany = osszes > 0 ? static_cast<double>(sikeresSum) / osszes * 100 : 0;

        writer << nyelv << ";" << osszes << ";" << std::fixed << std::setprecision(2) << arany << std::endl;
    }
}