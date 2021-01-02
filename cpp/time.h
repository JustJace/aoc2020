#include <chrono>
#include <iostream>
#include <string>

using namespace std;

template<typename T>
void run_with_time(T (*fn)()) {
    auto start = chrono::high_resolution_clock::now();
    cout << fn() << " in ";
    auto finish = chrono::high_resolution_clock::now();
    auto nanoseconds = chrono::duration_cast<chrono::nanoseconds>(finish-start).count();
    auto milliseconds = nanoseconds / 1000 / 1000;
    auto remainder = to_string(nanoseconds % (1000 * 1000));
    if (remainder.size() < 6) 
        remainder.insert(0, 6 - remainder.size(), '0');

    cout << milliseconds << "." << remainder << "ms" << endl;
}
