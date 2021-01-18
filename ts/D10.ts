import { readFileSync } from 'fs';
import { timeAndPrint } from './time-and-print';

function parseInput(): number[] {
    const numbers = [];
    const content = readFileSync('../inputs/D10.input', 'utf8');
    for (let line of content.split("\r\n")) {
        numbers.push(+line);
    }
    return numbers;
}

function p1() {
    const jolts = parseInput();
    jolts.push(0);
    jolts.push(Math.max(...jolts) + 3);
    jolts.sort((a, b) => a - b);
    let ones = 0, threes = 0;

    for (let i = 0; i + 1 < jolts.length; i++) {
        const diff = jolts[i + 1] - jolts[i];
        switch (diff) {
            case 1: ones++; break;
            case 3: threes++; break;
            default: continue;
        }
    }

    return ones * threes;
}

function p2() {
    const jolts = parseInput();
    jolts.push(0);
    jolts.push(Math.max(...jolts) + 3);
    jolts.sort((a, b) => a - b);

    const dp = new Array<number>(jolts.length);
    dp[jolts.length - 1] = 0;
    dp[jolts.length - 2] = 1;

    for (let i = jolts.length - 3; i >= 0; i--) {
        let count = 0;
        if (jolts[i + 1] - jolts[i] <= 3)
            count += dp[i + 1];
        if (jolts[i + 2] - jolts[i] <= 3)
            count += dp[i + 2];
        if (i + 3 < jolts.length && jolts[i + 3] - jolts[i] <= 3)
            count += dp[i + 3];
        dp[i] = count;
    }

    return dp[0];
}

timeAndPrint(p1);
timeAndPrint(p2);