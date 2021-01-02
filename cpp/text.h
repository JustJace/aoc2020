#include <fstream>
#include <vector>
#include <iostream>

using namespace std;

vector<vector<string>> split_blocks(ifstream* stream) {
    vector<vector<string>> blocks;

    vector<string> current;
    string line;
    while (getline(*stream, line)) {
        if (line == "") {
            blocks.push_back(current);
            current.clear();
        } else {
            current.push_back(line);
        }
    }

    if (current.size()) {
        blocks.push_back(current);
    }

    return blocks;
}

vector<string> split_lines(ifstream* stream) {
    vector<string> lines; string line;
    while (getline(*stream, line)) lines.push_back(line);
    return lines;
}

vector<string> split(string s, string delimiter) {
    vector<string> splits;

    size_t pos = 0;
    string token;
    while ((pos = s.find(delimiter)) != string::npos) {
        token = s.substr(0, pos);
        splits.push_back(token);
        s.erase(0, pos + delimiter.length());
    }

    splits.push_back(s);

    return splits;
}