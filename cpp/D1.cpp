#include <iostream>
#include <fstream>
#include <vector>
#include "time.h"

using namespace std;

vector<int> read_input() {
    ifstream input("inputs\\D1.input");
    vector<int> nums;
    string line;
    while (getline(input, line)) {
        nums.push_back(stoi(line));
    }
    return nums;
}

int P1(vector<int> nums) {
    for (int n : nums)
    for (int m : nums)
        if (n + m == 2020)
            return n*m;
    return -1;
}

int P2(vector<int> nums) {
    for (int n : nums)
    for (int m : nums)
    for (int o : nums)
        if (n + m + o == 2020)
            return n*m*o;
    return -1;
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

