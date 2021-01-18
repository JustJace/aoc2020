import { readFileSync } from 'fs';
import { performance } from 'perf_hooks';

function parseInput(): number[] {
    const numbers = [];
    const content = <string>readFileSync('../inputs/D1.input','utf8');
    for (let n of content.split("\n"))
        numbers.push(+n);
    return numbers;
}

function p1(): number {
    const numbers = parseInput().sort();
    let L = 0, R = numbers.length - 1;

    while (numbers[L] + numbers[R] != 2020) {
        if (numbers[L] + numbers[R] > 2020)
            R--;
        else
            L++;
    }

    return numbers[L] * numbers[R];
}

function p2(): number {
    const numbers = parseInput().sort();

    for (let i = 0; i < numbers.length; i++) {
        const sum = 2020 - numbers[i];
        let L = i + 1, R = numbers.length - 1;

        while (L < R) {
            if (numbers[L] + numbers[R] == sum)
                return numbers[L] * numbers[R] * numbers[i];
            else if (numbers[L] + numbers[R] > sum)
                R--;
            else 
                L++;
        }
    }
    return -1;
}

function p2_bruteforce(): number {
    const n = parseInput();

    for (let i = 0; i < n.length; i++)
    for (let j = i + 1; j < n.length; j++)
    for (let k = j + 1; k < n.length; k++)
    if (n[i] + n[j] + n[k] == 2020)
        return n[i] * n[j] * n[k];
}

function timeAndPrint<T>(fn: Function) {
    const start = performance.now();
    const answer = fn();
    const end = performance.now();
    console.log(`${fn.name.padEnd(15)} ${(end-start).toString().padEnd(20)}ms -> ${answer}`);
}

timeAndPrint(p1);
timeAndPrint(p2);
timeAndPrint(p2_bruteforce);