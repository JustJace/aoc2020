#include <iostream>
#include <fstream>
#include <vector>
#include <regex>
#include "time.h"

using namespace std;

vector<string> read_input() {
    ifstream input("inputs\\D3.input");
    vector<string> map;
    string line;
    while (getline(input, line)) {
        map.push_back(line);
    }
    return map;
}

int count_trees(vector<string> map, int dr, int dc) {
    int cr = 0, cc = 0, count = 0;
    int rows = map.size();
    int cols = map[0].size();
    while (cr + dr < rows) {
        cr += dr; cc += dc;
        if (map[cr][cc % cols] == '#')
            count++;
    }
    return count;
}


int P1(vector<string> map) {
    return count_trees(map, 1, 3);
}

unsigned long P2(vector<string> map) {
    return count_trees(map, 1, 1)
         * count_trees(map, 1, 3)
         * count_trees(map, 1, 5)
         * count_trees(map, 1, 7)
         * count_trees(map, 2, 1);
}


int main()
{
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<unsigned long>([](void) {
        return P2(read_input());
    });
}

