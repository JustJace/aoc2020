import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

function parseInput(): string[] {
    const map = [];
    const content = readFileSync('../inputs/D3.input','utf8');
    for (let line of content.split("\r\n")) {
        map.push(line);
    }
    return map;
}

function treeSlope(map: string[], dr: number, dc: number): number {
    let count = 0;
    let r = 0, c = 0, w = map[0].length;
    while (r < map.length) {
        if (map[r][c % w] == '#')
            count++;
        r += dr;
        c += dc;
    }
    return count;
}

function p1() {
    const map = parseInput();
    return treeSlope(map, 1, 3);
}

function p2() {
    const map = parseInput();
    return treeSlope(map, 1, 1)
         * treeSlope(map, 1, 3)
         * treeSlope(map, 1, 5)
         * treeSlope(map, 1, 7)
         * treeSlope(map, 2, 1);
}

timeAndPrint(p1);
timeAndPrint(p2);