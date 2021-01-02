#include <iostream>
#include <fstream>
#include <vector>
#include <regex>
#include <unordered_set>
#include <unordered_map>
#include "time.h"
#include "text.h"

using namespace std;

vector<vector<string>> read_input() {
    ifstream input("inputs\\D6.input");
    vector<vector<string>> blocks = split_blocks(&input);
    vector<vector<string>> groups;
    for (vector<string> block : blocks) {
        vector<string> group;
        for (string person : block) {
            group.push_back(person);
        }
        groups.push_back(group);
    }
    
    return groups;
}

int P1(vector<vector<string>> groups) {
    int count = 0;
    for (vector<string> group : groups) {
        unordered_set<char> questions;
        for (string person : group)
        for (char question : person)
            questions.insert(question);
        count += questions.size();
    }
    return count;
}

int P2(vector<vector<string>> groups) {
    int count = 0;
    for (vector<string> group : groups) {
        unordered_map<char,int> questions;
        for (string person : group)
        for (char question : person)
            questions[question]++;

        for (auto kvp : questions)
            if (kvp.second == group.size())
                count++;
    }
    return count;
}

int main() {
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<int>([](void) { 
        return P2(read_input()); 
    });
}

