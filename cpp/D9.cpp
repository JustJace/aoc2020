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

vector<long long> read_input() {
    vector<long long> numbers;

    ifstream input("inputs\\D9.input");
    string line;
    while (getline(input, line)) {
       numbers.push_back(stoll(line));
    }

    return numbers;
}

bool has_sum(vector<long long> numbers, int index) {
    for (int i = index - 25; i < index; i++)
        for (int j = i + 1; j < index; j++)
            if (numbers[i] + numbers[j] == numbers[index])
                return true;
    return false;
}

long long P1(vector<long long> numbers) {
    for (int i = 25; i <= numbers.size(); i++)
        if (!has_sum(numbers, i)) 
            return numbers[i];
    return -1;
}

long long P2(vector<long long> numbers) {
    long long weakness = P1(numbers);
    long long runningSum = numbers[0];
    int L = 0;
    int R = 0;
    while (runningSum != weakness) {
        if (runningSum < weakness) {
            R++;
            runningSum += numbers[R];
        } 
        else {
            runningSum -= numbers[L];
            L++;
        }
    }
    long long min = LONG_LONG_MAX;
    long long max = LONG_LONG_MIN;
    for (int i = L; i <= R; i++) {
        if (numbers[i] < min) min = numbers[i];
        if (numbers[i] > max) max = numbers[i];
    }
    return min + max;
}

int main() {
    run_with_time<long long>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<long long>([](void) { 
        return P2(read_input()); 
    });
}

