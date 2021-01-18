import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

class policy {
    low: number;
    high: number;
    rule: string;
    password: string;
}

function parseInput(): policy[] {
    const policies: policy[] = [];
    const content = readFileSync('../inputs/D2.input','utf8');
    for (let line of content.split("\r\n")) {
        const match = line.match(/([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)/)
        policies.push(<policy>{
            low: +match[1],
            high: +match[2],
            rule: match[3],
            password: match[4]
        });
    }
    return policies;
}

function p1() {
    const policies = parseInput();
    let valid = 0;

    for (let policy of policies) {
        let count = 0;

        for (let char of policy.password) {
            if (char == policy.rule) count++;
            if (count > policy.high) break;
        }

        if (count >= policy.low && count <= policy.high)
            valid++;
    }

    return valid;
}

function p2() {
    const policies = parseInput();
    let valid = 0;

    for (let policy of policies) {
        if ((policy.password[policy.low - 1] == policy.rule) 
        !== (policy.password[policy.high - 1] == policy.rule))
            valid ++;
    }

    return valid;
}

timeAndPrint(p1);
timeAndPrint(p2);