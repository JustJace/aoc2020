#include <iostream>
#include <fstream>
#include <vector>
#include <regex>
#include "time.h"

using namespace std;

struct policy {
    int min;
    int max;
    char c;
    string password;
    policy(int min, int max, char c, string password)
     : c(c), min(min), max(max), password(password) {}
};

vector<policy> read_input() {
    ifstream input("inputs\\D2.input");
    vector<policy> policies;
    string line;
    regex rgx("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");
    while (getline(input, line)) {
        smatch rgxmatch;
        regex_match(line, rgxmatch, rgx);
        policies.push_back(policy(
            stoi(rgxmatch[1]),
            stoi(rgxmatch[2]),
            string(rgxmatch[3])[0],
            rgxmatch[4]
        ));
    }
    return policies;
}


int P1(vector<policy> policies) {
    int valid = 0;

    for (policy p : policies) {
        int count = 0;
        for (char c : p.password) {
            if (c == p.c) count++;
            if (count > p.max) break;
        }
        if (count >= p.min && count <= p.max) valid++;
    }

    return valid;
}

int P2(vector<policy> policies) {
    int valid = 0;

    for (policy p : policies) {
        if (p.password[p.min-1] == p.c 
          ^ p.password[p.max-1] == p.c)
            valid++;
    }

    return valid;
}

int main()
{
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<int>([](void) {
        return P2(read_input());
    });
}

