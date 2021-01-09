#include <iostream>
#include <fstream>
#include <vector>
#include <regex>
#include <unordered_set>
#include <set>
#include <unordered_map>
#include <map>
#include <queue>
#include "time.h"
#include "text.h"

using namespace std;

vector<int> read_input() {
    vector<int> numbers;

    ifstream input("inputs\\D10.input");
    string line;
    while (getline(input, line)) {
       numbers.push_back(stoi(line));
    }

    return numbers;
}

int P1(vector<int> jolts) {
    jolts.push_back(0);
    sort(jolts.begin(), jolts.end());
    jolts.push_back(jolts[jolts.size()-1] + 3);
    int ones = 0, threes = 0;

    for (int i = 0; i + 1 < jolts.size(); i++) {
        int djolt = jolts[i + 1] - jolts[i];
        switch (djolt) {
            case 1: ones++; break;
            case 3: threes++; break;
        }
    }

    return ones * threes;
}

long long P2(vector<int> jolts) {
    jolts.push_back(0);
    sort(jolts.begin(), jolts.end());
    jolts.push_back(jolts[jolts.size()-1] + 3);

    long long dp [jolts.size()];
    dp[jolts.size() - 1] = 1;

    for (int i = jolts.size() - 2; i >= 0; i--) {
        long long a = 0;

        if (i + 1 < jolts.size() && (jolts[i + 1] - jolts[i]) <= 3) a += dp[i + 1];
        if (i + 2 < jolts.size() && (jolts[i + 2] - jolts[i]) <= 3) a += dp[i + 2];
        if (i + 3 < jolts.size() && (jolts[i + 3] - jolts[i]) <= 3) a += dp[i + 3];

        dp[i] = a;
    }

    return dp[0];
}


int main() {
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<long long>([](void) { 
        return P2(read_input()); 
    });
}

