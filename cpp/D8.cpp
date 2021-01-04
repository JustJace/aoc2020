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

enum operation {
    acc, jmp, nop
};

struct instruction {
    operation op;
    int amount;
    instruction(operation op, int amount) : op(op), amount(amount) {}
};

unordered_map<string, operation> op_map {
    { "acc", operation:: acc },
    { "jmp", operation:: jmp },
    { "nop", operation:: nop }
};


vector<instruction> read_input() {
    vector<instruction> instructions;

    ifstream input("inputs\\D8.input");
    string line;
    while (getline(input, line)) {
        vector<string> parts = split(line, " ");
        operation op = op_map[parts[0]];
        int amount = stoi(parts[1]);
        instructions.push_back(instruction(op, amount));
    }

    return instructions;
}

int run_program(vector<instruction> instructions, bool* halts) {
    int acc = 0, pptr = 0;
    unordered_set<int> seen;

    while (true) {
        if (seen.find(pptr) != seen.end()) {
            *halts = false;
            return acc;
        }
        if (pptr >= instructions.size()) {
            *halts = true;
            return acc;
        }
        seen.insert(pptr);
        instruction inst = instructions[pptr];
        //cout << inst.op << " " << inst.amount << " <-> " << acc << endl;
        switch (inst.op) {
            case operation::acc: 
                acc += inst.amount;
                pptr++;
            break;
            case operation::jmp:
                pptr += inst.amount;
            break;
            case operation::nop:
                pptr++;
            break;
        }
    }
}

int P1(vector<instruction> instructions) {
    bool halts = false;
    return run_program(instructions, &halts);
}

int P2(vector<instruction> instructions) {
    for (int p = 0; p < instructions.size(); p++) {
        vector<instruction> modified = instructions;
        switch (modified[p].op) {
            case operation::acc: continue;
            case operation::jmp: 
                modified[p].op = operation::nop;
            break;
            case operation::nop:
                modified[p].op = operation::jmp;
            break;
        }
        bool halts;
        int acc = run_program(modified, &halts);
        if (halts) return acc;
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

