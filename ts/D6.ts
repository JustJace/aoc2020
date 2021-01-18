import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

function parseInput(): string[][] {
    const groups = [];
    const content = readFileSync('../inputs/D6.input','utf8');
    for (let section of content.split("\r\n\r\n")) {
        const group = [];
        for (let line of section.split("\r\n")) {
            group.push(line);
        }
        groups.push(group);
    }
    return groups;
}

function countAnsweredQuestions(group: string[]): number {
    const set = new Set<string>();
    for (let person of group)
        for (let question of person)
            set.add(question);
    return set.size;
}

function p1() {
    return parseInput()
        .map(countAnsweredQuestions)
        .reduce((a, b) => a + b);
}

function countUnamiousQuestions(group: string[]): number {
    const hash: { [key: string]: number } = {};
    for (let person of group)
        for (let question of person)
            hash[question] ? hash[question]++ : hash[question] = 1;

    return Object.keys(hash)
        .filter(k => hash[k] == group.length)
        .length;
}

function p2() {
    return parseInput()
        .map(countUnamiousQuestions)
        .reduce((a,b) => a + b);
}

timeAndPrint(p1);
timeAndPrint(p2);