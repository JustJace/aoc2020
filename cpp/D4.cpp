#include <iostream>
#include <fstream>
#include <vector>
#include <regex>
#include <unordered_set>
#include "time.h"
#include "text.h"

using namespace std;

struct field {
    string name;
    string value;
    field(string name, string value): name(name), value(value) { }
};

vector<vector<field>> read_input() {
    ifstream input("inputs\\D4.input");
    vector<vector<string>> blocks = split_blocks(&input);
    vector<vector<field>> passports;

    for (vector<string> block : blocks) {
        vector<field> passport;
        for (string fieldSet : block) {
            for (string f : split(fieldSet, " ")) {
                vector<string> sides = split(f, ":");
                passport.push_back(field(sides[0], sides[1]));
            }
        }
        passports.push_back(passport);
    }

    return passports;
}

bool validate_fields(vector<field> passport) {
    unordered_set<string> fields;
    fields.insert("cid");

    for (field f : passport) {
        fields.insert(f.name);
    }

    return fields.size() == 8;
}

const string digits = "0123456789";
const string hex_digits = digits + "abcdef";
const unordered_set<string> eye_colors { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

bool validate_values(vector<field> passport) {

    for (field f : passport) {
        if (f.name == "byr") {
            if (stoi(f.value) < 1920 || stoi(f.value) > 2002)
                return false;
        }
        else if (f.name == "iyr") {
            if (stoi(f.value) < 2010 || stoi(f.value) > 2020)
                return false;
        } 
        else if (f.name == "eyr") {
            if (stoi(f.value) < 2020 || stoi(f.value) > 2030)
                return false;
        }
        else if (f.name == "hgt") {
            if (f.value.size() <= 2) return false;
            const string nums = f.value.substr(0, f.value.size() - 2);
            int hgt = stoi(nums);
            if (f.value.find("cm") != string::npos) {
                if (hgt < 150 || hgt > 193)
                    return false;
            }
            else if (f.value.find("in") != string::npos){
                if (hgt < 59 || hgt > 76)
                    return false;
            }
            else {
                return false;
            }
        }
        else if (f.name == "hcl") {
            if (f.value[0] != '#') return false;
            if (f.value.size() != 7) return false;
            for (char c : f.value.substr(1))
                if (hex_digits.find(c) == string::npos)
                    return false;
        }
        else if (f.name == "ecl") {
            if (eye_colors.find(f.value) == eye_colors.end())
                return false;
        }
        else if (f.name == "pid") {
            if (f.value.size() != 9) return false;
            for (char c : f.value) 
                if (digits.find(c) == string::npos)
                    return false;
        }
        else if (f.name == "cid") { } 
        else { }
    }

    return true;
}


int P1(vector<vector<field>> passports) {
    int valid = 0;
    for (vector<field> passport : passports)
        if (validate_fields(passport)) 
            valid++;
    return valid;
}

int P2(vector<vector<field>> passports) {
    int valid = 0;
    for (vector<field> passport : passports)
        if (validate_fields(passport)
         && validate_values(passport)) 
            valid++;
    return valid;
}


int main() {
    run_with_time<int>([](void) { 
        return P1(read_input()); 
    });

    run_with_time<int>([](void) {
        return P2(read_input());
    });
}

