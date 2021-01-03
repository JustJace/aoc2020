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

struct bag_count {
    string name;
    int count;
    bag_count(string name, int count) : name(name), count(count) {}
};

struct bag {
    string name;
    vector<string> parents;
    vector<bag_count> bags;
    bag() : name(), bags() {};
    bag(string name) : name(name) {}
};

vector<bag_count> parse_contained(string s) {
    vector<string> contained = split(s, ", ");
    vector<bag_count> bags;
    for (string contain : contained) {
        vector<string> parts = split(contain, " ");
        int count = stoi(parts[0]);
        string name = parts[1] + " " + parts[2];
        bags.push_back(bag_count(name, count));
    }
    return bags;
}

map<string, bag> read_input() {
    map<string, bag> bags;

    ifstream input("inputs\\D7.input");
    string line;
    while (getline(input, line)) {
        vector<string> parts = split(line, " bags contain ");
        string name = parts[0];
        string rest = parts[1];
        if (bags.find(name) == bags.end())
            bags[name] = bag(name);

        if (rest == "no other bags.") continue;

        for (bag_count contained : parse_contained(rest)) {
            if (bags.find(contained.name) == bags.end())
                bags[contained.name] = bag(contained.name);
            
            bags[contained.name].parents.push_back(name);
            bags[name].bags.push_back(contained);
        }
    }

    return bags;
}

int P1(map<string, bag> bags) {
    bag shiny_gold = bags["shiny gold"];
    unordered_set<string> seen;
    queue<string> next;
    next.push(shiny_gold.name);
    while (next.size()) {
        string current = next.front(); next.pop();
        seen.insert(current);

        for (string parent : bags[current].parents)
            if (seen.find(parent) == seen.end())
                next.push(parent);
    }
    return seen.size() - 1;
}

int count_bags_r(map<string, bag>& bags, map<string,int>& memory, bag bag) {
    if (!bag.bags.size()) return 0;
    if (memory.find(bag.name) != memory.end()) return memory[bag.name];

    int count = 0;

    for (bag_count bc : bag.bags) {
        count += bc.count * (1 + count_bags_r(bags, memory, bags[bc.name]));
    }

    return memory[bag.name] = count;
}

int P2(map<string, bag> bags) {
    bag shiny_gold = bags["shiny gold"];
    map<string, int> memory;
    return count_bags_r(bags, memory, shiny_gold);
}

int main() {
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<int>([](void) { 
        return P2(read_input()); 
    });
}

