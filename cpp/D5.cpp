#include <iostream>
#include <fstream>
#include <vector>
#include <regex>
#include <unordered_set>
#include "time.h"
#include "text.h"

using namespace std;

vector<string> read_input() {
    ifstream input("inputs\\D5.input");
    return split_lines(&input);
}

int binary(string s) {
    string bin;
    for (const char c : s) {
        if (c == 'F' || c == 'L') bin += '0';
        if (c == 'B' || c == 'R') bin += '1';
    }
    return stoi(bin, nullptr, 2);
}

int seat_id(string seat) { 
    int row = binary(seat.substr(0,7));
    int col = binary(seat.substr(7));
    return row * 8 + col; 
}

int P1(vector<string> seats) {
    int max = 0;
    for (string seat : seats) {
        int id = seat_id(seat);
        if (id > max) max = id;
    }
    return max;
}

int P2(vector<string> seats) {
    unordered_set<int> ids;
    int min = INT_MAX;
    int max = INT_MIN;
    for (string seat : seats) {
        const int id = seat_id(seat);
        if (id < min) min = id;
        if (id > max) max = id;
        ids.insert(id);
    }

    for (int i = min; i <= max; i++) {
        if (ids.find(i) == ids.end())
            return i;
    }

    return -1;
}

int main() {
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<int>([](void) { 
        return P2(read_input()); 
    });
}

