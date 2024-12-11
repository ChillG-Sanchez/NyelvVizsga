#include <iostream>
#include <string>
#include <vector>
#include <map>
#include <filesystem>
#include "Feladatok.h"
#include "DataReader.h"

int main() {
    std::filesystem::path basePath = std::filesystem::current_path().parent_path().parent_path() / "Forras";
    std::string sikeresFile = (basePath / "sikeres.csv").string();
    std::string sikertelenFile = (basePath / "sikertelen.csv").string();

    std::cout << "1. FELADAT: Adatok beolvasása" << std::endl;
    auto sikeresVizsgak = DataReader::BeolvasFajl(sikeresFile);
    auto sikertelenVizsgak = DataReader::BeolvasFajl(sikertelenFile);

    if (sikeresVizsgak.empty() || sikertelenVizsgak.empty()) {
        std::cerr << "Hiba: Az adatok beolvasása sikertelen." << std::endl;
        return 1;
    }

    std::cout << "Adatok beolvasása kész." << std::endl;

    std::cout << "2. FELADAT: A legnépszerűbb nyelvek meghatározása" << std::endl;
    Feladatok::LegnepszerubbNyelvek(sikeresVizsgak, sikertelenVizsgak);

    std::cout << "3. FELADAT: Kérjen be egy évet" << std::endl;
    int ev = Feladatok::BekerEv();

    std::cout << "4. FELADAT: A legnagyobb sikertelen vizsgák aránya" << std::endl;
    Feladatok::LegnagyobbSikertelenVizsgakAranya(sikeresVizsgak, sikertelenVizsgak, ev);

    std::cout << "5. FELADAT: Nyelvek, amelyekből nem volt vizsgázó" << std::endl;
    Feladatok::NemVoltVizsgazo(sikeresVizsgak, sikertelenVizsgak, ev);

    std::cout << "6. FELADAT: Összesítés készítése" << std::endl;
    Feladatok::Osszesites(sikeresVizsgak, sikertelenVizsgak, "osszesites.csv");

    return 0;
}